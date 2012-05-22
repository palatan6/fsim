using System;
using System.Windows.Forms;

namespace fmDataGrid
{
    public class fmDataGridViewNumericalTextBoxColumn : DataGridViewColumn
    {
        public fmDataGridViewNumericalTextBoxColumn() : base(new fmDataGridViewNumericalTextBoxCell()) { }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(fmDataGridViewNumericalTextBoxCell)))
                    throw new InvalidCastException("Cell must be a NumericalTextBoxCell");
                base.CellTemplate = value;
            }
        }
    }
}
