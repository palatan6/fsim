using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Calculator.Dialogs;
using CalculatorModules;
using CalculatorModules.Base_Controls;
using Microsoft.Win32;
using Parameters;
using Units;
using Value;

namespace SmallCalculator2
{
    public partial class fsSmallCalculatorMainWindow : Form
    {
        #region Data

        private readonly Dictionary<string, fsCalculatorControl> m_modules =
            new Dictionary<string, fsCalculatorControl>();

        public fsCalculatorControl SelectedCalculatorControl { get; private set; }

        private string CurrentFilePath;

        #endregion

        #region Constructor

        public fsSmallCalculatorMainWindow()
        {
            InitializeComponent();

            object regValue = Registry.GetValue(
                @"HKEY_CURRENT_USER\Software\NICIFOS\FiltratonCalculator",
                "LastFile",
                "");

            string filePath;

            if (regValue!=null)
            {
                filePath = regValue.ToString();
                if (!string.IsNullOrEmpty(filePath))
                {
                    CurrentFilePath = filePath;
                }
            }
        }

        public fsSmallCalculatorMainWindow(string filePath)
        {
            InitializeComponent();
            CurrentFilePath = filePath;
        }

        #endregion

        #region Form Load

        string DBPath;

        OleDbConnection conn;
        OleDbDataAdapter adapter;
        DataTable dtMain;

