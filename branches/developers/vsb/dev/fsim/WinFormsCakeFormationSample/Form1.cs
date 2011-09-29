﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Parameters;
using Value;
using StepCalculators;
using System.Threading;
using UpdateHandler;

namespace WinFormsCakeFormationSample
{
    public partial class Form1 : Form
    {
        private Thread m_currentCalculation;

        class DataContainer
        {
            public Dictionary<fsParameterIdentifier, DataGridViewCell> m_parameterCell;
            public Dictionary<DataGridViewCell, fsParameterIdentifier> m_cellParameter;
            public Dictionary<fsParameterIdentifier, fsSimulationParameter> m_parameterValue;
            public List<fsCalculator> m_calculatorList;

            public DataContainer()
            {
                m_parameterCell = new Dictionary<fsParameterIdentifier, DataGridViewCell>();
                m_cellParameter = new Dictionary<DataGridViewCell, fsParameterIdentifier>();
                m_parameterValue = new Dictionary<fsParameterIdentifier, fsSimulationParameter>();
                m_calculatorList = new List<fsCalculator>();
            }
        }

        DataContainer materialData = new DataContainer();
        DataContainer cakeFormationData = new DataContainer();
        DataContainer deliquoringData = new DataContainer();

        DataContainer[] dataContainers;


        public Form1()
        {
            InitializeComponent();
        }

        void AddParameters(DataGridView dataGrid,
            DataContainer dataContainer,
            params fsParameterIdentifier [] parameters)
        {
            foreach (var p in parameters)
            {
                dataGrid.Rows.Add(new object[] { p.Name + " (" + p.Units.CurrentName + ")" });
                var cell = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[1];
                dataContainer.m_parameterCell[p] = cell;
                dataContainer.m_cellParameter[cell] = p;
                dataContainer.m_parameterValue[p] = new fsSimulationParameter(p);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            materialData.m_calculatorList.Add(new fsDensityConcentrationCalculator());

            cakeFormationData.m_calculatorList.Add(new fsEps0Kappa0Calculator());
            cakeFormationData.m_calculatorList.Add(new fsPc0rc0alpha0Calculator());
            cakeFormationData.m_calculatorList.Add(new fsRm0hce0Calculator());
            cakeFormationData.m_calculatorList.Add(new fsCakeFormationDpConstCalculator());

            dataContainers = new[] {
                materialData,
                cakeFormationData,
                deliquoringData
            };

            InitMaterialParameters();
            InitCakeFormationParameters();
            //InitDeliquoringParameters();
        }

        private void InitDeliquoringParameters()
        {
            AddParameters(
                DeliquoringDataGrid,
                deliquoringData,
                fsParameterIdentifier.Porosity0,
                fsParameterIdentifier.Ne,
                fsParameterIdentifier.Porosity);
        }

        private void InitCakeFormationParameters()
        {
            AddParameters(
                CakeFormationDataGrid,
                cakeFormationData,
                fsParameterIdentifier.Porosity0,
                fsParameterIdentifier.Kappa0,
                fsParameterIdentifier.Ne,
                fsParameterIdentifier.Pc0,
                fsParameterIdentifier.Rc0,
                fsParameterIdentifier.Alpha0,
                fsParameterIdentifier.Nc,
                fsParameterIdentifier.Hce0,
                fsParameterIdentifier.Rm0,
                fsParameterIdentifier.FilterArea,
                fsParameterIdentifier.Pressure,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.FormationRelativeTime,
                fsParameterIdentifier.FormationTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SuspensionMassFlowrate);

            cakeFormationData.m_parameterValue[fsParameterIdentifier.FilterArea].IsInput = true;
            cakeFormationData.m_parameterValue[fsParameterIdentifier.Pressure].IsInput = true;
        }

        private void InitMaterialParameters()
        {
            AddParameters(
                MaterialParametersDataGrid,
                materialData,
                fsParameterIdentifier.FiltrateViscosity,
                fsParameterIdentifier.FiltrateDensity,
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity,
                fsParameterIdentifier.SolidsMassFraction,
                fsParameterIdentifier.SolidsVolumeFraction,
                fsParameterIdentifier.SolidsConcentration);

            materialData.m_parameterValue[fsParameterIdentifier.FiltrateDensity].IsInput = true;
            materialData.m_parameterValue[fsParameterIdentifier.SolidsDensity].IsInput = true;
            materialData.m_parameterValue[fsParameterIdentifier.SuspensionDensity].IsInput = true;
            materialData.m_parameterCell[fsParameterIdentifier.SolidsMassFraction].ReadOnly = true;
            materialData.m_parameterCell[fsParameterIdentifier.SolidsVolumeFraction].ReadOnly = true;
            materialData.m_parameterCell[fsParameterIdentifier.SolidsConcentration].ReadOnly = true;
        }

        private void MaterialParametersDataGrid_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            ProcessParameterChange(((DataGridView) sender)[e.ColumnIndex, e.RowIndex]);
        }

