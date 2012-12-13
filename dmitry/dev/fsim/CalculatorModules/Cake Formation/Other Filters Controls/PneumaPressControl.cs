using System;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsPneumaPressControl : fsCommonCakeFormationControl
    {
        public fsPneumaPressControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.55));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(0.5e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(4e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(10 * 60));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.050));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.010, 0.100),
                    new[] { fsParameterIdentifier.Qms },
                    new[] { fsParameterIdentifier.CycleTime }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.010, 0.100),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.CycleTime }));
        }
    }
}
