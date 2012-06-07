using System.Windows.Forms;

namespace fmDataGrid
{
    public partial class fmDataGrid : DataGridView
    {
        public bool HighLightCurrentRow { get; set; }

        public fmDataGrid()
        {
            HighLightCurrentRow = false;
            InitializeComponent();
        }

        #region MouseWheel
        public bool MoveCursor(int deltaRow)
        {
            int rowIndex = CurrentCell.RowIndex + deltaRow;

            while (0 <= rowIndex
                && rowIndex < Rows.Count
                && Rows[rowIndex].Visible == false)
            {
                rowIndex += deltaRow;
            }

            if (0 <= rowIndex
                && rowIndex < Rows.Count)
            {
                CurrentCell = Rows[rowIndex].Cells[CurrentCell.ColumnIndex];
                return true;
            }
            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x20a)
            {
                if (CurrentCell != null
                    && CurrentCell.RowIndex != -1
                    && CurrentCell.ColumnIndex != -1)
                {
                    int x = (int)(((long)m.WParam >> 16) & ((1 << 16) - 1));
                    int delta = x < (1 << 15) ? x : x - (1 << 16);

                    if (delta > 0)
                    {
                        MoveCursor(-1);
                    }
                    else if (delta < 0)
                    {
                        MoveCursor(1);
                    }

                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        public T AddColumn<T>(string headerText) where T : DataGridViewColumn, new()
        {
            var column = new T {HeaderText = headerText};
            Columns.Add(column);
            return column;
        }
    }
}

