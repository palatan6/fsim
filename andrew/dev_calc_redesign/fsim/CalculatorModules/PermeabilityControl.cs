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

        protected override void InitializeCalculators()
        {
            Calculator.AddEquations(new fsPermeabilityCalculator());
        }

        protected override void InitializeGroups()
        {
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
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(calculationOptionComboBox.Items, typeof(fsCalculationOption));
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculationOptionComboBox);
            EstablishCalculationOption(fsCalculationOption.CalcPcRcAlpha);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid, calculationOptionComboBox };
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(2300));
            SetDefaultValue(fsParameterIdentifier.CakePorosity, new fsValue(0.55));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(2e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.3));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] {fsCalculationOption.CalcPcRcAlpha},
                new DiagramConfiguration(
                    fsParameterIdentifier.PressureDifference,
                    new DiagramConfiguration.DiagramRange(0.5e5, 4e5),
                    new[] {fsParameterIdentifier.CakePermeability},
                    new[] {fsParameterIdentifier.CakeResistance, fsParameterIdentifier.CakeResistanceAlpha}));

            m_defaultDiagrams.Add(
                new Enum[] { fsCalculationOption.CalcPressure },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakePermeability,
                    new DiagramConfiguration.DiagramRange(1e-13, 3e-13),
                    new[] { fsParameterIdentifier.PressureDifference },
                    new[] { fsParameterIdentifier.CakeResistance, fsParameterIdentifier.CakeResistanceAlpha }));
            
            m_defaultDiagrams.Add(
                new Enum[] { fsCalculationOption.CalcNc },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakePermeability,
                    new DiagramConfiguration.DiagramRange(0.2e-13, 1e-13),
                    new[] { fsParameterIdentifier.CakeCompressibility }));

            m_defaultDiagrams.Add(
                    new Enum[] { fsCalculationOption.CalcPc0Rc0Alpha0 },
                    new DiagramConfiguration(
                        fsParameterIdentifier.CakeCompressibility,
                        new DiagramConfiguration.DiagramRange(0, 1),
                        new[] { fsParameterIdentifier.CakePermeability0 },
                        new[] { fsParameterIdentifier.CakeResistance0 }));
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