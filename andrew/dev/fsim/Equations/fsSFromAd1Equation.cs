using Parameters;
using Value;

namespace Equations
{
    public class fsSFromAd1Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_S;
        private readonly IEquationParameter m_Sr;
        private readonly IEquationParameter m_ad2;
        private readonly IEquationParameter m_K;
        private readonly IEquationParameter m_ad1;

        #endregion

        public fsSFromAd1Equation(
            IEquationParameter S,
            IEquationParameter Sr,
            IEquationParameter ad2,
            IEquationParameter K,
            IEquationParameter ad1)
            : base(
                S,
                Sr,
                ad2,
                K,
                ad1)
        {
            m_S = S;
            m_Sr = Sr;
            m_ad2 = ad2;
            m_K = K;
            m_ad1 = ad1;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_S, SFormula);
            AddFormula(m_ad1, Ad1Formula);
            AddFormula(m_K, KFormula);
        }

        #region Formulas

        private void SFormula()
        {
            m_S.Value = m_Sr.Value + (1 - m_Sr.Value) * fsValue.Pow((1 + m_ad2.Value * m_K.Value), -m_ad1.Value);
        }

        private void Ad1Formula()
        {
            m_ad1.Value = -fsValue.Log((m_S.Value - m_Sr.Value)/(1 - m_Sr.Value))/fsValue.Log(1 + m_ad2.Value*m_K.Value);
        }

        private void KFormula()
        {
            m_K.Value = (fsValue.Pow((1-m_Sr.Value ) / (m_S.Value - m_Sr.Value), 1 / m_ad1.Value)-1) /
                        m_ad2.Value;
        }
        #endregion
    }
}