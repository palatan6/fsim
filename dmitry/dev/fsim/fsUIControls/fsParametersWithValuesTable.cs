using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace fsUIControls
{
    public partial class fsParametersWithValuesTable : UserControl
    {
        public fsParametersWithValuesTable()
        {
            InitializeComponent();

            #region Assign CellValueChangedByUser

            this.fmDataGrid1.CellValueChangedByUser += ProcessThisValueChangedByUser;
            this.fmDataGrid1.RowTemplate.Height = 18;// Initially it was equal to 16

            #endregion
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

        private void ProcessThisValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            if (CellValueChangedByUser != null)
            {
                CellValueChangedByUser(sender, e);
            }
        }
    }
}
