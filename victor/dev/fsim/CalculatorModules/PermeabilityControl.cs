using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;

namespace CalculatorModules
{
    public sealed partial class fsPermeabilityControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        private enum fsCalculationOption
        {
            [Description("Pc0, rc0, alpha0")] CalcPc0Rc0Alpha0,
            [Description("Cake Compressibility nc")] CalcNc,
            [Description("Pressure (Dp)")] CalcPressure,
            [Description("Pc, rc, alpha")] CalcPcRcAlpha
        }

        #endregion

        protected override void InitializeCalculatorControl()
        {
            Calculators.Add(new fsPermeabilityCalculator());

            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.CakePorosity);
            fsParametersGroup pc0Rc0A0Group = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0);
            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);
            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);
            fsParametersGroup pcRcAGroup = AddGroup(
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha);

            AddGroupToUI(dataGrid, solidsGroup, Color.FromArgb(230, 230, 255));
            AddGroupToUI(dataGrid, porosityGroup, Color.FromArgb(255, 255, 230));
            AddGroupToUI(dataGrid, pc0Rc0A0Group, Color.FromArgb(230, 230, 255));
            AddGroupToUI(dataGrid, ncGroup, Color.FromArgb(255, 255, 230));
            AddGroupToUI(dataGrid, pressureGroup, Color.FromArgb(230, 230, 255));
            AddGroupToUI(dataGrid, pcRcAGroup, Color.FromArgb(255, 230, 230));

            fsMisc.FillList(calculationOptionComboBox.Items, typeof(fsCalculationOption));
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculationOptionComboBox);
            EstablishCalculationOption(fsCalculationOption.CalcPcRcAlpha);

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid, calculationOptionComboBox);
        }

        public fsPermeabilityControl()
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
                case fsCalculationOption.CalcNc:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakeCompressibility];
                    break;
                case fsCalculationOption.CalcPc0Rc0Alpha0:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakePermeability0];
                    break;
                case fsCalculationOption.CalcPcRcAlpha:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakePermeability];
                    break;
                case fsCalculationOption.CalcPressure:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.PressureDifference];
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