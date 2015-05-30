using System.Net;
using fsNumericalMethods;
using Parameters;
using Value;

namespace Equations
{
    public class fsKFromVgEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Vg;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_Dpd;
        private readonly IEquationParameter m_pke;
        private readonly IEquationParameter m_theta;
        private readonly IEquationParameter m_ag1;
        private readonly IEquationParameter m_ag2;
        private readonly IEquationParameter m_ag3;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_K;
        private readonly IEquationParameter m_etag;
        private readonly IEquationParameter m_eta;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_hce;

        #endregion

        public fsKFromVgEquation(
            IEquationParameter Vg,
            IEquationParameter A,
            IEquationParameter pc,
            IEquationParameter Dpd,
            IEquationParameter pke,
            IEquationParameter theta,
            IEquationParameter ag1,
            IEquationParameter ag2,
            IEquationParameter ag3,
            IEquationParameter eps,
            IEquationParameter K,
            IEquationParameter etag,
            IEquationParameter eta,
            IEquationParameter hc,
            IEquationParameter hce)
            : base(
                Vg,
                A,
                pc,
                Dpd,
                pke,
                theta,
                ag1,
                ag2,
                ag3,
                eps,
                K,
                etag,
                eta,
                hc,
                hce)
        {
            m_Vg = Vg;
            m_A = A;
            m_pc = pc;
            m_Dpd = Dpd;
            m_pke = pke;
            m_theta = theta;
            m_ag1 = ag1;
            m_ag2 = ag2;
            m_ag3 = ag3;
            m_eps = eps;
            m_K = K;
            m_etag = etag;
            m_eta = eta;
            m_hc = hc;
            m_hce = hce;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_K, KFormula);
        }

        #region Formulas

        #region Help Equation Class

        class KCalculationFunction : fsFunction
        {
            #region Parameters

            private readonly fsValue m_A;
            private readonly fsValue m_pc;
            private readonly fsValue m_Dpd;
            private readonly fsValue m_pke;
            private readonly fsValue m_Vg;
            private readonly fsValue m_theta;
            private readonly fsValue m_ag1;
            private readonly fsValue m_ag2;
            private readonly fsValue m_ag3;
            private readonly fsValue m_eps;
            private readonly fsValue m_etag;
            private readonly fsValue m_eta;
            private readonly fsValue m_hc;
            private readonly fsValue m_hce;
            #endregion

            public KCalculationFunction(

                fsValue A,
                fsValue pc,
                fsValue Dpd,
                fsValue pke,
                fsValue Vg,
                fsValue theta,
                fsValue ag1,
                fsValue ag2,
                fsValue ag3,
                fsValue eps,
                fsValue etag,
                fsValue eta,
                fsValue hc,
                fsValue hce)
            {
                m_A = A;
                m_pc = pc;
                m_Dpd = Dpd;
                m_pke = pke;
                m_Vg = Vg;
                m_theta = theta;
                m_ag1 = ag1;
                m_ag2 = ag2;
                m_ag3 = ag3;
                m_eps = eps;
                m_etag = etag;
                m_eta = eta;
                m_hc = hc;
                m_hce = hce;
            }

            public override fsValue Eval(fsValue K)
            {
                fsValue bar = new fsValue(1e5);
                fsValue Tn = new fsValue(273);
                fsValue T = m_theta.Value + Tn;
                fsValue m_pmoverpn = 1 + m_Dpd.Value / (2 * bar);

                fsValue Qgimax = (m_A * m_pc * m_pmoverpn * m_Dpd * T / (m_etag * (m_hc + m_hce) * Tn)) *
                                 (m_ag1 + m_ag2 * fsValue.Log(m_Dpd / bar));

                fsValue S = K - (1 - fsValue.Exp(-m_ag3*K))/m_ag3 -
                            (m_pc*(m_Dpd - m_pke)*m_Vg)/(m_eps*m_eta*m_hc*(m_hc + m_hce)*Qgimax);
                return S;
            }
        }
        #endregion

        private void KFormula()
        {
            var f = new KCalculationFunction(m_A.Value, m_pc.Value, m_Dpd.Value, m_pke.Value, m_Vg.Value, m_theta.Value,
                m_ag1.Value, m_ag2.Value, m_ag3.Value, m_eps.Value, m_etag.Value, m_eta.Value,m_hc.Value, m_hce.Value);
            m_K.Value = fsBisectionMethod.FindRoot(f, new fsValue(0), new fsValue(1000), 400);
        }
        #endregion
    }
}
