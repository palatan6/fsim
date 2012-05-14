using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CalculatorModules.User_Controls.Help_Dialogs;
using Parameters;
using ParametersIdentifiers;
using ParametersIdentifiers.Ranges;
using StepCalculators;
using Value;

namespace CalculatorModules.User_Controls
{
    public partial class fsTableAndChart : UserControl
    {
        #region Private Data

        private readonly List<fsDiagramWithTable.fsNamedArray> m_y2Curves = new List<fsDiagramWithTable.fsNamedArray>();
        private readonly List<fsDiagramWithTable.fsNamedArray> m_yCurves = new List<fsDiagramWithTable.fsNamedArray>();
        private List<fsCalculator> m_calculators;

        private Dictionary<fsParameterIdentifier, List<fsSimulationModuleParameter>> m_data =
            new Dictionary<fsParameterIdentifier, List<fsSimulationModuleParameter>>();

        private List<fsParametersGroup> m_groups;
        private Dictionary<fsParameterIdentifier, fsParametersGroup> m_parameterToGroup;
        private Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> m_values;
        private fsParameterIdentifier m_iterationParameter;

        private readonly List<fsParameterIdentifier> m_yAxisParameters = new List<fsParameterIdentifier>();
        private readonly List<fsParameterIdentifier> m_y2AxisParameters = new List<fsParameterIdentifier>();

        #endregion

        #region Constructor

        public fsTableAndChart()
        {
            InitializeComponent();

            m_values = new Dictionary<fsParameterIdentifier, fsSimulationModuleParameter>();
        }

        #endregion

        public void AssignCalculatorData(
            Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> values,
            List<fsParametersGroup> groups,
            Dictionary<fsParameterIdentifier, fsParametersGroup> parameterToGroup,
            List<fsCalculator> calculators)
        {
            m_values = new Dictionary<fsParameterIdentifier, fsSimulationModuleParameter>(values);
            m_groups = new List<fsParametersGroup>(groups);
            m_parameterToGroup = new Dictionary<fsParameterIdentifier, fsParametersGroup>(parameterToGroup);
            m_calculators = new List<fsCalculator>(calculators);
        }

        public void SetDefaultDiagram(
            fsParameterIdentifier iterationParameter,
            fsParameterIdentifier yAxisParameter,
            fsParameterIdentifier y2AxisParameter)
        {
            m_iterationParameter = iterationParameter;
            m_yAxisParameters.Clear();
            m_yAxisParameters.Add(yAxisParameter);
            m_y2AxisParameters.Clear();
            m_y2AxisParameters.Add(y2AxisParameter);
        }

        #region Reprocessing

        private bool m_inputRefreshing;

        public void RefreshAndRecalculateAll()
        {
            RefreshInputAndReadIterationParameter();
            CalculateData();
            RefreshOutputAndReadXyParameters();
            RedrawTableAndChart();
        }

        private void RefreshOutputAndReadXyParameters()
        {
            RefreshXAxisList(m_xAxisComboBox);
            RefreshYAxisList(m_yAxisParameters, m_yAxisList);
            RefreshYAxisList(m_y2AxisParameters, m_y2AxisList);
        }

        private void RefreshInputAndReadIterationParameter()
        {
            if (!m_inputRefreshing)
            {
                m_inputRefreshing = true;

                RefreshIterationList();

                m_iterationParameter = m_values.Keys.FirstOrDefault(parameter => parameter.Name == iterationList.Text);

                RefreshRangesBoxes();

                if (detalizationBox.Text == "")
                {
                    detalizationBox.Text = @"50";
                }

                RefreshInputsBox();

                m_inputRefreshing = false;
            }
        }

        private void RefreshRangesBoxes()
        {
            fsRange range = m_values[m_iterationParameter].Range;
            double factor = m_values[m_iterationParameter].Unit.Coefficient;
            rangeFrom.Text = (range.From / factor).ToString();
            rangeTo.Text = (range.To / factor).ToString();
        }

        #region RefreshInputs

        private void RefreshInputsBox()
        {
            var materialInputData = new List<string>();
            var machiningSettingsInputData = new List<string>();
            foreach (fsParametersGroup group in m_groups)
            {
                if (group.Parameters.Contains(m_iterationParameter))
                    continue;

                if (group.IsInput)
                {
                    fsParameterIdentifier parameter = group.Representator;
                    string line = parameter.Name
                        + "\t" + m_values[parameter].Unit.Name
                        + "\t" + m_values[parameter].GetValueInUnits();
                    if (group.Kind == fsParametersGroup.ParametersGroupKind.MaterialParameters)
                    {
                        materialInputData.Add(line);
                    }
                    else
                    {
                        machiningSettingsInputData.Add(line);
                    }
                }
            }

            var lines = new List<string>();
            lines.AddRange(materialInputData);
            lines.Add("------------------------------------");
            lines.AddRange(machiningSettingsInputData);
            
            inputsTextBox.ForeColor = Color.Blue;
            inputsTextBox.Lines = lines.ToArray();
        }

