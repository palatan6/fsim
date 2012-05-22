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

        internal List<fsParameterIdentifier> GetCheckedYAxisParameters()
        {
            return y1SelectionControl.GetCheckedYAxisParameters();
        }

        internal List<fsParameterIdentifier> GetCheckedY2AxisParameters()
        {
            return y2SelectionControl.GetCheckedYAxisParameters();
        }

        internal void AssignYAxisParameters(List<fsYAxisParameterWithChecking> list)
        {
            y1SelectionControl.AssignYAxisParameters(list);
        }

        internal void AssignY2AxisParameters(List<fsYAxisParameterWithChecking> list)
        {
            y2SelectionControl.AssignYAxisParameters(list);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
        }

        private void fsTablesAndChartsParametersSelectionDialog_Load(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = y2SelectionControl.GetCheckedYAxisParameters().Count == 0;
        }
    }
}
