using Parameters;

namespace Equations
{
    public class fsKFromTdEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_K;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_Dp_d;
        private readonly IEquationParameter m_pke;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_etag;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_td;

        #endregion

        public fsKFromTdEquation(
            IEquationParameter K,
            IEquationParameter pc,
            IEquationParameter Dp_d,
            IEquationParameter pke,
            IEquationParameter eps,
            IEquationParameter etag,
            IEquationParameter hc,
            IEquationParameter hce,
            IEquationParameter td)
            : base(
                K,
                pc,
                Dp_d,
                pke,
                eps,
                etag,
                hc,
                hce,
                td)
        {
            m_K = K;
            m_pc = pc;
            m_Dp_d = Dp_d;
            m_pke = pke;
            m_eps = eps;
            m_etag = etag;
            m_hc = hc;
            m_hce = hce;
            m_td = td;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_K, KFormula);
            AddFormula(m_td, TdFormula);
        }

        #region Formulas

        private void KFormula()
        {
            m_K.Value = (m_pc.Value*(m_Dp_d.Value - m_pke.Value)*m_td.Value)/
                        (m_eps.Value*m_etag.Value*m_hc.Value*(m_hc.Value + m_hce.Value));
        }

        private void TdFormula()
        {
            m_td.Value = (m_eps.Value*m_etag.Value*m_hc.Value*(m_hc.Value + m_hce.Value)*m_K.Value)/
                         (m_pc.Value*(m_Dp_d.Value - m_pke.Value));
        }

        #endregion
    }
}