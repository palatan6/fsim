using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsRotaryPressureFilters : fsCommonCakeFormationControl
    {
        public fsRotaryPressureFilters()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.6));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(0.3e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(0.001));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(2.0));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(4e5));
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(0.30));
            SetDefaultValue(fsParameterIdentifier.RotationalSpeed, new fsValue(0.5 / 60.0));
        }
    }
}
