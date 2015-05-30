using Parameters;

namespace Equations
{
    public class fsvcConcaveAreaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_vc;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_D;

        #endregion

        // vc = hc*(1 - hc/d)

        public fsvcConcaveAreaEquation(
            IEquationParameter vc,
            IEquationParameter hc,
            IEquationParameter D)
            : base(
                vc,
                hc,
                D)
        {
            m_vc = vc;
            m_hc = hc;
            m_D = D;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_vc, VcFormula);
        }

        #region Formulas

        private void VcFormula()
        {
            m_vc.Value = m_hc.Value*(1 - m_hc.Value/m_D.Value);
        }

        #endregion
    }
}
