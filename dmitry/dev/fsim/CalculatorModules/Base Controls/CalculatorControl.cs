using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Parameters;
using ParametersIdentifiers;
using ParametersIdentifiers.Ranges;
using StepCalculators;
using Units;
using Value;
using fsUIControls;
using CalculatorModules.Machine_Ranges;

namespace CalculatorModules
{
    public class fsCalculatorControl : UserControl
    {
        #region Calculation Data

        protected Dictionary<Type, Enum> CalculationOptions = new Dictionary<Type, Enum>();
        protected Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> Values { get; private set; }

        #endregion

        #region UI data

        protected Dictionary<Type, Control> CalculationOptionToControl = new Dictionary<Type, Control>();
        protected Dictionary<object, fsParametersGroup> CalculationOptionToGroup =
            new Dictionary<object, fsParametersGroup>();
        protected Dictionary<Control, Type> ControlToCalculationOption = new Dictionary<Control, Type>();
        protected Dictionary<fsParameterIdentifier, DataGridViewCell> ParameterToCell { get; private set; }
        protected Dictionary<DataGridViewCell, fsParameterIdentifier> CellToParameter { get; private set; }

        #endregion


        #region CalculatorControl Initialization

        protected bool IsCalculatorControlInitialized;

        protected virtual Control[] GetUIControlsToConnectWithDataUpdating()
        {
            // This method should return array of Controls that may change
            // data(values) or calculation options (like datagrids, checkboxes, radiobuttons)
            return null;
        }

        void CalculatorControlLoad(object sender, EventArgs e)
        {
            InitializeCalculatorControl();
        }

        protected virtual void InitializeCalculatorControl()
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
            RecalculateAndRedraw();
            ConnectUIWithDataUpdating(GetUIControlsToConnectWithDataUpdating());
        }

        protected virtual void InitializeCalculators()
        {
            // This method should contain initialization of Calculators in derived class.
        }

        protected virtual void InitializeGroups()
        {
            // This method should contain initialization of Groups in derived class.
        }

        protected virtual void InitializeCalculationOptionsUIControls()
        {
            // This method should contain initialization of UI controls
            // (like comboboxes) with data corresponding to possible calculation
            // options in derived class.
        }

        protected virtual void InitializeParametersValues()
        {
            // This method should contain assigning values that will be used on startup of the module
        }

        protected virtual void InitializeUnits()
        {
            SetUnits(fsCharacteristicScheme.PilotIndustrialScale.CharacteristicToUnit);
        }

        #endregion

