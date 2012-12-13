using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsPanFilterControl : fsCommonCakeFormationControl
    {
        public fsPanFilterControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.45));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(10e-13));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.1));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(0.005));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.6e5));
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(0.33));
            SetDefaultValue(fsParameterIdentifier.RotationalSpeed, new fsValue(0.5 / 60.0));
        }
    }
}
