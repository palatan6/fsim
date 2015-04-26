using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace fsUIControls
{
    public class fsCheckBoxesTreeView : TreeView
    {
        public fsCheckBoxesTreeView()
        {
            CheckBoxes = true;
            AfterCheck += TreeViewAfterCheck;
            NodeMouseClick += fsCheckBoxesTreeView_NodeMouseClick;
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

            GrayNodeIfItsLastChecked(e.Node);

            m_isTreeUpdating = false;
        }

        void GrayNodeIfItsLastChecked(TreeNode node)
        {
            if (node.Parent != null)
            {
                var parentNode = node.Parent;
                int checkedCount = 0;

                TreeNode lastCheckedNode= new TreeNode();

                foreach (TreeNode childeNode in parentNode.Nodes)
                {
                    if (childeNode.Checked)
                    {
                        ++checkedCount;
                        lastCheckedNode = childeNode;
                    }

                    childeNode.ForeColor = Color.Black;
                }

                if (checkedCount==1)
                {
                    lastCheckedNode.ForeColor = Color.Gray;
                }
            }
        }

        private void fsCheckBoxesTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) // avoid all treeNodes unchecked in sud tree
        {
            if (e.Node.Parent != null)
            {
                var parentNode = e.Node.Parent;
                bool isAllUnchecked = true;
                foreach (TreeNode childeNode in parentNode.Nodes)
                {
                    if (childeNode != e.Node && childeNode.Checked)
                    {
                        isAllUnchecked = false;
                    }
                }

                if (isAllUnchecked)
                {
                    e.Node.Checked = true;
                    SystemSounds.Hand.Play();
                }
            }
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
