using System;
using System.Windows.Forms;

namespace fmDataGrid
{
    public class fmDataGridViewNumericalTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        public fmDataGridViewNumericalTextBoxEditingControl()
        {
            KeyPress += textBox_KeyPress;
            TextChanged += textBox_TextChanged;
        }

        // ReSharper disable InconsistentNaming
        public void textBox_KeyPress(object sender, KeyPressEventArgs e)
        // ReSharper restore InconsistentNaming
        {
            var key = (Keys)e.KeyChar;
            fmNumericalTextBoxHelper.CheckKey(ref key);
            e.KeyChar = (char)key;
        }

        // ReSharper disable InconsistentNaming
        public void textBox_TextChanged(object sender, EventArgs e)
        // ReSharper restore InconsistentNaming
        {
            fmNumericalTextBoxHelper.CheckTextBox(sender as TextBox);
        }
    }
}