        protected virtual void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // You should set up groups input info in this method. Override this method in derived class
        }

        protected virtual void UpdateEquationsFromCalculationOptions()
        {
            // You should set up equations here. Override this method in derived class
        }

        protected void SetDefaultValue(fsParameterIdentifier identifier, fsValue value)
        {
            if (Values.ContainsKey(identifier))
            {
                ParameterToGroup[identifier].Representator = identifier;
                Values[identifier].Value = value;
            }
        }

        protected fsCalculatorControl()
        {
            Values = new Dictionary<fsParameterIdentifier, fsSimulationModuleParameter>();
            ParameterToCell = new Dictionary<fsParameterIdentifier, DataGridViewCell>();
            CellToParameter = new Dictionary<DataGridViewCell, fsParameterIdentifier>();
            Calculators = new List<fsCalculator>();
            Groups = new List<fsParametersGroup>();
            ParameterToGroup = new Dictionary<fsParameterIdentifier, fsParametersGroup>();

            Load += CalculatorControlLoad;
        }

        public virtual Control ControlToResizeForExpanding { get; set; }

        #region Routines

        protected List<fsCalculator> Calculators { get; set; }
        protected List<fsParametersGroup> Groups { get; private set; }
        protected Dictionary<fsParameterIdentifier, fsParametersGroup> ParameterToGroup { get; private set; }

        protected void EstablishCalculationOption(Enum option)
        {
            CalculationOptions[option.GetType()] = option;
        }

        protected void AssignCalculationOptionAndControl(Type type, ComboBox comboBox)
        {
            CalculationOptionToControl[type] = comboBox;
            ControlToCalculationOption[comboBox] = type;
        }

        protected virtual void UpdateUIFromData()
        {
            UpdateCellForeColors();
            WriteValuesToDataGrid();
            WriteCalculationOptionsToUI();
        }

        private void WriteCalculationOptionsToUI()
        {
            foreach (var keyValuePair in ControlToCalculationOption)
            {
                keyValuePair.Key.Text = fsMisc.GetEnumDescription(CalculationOptions[keyValuePair.Value]);
            }
        }

        protected virtual void CalculationOptionChanged(object sender, EventArgs e)
        {
            UpdateCalculationOptionFromUI();
            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            RecalculateAndRedraw();
        }

        protected void ConnectUIWithDataUpdating(params Control[] controls)
        {
            if (controls == null)
                return;

            foreach (Control control in controls)
            {
                if (control is fsParametersWithValuesTable)
                {
                    var grid = control as fsParametersWithValuesTable;
                    grid.CellValueChangedByUser += DataGridCellValueChangedByUser;
                }
                else if (control is RadioButton)
                {
                    throw new Exception("radio buttons doesn't supported");
                    //var radioButton = control as RadioButton;
                    //radioButton.CheckedChanged += CalculationOptionChanged;
                }
                else if (control is ComboBox)
                {
                    var comboBox = control as ComboBox;
                    comboBox.SelectedIndexChanged += CalculationOptionChanged;
                }
            }
        }

        protected void UpdateCalculationOptionFromUI()
        {
            foreach (var pair in ControlToCalculationOption)
            {
                CalculationOptions[pair.Value] = fsMisc.GetEnum(pair.Value, pair.Key.Text);
            }
        }

        protected void SetGroupInput(fsParametersGroup group, bool value)
        {
            if (!group.GetIsOnlyCalculatedFlag())
            {
                group.SetIsInputFlag(value);
            }
            bool isReadOnly = !group.GetIsInputFlag();
            foreach (fsParameterIdentifier parameter in group.Parameters)
            {
                ParameterToCell[parameter].ReadOnly = isReadOnly;
            }
        }

        public fsParametersGroup AddGroup(params fsParameterIdentifier[] parameters)
        {
            return AddGroup(false, parameters);
        }

        public fsParametersGroup AddGroup(bool isOnlyCalculated, params fsParameterIdentifier[] parameters)
        {
            var group = new fsParametersGroup(isOnlyCalculated);
            foreach (fsParameterIdentifier parameter in parameters)
            {
                group.Parameters.Add(parameter);
                ParameterToGroup[parameter] = group;
            }
            group.Representator = group.Parameters[0];
            Groups.Add(group);
            return group;
        }

        public fsParametersGroup AddOnlyCalculatedGroup(params fsParameterIdentifier[] parameters)
        {
            fsParametersGroup group = AddGroup(true, parameters);
            return group;
        }

        protected void AddGroupsToUI(fsParametersWithValuesTable dataGrid, fsParametersGroup[] groups)
        {
            if (groups == null)
                return;

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };


            dataGrid.Rows.Clear();
            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
                SetGroupInput(groups[i], true);
            }
        }
            
        protected void AddGroupToUI(fsParametersWithValuesTable dataGrid, fsParametersGroup group, Color color)
        {
            foreach (fsParameterIdentifier identifier in group.Parameters)
            {
                if (!Values.ContainsKey(identifier))
                {
                    var parameter = new fsSimulationModuleParameter(identifier);
                    var defaultRanges = fsMachineRanges.DefaultMachineRanges.Ranges;
                    if (defaultRanges.ContainsKey(identifier))
                    {
                        parameter.Range = fsMachineRanges.DefaultMachineRanges.Ranges[identifier].Range;
                    }
                    Values.Add(identifier, parameter);
                    AddRow(dataGrid, parameter, color);
                }
                else
                {
                    AddRow(dataGrid, Values[identifier], color);
                }
            }
            
        }

        protected void DataGridCellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView) sender).CurrentCell;
            if (cell == null || !CellToParameter.ContainsKey(cell))
                return;

            fsParameterIdentifier parameter = CellToParameter[cell];
            UpdateInputInGroup(parameter);
            ReadEnteredValue(cell, parameter);
            RecalculateAndRedraw();
        }

        protected void AddRow(fsParametersWithValuesTable dataGrid, fsSimulationModuleParameter parameter, Color color)
        {
            int rowIndex = dataGrid.Rows.Add(new[] {parameter.Identifier.Name, parameter.Unit.Name, ""});
            int valueColIndex = dataGrid.Rows[rowIndex].Cells.Count - 1;
            SetRowColor(dataGrid, rowIndex, color);
            AssignParameterAndCell(parameter.Identifier, dataGrid.Rows[rowIndex].Cells[valueColIndex]);
            dataGrid.Rows[rowIndex].Cells[0].ToolTipText = parameter.Identifier.FullName + " (" + parameter.Identifier.Name + ")";
        }

        protected void SetRowColor(fsParametersWithValuesTable dataGrid, int ind, Color color)
        {
            foreach (DataGridViewCell cell in dataGrid.Rows[ind].Cells)
            {
                cell.Style.BackColor = color;
            }
        }

        protected void AssignParameterAndCell(fsParameterIdentifier parameter, DataGridViewCell dataGridViewCell)
        {
            if (ParameterToCell.ContainsKey(parameter))
            {
                ParameterToCell.Remove(parameter);
            }
            ParameterToCell.Add(parameter, dataGridViewCell);

            if (CellToParameter.ContainsKey(dataGridViewCell))
            {
                CellToParameter.Remove(dataGridViewCell);
            }
            CellToParameter.Add(dataGridViewCell, parameter);
        }

        protected void Recalculate()
        {
            fsCalculationProcessor.ProcessCalculatorParameters(Values, ParameterToGroup, Calculators);
        }

        protected void WriteValuesToDataGrid()
        {
            foreach (fsParameterIdentifier p in Values.Keys)
            {
                ParameterToCell[p].Value = Values[p].GetValueInUnits();
            }
        }

        protected void UpdateCellForeColors()
        {
            foreach (var pair in ParameterToCell)
            {
                fsParametersGroup group = ParameterToGroup[pair.Key];
                if (group.GetIsInputFlag())
                {
                    pair.Value.Style.ForeColor = group.Representator == pair.Key
                                                     ? Color.Blue
                                                     : Color.Black;
                }
                else
                {
                    pair.Value.Style.ForeColor = Color.Black;
                }
            }
        }

        protected void UpdateInputInGroup(fsParameterIdentifier parameter)
        {
            fsParametersGroup g = ParameterToGroup[parameter];
            g.Representator = parameter;
            foreach (fsParameterIdentifier p in g.Parameters)
            {
                ParameterToCell[p].Style.ForeColor = p == parameter ? Color.Blue : Color.Black;
            }
        }

        protected void ReadEnteredValue(DataGridViewCell cell, fsParameterIdentifier parameter)
        {
            fsSimulationModuleParameter value = Values[parameter];
            value.SetValueInUnits(fsValue.ObjectToValue(cell.Value));
        }

        protected internal virtual void StopGridsEdit()
        {
            //throw new Exception("You must implement StopGridsEdit in derivative class."); // пока что
        }

        #endregion

        #region Show/Hide parameters methods

        virtual public void ShowAndHideParameters(Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide)
        {
            foreach (fsParameterIdentifier identifier in parametersToShowAndHide.Keys)
            {
                ParameterToCell[identifier].OwningRow.Visible = parametersToShowAndHide[identifier];
            }
        }

        virtual public Dictionary<fsParameterIdentifier, bool> GetInvolvedParametersWithVisibleStatus()
        {
            var involvedParameters = new Dictionary<fsParameterIdentifier, bool>();
            foreach (var pair in ParameterToCell)
            {
                involvedParameters.Add(pair.Key, pair.Value.OwningRow.Visible);
            }
            return involvedParameters;
        }

        #endregion

        #region Methods to change/update view of the module

        public virtual void SetUnits(Dictionary<fsCharacteristic, fsUnit> dictionary)
        {
            StopGridsEdit();

            foreach (fsParameterIdentifier identifier in Values.Keys)
            {
                fsSimulationModuleParameter parameter = Values[identifier];
                if (dictionary.ContainsKey(identifier.MeasurementCharacteristic))
                {
                    parameter.Unit = dictionary[identifier.MeasurementCharacteristic];
                    DataGridViewCell valueCell = ParameterToCell[identifier];
                    DataGridViewCell unitsCell = valueCell.DataGridView[valueCell.ColumnIndex - 1, valueCell.RowIndex];
                    unitsCell.Value = parameter.Unit.Name;
                    valueCell.Value = parameter.GetValueInUnits();
                }
            }
        }

        public virtual void RecalculateAndRedraw()
        {
            Recalculate();
            UpdateUIFromData();
        }

        #endregion

        #region Methods to change internal data

        public void SetRanges(Dictionary<fsParameterIdentifier, fsRange> dictionary)
        {
            foreach (fsParameterIdentifier identifier in Values.Keys)
            {
                fsSimulationModuleParameter parameter = Values[identifier];
                if (dictionary.ContainsKey(identifier))
                {
                    parameter.Range = dictionary[identifier];
                }
            }
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // fsCalculatorControl
            // 
            this.Name = "fsCalculatorControl";
            this.Size = new System.Drawing.Size(278, 400);
            this.ResumeLayout(false);

        }
    }
}