using System;
using Parameters;
using StepCalculators.Simulation_Calculators.Cake_Formation;
using Value;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsContinuousNonModularBeltFilterControl : fsCakeFormationBaseControl
    {
        public fsContinuousNonModularBeltFilterControl()
        {
            InitializeComponent();
        }

        protected override void AddCakeFormationCalculator()
        {
            Calculators.Add(new fsContinuousNonModularBeltFilterCalculator());
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.02));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(5e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.2));
            SetDefaultValue(fsParameterIdentifier.MachineWidth, new fsValue(2));
            SetDefaultValue(fsParameterIdentifier.l_over_b, new fsValue(4.5));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.7e5));
            SetDefaultValue(fsParameterIdentifier.u, new fsValue(5.0 / 60));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] {fsCakeFormationCalculationOption.StandardCalculation},
                new DiagramConfiguration(
                    fsParameterIdentifier.u,
                    new DiagramConfiguration.DiagramRange(2.0 / 60.0, 10.0 / 60.0),
                    new[] {fsParameterIdentifier.CakeHeight},
                    new[] {fsParameterIdentifier.Qms}));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.u,
                    new DiagramConfiguration.DiagramRange(2.0 / 60.0, 10.0 / 60.0),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.SpecificFiltrationTime }));
        }

        override protected fsParametersGroup[] MakeMachiningStandardGroups()
        {
            fsParametersGroup abGroup = AddGroup(
               fsParameterIdentifier.FilterArea,
               fsParameterIdentifier.MachineWidth);

            fsParametersGroup geometryGroup = AddGroup(
                fsParameterIdentifier.FilterLength,
                fsParameterIdentifier.l_over_b);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup specificTimeGroup = AddGroup(
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.SpecificResidualTime,
                fsParameterIdentifier.ResidualTime);

            fsParametersGroup timeQGroup = AddGroup(
                fsParameterIdentifier.u,
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.qft,
                fsParameterIdentifier.qmft,
                fsParameterIdentifier.Qms,
                fsParameterIdentifier.Qsus,
                fsParameterIdentifier.SuspensionMassFlowrate);

            fsParametersGroup resultsGroup = AddOnlyCalculatedGroup(
                fsParameterIdentifier.MeanHeightRate,
                fsParameterIdentifier.HcOverTc,
                fsParameterIdentifier.DiffHeightRate,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SpecificSuspensionMass,
                fsParameterIdentifier.SpecificSuspensionVolume,
                fsParameterIdentifier.Qmsust,
                fsParameterIdentifier.Qmsusd,
                fsParameterIdentifier.Qsust,
                fsParameterIdentifier.Qsusd,
                fsParameterIdentifier.qmsust,
                fsParameterIdentifier.qmsusd,
                fsParameterIdentifier.qsust,
                fsParameterIdentifier.qsusd);

            return new[]
                       {
                           abGroup,
                           geometryGroup,
                           dpGroup,
                           specificTimeGroup,
                           timeQGroup,
                           resultsGroup
                       };
        }

        override protected fsParametersGroup[] MakeMachiningDesignGroups()
        {
            fsParametersGroup qsusGroup = AddGroup(
                fsParameterIdentifier.Qms,
                fsParameterIdentifier.Qsus,
                fsParameterIdentifier.SuspensionMassFlowrate);

            fsParametersGroup geometryGroup = AddGroup(
                fsParameterIdentifier.l_over_b);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup cycleGroup = AddGroup(
                fsParameterIdentifier.u,
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.SpecificResidualTime,
                fsParameterIdentifier.ResidualTime);

            fsParametersGroup filtrationGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.qft,
                fsParameterIdentifier.qmft);

            fsParametersGroup resultsGroup = AddOnlyCalculatedGroup(
                fsParameterIdentifier.FilterArea,
                fsParameterIdentifier.MachineWidth,
                fsParameterIdentifier.FilterLength,
                fsParameterIdentifier.MeanHeightRate,
                fsParameterIdentifier.HcOverTc,
                fsParameterIdentifier.DiffHeightRate,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SpecificSuspensionMass,
                fsParameterIdentifier.SpecificSuspensionVolume,
                fsParameterIdentifier.Qmsust,
                fsParameterIdentifier.Qmsusd,
                fsParameterIdentifier.Qsust,
                fsParameterIdentifier.Qsusd,
                fsParameterIdentifier.qmsust,
                fsParameterIdentifier.qmsusd,
                fsParameterIdentifier.qsust,
                fsParameterIdentifier.qsusd);

            return new[]
                       {
                           qsusGroup,
                           geometryGroup,
                           dpGroup,
                           cycleGroup,
                           filtrationGroup,
                           resultsGroup
                       };
        }
    }
}
