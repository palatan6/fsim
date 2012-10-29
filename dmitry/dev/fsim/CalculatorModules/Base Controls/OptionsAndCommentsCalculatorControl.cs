using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Parameters;
using Units;
using Value;

namespace CalculatorModules
{
    public partial class fsOptionsAndCommentsCalculatorControl : fsCalculatorControl
    {
        public fsOptionsAndCommentsCalculatorControl()
        {
            InitializeComponent();
            splitContainer1.Panel2Collapsed = true;
        }

        #region Showing/Hiding Diagram

        private bool m_isDiagramVisible;
        private int m_windowHeightBeforeExpansion;

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

            const int minHeightOfWindowWithDiagram = 600;

            if (isVisible)
            {
                rightPanel.Visible = true;
                showHideCommentsButton.Text = @"<";
                if (controlToResize != null)
                {
                    splitContainer1.Panel2Collapsed = false;
                    int w = splitContainer1.Width;
                    int a = splitContainer1.Panel1.Width;
                    int s = splitContainer1.SplitterWidth;
                    int newWidth = (2 * w * w + s * a) / (2 * a);
                    controlToResize.Width += newWidth - w;
                    if (controlToResize is Form)
                    {
                        var form = (Form) controlToResize;
                        if (form.WindowState == FormWindowState.Normal)
                        {
                            m_windowHeightBeforeExpansion = controlToResize.Height;                                                    
                        }
                    }
                    if (controlToResize.Height < minHeightOfWindowWithDiagram)
                    {
                        controlToResize.Height = minHeightOfWindowWithDiagram;
                    }
                }   
            }
            else
            {
                rightPanel.Visible = false;
                showHideCommentsButton.Text = @">";
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

                    controlToResize.Height = m_windowHeightBeforeExpansion;
                }
                splitContainer1.Panel2Collapsed = true;
                rightPanel.Width = 0;
            }
        }

        #endregion

        // This property allows user to see 'expand' button
        public bool AllowDiagramView
        {
            set { showHideDiagramPanel.Visible = value; }
        }

        protected override void InitializeCalculatorControl()
        {
            if (IsCalculatorControlInitialized)
                return;

            IsCalculatorControlInitialized = true;

            InitializeCalculators();
            InitializeGroups();
            InitializeCalculationOptionsUIControls();
            UpdateGroupsInputInfoFromCalculationOptions();
            InitializeUnits(); 
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
            public class DiagramRange
            {
                public double From;
                public double To;

                public DiagramRange(double from, double to)
                {
                    From = from;
                    To = to;
                }
            }

            public fsParameterIdentifier xAxisParameter { get; set; }
            public fsParameterIdentifier[] yAxisParameters { get; set; }
            public fsParameterIdentifier[] y2AxisParameters { get; set; }
            public DiagramRange range { get; set; }

            public DiagramConfiguration(
                fsParameterIdentifier xAxisParameter,
                DiagramRange range,
                fsParameterIdentifier [] yAxisParameters)
            {
                this.xAxisParameter = xAxisParameter;
                this.range = range;
                this.yAxisParameters = yAxisParameters;
                this.y2AxisParameters = new fsParameterIdentifier[0];
            }

            public DiagramConfiguration(
               fsParameterIdentifier xAxisParameter,
               DiagramRange range,
               fsParameterIdentifier[] yAxisParameters,
               fsParameterIdentifier[] y2AxisParameters)
            {
                this.xAxisParameter = xAxisParameter;
                this.range = range;
                this.yAxisParameters = yAxisParameters;
                this.y2AxisParameters = y2AxisParameters;
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
                int key = 1;
                foreach (Enum e in obj)
                {
                    key = key * e.GetHashCode();
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
                fsTableAndChart1.SetDiagram(diagram.xAxisParameter, diagram.yAxisParameters, diagram.y2AxisParameters);
                if (diagram.range != null)
                {
                    Values[diagram.xAxisParameter].Range.From = new fsValue(diagram.range.From);
                    Values[diagram.xAxisParameter].Range.To = new fsValue(diagram.range.To);
                }
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