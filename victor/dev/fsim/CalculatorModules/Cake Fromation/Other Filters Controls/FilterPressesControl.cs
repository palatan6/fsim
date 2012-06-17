using System;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsFilterPressesControl : fsCommonCakeFormationControl
    {
        public fsFilterPressesControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.65));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(0.1e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.7));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(50));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(10 * 60));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.015));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.020),
                    new[] { fsParameterIdentifier.Qms },
                    new[] { fsParameterIdentifier.CycleTime }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.020),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.CycleTime }));
        }
    }
}
