using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Parameters;

namespace CalculatorModules.Base_Controls
{
    public partial class OptionsQuadraTableAndCommentsCalculatorControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        protected List<fsParameterIdentifier> ResultParamatersList = new List<fsParameterIdentifier>(); // parameters for Results table
        protected List<fsParameterIdentifier> InputedParamatersList = new List<fsParameterIdentifier>(); // parameters for Inputs table

        protected Dictionary<fsParameterIdentifier, DataGridViewCell> InputsParameterToCell { get; private set; }
        protected Dictionary<DataGridViewCell, fsParameterIdentifier> InputsCellToParameter { get; private set; }

        public OptionsQuadraTableAndCommentsCalculatorControl()
        {
            InitializeComponent();
            InputsParameterToCell = new Dictionary<fsParameterIdentifier, DataGridViewCell>();
            InputsCellToParameter = new Dictionary<DataGridViewCell, fsParameterIdentifier>();
        }

        protected override void InitializeCalculatorContorl()
        {
            if (IsCalculatorControlInitialized)
                return;

            IsCalculatorControlInitialized = true;

            InitializeCalculators();
            InitializeGroups();
            InitializeResultParamatersList();
            InitializeInputsListAndTable();
            InitializeCalculationOptionsUIControls();
            UpdateGroupsInputInfoFromCalculationOptions();
            InitializeUnits();
            InitializeParametersValues();
            UpdateEquationsFromCalculationOptions();
            RecalculateAndRedraw();
            ConnectUIWithDataUpdating(GetUIControlsToConnectWithDataUpdating());
        }

        protected internal override void StopGridsEdit()
        {
            base.StopGridsEdit();
            ResultsTable.EndEdit();
            InputsTable.EndEdit();
        }

        protected virtual void InitializeResultParamatersList()
        {
            // This method should contain initialization of parameters of the Result table in derived class.
        }

        protected void AddResultParameter(fsParameterIdentifier parameter)
        {
            ResultParamatersList.Add(parameter);
        }

        public override void RecalculateAndRedraw()
        {
            base.RecalculateAndRedraw();
            BuildResultsTable();
            InitializeInputsListAndTable();
        }

        protected void BuildResultsTable()
        {
            ResultsTable.Rows.Clear();
            if (ResultParamatersList.Count < 1)
                return;
            for (int i = 0; i < ResultParamatersList.Count; ++i)
            {
                if (!isParameterInputed(ResultParamatersList[i]))
                {
                    int rowIndex =
                        ResultsTable.Rows.Add(new object[]
                        {
                            ResultParamatersList[i].Name,
                            ResultParamatersList[i].MeasurementCharacteristic.CurrentUnit.Name,
                            Values[ResultParamatersList[i]].GetValueInUnits().ToString()
                        });

                    ResultsTable.Rows[rowIndex].ReadOnly = true;
                }
            }
        }

        private bool isParameterInputed(fsParameterIdentifier param)
        {
            bool resultFlag = false;
            for (int j = 0; j < Groups.Count; ++j)
            {
                if (Groups[j].Representator == param)
                    resultFlag = true;
            }
            return resultFlag;
        }

        private void InitializeInputsListAndTable()
        {
            InputsTable.Rows.Clear();
            InputedParamatersList.Clear();

            for (int i = 0; i < Groups.Count; ++i)
            {
                if (Groups[i].Kind == fsParametersGroup.fsParametersGroupKind.MachiningSettingsParameters)
                {
                    InputedParamatersList.Add(Groups[i].Representator);
                }
            }

            for (int i = 0; i < InputedParamatersList.Count; ++i)
            {
                int rowIndex =
                    InputsTable.Rows.Add(new object[]
                    {
                        InputedParamatersList[i].Name,
                        InputedParamatersList[i].MeasurementCharacteristic.CurrentUnit.Name,
                        Values[InputedParamatersList[i]].GetValueInUnits().ToString()
                    });
                int valueColIndex = dataGrid.Rows[rowIndex].Cells.Count - 1;
                InputsTable.Rows[rowIndex].Cells[valueColIndex].Style.ForeColor = Color.Blue;
                AssignInputsParameterAndCell(InputedParamatersList[i], InputsTable.Rows[rowIndex].Cells[valueColIndex]);
            }
        }

        private void AssignInputsParameterAndCell(fsParameterIdentifier parameter, DataGridViewCell dataGridViewCell)
        {
            if (InputsParameterToCell.ContainsKey(parameter))
            {
                InputsParameterToCell.Remove(parameter);
            }
            InputsParameterToCell.Add(parameter, dataGridViewCell);

            if (InputsCellToParameter.ContainsKey(dataGridViewCell))
            {
                InputsCellToParameter.Remove(dataGridViewCell);
            }
            InputsCellToParameter.Add(dataGridViewCell, parameter);
        }

        private void InputsTable_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
            if (cell == null || !InputsCellToParameter.ContainsKey(cell))
                return;

            fsParameterIdentifier parameter = InputsCellToParameter[cell];
            ReadEnteredValue(cell, parameter);
            RecalculateAndRedrawButNotInputsTable();
        }

        private void RecalculateAndRedrawButNotInputsTable()
        {
            base.RecalculateAndRedraw();
            BuildResultsTable();
        }
    }
}
