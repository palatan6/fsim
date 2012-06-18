using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CalculatorModules;
using CalculatorModules.BeltFiltersWithReversibleTrays;
using CalculatorModules.Cake_Fromation.Other_Filters_Controls;
using CalculatorModules.Hydrocyclone;
using CalculatorModules.CakeWashing;
using CalculatorModules.Cake_Fromation;

namespace Calculator
{
    public partial class fsModulesForm : Form
    {
        private readonly Dictionary<string, fsCalculatorControl> m_modules =
            new Dictionary<string, fsCalculatorControl>();

        public fsModulesForm()
        {
            InitializeComponent();
            SelectedCalculatorControl = null;
        }

        public fsCalculatorControl SelectedCalculatorControl { get; private set; }
        public string SelectedCalculatorControlName { get; private set; }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            SelectedCalculatorControl.Dock = DockStyle.None;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ModulesFormLoad(object sender, EventArgs e)
        {
            AddSimulationGroup(treeView1.Nodes);
            AddHelpGroup(treeView1.Nodes);

            treeView1.ExpandAll();

            treeView1.SelectedNode = treeView1.Nodes[0];
            while (treeView1.SelectedNode.Nodes.Count > 0)
            {
                treeView1.SelectedNode = treeView1.SelectedNode.Nodes[0];
            }
        }

        private void AddSimulationGroup(TreeNodeCollection treeNodeCollection)
        {
            const string name = "Simulation Modules";
            TreeNode node = treeNodeCollection.Add(name);
            AddGroupToTree("Cake Formation", node.Nodes, new[]
                                                             {
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Belt Filters with Reversible Trays",
                                                                     new fsBeltFilterWithReversibleTrayControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Continuous Belt Filters (modular)",
                                                                     new fsContinuousModularBeltFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Continuous Belt Filters (non modular)",
                                                                     new fsContinuousNonModularBeltFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Drum Filters",
                                                                     new fsDrumFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Disc Filters",
                                                                     new fsDiscFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Pan Filters",
                                                                     new fsPanFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Rotary Pressure Filters",
                                                                     new fsRotaryPressureFilters()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Nutche Filters",
                                                                     new fsNutcheFilters()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Pressure Leaf Filters",
                                                                     new fsPressureLeafFilters()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Filter Presses",
                                                                     new fsFilterPressesControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Filter Press Automats",
                                                                     new fsFilterPressAutomatControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Pneuma Presses",
                                                                     new fsPneumaPressControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Laboratory Pressure Nutsche Filters",
                                                                     new fsLaboratoryPressureNutscheFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Laboratory Vacuum Filters",
                                                                     new fsLaboratoryVacuumFilterControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Other",
                                                                     new fsCommonCakeFormationControl()),
                                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                                     "Cake Formation Overmodule",
                                                                     new fsCakeFormationOvermoduleControl())

                                                             });
        }

        private void AddHelpGroup(TreeNodeCollection treeNodeCollection)
        {
            const string name = "Help Modules";
            TreeNode node = treeNodeCollection.Add(name);
            AddGroupToTree("Suspension", node.Nodes, new[]
                                             {
                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                     "Densities and Suspension Solids Content",
                                                     new fsDensityConcentrationControl()),
                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                     "Suspension Solids Mass Fraction",
                                                     new SuspensionSolidsMassFractionControl())
                                             });
            AddGroupToTree("Filter Cake", node.Nodes, new[]
                                              {
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Filter Cake & Suspension Relations", new fsMsusAndHcControl()),
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Cake Porosity from Test Data",
                                                      new fsCakePorossityControl
                                                          ()),
                                                  new KeyValuePair<string, fsCalculatorControl>(
                                                      "Cake Permeability/Resistance and Cake Compressibility",
                                                      new fsPermeabilityControl())
                                              });
            AddGroupToTree("Cake Formation", node.Nodes, new[]
                                                 {
                                                     new KeyValuePair<string, fsCalculatorControl>(
                                                         "Calculations Cake Formation", new fsLaboratoryFiltrationTime()),
                                                     new KeyValuePair<string, fsCalculatorControl>(
                                                         "Cake Formation Analysis", new CakeFormationAnalysisControl())
                                                 });
            AddGroupToTree("Cake Deliquoring", node.Nodes, new[]
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
            AddGroupToTree("Cake Washing", node.Nodes, new[]
                                               {   new KeyValuePair<string, fsCalculatorControl>(
                                                       "Calculation of Cake Washing", new fsCakeWashingControl()),
                                                   new KeyValuePair<string, fsCalculatorControl>(
                                                       "Cake Wash Out Content X", new fsCakeWashOutContentControl())
                                               });
            AddGroupToTree("Hydrocyclone", node.Nodes, new[]
                                               {
                                                   new KeyValuePair<string, fsCalculatorControl>(
                                                       "Hydrocyclone", new fsHydrocycloneControl())
                                               });
        }

        private void AddGroupToTree(
            string nodeName,
            TreeNodeCollection treeNodeCollection,
            IEnumerable<KeyValuePair<string, fsCalculatorControl>> calculationControls)
        {
            var node = new TreeNode(nodeName);
            foreach (var pair in calculationControls)
            {
                fsCalculatorControl calculatorControl = pair.Value;
                AddModuleToTree(node, pair.Key, calculatorControl);
            }
            treeNodeCollection.Add(node);
        }

        private void AddModuleToTree(TreeNode treeNode, string moduleName, fsCalculatorControl control)
        {
            if (m_modules.ContainsKey(moduleName))
                throw new Exception("Module with such name is already added.");
            m_modules[moduleName] = control;
            treeNode.Nodes.Add(moduleName).NodeFont = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
        }

        private void TreeView1AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!m_modules.ContainsKey(treeView1.SelectedNode.Text))
                return;

            SelectedCalculatorControlName = treeView1.SelectedNode.Text;
            currentModuleTitleLabel.Text = SelectedCalculatorControlName;

            fsCalculatorControl prevCalculatorControl = SelectedCalculatorControl;
            SelectedCalculatorControl = m_modules[SelectedCalculatorControlName];
            SelectedCalculatorControl.Parent = modulePanel;
            SelectedCalculatorControl.Dock = DockStyle.Fill;
            if (prevCalculatorControl != null)
            {
                prevCalculatorControl.Parent = null;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (!m_modules.ContainsKey(treeView1.SelectedNode.Text))
                return;
            OkButtonClick(sender, e);
        }
    }
}