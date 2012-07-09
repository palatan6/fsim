using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsRm0Hce0Calculator : fsCalculatorEquationsList
    {
        readonly fsCalculatorVariable m_hce0;
        readonly fsCalculatorVariable m_rm0;
        readonly fsCalculatorConstant m_pc0;

        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter hce0 = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            IEquationParameter rm0 = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsProductEquation(m_hce0, m_pc0, m_rm0));

            #endregion
        }
    }
}
