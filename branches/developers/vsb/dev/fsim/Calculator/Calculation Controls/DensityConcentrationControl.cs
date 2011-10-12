﻿using System.ComponentModel;
using System.Drawing;
using Parameters;
using StepCalculators;

namespace Calculator.Calculation_Controls
{
    public sealed partial class fsDensityConcentrationControl : fsCalculatorControl
    {
        #region Calculation Data

        enum fsCalculationOption
        {
            [Description("Filtrate Density")]
            CalcFiltrateDensity,
            [Description("Solids Density")]
            CalcSolidsDensity,
            [Description("Suspension Density")]
            CalcSuspensionDensity,
            [Description("Concentrations")]
            CalcConcentrations
        }
       
        #endregion

        public fsDensityConcentrationControl()
        {
            InitializeComponent();

            Calculators.Add(new fsDensityConcentrationCalculator());

            var filtrateGroup = AddGroup(
                fsParameterIdentifier.FiltrateDensity);
            var solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);
            var suspensionGroup = AddGroup(
                fsParameterIdentifier.SuspensionDensity);
            var concentrationGroup = AddGroup(
                fsParameterIdentifier.SolidsMassFraction,
                fsParameterIdentifier.SolidsVolumeFraction,
                fsParameterIdentifier.SolidsConcentration);

            AddGroupToUI(dataGrid, filtrateGroup, Color.FromArgb(230, 255, 255));
            AddGroupToUI(dataGrid, solidsGroup, Color.FromArgb(255, 230, 255));
            AddGroupToUI(dataGrid, suspensionGroup, Color.FromArgb(255, 255, 230));
            AddGroupToUI(dataGrid, concentrationGroup, Color.FromArgb(230, 230, 230));

            AssignCalculationOption(fsCalculationOption.CalcFiltrateDensity, filtrateRadioButton, filtrateGroup);
            AssignCalculationOption(fsCalculationOption.CalcSolidsDensity, solidsRadioButton, solidsGroup);
            AssignCalculationOption(fsCalculationOption.CalcSuspensionDensity, suspensionRadioButton, suspensionGroup);
            AssignCalculationOption(fsCalculationOption.CalcConcentrations, concentrationsRadioButton, concentrationGroup);

            CalculationOption = fsCalculationOption.CalcSuspensionDensity;
            fsMisc.FillComboBox(calculateSelectionComboBox.Items, typeof(fsCalculationOption));
            UpdateUIFromData();

            UpdateCalculationOptionAndInputGroupsFromUI();

            ConnectUIWithDataUpdating(dataGrid);
            UpdateUIFromData();
        }

        #region Routine Methods

        override protected void UpdateCalculationOptionAndInputGroupsFromUI()
        {
            base.UpdateCalculationOptionAndInputGroupsFromUI();
            CalculationOption =
                (fsCalculationOption)fsMisc.GetEnum(typeof(fsCalculationOption), calculateSelectionComboBox.Text);
        }

        protected override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
            calculateSelectionComboBox.Text = fsMisc.GetEnumDescription((fsCalculationOption)CalculationOption);
        }

        protected override void ConnectUIWithDataUpdating(fmDataGrid.fmDataGrid grid)
        {
            base.ConnectUIWithDataUpdating(grid);
            calculateSelectionComboBox.TextChanged += RadioButtonCheckedChanged;
        }

        #endregion
    }
}
