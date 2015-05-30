using fsNumericalMethods;
using Parameters;
using Value;

namespace Equations
{
    public class fsQgtFromAg1Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Qgt;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_Dpd;
        private readonly IEquationParameter m_theta;
        private readonly IEquationParameter m_ag1;
        private readonly IEquationParameter m_ag2;
        private readonly IEquationParameter m_ag3;
        private readonly IEquationParameter m_K;
        private readonly IEquationParameter m_eta;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_hce;

        #endregion

        public fsQgtFromAg1Equation(
            IEquationParameter Qgt,
            IEquationParameter A,
            IEquationParameter pc,
            IEquationParameter Dpd,
            IEquationParameter theta,
            IEquationParameter ag1,
            IEquationParameter ag2,
            IEquationParameter ag3,
            IEquationParameter K,
            IEquationParameter etag,
            IEquationParameter hc,
            IEquationParameter hce)
            : base(
                Qgt,
                A,
                pc,
                Dpd,
                theta,
                ag1,
                ag2,
                ag3,
                K,
                etag,
                hc,
                hce)
        {
            m_Qgt = Qgt;
            m_A = A;
            m_pc = pc;
            m_Dpd = Dpd;
            m_theta = theta;
            m_ag1 = ag1;
            m_ag2 = ag2;
            m_ag3 = ag3;
            m_K = K;
            m_eta = etag;
            m_hc = hc;
            m_hce = hce;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Qgt, QgiFormula);
            AddFormula(m_ag1, Ag1Formula);
            AddFormula(m_K, KFormula);
        }

        #region Formulas

        private void QgiFormula()
        {
            fsValue bar = new fsValue(1e5);
            fsValue Tn = new fsValue(273);
            fsValue T = m_theta.Value + Tn;
            fsValue m_pmoverpn = 1 + m_Dpd.Value / (2 * bar);
            m_Qgt.Value = m_A.Value * m_pc.Value * m_pmoverpn * m_Dpd.Value * T / (m_eta.Value * (m_hc.Value + m_hce.Value) * Tn) *
                          (m_ag1.Value + m_ag2.Value * fsValue.Log(m_Dpd.Value / bar)) * (1-(1 - fsValue.Exp(-m_ag3.Value * m_K.Value))/(m_ag3.Value*m_K.Value));
        }

        private void Ag1Formula()
        {
            fsValue bar = new fsValue(1e5);
            fsValue Tn = new fsValue(273);
            fsValue T = m_theta.Value + Tn;
            fsValue m_pmoverpn = 1 + m_Dpd.Value / (2 * bar);
            m_ag1.Value = m_Qgt.Value/
                          (m_A.Value*m_pc.Value*m_pmoverpn*m_Dpd.Value*T/(m_eta.Value*(m_hc.Value + m_hce.Value)*Tn)*
                           (1 - (1 - fsValue.Exp(-m_ag3.Value*m_K.Value))/(m_ag3.Value*m_K.Value))) -
                          (m_ag2.Value*fsValue.Log(m_Dpd.Value/bar));
        }

        #region Help Equation Class

        class KCalculationFunction : fsFunction
        {
            #region Parameters

            private readonly fsValue m_Qgt;
            private readonly fsValue m_A;
            private readonly fsValue m_pc;
            private readonly fsValue m_Dpd;
            private readonly fsValue m_theta;
            private readonly fsValue m_ag1;
            private readonly fsValue m_ag2;
            private readonly fsValue m_ag3;
            private readonly fsValue m_eta;
            private readonly fsValue m_hc;
            private readonly fsValue m_hce;
            #endregion

            public KCalculationFunction(
                
            fsValue Qgt,
            fsValue A,
            fsValue pc,
            fsValue Dpd,
            fsValue theta,
            fsValue ag1,
            fsValue ag2,
            fsValue ag3,
            fsValue eta,
            fsValue hc,
            fsValue hce)
            {
                m_Qgt = Qgt;
                m_A = A;
                m_pc = pc;
                m_Dpd = Dpd;
                m_theta = theta;
                m_ag1 = ag1;
                m_ag2 = ag2;
                m_ag3 = ag3;
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

                fsValue Qgimax = (m_A*m_pc*m_pmoverpn*m_Dpd*T/(m_eta*(m_hc + m_hce)*Tn))*
                                 (m_ag1 + m_ag2*fsValue.Log(m_Dpd/bar));

                fsValue S = (1 - fsValue.Exp(-m_ag3*K))/K - m_ag3*(1 - m_Qgt/Qgimax);
                return S;
            }
        }
        #endregion

        private void KFormula()
        {
            var f = new KCalculationFunction(m_Qgt.Value, m_A.Value, m_pc.Value, m_Dpd.Value, m_theta.Value, m_ag1.Value,
                m_ag2.Value, m_ag3.Value, m_eta.Value, m_hc.Value, m_hce.Value);
            m_K.Value = fsBisectionMethod.FindRoot(f, new fsValue(0), new fsValue(1000), 400);
        }
        #endregion
    }
}
