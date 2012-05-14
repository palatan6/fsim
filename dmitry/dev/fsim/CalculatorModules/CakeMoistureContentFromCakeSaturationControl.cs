using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;

namespace CalculatorModules
{
    public sealed partial class fsCakeMoistureContentFromCakeSaturationControl :
        fsOptionsSingleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        private enum fsCalculationOption
        {
            [Description("Cake Porosity")] CakePorosity,
            [Description("Cake Saturation")] CakeSaturation,
            [Description("Cake Moisture Content")] CakeMoistureContent
        }

        #endregion

        public fsCakeMoistureContentFromCakeSaturationControl()
        {
            InitializeComponent();

            Calculators.Add(new fsRfFromCakeSaturationCalculator());

            fsParametersGroup liquidGroup = AddGroup(
                fsParameterIdentifier.LiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.CakePorosity);
            fsParametersGroup saturationGroup = AddGroup(
                fsParameterIdentifier.CakeSaturation);
            fsParametersGroup rfGroup = AddGroup(
                fsParameterIdentifier.CakeMoistureContent);

            var groups = new[]
                             {
                                 liquidGroup,
                                 solidsGroup,
                                 porosityGroup,
                                 saturationGroup,
                                 rfGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i].IsInput = true;
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            rfGroup.IsInput = false;
            ParameterToCell[fsParameterIdentifier.CakeMoistureContent].ReadOnly = true;

            fsMisc.FillList(calculationOptionComboBox.Items, typeof (fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.CakeMoistureContent);
            AssignCalculationOptionAndControl(typeof (fsCalculationOption), calculationOptionComboBox);

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid, calculationOptionComboBox);
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // this control has only one equation
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption) CalculationOptions[typeof (fsCalculationOption)];
            fsParametersGroup calculateGroup = null;
            switch (calculationOption)
            {
                case fsCalculationOption.CakeMoistureContent:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakeMoistureContent];
                    break;
                case fsCalculationOption.CakePorosity:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakePorosity];
                    break;
                case fsCalculationOption.CakeSaturation:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakeSaturation];
                    break;
            }
            foreach (fsParametersGroup group in ParameterToGroup.Values)
            {
                SetGroupInput(group, group != calculateGroup);
            }
        }
    }
}