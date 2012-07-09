using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;
using StepCalculators.Simulation_Calculators.Cake_Formation;
using Value;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCommonCakeFormationControl : fsCakeFormationBaseControl
    {
        public fsCommonCakeFormationControl()
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
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(2e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.2));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(0.002));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(20));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.7e5));
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(0.25));
            SetDefaultValue(fsParameterIdentifier.RotationalSpeed, new fsValue(0.5 / 60));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.RotationalSpeed,
                    new DiagramConfiguration.DiagramRange(0.2 / 60.0, 2.0 / 60.0),
                    new[] { fsParameterIdentifier.CakeHeight },
                    new[] { fsParameterIdentifier.Qms }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.RotationalSpeed,
                    new DiagramConfiguration.DiagramRange(0.2 / 60.0, 2.0 / 60.0),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.SpecificFiltrationTime }));
        }

        override protected fsParametersGroup[] MakeMachiningStandardGroups()
        {
            fsParametersGroup abGroup = AddGroup(
               fsParameterIdentifier.FilterArea);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup specificTimeGroup = AddGroup(
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.SpecificResidualTime,
                fsParameterIdentifier.ResidualTime);

            fsParametersGroup timeQGroup = AddGroup(
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

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup cycleGroup = AddGroup(
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
                           dpGroup,
                           cycleGroup,
                           filtrationGroup,
                           resultsGroup
                       };
        }
    }
}
