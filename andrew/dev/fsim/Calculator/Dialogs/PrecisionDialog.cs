using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Value;

namespace Calculator.Dialogs
{
    public partial class PrecisionDialog : Form
    {
        public PrecisionDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fsValue.OutputPrecision = Convert.ToInt32(numericUpDown1.Value);
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PrecisionDialog_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = fsValue.OutputPrecision;
        }
    }
}
