using Parameters;

namespace Equations
{
    public class fsSFromRhoBulkEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_S;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_rhol;
        private readonly IEquationParameter m_rhos;
        private readonly IEquationParameter m_rhobulk;

        #endregion

        public fsSFromRhoBulkEquation(
            IEquationParameter S,
            IEquationParameter eps,
            IEquationParameter rhol,
            IEquationParameter rhos,
            IEquationParameter rhobulk)
            : base(
                S,
                eps,
                rhol,
                rhos,
                rhobulk)
        {
            m_S = S;
            m_eps = eps;
            m_rhol = rhol;
            m_rhos = rhos;
            m_rhobulk = rhobulk;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_S, SFormula);
            AddFormula(m_rhobulk, RhobulkFormula);
        }

        #region Formulas

        private void SFormula()
        {
            m_S.Value = 1 -
                        ((1 + (1 - m_eps.Value)*(m_rhos.Value/m_rhol.Value - 1)) - m_rhobulk.Value/m_rhol.Value)/
                        m_eps.Value;
        }

        private void RhobulkFormula()
        {
            m_rhobulk.Value = m_rhol.Value*
                              (1 + (1 - m_eps.Value)*(m_rhos.Value/m_rhol.Value - 1) - m_eps.Value*(1 - m_S.Value));
        }
        #endregion
    }
}