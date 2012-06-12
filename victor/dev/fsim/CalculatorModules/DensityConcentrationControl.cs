using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;

namespace CalculatorModules
{
    public sealed partial class fsDensityConcentrationControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        private enum fsCalculationOption
        {
            [Description("Filtrate Density")] CalcFiltrateDensity,
            [Description("Solids Density")] CalcSolidsDensity,
            [Description("Suspension Density")] CalcSuspensionDensity,
            [Description("Solids Content")] CalcConcentrations
        }

        #endregion

        
        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDensityConcentrationCalculator());
        }

        protected override void InitializeGroups()
        {
            fsParametersGroup filtrateGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);
            fsParametersGroup suspensionGroup = AddGroup(
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);

            AddGroupToUI(dataGrid, filtrateGroup, Color.FromArgb(230, 255, 255));
            AddGroupToUI(dataGrid, solidsGroup, Color.FromArgb(255, 230, 255));
            AddGroupToUI(dataGrid, suspensionGroup, Color.FromArgb(255, 255, 230));
            AddGroupToUI(dataGrid, concentrationGroup, Color.FromArgb(230, 230, 230));
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(calculateSelectionComboBox.Items, typeof(fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.CalcSuspensionDensity);
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculateSelectionComboBox);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid, calculateSelectionComboBox };
        }

        public fsDensityConcentrationControl()
        {
            InitializeComponent();
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption) CalculationOptions[typeof (fsCalculationOption)];
            fsParametersGroup calculateGroup = null;
            switch (calculationOption)
            {
                case fsCalculationOption.CalcFiltrateDensity:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.MotherLiquidDensity];
                    break;
                case fsCalculationOption.CalcSolidsDensity:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SolidsDensity];
                    break;
                case fsCalculationOption.CalcSuspensionDensity:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SuspensionDensity];
                    break;
                case fsCalculationOption.CalcConcentrations:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SuspensionSolidsMassFraction];
                    break;
            }
            foreach (fsParametersGroup group in ParameterToGroup.Values)
            {
                SetGroupInput(group, group != calculateGroup);
            }
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // this control uses only one calculator
        }

        #endregion
    }
}