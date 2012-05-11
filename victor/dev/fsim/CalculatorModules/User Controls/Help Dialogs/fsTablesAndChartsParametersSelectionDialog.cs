using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Parameters;

namespace CalculatorModules.User_Controls.Help_Dialogs
{
    public partial class fsTablesAndChartsParametersSelectionDialog : Form
    {
        public fsTablesAndChartsParametersSelectionDialog()
        {
            InitializeComponent();
        }

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        internal List<fsParameterIdentifier> GetCheckedParameters()
        {
            return fsTablesAndChartsParametersSelectionControl1.GetCheckedParameters();
        }

        internal void AssignParameters(List<fsYAxisParameterWithChecking> list)
        {
            fsTablesAndChartsParametersSelectionControl1.AssignParameters(list);
        }
    }
}