        private void RefreshIterationList()
        {
            iterationList.Items.Clear();
            foreach (fsParametersGroup group in m_groups)
            {
                if (group.IsInput)
                {
                    foreach (fsParameterIdentifier parameter in group.Parameters)
                    {
                        iterationList.Items.Add(parameter.Name);
                    }
                }
            }
            if (m_iterationParameter != null && iterationList.Items.Contains(m_iterationParameter.Name))
            {
                iterationList.SelectedIndex = iterationList.Items.IndexOf(m_iterationParameter.Name);
            }
            else
            {
                iterationList.SelectedItem = iterationList.Items[0];
            }
        }

        #endregion

        #region Calculations

        private void CalculateData()
        {
            if (m_inputRefreshing)
                return;

            var detalization = (int)fsValue.StringToValue(detalizationBox.Text).Value;
            if (detalization < 2)
            {
                detalization = 2;
            }
            double factor = m_values[m_iterationParameter].Unit.Coefficient;
            fsValue from = fsValue.StringToValue(rangeFrom.Text) * factor;
            fsValue to = fsValue.StringToValue(rangeTo.Text) * factor;

            m_data = new Dictionary<fsParameterIdentifier, List<fsSimulationModuleParameter>>();
            for (int i = 0; i <= detalization; ++i)
            {
                Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> currentValues =
                    m_values.ToDictionary(pair => pair.Key, pair => new fsSimulationModuleParameter(pair.Value));

                fsParametersGroup xInitialgroup = m_parameterToGroup[m_iterationParameter];
                var xNewGroup = new fsParametersGroup(xInitialgroup) {Representator = m_iterationParameter};
                SubstituteGroup(m_parameterToGroup, xInitialgroup, xNewGroup);

                currentValues[m_iterationParameter].Value = from + (to - from) * i / detalization;
                fsCalculationProcessor.ProcessCalculatorParameters(currentValues, m_parameterToGroup, m_calculators);

                SubstituteGroup(m_parameterToGroup, xNewGroup, xInitialgroup);

                foreach (var pair in currentValues)
                {
                    if (m_data.ContainsKey(pair.Key) == false)
                    {
                        m_data.Add(pair.Key, new List<fsSimulationModuleParameter>());
                    }
                    m_data[pair.Key].Add(pair.Value);
                }
            }
        }

        private void RedrawTableAndChart()
        {
            if (m_inputRefreshing)
                return;

            BuildCurves();
            RefreshDiagramAndTable();
        }

        private void BuildCurves()
        {
            BuildCurves(m_yCurves, m_yAxisList);
            BuildCurves(m_y2Curves, m_y2AxisList);
        }

        private void BuildCurves(List<fsDiagramWithTable.fsNamedArray> curves, ListView yAxisListView)
        {
            if (m_data == null)
                return;

            curves.Clear();
            curves.AddRange(from pair in m_data
                            select pair.Key
                            into parameter where IsContains(yAxisListView.CheckedItems, parameter.Name)
                            select GetArray(parameter));
        }

        private fsDiagramWithTable.fsNamedArray GetArray(fsParameterIdentifier yParameter)
        {
            List<fsSimulationModuleParameter> values = m_data[yParameter];
            var array = new fsDiagramWithTable.fsNamedArray
                            {Name = yParameter.Name + " [" + m_data[yParameter][0].Unit.Name + "]", Array = new fsValue[values.Count]};
            for (int i = 0; i < values.Count; ++i)
            {
                array.Array[i] = values[i].GetValueInUnits();
            }
            return array;
        }

        #endregion

        #region Refresh Output

        private void RefreshXAxisList(ComboBox xAxisComboBox)
        {
            if (m_inputRefreshing)
                return;

            string oldText = xAxisComboBox.Text;
            xAxisComboBox.Items.Clear();
            IEnumerable<fsYAxisParameter> classifyiedParameters = GetSelectionParameters(m_values.Keys);
            foreach (fsYAxisParameter classifyiedParameter in classifyiedParameters)
            {
                if (classifyiedParameter.Kind == fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter)
                {
                    xAxisComboBox.Items.Add(classifyiedParameter.Identifier.Name);
                }
            }
            xAxisComboBox.Text = xAxisComboBox.Items.Contains(oldText) ? oldText : m_iterationParameter.Name;
        }

