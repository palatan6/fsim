using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Parameters;
using CalculatorModules;

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

        public void InitializeTree(Dictionary<fsParameterIdentifier, bool> involvedParametersWithCheckStatus,List<fsParametersGroup> groups)
        {
            m_involvedParametersWithCheckStatus = involvedParametersWithCheckStatus;

            treeView1.Nodes.Clear();

            foreach (var group in groups)
            {
                if (group.Parameters.Count > 1)
                {

                    AddGroupToTree(group.Parameters[0].Name + " Group", treeView1.Nodes,
                        group.Parameters);
                }
            }

            treeView1.ExpandAll();

            if (treeView1.Nodes.Count<1)
            {
                label1.Visible = true;
            }
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
                    TreeNode leaf = groupNode.Nodes.Add(parameterIdentifier.Name);
                    if (m_involvedParametersWithCheckStatus[parameterIdentifier])
                    {
                        leaf.Checked = true;
                        ++checkedLeafsCount;
                        GrayNodeIfItsLastChecked(leaf);
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

        void GrayNodeIfItsLastChecked(TreeNode node)
        {
            if (node.Parent != null)
            {
                var parentNode = node.Parent;
                int checkedCount = 0;

                TreeNode lastCheckedNode = new TreeNode();

                foreach (TreeNode childeNode in parentNode.Nodes)
                {
                    if (childeNode.Checked)
                    {
                        ++checkedCount;
                        lastCheckedNode = childeNode;
                    }

                    childeNode.ForeColor = Color.Black;
                }

                if (checkedCount == 1)
                {
                    lastCheckedNode.ForeColor = Color.Gray;
                }
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

        public int GetNodesNumber()
        {
            return treeView1.GetNodeCount(true);
        }

        public int GetNodeHieght()
        {
            return treeView1.ItemHeight;
        }
    }
}
