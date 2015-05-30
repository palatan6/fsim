using Parameters;

namespace Equations
{
    public class fsMcFromMsAndEpsEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_mc;
        private readonly IEquationParameter m_ms;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_rho;
        private readonly IEquationParameter m_hc;

        //mc  = ms + (eps * A * rho * hc);

        #endregion

        public fsMcFromMsAndEpsEquation(
            IEquationParameter Mc,
            IEquationParameter Ms,
            IEquationParameter eps,
            IEquationParameter A,
            IEquationParameter rho,
            IEquationParameter hc)
            : base(
                Mc,
                Ms,
                eps,
                A,
                rho,
                hc)
        {
            m_mc = Mc;
            m_ms = Ms;
            m_eps = eps;
            m_A = A;
            m_rho = rho;
            m_hc = hc;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_mc, McFormula);
            AddFormula(m_ms, MsFormula);
        }

        #region Formulas

        private void McFormula()
        {
            m_mc.Value = m_ms.Value + (m_eps.Value*m_A.Value*m_rho.Value*m_hc.Value);
        }

        private void MsFormula()
        {
            m_ms.Value = m_mc.Value - (m_eps.Value * m_A.Value * m_rho.Value * m_hc.Value);
        }

        #endregion
    }
}
