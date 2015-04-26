using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules;
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


        public void InvolveParameters(Dictionary<fsParameterIdentifier, bool> involvedParameters, List<fsParametersGroup> groups)
        {
            fsParametersCheckBoxesTree1.InitializeTree(involvedParameters, groups);
        }

        public Dictionary<fsParameterIdentifier, bool> GetParametersToShowAndHide()
        {
            return fsParametersCheckBoxesTree1.GetParametersToShowAndHide();
        }

        private void fsShowHideParametersDialog_Load(object sender, EventArgs e)
        {
            ResizeWindowToFitAllTreeNodes();
        }

        void ResizeWindowToFitAllTreeNodes()
        {
            int neededHeight = fsParametersCheckBoxesTree1.GetNodeHieght() * fsParametersCheckBoxesTree1.GetNodesNumber();

            int maxHeight = Convert.ToInt32(Screen.GetWorkingArea(this).Height * 0.8);

            int newControlHeight;

            if (neededHeight > fsParametersCheckBoxesTree1.Height)
            {
                newControlHeight = neededHeight - fsParametersCheckBoxesTree1.Height;
                Height += newControlHeight + 8;

                if (Height > maxHeight)
                {
                    Height = maxHeight;
                }
            }
        }
    }
}