        private void RefreshYAxisList(IEnumerable<fsParameterIdentifier> parameters, ListView yAxisListView)
        {
            if (m_inputRefreshing)
                return;

            var inputList = new List<KeyValuePair<string, bool>>();
            var constResultsList = new List<KeyValuePair<string, bool>>();
            var variableResultsList = new List<KeyValuePair<string, bool>>();

            foreach (fsYAxisParameter selectionParameter in GetSelectionParameters(parameters))
            {
                string parameterName = selectionParameter.Identifier.Name;
                bool isChecked = IsContains(yAxisListView.CheckedItems, parameterName)
                                 || !IsContains(yAxisListView.Items, parameterName);
                var pair = new KeyValuePair<string, bool>(parameterName, isChecked);
                if (selectionParameter.Kind == fsYAxisParameter.fsYParameterKind.InputParameter)
                {
                    inputList.Add(pair);
                }
                if (selectionParameter.Kind == fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter)
                {
                    constResultsList.Add(pair);
                }
                if (selectionParameter.Kind == fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter)
                {
                    variableResultsList.Add(pair);
                }
            }

            var newList = new List<ListViewItem>();
            newList.AddRange(
                variableResultsList.Select(
                    keyValuePair =>
                    new ListViewItem(keyValuePair.Key) {Checked = keyValuePair.Value, ForeColor = Color.Black}));
            newList.AddRange(
                constResultsList.Select(
                    keyValuePair =>
                    new ListViewItem(keyValuePair.Key) {Checked = keyValuePair.Value, ForeColor = Color.LightGray}));
            newList.AddRange(
                inputList.Select(
                    keyValuePair =>
                    new ListViewItem(keyValuePair.Key) {Checked = keyValuePair.Value, ForeColor = Color.Blue}));
            if (yAxisListView.Items.Count != newList.Count)
            {
                ListViewItem[] array = newList.ToArray();
                yAxisListView.Items.Clear();
                yAxisListView.Items.AddRange(array);
            }
            else
            {
                bool different = false;
                if (
                    newList.Where(
                        (t, i) =>
                        t.Text != yAxisListView.Items[i].Text || t.Checked != yAxisListView.Items[i].Checked ||
                        t.ForeColor != yAxisListView.Items[i].ForeColor).Any())
                {
                    different = true;
                }
                if (different)
                {
                    for (int i = 0; i < newList.Count; ++i)
                    {
                        yAxisListView.Items[i].Text = newList[i].Text;
                        yAxisListView.Items[i].Checked = newList[i].Checked;
                        yAxisListView.Items[i].ForeColor = newList[i].ForeColor;
                    }
                }
            }
        }

        private static bool IsConstantList(List<fsSimulationModuleParameter> list)
        {
            if (list.Count <= 1)
            {
                return true;
            }
            fsValue minValue = list[0].GetValueInUnits();
            fsValue maxValue = list[0].GetValueInUnits();
            for (int i = 1; i < list.Count; ++i)
            {
                fsValue currentValue = list[i].GetValueInUnits();
                if (!minValue.Defined
                    || currentValue.Defined && minValue.Value > currentValue.Value)
                {
                    minValue = currentValue;
                }
                if (!maxValue.Defined
                    || currentValue.Defined && maxValue.Value < currentValue.Value)
                {
                    maxValue = currentValue;
                }
            }
            fsValue delta = maxValue - minValue;
            fsValue deviation = delta / fsValue.Max(fsValue.Abs(minValue), fsValue.Abs(maxValue));
            return deviation <= new fsValue(1e-9);
        }

        private void RefreshDiagramAndTable()
        {
            if (m_data.Count == 0)
                return;

            fsParameterIdentifier xParameter = m_iterationParameter;
            foreach (fsParameterIdentifier parameterIdentifier in m_values.Keys)
            {
                if (parameterIdentifier.Name == m_xAxisComboBox.Text)
                {
                    xParameter = parameterIdentifier;
                    break;
                }
            }
            fsDiagramWithTable.fsNamedArray xArray = GetArray(xParameter);
            fsDiagramWithTable.fsNamedArray niceXArray = MakeIncreasingNiceNodes(xArray);

            fsDiagramWithTable1.SetXAxis(niceXArray);

            fsDiagramWithTable1.ClearYAxis();
            foreach (fsDiagramWithTable.fsNamedArray curve in m_yCurves)
            {
                fsDiagramWithTable1.AddYAxis(CalculteWithLinearization(curve, xArray, niceXArray));
            }

            fsDiagramWithTable1.ClearY2Axis();
            foreach (fsDiagramWithTable.fsNamedArray curve in m_y2Curves)
            {
                fsDiagramWithTable1.AddY2Axis(CalculteWithLinearization(curve, xArray, niceXArray));
            }

            fsDiagramWithTable1.Redraw();
        }