        private void Form1Load(object sender, EventArgs e)
        {
            AddGroupToTree("Suspension", new[]
                                             {
                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                     "Densities and Suspension Solids Content",
                                                     new fsDensityConcentrationControl()),
                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                     "Suspension Solids Mass Fraction",
                                                     new SuspensionSolidsMassFractionControl())
                                             });
            AddGroupToTree("Filter Cake", new[]
                                              {
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Filter Cake & Suspension Relations", new fsMsusAndHcControl()),
                                                  new KeyValuePair<string, fsCalculatorControl>("Cake Porosity from Test Data",
                                                                                                new CakePorossityOvermoduleControl
                                                                                                    ()),  
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Cake Permeability/Resistance and Cake Compressibility",
                                                      new fsPermeabilityControl())
                                              });
            AddGroupToTree("Cake Formation", new[]
                                                 {
                                                     new KeyValuePair<string, fsCalculatorControl>(
                                                         "Calculations Cake Formation", new fsLaboratoryFiltrationTime()),
                                                     new KeyValuePair<string,fsCalculatorControl>(
                                                         "Cake Formation Analysis", new fsCakeFormationAnalysisOvermoduleControl())
                                                 });
            AddGroupToTree("Cake Deliquoring", new[]
                                                   {
                                                       new KeyValuePair<string, fsCalculatorControl>(
                                                           "Cake Moisture Content from Wet and Dry Cake Mass",
                                                           new fsCakeMoistureContentFromWetAndDryCakeMassControl()),
                                                       new KeyValuePair<string, fsCalculatorControl>(
                                                           "Cake Moisture Content from Cake Saturation",
                                                           new fsCakeMoistureContentFromCakeSaturationControl()),
                                                       new KeyValuePair<string, fsCalculatorControl>(
                                                           "Capillary Pressure pke from Cake Permeability/Resistance",
                                                           new fsPkeFromPcRcControl())
                                                   });
            AddGroupToTree("Cake Washing", new[]
                                               {
                                                   new KeyValuePair<string, fsCalculatorControl>(
                                                       "Cake Wash Out Content X", new fsCakeWashOutContentControl())
                                               });

            treeView1.ExpandAll();
            treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];

            if (!string.IsNullOrEmpty(CurrentFilePath))
            {
                OpenMdbFileFromPath(CurrentFilePath);
            }
        }

        private void AddGroupToTree(string nodeName,
                            IEnumerable<KeyValuePair<string, fsCalculatorControl>> calculationControls)
        {
            var node = new TreeNode(nodeName);
            foreach (var pair in calculationControls)
            {
                fsCalculatorControl calculatorControl = pair.Value;
                AddModuleToTree(node, pair.Key, calculatorControl);
                if (calculatorControl is fsOptionsSingleTableAndCommentsCalculatorControl)
                {
                    (calculatorControl as fsOptionsSingleTableAndCommentsCalculatorControl).AllowDiagramView = false;
                }
            }
            treeView1.Nodes.Add(node);
        }

        private void AddModuleToTree(TreeNode treeNode, string moduleName, fsCalculatorControl control)
        {
            if (m_modules.ContainsKey(moduleName))
                throw new Exception("Module with such name is already added.");
            m_modules[moduleName] = control;
            treeNode.Nodes.Add(moduleName).NodeFont = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
        }

        #endregion

        #region Events

        private void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Nodes.Count > 0)
            {
                TreeNode node = treeView1.SelectedNode;
                treeView1.SelectedNode = node.Nodes[0];
                return;
            }

            if (!m_modules.ContainsKey(treeView1.SelectedNode.Text))
                return;

            string selectedCalculatorControlName = treeView1.SelectedNode.Text;
            currentModuleTitleLabel.Text = selectedCalculatorControlName;

            if (SelectedCalculatorControl != null)
            {
                SelectedCalculatorControl.Parent = null;
            }

            SelectedCalculatorControl = m_modules[selectedCalculatorControlName];
            ResizeWindowToFitBigModule(SelectedCalculatorControl);
            SelectedCalculatorControl.Parent = modulePanel;
            SelectedCalculatorControl.Dock = DockStyle.Fill;
            SelectedCalculatorControl.AplySelectedCalculatorSettings();
        }

        void ResizeWindowToFitBigModule(fsCalculatorControl module)
        {
            int neededHeight = module.Height;

            int maxHeight = Convert.ToInt32(Screen.GetWorkingArea(this).Height * 0.8);

            int newControlHeight;

            if (neededHeight > modulePanel.Height)
            {
                newControlHeight = neededHeight - modulePanel.Height;
                Height += newControlHeight + 8;

                if (Height > maxHeight)
                {
                    Height = maxHeight;
                }
            }
        }

        private void UnitsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var unitsDialog = new fsUnitsDialog();
            unitsDialog.ShowDialog();
            if (unitsDialog.DialogResult == DialogResult.OK)
            {
                SelectedCalculatorControl.SetUnits(unitsDialog.Characteristics);
                foreach (var unit in unitsDialog.Characteristics)
                {
                    //unit.Key.CurrentUnit = unit.Value;
                }
            }
        }

        #endregion

        #region Save and Open files

        class SavingTags
        {
            public const string CurrentSelectedModuleTableName = "CurrentSelectedModuleName";
            public const string CurrentSelectedModuleColumnName = "ModuleName";

            public const string CurrentSelectedUnitsTableName = "CurrentSelectedUnits";
            public const string CurrentSelectedUnitsCharacteristicColumnName = "Characteristic";
            public const string CurrentSelectedUnitsUnitColumnName = "Unit";

            public const string UnitsSchemeTableName = "UnitsSchemeTableName";

            public const string ModuleDataParameterNameColumnName = "ParameterName";
            public const string ModuleDataParameterValueColumnName = "ParameterValue";

            public const string ModulesCalculationOptionsTableName = "CalculationOptionNames";
            public const string ModulesCalculationOptionsModuleNameColumn = "Module";
            public const string ModulesCalculationOptionsComboboxNameColumn = "ComboBox";
            public const string ModulesCalculationOptionsCalculationOptionNameColumn = "CalculationOption";

            public const string CommentsModulesTableName = "CommentsModulesTable";
            public const string ModuleColumnName = "ModuleName";
            public const string CommentsColumnName = "Comments";

            public const string ShowHideTableName = "ShowHide";
            public const string ShowHideParameterNameColumnName = "ParameterName";
            public const string ShowHideValueColumnName = "Value";
        }

        public void SetCurrentFileNameAndCaption(string newFileName)
        {
            CurrentFilePath = newFileName;
            string currentFileName;

            if (CurrentFilePath.LastIndexOf('\\') != -1)
            {
                currentFileName = CurrentFilePath.Remove(0, CurrentFilePath.LastIndexOf('\\') + 1);

                Text = "Filtration Calculator";
                Text = currentFileName + " - " + Text;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Filtration Calculator data files(*.fcd)|*.fcd";
            
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CreateDataBaseAndConnectToIt(saveFileDialog1.FileName);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CurrentFilePath))
            {
                saveAsToolStripMenuItem_Click(sender, e);
                return;
            }

            CreateDataBaseAndConnectToIt(CurrentFilePath);
        }

        private void CreateDataBaseAndConnectToIt(string path)
        {
            DBPath = path;

            // create DB via ADOX if not exists
            if (!File.Exists(DBPath))
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath);
                cat = null;
            }

            // connect to DB
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath);
            conn.Open();


            CreateTableForModulesCalculationOptions();

            foreach (fsCalculatorControl module in m_modules.Values)
            {
                if (module.GetGroups().Count > 0)
                {
                    CreateTableForModuleInMdbFile(module);
                    SaveDataFromModuleToTable(module);
                    SaveModulesCalculationOptions(module);
                    CreateTableForCurrentSelectedUnits(module);
                    CreateTableForModulesShowHide(module);
                }

                if (module.SubCalculatorControls.Count > 0)
                {
                    SaveModulesCalculationOptions(module);
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        if (subModule.GetGroups().Count > 0)
                        {
                            CreateTableForModuleInMdbFile(subModule);
                            SaveDataFromModuleToTable(subModule);
                            SaveModulesCalculationOptions(subModule);
                            CreateTableForCurrentSelectedUnits(subModule);
                            CreateTableForModulesShowHide(subModule);
                        }
                    }
                }
            }
            CreateTableFordUnitsSchemes();
            CreateTableForCurrentSelectedModuleName();
            CreateTableForModulesComments(m_modules);
            conn.Close();
            SetCurrentFileNameAndCaption(DBPath);
        }

        private void CreateTableForModuleInMdbFile(fsCalculatorControl module)
        {
            string ParameterNameColumn = SavingTags.ModuleDataParameterNameColumnName;
            string ParameterValueColumn = SavingTags.ModuleDataParameterValueColumnName;

            try
            {
                using (OleDbCommand cmd = new OleDbCommand("DROP TABLE [" + module.Name + "Data];", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString =
                        String.Format("CREATE TABLE [{0}Data] ([{1}] STRING, [{2}] NUMBER);",
                            module.Name,
                            ParameterNameColumn,
                            ParameterValueColumn);

                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }
        }

        private void SaveDataFromModuleToTable(fsCalculatorControl module)
        {
            var involvedParameters = module.GetInvolvedParametersWithValue();

            List<fsParametersGroup> groups = module.GetGroups();

            string ParameterNameColumn = SavingTags.ModuleDataParameterNameColumnName;
            string ParameterValueColumn = SavingTags.ModuleDataParameterValueColumnName;

            foreach (var group in groups)
            {
                try
                {
                    string commandString =
                        String.Format("Insert into [{0}Data] ({1}, {2}) VALUES ('{3}',{4})",
                            module.Name,
                            ParameterNameColumn,
                            ParameterValueColumn,
                            group.Representator.Name,
                            involvedParameters[group.Representator].Value.Value.ToString(CultureInfo.InvariantCulture));
                                       
                    using (
                        OleDbCommand cmd =
                            new OleDbCommand(commandString,conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }
            }
        }

        private void CreateTableForModulesCalculationOptions()
        {
            string tableName = SavingTags.ModulesCalculationOptionsTableName;

            try
            {
                using (OleDbCommand cmd = new OleDbCommand("DROP TABLE [" + tableName + "];", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = string.Format("CREATE TABLE [{0}] ([{1}] STRING,[{2}] STRING,[{3}] STRING);",
                    tableName, SavingTags.ModulesCalculationOptionsModuleNameColumn,
                    SavingTags.ModulesCalculationOptionsComboboxNameColumn,
                    SavingTags.ModulesCalculationOptionsCalculationOptionNameColumn);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }
        }

        private void SaveModulesCalculationOptions(fsCalculatorControl module)
        {
            var comboBoxes = module.GetCurrentCalculationOptions();
            string tableName = SavingTags.ModulesCalculationOptionsTableName;
            

            foreach (var cb in comboBoxes)
            {
                try
                {
                    ComboBox comboBox = cb as ComboBox;
                    string commandString =
                        string.Format("Insert into [{0}] ([{1}], [{2}], [{3}]) VALUES ('{4}','{5}','{6}')", tableName,
                            SavingTags.ModulesCalculationOptionsModuleNameColumn,
                            SavingTags.ModulesCalculationOptionsComboboxNameColumn,
                            SavingTags.ModulesCalculationOptionsCalculationOptionNameColumn,
                            module.Name, comboBox.Name, comboBox.SelectedItem.ToString());
                    
                    using (
                        OleDbCommand cmd =
                            new OleDbCommand(commandString,conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }
            }
        }

        private void CreateTableForCurrentSelectedModuleName()
        {
            string tableName = SavingTags.CurrentSelectedModuleTableName;
            string columnName = SavingTags.CurrentSelectedModuleColumnName;

            try
            {
                string commandString = String.Format("DROP TABLE [{0}]",tableName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = String.Format("CREATE TABLE [{0}] ([{1}] STRING);", tableName, columnName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = String.Format("Insert into [{0}] ([{1}]) VALUES ('{2}')", tableName, columnName,
                    treeView1.SelectedNode.Text);
                using (
                    OleDbCommand cmd = new OleDbCommand(commandString,conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                if (ex != null) ex = null;
            }
        }

        private void CreateTableForCurrentSelectedUnits(fsCalculatorControl module)
        {
            string tableName = module.Name + SavingTags.CurrentSelectedUnitsTableName;
            string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
            string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

            try
            {
                string commandString = String.Format("DROP TABLE [{0}]", tableName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = String.Format("CREATE TABLE [{0}] ([{1}] STRING, [{2}] STRING);", tableName, characteristicColumnName, unitsColumnName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            foreach (var unit in module.GetUnits())
            {
                try
                {
                    string commandString = String.Format("Insert into [{0}] ([{1}], [{2}]) VALUES ('{3}', '{4}')",
                        tableName, characteristicColumnName, unitsColumnName,
                        unit.Key.Name, unit.Value.Name);
                    using (
                        OleDbCommand cmd =
                            new OleDbCommand(commandString,conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }
            }
        }

        private void CreateTableFordUnitsSchemes()
        {
            foreach (FieldInfo field in typeof (fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme) field.GetValue(null));

                string tableName = scheme.Name + SavingTags.UnitsSchemeTableName;
                string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
                string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

                try
                {
                    string commandString = String.Format("DROP TABLE [{0}]", tableName);
                    using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }

                try
                {
                    string commandString = String.Format("CREATE TABLE [{0}] ([{1}] STRING, [{2}] STRING);", tableName,
                        characteristicColumnName, unitsColumnName);
                    using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }

                foreach (var unit in scheme.CharacteristicToUnit)
                {
                    try
                    {
                        string commandString = String.Format("Insert into [{0}] ([{1}], [{2}]) VALUES ('{3}', '{4}')",
                            tableName, characteristicColumnName, unitsColumnName,
                            unit.Key.Name, unit.Value.Name);
                        using (
                            OleDbCommand cmd =
                                new OleDbCommand(commandString, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex != null) ex = null;
                    }
                }
            }
        }

        public void CreateTableForModulesComments(Dictionary<string, fsCalculatorControl> modules)
        {
            string tableName = SavingTags.CommentsModulesTableName;
            string moduleNameColumn = SavingTags.ModuleColumnName;
            string commentColumn = SavingTags.CommentsColumnName;

            try
            {
                string commandString = String.Format("DROP TABLE [{0}]", tableName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = String.Format("CREATE TABLE [{0}] ([{1}] STRING, [{2}] STRING);", tableName, moduleNameColumn, commentColumn);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            foreach (var module in modules.Values)
            {
                if (module.SubCalculatorControls.Count > 0)
                {
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        InsertModuleCommentsInTable(tableName, moduleNameColumn, commentColumn, subModule);
                    }
                }
                else
                {
                    InsertModuleCommentsInTable(tableName, moduleNameColumn, commentColumn, module);
                }
            }
        }

        void InsertModuleCommentsInTable(string tableName, string moduleNameColumn, string commentColumn,
            fsCalculatorControl module)
        {
            try
            {
                string commandString = String.Format("Insert into [{0}] ([{1}], [{2}]) VALUES ('{3}', '{4}')",
                    tableName, moduleNameColumn, commentColumn, module.Name, module.GetCommentsText());
                using (
                    OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }
        }

        private void CreateTableForModulesShowHide(fsCalculatorControl module)
        {
            string tableName = module.Name + SavingTags.ShowHideTableName;
            string parameterColumnName = SavingTags.ShowHideParameterNameColumnName;
            string valueColumnName = SavingTags.ShowHideValueColumnName;

            try
            {
                string commandString = String.Format("DROP TABLE [{0}]", tableName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            try
            {
                string commandString = String.Format("CREATE TABLE [{0}] ([{1}] STRING, [{2}] STRING);", tableName, parameterColumnName, valueColumnName);
                using (OleDbCommand cmd = new OleDbCommand(commandString, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { if (ex != null) ex = null; }

            foreach (var parameter in module.GetInvolvedParametersWithVisibleStatus())
            {
                try
                {
                    string commandString = String.Format("Insert into [{0}] ([{1}], [{2}]) VALUES ('{3}', '{4}')",
                        tableName, parameterColumnName, valueColumnName,
                        parameter.Key.FullName + parameter.Key.Name, parameter.Value);
                    using (
                        OleDbCommand cmd =
                            new OleDbCommand(commandString, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    if (ex != null) ex = null;
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Filtration Calculator data files (*.fcd)|*.fcd|All files |*.*";

            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenMdbFileFromPath(openFileDialog1.FileName);
            }
        }

        private void OpenMdbFileFromPath(string path)
        {
            DBPath = path;

            if (!File.Exists(DBPath))
            {
                MessageBox.Show("Error! File missing!");
            }

            // connect to DB
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath);
            conn.Open();

            foreach (fsCalculatorControl module in m_modules.Values)
            {
                module.Initialize();
                if (module.SubCalculatorControls.Count>0)
                {
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        subModule.Initialize();
                    }
                }
            }

            LoadModulesCalclationOptions();

            foreach (fsCalculatorControl module in m_modules.Values)
            {
                if (module.SubCalculatorControls.Count>0)
                {
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        LoadModulesData(subModule);
                        LoadCurrentSelectedUnits(subModule);
                        LoadModulesComments(subModule);
                        LoadModulesShowHide(subModule);
                    }
                }
                else
                {
                    LoadModulesData(module);
                    LoadCurrentSelectedUnits(module);
                    LoadModulesComments(module);
                    LoadModulesShowHide(module);
                }
            }
            LoadUnitsShemes();
            LoadCurrentSelectedModule();

            conn.Close();
            SetCurrentFileNameAndCaption(DBPath);
        }

        private void LoadCurrentSelectedModule()
        {
            string tableName = SavingTags.CurrentSelectedModuleTableName;
            
            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();
            adapter.Fill(dtMain);

            string moduleNameToSelect = dtMain.Rows[0][0].ToString();

            foreach (TreeNode node in treeView1.Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Text == moduleNameToSelect)
                    {
                        treeView1.SelectedNode = childNode;
                        //return;
                    }
                }
            }
            SelectedCalculatorControl.AplySelectedCalculatorSettings();
        }

        private void LoadCurrentSelectedUnits(fsCalculatorControl module)
        {
            string tableName = module.Name + SavingTags.CurrentSelectedUnitsTableName;
            string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
            string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

            bool tableFound = false;
            using (DataTable dt = conn.GetSchema("Tables"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_TYPE")].ToString() == "TABLE")
                    {
                        if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")].ToString() == String.Format(tableName))
                        {
                            tableFound = true;
                        }
                    }
                }
            }

            if (!tableFound)
                return;

            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();

            adapter.Fill(dtMain);

            Dictionary<fsCharacteristic, fsUnit> characteristicsWithCurrentUnit =
                new Dictionary<fsCharacteristic, fsUnit>();

            foreach (var characteristicWithUnit in module.GetUnits())
            {
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    if (dtMain.Rows[i][characteristicColumnName].ToString() == characteristicWithUnit.Key.Name)
                    {
                        foreach (fsUnit unit in characteristicWithUnit.Key.Units)
                        {
                            if (unit.Name == dtMain.Rows[i][unitsColumnName].ToString())
                            {
                                characteristicWithUnit.Key.CurrentUnit = unit;
                                characteristicsWithCurrentUnit.Add(characteristicWithUnit.Key, unit);
                                break;
                            }
                        }
                    }
                }
            }
            //module.SetUnits(characteristicsWithCurrentUnit);
            module.LoadUnits(characteristicsWithCurrentUnit);
        }

        private void LoadUnitsShemes()
        {
            foreach (FieldInfo field in typeof (fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));

                string tableName = scheme.Name + SavingTags.UnitsSchemeTableName;
                string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
                string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

                bool tableFound = false;
                using (DataTable dt = conn.GetSchema("Tables"))
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_TYPE")].ToString() == "TABLE")
                        {
                            if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")].ToString() ==
                                String.Format(tableName))
                            {
                                tableFound = true;
                            }
                        }
                    }
                }

                if (!tableFound)
                    return;

                string commandString = String.Format("SELECT * FROM {0}", tableName);

                adapter = new OleDbDataAdapter(commandString, conn);
                new OleDbCommandBuilder(adapter);

                dtMain = new DataTable();

                adapter.Fill(dtMain);

                Dictionary<fsCharacteristic, fsUnit> characteristicsWithCurrentUnit =
                    new Dictionary<fsCharacteristic, fsUnit>();

                foreach (var characteristicWithUnit in scheme.CharacteristicToUnit)
                {
                    for (int i = 0; i < dtMain.Rows.Count; i++)
                    {
                        if (dtMain.Rows[i][characteristicColumnName].ToString() == characteristicWithUnit.Key.Name)
                        {
                            foreach (fsUnit unit in characteristicWithUnit.Key.Units)
                            {
                                if (unit.Name == dtMain.Rows[i][unitsColumnName].ToString())
                                {
                                    characteristicWithUnit.Key.CurrentUnit = unit;
                                    characteristicsWithCurrentUnit.Add(characteristicWithUnit.Key, unit);
                                    break;
                                }
                            }
                        }
                    }
                }
                scheme.SetCharacteristics(characteristicsWithCurrentUnit);
            }
        }

        private void LoadModulesData(fsCalculatorControl module)
        {
            bool tableFound=false;
            using (DataTable dt = conn.GetSchema("Tables"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_TYPE")].ToString() == "TABLE")
                    {
                        if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")].ToString()== String.Format(module.Name+"Data"))
                        {
                            tableFound = true;
                        }
                    }
                }
            }

            if(!tableFound)
                return;

            List<fsParametersGroup> groups = module.GetGroups();

            string ParameterNameColumn = SavingTags.ModuleDataParameterNameColumnName;
            string ParameterValueColumn = SavingTags.ModuleDataParameterValueColumnName;

            string commandString = String.Format("SELECT * FROM {0}Data", module.Name);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();
            adapter.Fill(dtMain);


            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                foreach (fsParametersGroup group in groups)
                {
                    foreach (fsParameterIdentifier parameter in group.Parameters)
                    {
                        if (parameter.Name == dtMain.Rows[i][ParameterNameColumn].ToString())
                        {
                            module.SetParamatersValue(parameter, new fsValue((double)dtMain.Rows[i][ParameterValueColumn]));
                        }
                    }
                }
            }
            
            module.RecalculateAndRedraw();
        }

        private void LoadModulesCalclationOptions()
        {
            string tableName = SavingTags.ModulesCalculationOptionsTableName;

            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();
            adapter.Fill(dtMain);

            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                foreach (fsCalculatorControl module in m_modules.Values)
                {
                    if (dtMain.Rows[i][SavingTags.ModulesCalculationOptionsModuleNameColumn].ToString() == module.Name)
                    {
                        foreach (var cb in module.GetCurrentCalculationOptions())
                        {
                            if (cb.Name ==
                                dtMain.Rows[i][SavingTags.ModulesCalculationOptionsComboboxNameColumn].ToString())
                            {
                                ComboBox comboBox = cb as ComboBox;

                                int index =
                                    comboBox.Items.IndexOf(
                                        dtMain.Rows[i][SavingTags.ModulesCalculationOptionsCalculationOptionNameColumn].ToString());

                                if (index>-1)
                                {
                                    comboBox.SelectedItem = comboBox.Items[index];
                                }
                            }
                        }
                    }

                    if (module.SubCalculatorControls.Count > 0)
                    {
                        foreach (var subModule in module.SubCalculatorControls)
                        {

                            if (dtMain.Rows[i][SavingTags.ModulesCalculationOptionsModuleNameColumn].ToString() == subModule.Name)
                            {
                                foreach (var cb in subModule.GetCurrentCalculationOptions())
                                {
                                    if (cb.Name ==
                                        dtMain.Rows[i][SavingTags.ModulesCalculationOptionsComboboxNameColumn].ToString())
                                    {
                                        ComboBox comboBox = cb as ComboBox;

                                        int index =
                                            comboBox.Items.IndexOf(
                                                dtMain.Rows[i][SavingTags.ModulesCalculationOptionsCalculationOptionNameColumn].ToString());

                                        if (index > -1)
                                        {
                                            comboBox.SelectedItem = comboBox.Items[index];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadModulesComments(fsCalculatorControl module)
        {
            string tableName = SavingTags.CommentsModulesTableName;
            if (!isDBContainsTable(tableName))
                return;

            string moduleNameColumn = SavingTags.ModuleColumnName;
            string commentColumn = SavingTags.CommentsColumnName;

            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();
            adapter.Fill(dtMain);

            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                if (module.Name == dtMain.Rows[i][moduleNameColumn].ToString())
                {
                    module.SetCommentsText(dtMain.Rows[i][commentColumn].ToString());
                }
            }

            module.RecalculateAndRedraw();
        }

        private void LoadModulesShowHide(fsCalculatorControl module)
        {
            string tableName = module.Name + SavingTags.ShowHideTableName;
            string parameterColumnName = SavingTags.ShowHideParameterNameColumnName;
            string valueColumnName = SavingTags.ShowHideValueColumnName;

            if (!isDBContainsTable(tableName))
                return;

            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();

            adapter.Fill(dtMain);

            Dictionary<fsParameterIdentifier, bool> parametersWithShowHideValue =
                new Dictionary<fsParameterIdentifier, bool>();

            foreach (var parameterShowHide in module.GetInvolvedParametersWithVisibleStatus())
            {
                for (int i = 0; i < dtMain.Rows.Count; i++)
                {
                    if (dtMain.Rows[i][parameterColumnName].ToString() == parameterShowHide.Key.FullName + parameterShowHide.Key.Name)
                    {
                        bool value = Convert.ToBoolean(dtMain.Rows[i][valueColumnName].ToString());
                        parametersWithShowHideValue.Add(parameterShowHide.Key, value);
                    }
                }
            }

            module.ShowAndHideParameters(parametersWithShowHideValue);
        }

        private bool isDBContainsTable(string tableName)
        {
            bool tableFound = false;

            using (DataTable dt = conn.GetSchema("Tables"))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_TYPE")].ToString() == "TABLE")
                    {
                        if (dt.Rows[i].ItemArray[dt.Columns.IndexOf("TABLE_NAME")].ToString() == String.Format(tableName))
                        {
                            tableFound = true;
                        }
                    }
                }
            }

            return tableFound;
        }

        #endregion

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_modules.Clear();
            treeView1.Nodes.Clear();
            Text = "Filtration Calculator";
            CurrentFilePath = "";
            Form1Load(sender, e);
        }

        private void commentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var commentsDialog = new CommentsWindow();
            commentsDialog.ShowDialog(SelectedCalculatorControl);
            if (commentsDialog.DialogResult == DialogResult.OK)
            {
                SelectedCalculatorControl.SetCommentsText(commentsDialog.GetText());
            }
        }

        private void showHideParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new fsShowHideParametersDialog();

            form.InvolveParameters(SelectedCalculatorControl.GetInvolvedParametersWithVisibleStatus(), SelectedCalculatorControl.GetCurrentGroups());
            form.ShowDialog();

            if (form.DialogResult == DialogResult.OK)
            {
                Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide = form.GetParametersToShowAndHide();
                SelectedCalculatorControl.ShowAndHideParameters(parametersToShowAndHide);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fsSmallCalculatorMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult res = MessageBox.Show("Do you want to save data before exit?", "Exit Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            //if (res == DialogResult.Yes)
            //    saveToolStripMenuItem_Click(sender, new EventArgs());

            //if (res == DialogResult.Cancel)
            //    e.Cancel = true;

            if (!string.IsNullOrEmpty(CurrentFilePath))
            {
                Registry.SetValue(
                    @"HKEY_CURRENT_USER\Software\NICIFOS\FiltratonCalculator",
                    "LastFile",
                    CurrentFilePath,
                    RegistryValueKind.String);
            }
        }
    }
}
