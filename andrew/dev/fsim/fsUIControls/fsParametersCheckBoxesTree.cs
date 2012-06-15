using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;

namespace fsUIControls
{
    public partial class fsParametersCheckBoxesTree : UserControl
    {
        private Dictionary<TreeNode, fsParameterIdentifier> m_leafsToParameters = new Dictionary<TreeNode, fsParameterIdentifier>();
        private Dictionary<fsParameterIdentifier, bool> m_involvedParametersWithCheckStatus = new Dictionary<fsParameterIdentifier, bool>();

        public fsParametersCheckBoxesTree()
        {
            InitializeComponent();

        }

        #region Tree Initialization

        public void InitializeTree(Dictionary<fsParameterIdentifier, bool> involvedParametersWithCheckStatus)
        {
            m_involvedParametersWithCheckStatus = involvedParametersWithCheckStatus;

            treeView1.Nodes.Clear();

            AddGroupToTree("Densities", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.MotherLiquidDensity,
                    fsParameterIdentifier.LiquidDensity,
                    fsParameterIdentifier.SolidsDensity,
                    fsParameterIdentifier.SuspensionDensity
                });

            AddGroupToTree("Concentrations", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                    fsParameterIdentifier.SuspensionSolidsConcentration
                });

            AddGroupToTree("eps0, kappa0 (Dp = 1)", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.Ne,
                    fsParameterIdentifier.CakePorosity0,
                    fsParameterIdentifier.Kappa0,
                    fsParameterIdentifier.DryCakeDensity0
                });

            AddGroupToTree("eps, kappa", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.CakePorosity,
                    fsParameterIdentifier.Kappa,
                    fsParameterIdentifier.DryCakeDensity
                });

            AddGroupToTree("Pc0, rc0, alpha0", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.CakePermeability0,
                    fsParameterIdentifier.CakeResistance0,
                    fsParameterIdentifier.CakeResistanceAlpha0
                });
            AddGroupToTree("n, tc, tr", treeView1.Nodes,
                new[]
                {
                    fsParameterIdentifier.NumberOfCyclones,                
                    fsParameterIdentifier.RotationalSpeed,
                    fsParameterIdentifier.ResidualTime
                });

            treeView1.ExpandAll();
        }

        private void AddGroupToTree(
            string nodeName,
            TreeNodeCollection treeNodeCollection,
            IEnumerable<fsParameterIdentifier> parameters)
        {
            var groupNode = new TreeNode(nodeName);
            int checkedLeafsCount = 0;
            foreach (var parameterIdentifier in parameters)
            {
                if (m_involvedParametersWithCheckStatus.ContainsKey(parameterIdentifier))
                {
                    TreeNode leaf = groupNode.Nodes.Add(parameterIdentifier.ToString());
                    if (m_involvedParametersWithCheckStatus[parameterIdentifier])
                    {
                        leaf.Checked = true;
                        ++checkedLeafsCount;
                    }
                    m_leafsToParameters.Add(leaf, parameterIdentifier);
                }
            }
            if (groupNode.Nodes.Count > 0)
            {
                groupNode.Checked = groupNode.Nodes.Count == checkedLeafsCount;
                treeNodeCollection.Add(groupNode);
            }
        }

        #endregion

        public Dictionary<fsParameterIdentifier, bool> GetParametersToShowAndHide()
        {
            var parametersToShowAndHide = new Dictionary<fsParameterIdentifier, bool>();
            foreach (TreeNode node in treeView1.Nodes)
            {
                AddCheckedParametersToList(node, parametersToShowAndHide);
            }
            return parametersToShowAndHide;
        }

        private void AddCheckedParametersToList(TreeNode node, Dictionary<fsParameterIdentifier, bool> checkedParameters)
        {
            if (node.Nodes.Count == 0)
            {
                checkedParameters.Add(m_leafsToParameters[node], node.Checked);
            }
            else
            {
                foreach (TreeNode subNode in node.Nodes)
                {
                    AddCheckedParametersToList(subNode, checkedParameters);
                }
            }
        }


    }
}
