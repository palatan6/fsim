using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ListViewWithChecking
{
    public class fsListViewWithCheckingEventArgs : EventArgs
    {
        private object m_item;

        private int m_index;

        private bool m_checked;

        private bool m_isPreparatory;

        private Dictionary<int, bool> m_indicesAndChecks;

        public fsListViewWithCheckingEventArgs()
        {
            m_isPreparatory = true;
        }

        public fsListViewWithCheckingEventArgs(ListViewItem item)
        {
            m_item = item.Text;
            m_index = item.Index;
            m_checked = item.Checked;
            m_isPreparatory = false;
        }

        public fsListViewWithCheckingEventArgs(string name, int index, bool checked_)
        {
            m_item = name;
            m_index = index;
            m_checked = checked_;
            m_isPreparatory = false;
        }

        public fsListViewWithCheckingEventArgs(Dictionary<int, bool> indicesAndChecks)
        {
            m_indicesAndChecks = indicesAndChecks;
        }

        /// <summary>
        /// A dragged item of a fsListViewWithChecking instance.
        /// </summary>
        public object Item { get { return m_item; } }

        /// <summary>
        /// The index of a dragged item  in a fsListViewWithChecking instance.
        /// </summary>
        public int Index { get { return m_index; } }

        /// <summary>
        /// The check value of a dragged item  in a fsListViewWithChecking instance.
        /// </summary>
        public bool Checked { get { return m_checked; } }

        /// <summary>
        /// True if the source fsListViewWithChecking instance is settled and  
        /// a target fsListViewWithChecking instance isn't settled in a drag-and-drop operation; 
        /// false, if the source fsListViewWithChecking instance and the target one are settled.
        /// </summary>
        public bool IsPreparatory { get { return m_isPreparatory; } }

        /// <summary>
        /// Indices of items with their new check values that changed check values after 
        /// a multiple checking operation has been done.
        /// </summary>
        public Dictionary<int, bool> IndicesAndChecks { get { return m_indicesAndChecks; } }
    }
}