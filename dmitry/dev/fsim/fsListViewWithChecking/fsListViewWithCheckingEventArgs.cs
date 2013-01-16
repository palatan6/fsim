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

        private List<fsCheckItemInfo> m_checkStates;

        private List<KeyValuePair<string, fsCheckItemInfo[]>> m_rechargeCommonStates;

        private List<KeyValuePair<string, fsCheckItemInfo>> m_rechargeRemoveStates;

        private List<KeyValuePair<string, fsCheckItemInfo>> m_addStates;

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

        public fsListViewWithCheckingEventArgs(List<fsCheckItemInfo> checkStates)
        {
            m_checkStates = checkStates;
        }

        public fsListViewWithCheckingEventArgs(List<KeyValuePair<string, fsCheckItemInfo>> addStates)
        {
            m_addStates = addStates;
        }

        public fsListViewWithCheckingEventArgs(
            List<KeyValuePair<string, fsCheckItemInfo>> removeStates,
            List<KeyValuePair<string, fsCheckItemInfo[]>> commonStates,
            List<KeyValuePair<string, fsCheckItemInfo>> addStates)
        {
            m_rechargeRemoveStates = removeStates;
            m_rechargeCommonStates = commonStates;
            m_addStates = addStates;
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
        /// True, if the source fsListViewWithChecking instance is settled and  
        /// a target fsListViewWithChecking instance isn't settled in a drag-and-drop operation; 
        /// false, if the source fsListViewWithChecking instance and the target one are settled.
        /// </summary>
        public bool IsPreparatory { get { return m_isPreparatory; } }

        /// <summary>
        /// List of specific states (indices, check values etc.) assosiated with some item names;
        /// </summary>
        public List<fsCheckItemInfo> CheckStates { get { return m_checkStates; } }

        public List<KeyValuePair<string, fsCheckItemInfo[]>>
            RechargeCommonStates { get { return m_rechargeCommonStates; } }

        public List<KeyValuePair<string, fsCheckItemInfo>>
            RechargeRemoveStates { get { return m_rechargeRemoveStates; } }

        public List<KeyValuePair<string, fsCheckItemInfo>>
            AddStates { get { return m_addStates; } }
    }
}