        private void ProcessParameterChange(DataGridViewCell cell)
        {
            foreach (var dataContainer in dataContainers)
            {
                if (dataContainer.m_cellParameter.ContainsKey(cell))
                {
                    var id = dataContainer.m_cellParameter[cell];
                    var param = dataContainer.m_parameterValue[id];

                    fsValue oldValue = param.Value;
                    fsValue newValue = fsValue.ObjectToValue(cell.Value) * param.Identifier.Units.CurrentCoefficient;
                    Text = param.Identifier.Name + @" changed from " + oldValue.ToString() + @" to " + newValue.ToString();
                    param.Value = newValue;

                    UpdateInputs(cell);

                    Recalculate();
                }
            }
        }

        private void UpdateInputs(DataGridViewCell cell, params fsParameterIdentifier [] parameters)
        {
            foreach (var dataContainer in dataContainers)
            {
                var parameter = dataContainer.m_cellParameter.ContainsKey(cell)
                    ? dataContainer.m_cellParameter[cell]
                    : null;
                if (parameters.Contains(parameter))
                    foreach (var p in parameters)
                    {
                        var pcell = dataContainer.m_parameterCell[p];
                        bool isInput = cell == pcell;
                        dataContainer.m_parameterValue[p].IsInput = isInput;
                        pcell.Style.ForeColor = isInput ? Color.Blue : Color.Black;
                    }
            }
        }

        private void UpdateInputs(DataGridViewCell cell)
        {
            UpdateInputs(cell, 
                fsParameterIdentifier.Porosity0,
                fsParameterIdentifier.Kappa0);

            UpdateInputs(cell,
                fsParameterIdentifier.Pc0,
                fsParameterIdentifier.Rc0,
                fsParameterIdentifier.Alpha0);

            UpdateInputs(cell,
                fsParameterIdentifier.Rm0,
                fsParameterIdentifier.Hce0);

            UpdateInputs(cell,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.RotationalSpeed);

            UpdateInputs(cell,
                fsParameterIdentifier.FormationTime,
                fsParameterIdentifier.FormationRelativeTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume);
        }

        private void Recalculate()
        {
            if (useMultiThreadingFlag.Checked)
            {
                if (m_currentCalculation != null)
                {
                    m_currentCalculation.Abort();
                    m_currentCalculation = null;
                }
                m_currentCalculation = new Thread(CalculateInThread);
                m_currentCalculation.Start();
            }
            else
            {
                CalculateInThread();
            }
        }

        private void CalculateInThread()
        {
            var uh = new fsCalculatorUpdateHandler(fsLabeledProgressBar1);
            errorMessageTextBox.Text = "";

            var calcsList = new List<KeyValuePair<fsCalculator, Dictionary<fsParameterIdentifier, fsSimulationParameter>>>();
            foreach (var dataContainer in dataContainers)
            {
                foreach (var calculator in dataContainer.m_calculatorList)
                {
                    calcsList.Add(new KeyValuePair<fsCalculator,Dictionary<fsParameterIdentifier,fsSimulationParameter>>(
                        calculator,
                        dataContainer.m_parameterValue));
                }
            }

            var progressSplitters = new double[calcsList.Count + 1];
            int totalAmount = calcsList.Sum(t => t.Key.GetToCalculateAmount());
            int currentAmount = 0;
            for (int i = 0; i < calcsList.Count; ++i)
            {
                progressSplitters[i] = (double)currentAmount / totalAmount;
                currentAmount += calcsList[i].Key.GetToCalculateAmount();
            }
            progressSplitters[calcsList.Count] = 1;

            for (int i = 0; i < calcsList.Count; ++i)
            {
                var calc = calcsList[i].Key;
                calc.SetUpdateHandler(uh.CreateSubHandler(
                    progressSplitters[i],
                    progressSplitters[i + 1]));
                ApplyCalculator(calc, calcsList[i].Value);
            }

            foreach (var dataContainer in dataContainers)
            {
                foreach (var p in dataContainer.m_parameterValue.Keys)
                {
                    var value = dataContainer.m_parameterValue[p];
                    if (value.IsInput == false)
                    {
                        dataContainer.m_parameterCell[p].Value = value.ValueToStringWithCurrentUnits();
                    }
                }
            }
        }
        private void ApplyCalculator(fsCalculator calculator, Dictionary<fsParameterIdentifier, fsSimulationParameter> parameterValue)
        {
            foreach (var dataContainer in dataContainers)
            {
                calculator.ReadDataFromStorage(parameterValue);
                calculator.Calculate();
                errorMessageTextBox.Text += calculator.GetStatusMessage();
                calculator.CopyValuesToStorage(parameterValue);
            }
        }
        private void CakeFormationDataGrid_CellValueChangedByUser(object sender, DataGridViewCellEventArgs e)
        {
            ProcessParameterChange(((DataGridView) sender)[e.ColumnIndex, e.RowIndex]);
        }
    }
}