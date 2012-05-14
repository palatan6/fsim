using System;
using Parameters;

namespace CalculatorModules
{
    public partial class fsOptionsAndCommentsCalculatorControl : fsCalculatorControl
    {
        public fsOptionsAndCommentsCalculatorControl()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
        }

        public bool AllowCommentsView
        {
            set { showHideCommentsPanel.Visible = value; }
        }

        protected override void Recalculate()
        {
            base.Recalculate();
            fsTableAndChart1.AssignCalculatorData(Values,
                Groups,
                ParameterToGroup,
                Calculators);
            fsTableAndChart1.RefreshAndRecalculateAll();
        }

        protected void SetDefaultDiagram(
            fsParameterIdentifier xAxisParameter,
            fsParameterIdentifier yAxisParameter,
            fsParameterIdentifier y2AxisParameter)
        {
            fsTableAndChart1.SetDefaultDiagram(xAxisParameter, yAxisParameter, y2AxisParameter);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            if (rightPanel.Visible)
            {
                rightPanel.Visible = false;
                showHideCommnetsButton.Text = @">";
                if (Parent != null)
                {
                    Parent.Width -= splitContainer1.Panel2.Width + splitContainer1.SplitterWidth;
                }
                splitContainer1.Panel2Collapsed = true;
                rightPanel.Width = 0;
            }
            else
            {
                rightPanel.Visible = true;
                showHideCommnetsButton.Text = @"<";
                if (Parent != null)
                {
                    splitContainer1.Panel2Collapsed = false;
                    int w = splitContainer1.Width;
                    int a = splitContainer1.Panel1.Width;
                    int s = splitContainer1.SplitterWidth;
                    int newWidth = (2 * w * w + s * a) / (2 * a);
                    Parent.Width += newWidth - w;
                }
            }
        }
    }
}