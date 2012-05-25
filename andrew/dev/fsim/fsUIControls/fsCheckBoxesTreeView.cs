using System.Windows.Forms;

namespace fsUIControls
{
    public class fsCheckBoxesTreeView : TreeView
    {
        public fsCheckBoxesTreeView()
        {
            CheckBoxes = true;
            AfterCheck += TreeViewAfterCheck;
        }

        private bool m_isTreeUpdating = false;

        private static void CheckSubtree(TreeNode treeNode, bool isChecked)
        {
            treeNode.Checked = isChecked;
            foreach (TreeNode node in treeNode.Nodes)
            {
                CheckSubtree(node, isChecked);
            }
        }

        private void TreeViewAfterCheck(object sender, TreeViewEventArgs e)
        {
            if (m_isTreeUpdating)
                return;

            m_isTreeUpdating = true;
            foreach (TreeNode node in e.Node.Nodes)
            {
                CheckSubtree(node, e.Node.Checked);
            }
            UpdateParentCheckStatus(e.Node.Parent);
            m_isTreeUpdating = false;
        }

        private void UpdateParentCheckStatus(TreeNode treeNode)
        {
            if (treeNode == null)
                return;

            int checkedCount = 0;
            foreach (TreeNode node in treeNode.Nodes)
            {
                if (node.Checked)
                {
                    ++checkedCount;
                }
            }
            treeNode.Checked = checkedCount == treeNode.Nodes.Count;
            UpdateParentCheckStatus(treeNode.Parent);
        }

    }
}
