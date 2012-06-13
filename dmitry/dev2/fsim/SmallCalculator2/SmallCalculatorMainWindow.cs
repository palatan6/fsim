using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CalculatorModules;
using CalculatorModules.Base_Controls;

namespace SmallCalculator2
{
    public partial class fsSmallCalculatorMainWindow : Form
    {
        #region Data

        private readonly Dictionary<string, fsCalculatorControl> m_modules =
            new Dictionary<string, fsCalculatorControl>();

        public fsCalculatorControl SelectedCalculatorControl { get; private set; }

        #endregion

        #region Constructor

        public fsSmallCalculatorMainWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Load

        private void Form1Load(object sender, EventArgs e)
        {
            CheckProtection();

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
                                                                                                new fsCakePorossityControl
                                                                                                    ()),
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Cake Permeability/Resistance and Cake Compressibility",
                                                      new fsPermeabilityControl())
                                              });
            AddGroupToTree("Cake Formation", new[]
                                                 {
                                                     new KeyValuePair<string, fsCalculatorControl>(
                                                         "Calculations Cake Formation", new fsLaboratoryFiltrationTime())
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
            if (!m_modules.ContainsKey(treeView1.SelectedNode.Text))
                return;

            string selectedCalculatorControlName = treeView1.SelectedNode.Text;
            currentModuleTitleLabel.Text = selectedCalculatorControlName;

            if (SelectedCalculatorControl != null)
            {
                SelectedCalculatorControl.Parent = null;
            }

            SelectedCalculatorControl = m_modules[selectedCalculatorControlName];
            SelectedCalculatorControl.Parent = modulePanel;
            SelectedCalculatorControl.Dock = DockStyle.Fill;
        }

        private void UnitsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var unitsDialog = new fsUnitsDialog();
            unitsDialog.ShowDialog();
            if (unitsDialog.DialogResult == DialogResult.OK)
            {
                foreach (var module in m_modules.Values)
                {
                    module.SetUnits(unitsDialog.Characteristics);
                }
                foreach (var unit in unitsDialog.Characteristics)
                {
                    unit.Key.CurrentUnit = unit.Value;
                }
            }
        }

        #endregion

        #region Wibu Protection

        bool m_checkProtectionWorks;

        private void CheckProtection()
        {
            if (m_checkProtectionWorks)
                return;

            m_checkProtectionWorks = true;

            var keys = new int[] {
                301150,
                701190,
                901170
            };

            bool isKeyFound = false;

            while (!isKeyFound)
            {

                foreach (int key in keys)
                {
                    if (ProtectionOK(key))
                    {
                        isKeyFound = true;
                        break;
                    }
                }

                if (!isKeyFound)
                {
                    DialogResult dialogResult = MessageBox.Show(null,
                        "A suitable WIBU_BOX entry wasn't found.\n\nYou can't work with the calculator without a key.\nPlease insert the key and press retry button.\nOr press cancel button to close the program.",
                        "Protection",
                        MessageBoxButtons.RetryCancel);
                    if (dialogResult == DialogResult.Cancel)
                    {
                        Close();
                        break;
                    }
                }
            }

            m_checkProtectionWorks = false;
        }
        private bool ProtectionOK(int usercode)
        {
            // no protection
            
            return true;



            // with protection

            //bool res = false;
            //try
            //{
            //    WIBUKEYLib.WibukeyClass wk = new WIBUKEYLib.WibukeyClass();
            //    if (wk != null)
            //    {
            //        wk.SetBoxEntryForEncryption();
            //        wk.WkBoxSimUsed = false;
            //        wk.FirmCode = 619;
            //        wk.UserCode = usercode;
            //        wk.Encrypt();
            //        res = wk.LastErrorCode == 0;
            //    }
            //}
            //catch (Exception e)
            //{
            //}

            //return res;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckProtection();
        }

        #endregion


    }
}
