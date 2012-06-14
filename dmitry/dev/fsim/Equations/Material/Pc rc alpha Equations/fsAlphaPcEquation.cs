using Parameters;

namespace Equations
{
    public class fsAlphaPcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_alpha;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_rhoS;

        #endregion

        public fsAlphaPcEquation(
            IEquationParameter alpha,
            IEquationParameter pc,
            IEquationParameter eps,
            IEquationParameter rhoS)
            : base(
                alpha,
                pc,
                eps,
                rhoS)
        {
            m_alpha = alpha;
            m_pc = pc;
            m_eps = eps;
            m_rhoS = rhoS;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_alpha, AlphaFormula);
            AddFormula(m_pc, PcFormula);
        }

        #region Formulas

        private void AlphaFormula()
        {
            m_alpha.Value = 1 / (m_pc.Value * (1 - m_eps.Value) * m_rhoS.Value);
        }

        private void PcFormula()
        {
            m_pc.Value = 1 / (m_alpha.Value * (1 - m_eps.Value) * m_rhoS.Value);
        }

        #endregion
    }
}