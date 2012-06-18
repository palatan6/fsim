using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Parameters;

namespace Calculator.Dialogs
{
    public partial class fsShowHideParametersDialog : Form
    {
        private Dictionary<fsParameterIdentifier, bool> m_involvedParameters = new Dictionary<fsParameterIdentifier, bool>();

        public fsShowHideParametersDialog()
        {
            InitializeComponent();
        }

        #region UI Events

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion


        internal void InvolveParameters(Dictionary<fsParameterIdentifier, bool> involvedParameters)
        {
            fsParametersCheckBoxesTree1.InitializeTree(involvedParameters);
        }

        internal Dictionary<fsParameterIdentifier, bool> GetParametersToShowAndHide()
        {
            return fsParametersCheckBoxesTree1.GetParametersToShowAndHide();
        }
    }
}