        private static fsDiagramWithTable.fsNamedArray MakeIncreasingNiceNodes(fsDiagramWithTable.fsNamedArray xArray)
        {
            double[] x = xArray.GetDoublesArray();
            if (x[0] > x[x.Length - 1])
            {
                x = x.Reverse().ToArray();
            }
            double low = x[0];
            double high = x[x.Length - 1];
            if (high == low)
            {
                var result = new fsDiagramWithTable.fsNamedArray
                                 {
                                     Name = xArray.Name,
                                     Array = new fsValue[xArray.Array.Length]
                                 };
                xArray.Array.CopyTo(result.Array, 0);
                return result;
            }
            double eps = (high - low) * 1e-9;
            var steps = new[]
                            {
                                1,
                                2,
                                2.5,
                                5
                            };
            for (var deg = (int)Math.Ceiling(Math.Log10((high - low) / steps[steps.Length - 1])); ; --deg)
            {
                for (int i = steps.Length - 1; i >= 0; --i)
                {
                    double step = steps[i] * Math.Pow(10.0, deg);
                    var nodesAmount = (int)(Math.Ceiling(high / step) - Math.Floor(low / step));
                    if (nodesAmount >= x.Length)
                    {
                        var nodes = new List<fsValue> {new fsValue(low)};
                        for (int j = 0; ; ++j)
                        {
                            double node = (Math.Ceiling(low / step + eps) + j) * step;
                            if (node >= high)
                            {
                                break;
                            }
                            nodes.Add(new fsValue(node));
                        }
                        nodes.Add(new fsValue(high));
                        var result = new fsDiagramWithTable.fsNamedArray {Name = xArray.Name, Array = nodes.ToArray()};
                        return result;
                    }
                }
            }
        }

        private static fsDiagramWithTable.fsNamedArray CalculteWithLinearization(
            fsDiagramWithTable.fsNamedArray yArray,
            fsDiagramWithTable.fsNamedArray xArray,
            fsDiagramWithTable.fsNamedArray niceXArray)
        {
            double[] x = xArray.GetDoublesArray();
            double[] y = yArray.GetDoublesArray();
            if (x[0] > x[x.Length - 1])
            {
                x = x.Reverse().ToArray();
                y = y.Reverse().ToArray();
            }

            var result = new fsDiagramWithTable.fsNamedArray
                             {
                                 Name = yArray.Name,
                                 Array = new fsValue[niceXArray.Array.Length]
                             };
            for (int i = 0, j = 0; i < niceXArray.Array.Length; ++i)
            {
                double currentX = niceXArray.Array[i].Value;
                while (x[j + 1] < currentX)
                {
                    ++j;
                }
                double xLow = x[j];
                double xHigh = x[j + 1];
                double yLow = y[j];
                double yHigh = y[j + 1];
                result.Array[i] = new fsValue(yLow + (currentX - xLow) / (xHigh - xLow) * (yHigh - yLow));
            }
            return result;
        }

        #endregion

        #endregion

        #region Misc

        private static bool IsContains(ListView.CheckedListViewItemCollection checkedListViewItemCollection, string name)
        {
            return checkedListViewItemCollection.Cast<ListViewItem>().Any(item => item.Text == name);
        }

        private static bool IsContains(ListView.ListViewItemCollection checkedListViewItemCollection, string name)
        {
            return checkedListViewItemCollection.Cast<ListViewItem>().Any(item => item.Text == name);
        }

        private static void SubstituteGroup(Dictionary<fsParameterIdentifier, fsParametersGroup> parameterToGroup,
                                            fsParametersGroup initialGroup, fsParametersGroup newGroup)
        {
            foreach (fsParameterIdentifier parameter in initialGroup.Parameters)
            {
                parameterToGroup[parameter] = newGroup;
            }
        }

        #endregion

        #region UI Event

