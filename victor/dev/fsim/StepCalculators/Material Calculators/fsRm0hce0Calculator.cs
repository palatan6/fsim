using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsRm0Hce0Calculator : fsCalculator
    {
        readonly fsCalculatorVariable m_hce0;
        readonly fsCalculatorVariable m_rm0;
        readonly fsCalculatorConstant m_pc0;

        public fsRm0Hce0Calculator()
        {
            #region Parameters Initialization

            m_hce0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            m_rm0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            m_pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);

            #endregion

            #region Equations Initialization

            AddEquation(new fsProductEquation(m_hce0, m_pc0, m_rm0));

            #endregion
        }
    }
}
