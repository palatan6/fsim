﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CalculatorModules;
using CalculatorModules.Base_Controls;
using CalculatorModules.BeltFiltersWithReversibleTrays;
using CalculatorModules.Hydrocyclone;
using CalculatorModules.CakeWashing;

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

        private void AddHelpGroup(TreeNodeCollection treeNodeCollection)
        {
            string name = "Help Modules";
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
                                                         "Calculations Cake Formation", new fsLaboratoryFiltrationTime())
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
                                                       "Hydrocyclone", new HydrocycloneControl())
                                               });
        }

        private void AddSimulationGroup(TreeNodeCollection treeNodeCollection)
        {
            string name = "Simulation Modules";
            TreeNode node = treeNodeCollection.Add(name);
            AddGroupToTree("Belt Filters with Reversible Trays", node.Nodes, new[]
                                             {
                                                 new KeyValuePair<string, fsCalculatorControl>(
                                                     "Belt Filters with Reversible Trays",
                                                     new fsBeltFilterWithReversibleTrayControl()),
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
                if (calculatorControl is fsOptionsSingleTableAndCommentsCalculatorControl)
                {
                    (calculatorControl as fsOptionsSingleTableAndCommentsCalculatorControl).AllowCommentsView = false;
                }
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

            if (SelectedCalculatorControl != null)
            {
                SelectedCalculatorControl.Parent = null;
            }

            SelectedCalculatorControl = m_modules[SelectedCalculatorControlName];
            SelectedCalculatorControl.Parent = modulePanel;
            SelectedCalculatorControl.Dock = DockStyle.Fill;
        }
    }
}