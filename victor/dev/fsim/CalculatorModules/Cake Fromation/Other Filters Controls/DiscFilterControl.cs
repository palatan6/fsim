using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsDiscFilterControl : fsCommonCakeFormationControl
    {
        public fsDiscFilterControl()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.45));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.1));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(96));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.6e5));
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(0.45));
            SetDefaultValue(fsParameterIdentifier.RotationalSpeed, new fsValue(1.0 / 60.0));
        }
    }
}
