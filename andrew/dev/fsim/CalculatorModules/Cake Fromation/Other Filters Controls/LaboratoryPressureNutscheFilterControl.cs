using System;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsLaboratoryPressureNutscheFilterControl : fsCommonCakeFormationControl
    {
        public fsLaboratoryPressureNutscheFilterControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.6));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(0.7e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(20e-4));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(300));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.020));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.050),
                    new[] { fsParameterIdentifier.Qms },
                    new[] { fsParameterIdentifier.CycleTime }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.050),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.CycleTime }));
        }
    }
}
