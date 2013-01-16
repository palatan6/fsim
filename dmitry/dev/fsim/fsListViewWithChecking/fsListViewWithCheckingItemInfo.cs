namespace ListViewWithChecking
{
    public class fsCheckItemInfo
    {
        private readonly int m_index;

        private readonly bool m_checked;

        public int Index { get { return m_index; } }

        public bool Checked { get { return m_checked; } }

        public fsCheckItemInfo(int index, bool checked_)
        {
            m_index = index;
            m_checked = checked_;
        }
    }

    public class fsDragDropItemInfo
    {
        private readonly int m_sourceIndex;

        private readonly int m_targetIndex;

        private readonly bool m_checked;

        protected readonly object m_item;

        public int SourceIndex { get { return m_sourceIndex; } }

        public int TargetIndex { get { return m_targetIndex; } }

        public bool Checked { get { return m_checked; } }

        public object Item { get { return m_item; } }

        public fsDragDropItemInfo(int sourceIndex, int targetIndex, bool checked_, object item)
        {
            m_sourceIndex = sourceIndex;
            m_targetIndex = targetIndex;
            m_checked = checked_;
            m_item = item;
        }
    }
}