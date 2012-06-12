using CalculatorModules.Cake_Fromation;
using Parameters;
using StepCalculators.Simulation_Calculators;

namespace CalculatorModules.BeltFiltersWithReversibleTrays
{
    public sealed partial class fsBeltFilterWithReversibleTrayControl : fsCakeFormationBaseControl
    {
        public fsBeltFilterWithReversibleTrayControl()
        {
            InitializeComponent();
        }

        protected override void AddCakeFormationCalculator()
        {
            Calculators.Add(new fsBeltFiltersWithReversibleTraysCalculator());
        }

        override protected fsParametersGroup[] MakeMachiningStandardGroups()
        {
            fsParametersGroup abGroup = AddGroup(
               fsParameterIdentifier.FilterArea,
               fsParameterIdentifier.MachineWidth);

            fsParametersGroup nsGroup = AddGroup(
                fsParameterIdentifier.ns);

            fsParametersGroup geometryGroup = AddGroup(
                fsParameterIdentifier.ls,
                fsParameterIdentifier.ls_over_b,
                fsParameterIdentifier.FilterLength,
                fsParameterIdentifier.l_over_b,
                fsParameterIdentifier.As);

            fsParametersGroup timeGroup = AddGroup(
                fsParameterIdentifier.StandardTechnicalTime,
                fsParameterIdentifier.TechnicalTime);

            fsParametersGroup lambdaGroup = AddGroup(
                fsParameterIdentifier.lambda);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup specificTimeGroup = AddGroup(
                fsParameterIdentifier.nsf,
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.nsr,
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
                           nsGroup,
                           geometryGroup,
                           timeGroup,
                           lambdaGroup,
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

            fsParametersGroup nsGroup = AddGroup(
                fsParameterIdentifier.ns);

            fsParametersGroup geometryGroup = AddGroup(
                fsParameterIdentifier.ls_over_b,
                fsParameterIdentifier.l_over_b,
                fsParameterIdentifier.ls);

            fsParametersGroup timeGroup = AddGroup(
                fsParameterIdentifier.StandardTechnicalTime,
                fsParameterIdentifier.TechnicalTime);

            fsParametersGroup lambdaGroup = AddGroup(
                fsParameterIdentifier.lambda);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup cycleGroup = AddGroup(
                fsParameterIdentifier.u,
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.nsf,
                fsParameterIdentifier.nsr,
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
                fsParameterIdentifier.As,
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
                           nsGroup,
                           geometryGroup,
                           timeGroup,
                           lambdaGroup,
                           dpGroup,
                           cycleGroup,
                           filtrationGroup,
                           resultsGroup
                       };
        }
    }
}

