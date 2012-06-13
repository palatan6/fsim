using System;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsLaboratoryVacuumFilterControl : fsCommonCakeFormationControl
    {
        public fsLaboratoryVacuumFilterControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(1.5e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.2));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(100e-4));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.6e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(120));
            SetDefaultValue(fsParameterIdentifier.FiltrationTime, new fsValue(60));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.FiltrationTime,
                    new DiagramConfiguration.DiagramRange(5, 300),
                    new[] { fsParameterIdentifier.Qms },
                    new[] { fsParameterIdentifier.CakeHeight }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.050),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.FiltrationTime }));
        }
    }
}
