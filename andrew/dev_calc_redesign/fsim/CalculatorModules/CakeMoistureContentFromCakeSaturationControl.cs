using System;
using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;
using Value;

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

        protected override void InitializeCalculators()
        {
            Calculator.AddEquations(new fsRfFromCakeSaturationCalculator());
        }

        protected override void InitializeGroups()
        {
            fsParametersGroup liquidGroup = AddGroup(
               fsParameterIdentifier.LiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.CakePorosity);
            fsParametersGroup saturationGroup = AddGroup(
                fsParameterIdentifier.CakeSaturation);
            fsParametersGroup rfGroup = AddGroup(
                fsParameterIdentifier.CakeMoistureContentRf);

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
                groups[i].SetIsInputFlag(true);
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            rfGroup.SetIsInputFlag(false);
            ParameterToCell[fsParameterIdentifier.CakeMoistureContentRf].ReadOnly = true;
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(calculationOptionComboBox.Items, typeof(fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.CakeMoistureContent);
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculationOptionComboBox);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid, calculationOptionComboBox };
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.LiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(2300));
            SetDefaultValue(fsParameterIdentifier.CakePorosity, new fsValue(0.55));
            SetDefaultValue(fsParameterIdentifier.CakeSaturation, new fsValue(1));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] {fsCalculationOption.CakeMoistureContent},
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeSaturation,
                    new DiagramConfiguration.DiagramRange(0, 1),
                    new[] {fsParameterIdentifier.CakeMoistureContentRf}));

            m_defaultDiagrams.Add(
                new Enum[] { fsCalculationOption.CakeSaturation },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakePorosity,
                    new DiagramConfiguration.DiagramRange(0.50, 0.70),
                    new[] { fsParameterIdentifier.CakeSaturation }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCalculationOption.CakePorosity },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeSaturation,
                    new DiagramConfiguration.DiagramRange(0.10, 1),
                    new[] { fsParameterIdentifier.CakePorosity }));
        }

        public fsCakeMoistureContentFromCakeSaturationControl()
        {
            InitializeComponent();
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
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakeMoistureContentRf];
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