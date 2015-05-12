using System;
using System.Windows.Forms;

namespace fsUIControls
{
    public partial class fsParametersWithValuesTable : UserControl
    {
        public fsParametersWithValuesTable()
        {
            InitializeComponent();

            #region Assign CellValueChangedByUser

            fmDataGrid1.CellValueChangedByUser += ProcessThisValueChangedByUser;
            fmDataGrid1.RowTemplate.Height = 16;
            fmDataGrid1.RowTemplate.MinimumHeight = 2;

            #endregion
        }

        public int MinimumRowHeight
        {
            get { return fmDataGrid1.RowTemplate.MinimumHeight; }
        }

        public int RowHeight
        {
            get { return fmDataGrid1.RowTemplate.Height; }
        }

        public DataGridViewRowCollection Rows
        {
            get { return fmDataGrid1.Rows; }
        }

        public bool EndEdit()
        {
            return fmDataGrid1.EndEdit();
        }

        public event DataGridViewCellEventHandler CellValueChangedByUser;

        public static event Action SomethingChanged;

        private void ProcessThisValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            if (CellValueChangedByUser != null)
            {
                CellValueChangedByUser(sender, e);
            }

            if (SomethingChanged!= null)
            {
                SomethingChanged();
            }
        }
    }
}
