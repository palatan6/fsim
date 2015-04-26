using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Data.OleDb;
using CalculatorModules;
using System.Windows.Forms;
using Parameters;
using Units;
using Value;

namespace SmallCalculator2
{
    class SaverLoader
    {
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
        }

        string DBPath;

        static OleDbConnection conn;
        OleDbDataAdapter adapter;
        DataTable dtMain;

        private void CreateDataBaseAndConnectToIt(string path, Dictionary<string, fsCalculatorControl> modules)
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

            foreach (fsCalculatorControl module in modules.Values)
            {
                if (module.GetGroups().Count > 0)
                {
                    CreateTableForModuleInMdbFile(module);
                    SaveDataFromModuleToTable(module);
                    SaveModulesCalculationOptions(module);
                    CreateTableForCurrentSelectedUnits(module);
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
                        }
                    }
                }
            }
            CreateTableFordUnitsSchemes();
            CreateTableForCurrentSelectedModuleName();
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

        private void CreateTableForCurrentSelectedModuleName()
        {
            string tableName = SavingTags.CurrentSelectedModuleTableName;
            string columnName = SavingTags.CurrentSelectedModuleColumnName;

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
                    GetCurrentSelectedModuleName());
                using (
                    OleDbCommand cmd = new OleDbCommand(commandString, conn))
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

        private void CreateTableFordUnitsSchemes()
        {
            foreach (FieldInfo field in typeof(fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));

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

        public static void CreateTableForModulesComments(Dictionary<string, fsCalculatorControl> modules)
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
                if (module.SubCalculatorControls.Count>0)
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

        static void InsertModuleCommentsInTable(string tableName, string moduleNameColumn, string commentColumn,
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

        private void SetCurrentFileNameAndCaption(string newFileName)
        {
            MessageBox.Show("Error!");
        }

        private string GetCurrentSelectedModuleName()
        {
            MessageBox.Show("Error 2!");
            return null;
        }

        /// <summary>
        /// Открывает mdb файл , который находится по указаному адресу
        /// </summary>
        /// <param name="path"></param>
        /// <param name="modules"></param>

        private void OpenMdbFileFromPath(string path, Dictionary<string, fsCalculatorControl> modules)
        {
            DBPath = path;

            if (!File.Exists(DBPath))
            {
                MessageBox.Show("Error! File missing!");
            }

            // connect to DB
            conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DBPath);
            conn.Open();

            foreach (fsCalculatorControl module in modules.Values)
            {
                module.Initialize();
                if (module.SubCalculatorControls.Count > 0)
                {
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        subModule.Initialize();
                    }
                }
            }

            LoadModulesCalclationOptions(modules);

            foreach (fsCalculatorControl module in modules.Values)
            {
                if (module.SubCalculatorControls.Count > 0)
                {
                    foreach (var subModule in module.SubCalculatorControls)
                    {
                        LoadModulesData(subModule);
                        LoadCurrentSelectedUnits(subModule);
                    }
                }
                else
                {
                    LoadModulesData(module);
                    LoadCurrentSelectedUnits(module);
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

            foreach (TreeNode node in GetTreeView().Nodes)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    if (childNode.Text == moduleNameToSelect)
                    {
                        GetTreeView().SelectedNode = childNode;
                    }
                }
            }
            SelectedCalculatorControl().AplySelectedCalculatorSettings();
        }

        private void LoadCurrentSelectedUnits(fsCalculatorControl module)
        {
            string tableName = module.Name + SavingTags.CurrentSelectedUnitsTableName;
            string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
            string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

            if (!isDBContainsTable(tableName))
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
            
            module.LoadUnits(characteristicsWithCurrentUnit);
        }

        private void LoadUnitsShemes()
        {
            foreach (FieldInfo field in typeof(fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));

                string tableName = scheme.Name + SavingTags.UnitsSchemeTableName;
                string characteristicColumnName = SavingTags.CurrentSelectedUnitsCharacteristicColumnName;
                string unitsColumnName = SavingTags.CurrentSelectedUnitsUnitColumnName;

                if (!isDBContainsTable(tableName))
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
            if (!isDBContainsTable(module.Name + "Data"))
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

        private void LoadModulesCalclationOptions(Dictionary<string, fsCalculatorControl> modules)
        {
            string tableName = SavingTags.ModulesCalculationOptionsTableName;

            string commandString = String.Format("SELECT * FROM {0}", tableName);

            adapter = new OleDbDataAdapter(commandString, conn);
            new OleDbCommandBuilder(adapter);

            dtMain = new DataTable();
            adapter.Fill(dtMain);

            for (int i = 0; i < dtMain.Rows.Count; i++)
            {
                foreach (fsCalculatorControl module in modules.Values)
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

                                if (index > -1)
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

        private fsCalculatorControl SelectedCalculatorControl()
        {
            MessageBox.Show("Error 3!");
            return null;
        }

        private TreeView GetTreeView()
        {
            MessageBox.Show("Error 4!");
            return null;
        }
    }
}
