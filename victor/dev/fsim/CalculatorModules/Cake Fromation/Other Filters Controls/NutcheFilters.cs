using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;
using Value;

namespace CalculatorModules.Cake_Fromation.Other_Filters_Controls
{
    public partial class fsNutcheFilters : fsCommonCakeFormationControl
    {
        public fsNutcheFilters()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            base.InitializeParametersValues();

            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.01));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(0.60));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.2));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(5e-13));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(10e-3));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(2));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.ResidualTime, new fsValue(15 * 60));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.25));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.StandardCalculation },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.1, 0.8),
                    new[] { fsParameterIdentifier.Qms },
                    new[] { fsParameterIdentifier.CycleTime }));

            m_defaultDiagrams.Add(
                new Enum[] { fsCakeFormationCalculationOption.FilterDesign },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.1, 0.8),
                    new[] { fsParameterIdentifier.FilterArea },
                    new[] { fsParameterIdentifier.CycleTime }));
        }
    }
}
