using System;
using System.Windows.Forms;

namespace fmDataGrid
{
    public class fmDataGridViewNumericalTextBoxCell : DataGridViewTextBoxCell
    {
        public fmDataGridViewNumericalTextBoxEditingControl EditBox { get; private set; }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            EditBox = DataGridView.EditingControl as fmDataGridViewNumericalTextBoxEditingControl;
            // ReSharper disable PossibleNullReferenceException
            EditBox.Height = OwningRow.Height;
            string stringInCell = Convert.ToString(Value);
            EditBox.Text = stringInCell;
            // ReSharper restore PossibleNullReferenceException
        }
        public override void DetachEditingControl()
        {
            base.DetachEditingControl();
            EditBox = null;
        }
         

        public override Type EditType
        {
            get
            {
                return typeof(fmDataGridViewNumericalTextBoxEditingControl);
            }
        }
    }
}
