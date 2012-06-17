using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using CalculatorModules.Machine_Ranges;
using Parameters;
using ParametersIdentifiers.Ranges;
using Units;
using Value;

namespace fsUIControls
{
    public partial class fsMachineSettings : UserControl
    {
        private readonly Dictionary<string, fsMachineRanges> m_machineRanges = new Dictionary<string, fsMachineRanges>();
        public Dictionary<fsParameterIdentifier, fsParameterRange> ParameterRanges;
        private Dictionary<DataGridViewRow, fsParameterIdentifier> m_rowToParameter;

        #region Constructing

        public fsMachineSettings()
        {
            InitializeComponent();
        }

        private void MachineSettingsLoad(object sender, EventArgs e)
        {
            FillMachinesCheckBox();
            comboBox1.SelectedItem = comboBox1.Items[0];
            DisplayRangesInTable();
        }

        private void FillMachinesCheckBox()
        {
            Type type = typeof (fsMachineRanges);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                var machineRanges = ((fsMachineRanges) field.GetValue(null));
                AddMachineRange(machineRanges);
            }
        }

        private void AddMachineRange(fsMachineRanges machineRanges)
        {
            comboBox1.Items.Add(machineRanges.Name);
            m_machineRanges.Add(machineRanges.Name, machineRanges);
        }

        #endregion

        #region Events

        private void DataGridCellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            fsParameterIdentifier parameter = m_rowToParameter[dataGrid.Rows[e.RowIndex]];
            double factor = ParameterRanges[parameter].Units.Coefficient;
            ParameterRanges[parameter].Range.From = fsValue.ObjectToValue(dataGrid[2, e.RowIndex].Value) * factor;
            ParameterRanges[parameter].Range.To = fsValue.ObjectToValue(dataGrid[3, e.RowIndex].Value) * factor;
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_machineRanges.ContainsKey(comboBox1.Text))
            {
                ParameterRanges = m_machineRanges[comboBox1.Text].Ranges;
            }
            DisplayRangesInTable();
        }

        #endregion

        private void DisplayRangesInTable()
        {
            dataGrid.Rows.Clear();
            m_rowToParameter = new Dictionary<DataGridViewRow, fsParameterIdentifier>();
            foreach (fsParameterIdentifier parameter in ParameterRanges.Keys)
            {
                int index = dataGrid.Rows.Add();
                dataGrid.Rows[index].Cells[0].Value = parameter.FullName;
                fsRange range = ParameterRanges[parameter].Range;
                fsUnit units = ParameterRanges[parameter].Units;
                dataGrid.Rows[index].Cells[1].Value = units.Name;
                dataGrid.Rows[index].Cells[2].Value = range.From / units.Coefficient;
                dataGrid.Rows[index].Cells[3].Value = range.To / units.Coefficient;
                m_rowToParameter.Add(dataGrid.Rows[index], parameter);
            }
        }
    }
}