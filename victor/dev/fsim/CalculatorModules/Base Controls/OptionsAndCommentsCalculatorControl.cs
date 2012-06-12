using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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

        private bool m_isDiagramVisible;

        public bool GetDiagramVisible()
        {
            return m_isDiagramVisible;
        }

        public void SetDiagramVisible(bool isVisible)
        {
            if (m_isDiagramVisible == isVisible)
            {
                return;
            }

            m_isDiagramVisible = isVisible;

            Control controlToResize = ControlToResizeForExpanding ?? Parent;

            if (isVisible)
            {
                rightPanel.Visible = true;
                showHideCommnetsButton.Text = @"<";
                if (controlToResize != null)
                {
                    splitContainer1.Panel2Collapsed = false;
                    int w = splitContainer1.Width;
                    int a = splitContainer1.Panel1.Width;
                    int s = splitContainer1.SplitterWidth;
                    int newWidth = (2 * w * w + s * a) / (2 * a);
                    controlToResize.Width += newWidth - w;
                }   
            }
            else
            {
                rightPanel.Visible = false;
                showHideCommnetsButton.Text = @">";
                if (controlToResize != null)
                {
                    if (controlToResize is Form)
                    {
                        var formToResize = (Form) controlToResize;
                        if (formToResize.WindowState == FormWindowState.Maximized)
                        {
                            formToResize.WindowState = FormWindowState.Normal;
                        }
                    }
                    controlToResize.Width -= splitContainer1.Panel2.Width + splitContainer1.SplitterWidth;
                }
                splitContainer1.Panel2Collapsed = true;
                rightPanel.Width = 0;
            }
        }

        // This property allows user to see 'expand' button
        public bool AllowDiagramView
        {
            set { showHideDiagramPanel.Visible = value; }
        }

        protected override void InitializeCalculatorContorl()
        {
            if (IsCalculatorControlInitialized)
                return;

            IsCalculatorControlInitialized = true;

            InitializeCalculators();
            InitializeGroups();
            InitializeCalculationOptionsUIControls();
            UpdateGroupsInputInfoFromCalculationOptions();
            InitializeParametersValues();
            UpdateEquationsFromCalculationOptions();
            InitializeDefaultDiagrams();
            SetDefaultDiagramFromCalculationOption();
            RecalculateAndRedraw();
            ConnectUIWithDataUpdating(GetUIControlsToConnectWithDataUpdating());
        }

        protected override void CalculationOptionChanged(object sender, EventArgs e)
        {
            UpdateCalculationOptionFromUI();
            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            SetDefaultDiagramFromCalculationOption();
            RecalculateAndRedraw();
        }

        #region Default Diagram For Every Calculation Option

        protected class DiagramConfiguration
        {
            public fsParameterIdentifier xAxisParameter { get; set; }
            public fsParameterIdentifier yAxisParameter { get; set; }
            public fsParameterIdentifier y2AxisParameter { get; set; }

            public DiagramConfiguration(
                fsParameterIdentifier xAxisParameter,
                fsParameterIdentifier yAxisParameter,
                fsParameterIdentifier y2AxisParameter)
            {
                this.xAxisParameter = xAxisParameter;
                this.yAxisParameter = yAxisParameter;
                this.y2AxisParameter = y2AxisParameter;
            }

            public DiagramConfiguration(
                fsParameterIdentifier xAxisParameter,
                fsParameterIdentifier yAxisParameter)
            {
                this.xAxisParameter = xAxisParameter;
                this.yAxisParameter = yAxisParameter;
                y2AxisParameter = null;
            }
        }

        private class EqualityComparer : IEqualityComparer<ICollection<Enum>>
        {

            public bool Equals(ICollection<Enum> x, ICollection<Enum> y)
            {
                if (x.Count != y.Count)
                {
                    return false;
                }
                return x.All(y.Contains);
            }

            public int GetHashCode(ICollection<Enum> obj)
            {
                int key = 0;
                foreach (Enum e in obj)
                {
                    key = key * 19 + e.GetHashCode();
                }
                return key;
            }
        }

        protected Dictionary<ICollection<Enum>, DiagramConfiguration> m_defaultDiagrams = new Dictionary<ICollection<Enum>, DiagramConfiguration>(new EqualityComparer());

        protected virtual void InitializeDefaultDiagrams()
        {
        }

        private void SetDefaultDiagramFromCalculationOption()
        {
            if (m_defaultDiagrams.ContainsKey(CalculationOptions.Values))
            {
                DiagramConfiguration diagram = m_defaultDiagrams[CalculationOptions.Values];
                fsTableAndChart1.SetDiagram(diagram.xAxisParameter, diagram.yAxisParameter, diagram.y2AxisParameter);
            }
        }

        #endregion

        #region Internal Routine

        public override void RecalculateAndRedraw()
        {
            base.RecalculateAndRedraw();
            fsTableAndChart1.AssignCalculatorData(Values,
                Groups,
                ParameterToGroup,
                Calculators);
            fsTableAndChart1.RefreshAndRecalculateAll();
        }

        private void Button1Click(object sender, EventArgs e)
        {
            SetDiagramVisible(!m_isDiagramVisible);
        }

        #endregion
    }
}