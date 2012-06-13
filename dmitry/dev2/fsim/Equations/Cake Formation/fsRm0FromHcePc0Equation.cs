using Parameters;
namespace Equations
{
    public class fsRm0FromHcePc0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Rm0;
        private readonly IEquationParameter m_hce0;
        private readonly IEquationParameter m_pc0;
        
        #endregion

        // Rm0 = hce0 / pc0

        public fsRm0FromHcePc0Equation(
            IEquationParameter FilterMediumResistanceRm0,
            IEquationParameter FilterMediumResistanceHce0,
            IEquationParameter CakePermeability0)
            : base(FilterMediumResistanceRm0, FilterMediumResistanceHce0, CakePermeability0)
        {
            m_Rm0 = FilterMediumResistanceRm0;
            m_hce0 = FilterMediumResistanceHce0;
            m_pc0 = CakePermeability0;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Rm0, Rm0Formula);
            AddFormula(m_hce0, hc0Formula);
        }

        #region Formulas

        private void Rm0Formula()
        {
            m_Rm0.Value = m_hce0.Value / m_pc0.Value;
        }

        private void hc0Formula()
        {
            m_hce0.Value = m_pc0.Value * m_Rm0.Value;
        }

        #endregion
    }
}
