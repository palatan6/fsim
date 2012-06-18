using System;
using System.Drawing;
using System.Windows.Forms;

namespace fsUIControls
{
    public class fsTreeView : TreeView
    {
        private readonly Color m_backColor = Color.Blue;
        private TreeNode m_node;

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            if (m_node != null)
            {
                m_node.BackColor = BackColor;
                m_node.ForeColor = ForeColor;
            }
            m_node = e.Node;
            base.OnAfterSelect(e);
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            e.Node.BackColor = m_backColor;
            e.Node.ForeColor = Color.White;
            base.OnBeforeSelect(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (m_node != null)
            {
                m_node.BackColor = m_backColor;
                m_node.ForeColor = Color.White;
            }
            base.OnLostFocus(e);
        }
    }
}