using Parameters;

namespace Equations
{
    public class fsvcConvexAreaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_vc;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_d;

        #endregion

        // vc = hc*(1 + hc/d)

        public fsvcConvexAreaEquation(
            IEquationParameter vc,
            IEquationParameter hc,
            IEquationParameter d)
            : base(
                vc,
                hc,
                d)
        {
            m_vc = vc;
            m_hc = hc;
            m_d = d;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_vc, VcFormula);
        }

        #region Formulas

        private void VcFormula()
        {
            m_vc.Value = m_hc.Value*(1 + m_hc.Value/m_d.Value);
        }

        #endregion
    }
}
