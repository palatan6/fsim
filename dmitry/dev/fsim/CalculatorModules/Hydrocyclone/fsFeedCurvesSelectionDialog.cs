using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Parameters;
using CalculatorModules.User_Controls.Help_Dialogs;

namespace CalculatorModules.Hydrocyclone.Feeds
{
    public partial class fsFeedCurvesSelectionDialog : Form
    {
        public fsFeedCurvesSelectionDialog()
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
            SetY2Visible(splitContainer1.Panel2Collapsed);
            
        }

        private void fsFeedCurvesSelectionDialog_Load(object sender, EventArgs e)
        {
            SetY2Visible(y2SelectionControl.GetCheckedYAxisParameters().Count != 0);
        }

        private void SetY2Visible(bool isVisible)
        {
            if (isVisible == !splitContainer1.Panel2Collapsed)
                return;

            if (isVisible)
            {
                splitContainer1.Panel2Collapsed = false;
                Width = Width * 2;
            }
            else
            {
                splitContainer1.Panel2Collapsed = true;
                Width = Width / 2;
            }
        }
    }
}
