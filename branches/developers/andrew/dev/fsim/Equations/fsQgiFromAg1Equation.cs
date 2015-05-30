using Parameters;
using Value;

namespace Equations
{
    public class fsQgiFromAg1Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Qgi;
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

        public fsQgiFromAg1Equation(
            IEquationParameter Qgi,
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
                Qgi,
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
            m_Qgi = Qgi;
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
            AddFormula(m_Qgi, QgiFormula);
            AddFormula(m_ag1, Ag1Formula);
            AddFormula(m_K, KFormula);
        }

        #region Formulas

        private void QgiFormula()
        {
            fsValue bar = new fsValue(1e5);
            fsValue Tn = new fsValue(273);
            fsValue T = m_theta.Value + Tn;
            fsValue m_pmoverpn = 1 + m_Dpd.Value/(2*bar);
            m_Qgi.Value = m_A.Value*m_pc.Value*m_pmoverpn*m_Dpd.Value*T/(m_eta.Value*(m_hc.Value + m_hce.Value)*Tn)*
                          (m_ag1.Value + m_ag2.Value*fsValue.Log(m_Dpd.Value/bar))*(1-fsValue.Exp(-m_ag3.Value*m_K.Value));

        }

        private void Ag1Formula()
        {
            fsValue bar = new fsValue(1e5);
            fsValue Tn = new fsValue(273);
            fsValue T = m_theta.Value + Tn;
            fsValue m_pmoverpn = 1 + m_Dpd.Value / (2 * bar);
            m_ag1.Value = m_Qgi.Value/
                          (m_A.Value*m_pc.Value*m_pmoverpn*m_Dpd.Value*T/(m_eta.Value*(m_hc.Value + m_hce.Value)*Tn)*
                           (1 - fsValue.Exp(-m_ag3.Value*m_K.Value))) - (m_ag2.Value*fsValue.Log(m_Dpd.Value/bar));
        }

        private void KFormula()
        {
            fsValue bar = new fsValue(1e5);
            fsValue Tn = new fsValue(273);
            fsValue T = m_theta.Value + Tn;
            fsValue m_pmoverpn = 1 + m_Dpd.Value / (2 * bar);

            fsValue Qgimax = (m_A.Value*m_pc.Value*m_pmoverpn*m_Dpd.Value*T/(m_eta.Value*(m_hc.Value + m_hce.Value)*Tn))*
                             (m_ag1.Value + m_ag2.Value*fsValue.Log(m_Dpd.Value/bar));

            m_K.Value = fsValue.Log(Qgimax/(Qgimax - m_Qgi.Value))/m_ag3.Value;
        }
        #endregion
    }
}
