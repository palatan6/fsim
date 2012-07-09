using System;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsPressureLeafFilters : fsCommonCakeFormationControl
    {
        public fsPressureLeafFilters()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(0.01));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.65));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(0.3e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(0.001));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(10));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(10 * 60));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.010));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] {fsCakeFormationCalculationOption.StandardCalculation},
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.020),
                    new[] {fsParameterIdentifier.Qms},
                    new[] {fsParameterIdentifier.CycleTime}));

            m_defaultDiagrams.Add(
                new Enum[] {fsCakeFormationCalculationOption.FilterDesign},
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.020),
                    new[] {fsParameterIdentifier.FilterArea},
                    new[] {fsParameterIdentifier.CycleTime}));
        }
    }
}