        private void IterationListSelectedIndexChanged(object sender, EventArgs e)
        {
            fsParameterIdentifier newIterationParameter = m_values.Keys.FirstOrDefault(parameter => parameter.Name == iterationList.Text);
            if (m_iterationParameter != newIterationParameter)
            {
                m_iterationParameter = newIterationParameter;
                RefreshRangesBoxes();
                RefreshInputsBox();
                CalculateData();
                RefreshOutputAndReadXyParameters();
                m_xAxisComboBox.Text = m_iterationParameter.Name;  // set the same x axis parameter as iteration parameter
                                                                // we should assign text after RefreshOutputAndReadXYParameters
                                                                // because it may happen that before RefreshOutputAndReadXYParameters there are no
                                                                // element with necessary value and assigning may fail
                RedrawTableAndChart();
            }
        }

        private void YAxisListItemChecked(object sender, ItemCheckedEventArgs e)
        {
            RedrawTableAndChart();
        }

        private void XAxisComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            RedrawTableAndChart();
        }

        private void RangeFromTextChanged(object sender, EventArgs e)
        {
            m_values[m_iterationParameter].Range.From = fsValue.StringToValue(rangeFrom.Text) *
                                                    m_values[m_iterationParameter].Unit.Coefficient;
            CalculateData();
            RefreshOutputAndReadXyParameters();
            RedrawTableAndChart();
        }

        private void RangeToTextChanged(object sender, EventArgs e)
        {
            m_values[m_iterationParameter].Range.To = fsValue.StringToValue(rangeTo.Text) *
                                                  m_values[m_iterationParameter].Unit.Coefficient;
            CalculateData();
            RefreshOutputAndReadXyParameters();
            RedrawTableAndChart();
        }

        private void DetalizationBoxTextChanged(object sender, EventArgs e)
        {
            CalculateData();
            RefreshOutputAndReadXyParameters();
            RedrawTableAndChart();
        }

        private void YAxisConfigureClick(object sender, EventArgs e)
        {
            var selectionForm = new fsTablesAndChartsParametersSelectionDialog();
            selectionForm.AssignYAxisParameters(GetSelectionParametersWithCheking(m_yAxisList));
            selectionForm.AssignY2AxisParameters(GetSelectionParametersWithCheking(m_y2AxisList));
            selectionForm.ShowDialog();
            if (selectionForm.DialogResult == DialogResult.OK)
            {
                m_yAxisParameters.Clear();
                m_yAxisParameters.AddRange(selectionForm.GetCheckedYAxisParameters());
                m_y2AxisParameters.Clear();
                m_y2AxisParameters.AddRange(selectionForm.GetCheckedY2AxisParameters());
                RefreshAndRecalculateAll();
            }
        }

        private void Y2AxisConfigureClick(object sender, EventArgs e)
        {
            var selectionForm = new fsTablesAndChartsParametersSelectionDialog();
            selectionForm.AssignYAxisParameters(GetSelectionParametersWithCheking(m_y2AxisList));
            selectionForm.ShowDialog();
            if (selectionForm.DialogResult == DialogResult.OK)
            {
                m_y2AxisParameters.Clear();
                m_y2AxisParameters.AddRange(selectionForm.GetCheckedYAxisParameters());
                RefreshAndRecalculateAll();
            }
        }

        #region Selection Parameters Help

        private List<fsYAxisParameterWithChecking> GetSelectionParametersWithCheking(ListView yAxisList)
        {
            var selectionParameters =
                   new List<fsYAxisParameterWithChecking>();
            foreach (fsYAxisParameter yParameter in GetSelectionParameters(m_values.Keys))
            {
                selectionParameters.Add(
                    new fsYAxisParameterWithChecking(
                        yParameter.Identifier,
                        yParameter.Kind,
                        IsContains(yAxisList.Items, yParameter.Identifier.Name)));
            }
            return selectionParameters;
        }

        private IEnumerable<fsYAxisParameter> GetSelectionParameters(IEnumerable<fsParameterIdentifier> parameters)
        {
            var selectionParameters = new List<fsYAxisParameter>();
            foreach (fsParameterIdentifier parameter in parameters)
            {
                fsParametersGroup group = m_parameterToGroup[parameter];
                fsYAxisParameter.fsYParameterKind kind;
                if (group == m_parameterToGroup[m_iterationParameter])
                {
                    kind = fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter;
                }
                else if (group.IsInput && parameter == group.Representator)
                {
                    kind = fsYAxisParameter.fsYParameterKind.InputParameter;
                }
                else
                {
                    kind = IsConstantList(m_data[parameter])
                        ? fsYAxisParameter.fsYParameterKind.CalculatedConstantParameter
                        : fsYAxisParameter.fsYParameterKind.CalculatedVariableParameter;
                }
                selectionParameters.Add(new fsYAxisParameter(parameter, kind));
            }
            return selectionParameters;
        }

        #endregion

        #endregion
    }
}
