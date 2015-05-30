using Parameters;
using Value;

namespace Equations
{
    public class fsBFromAAndLoverBEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_b;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_loverb;

        //b  = sqrt(A/(l/b))

        #endregion

        public fsBFromAAndLoverBEquation(
            IEquationParameter b,
            IEquationParameter A,
            IEquationParameter loverb)
            : base(
                b,
                A,
                loverb)
        {
            m_b = b;
            m_A = A;
            m_loverb = loverb;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_b, BFormula);
        }

        #region Formulas

        private void BFormula()
        {
            m_b.Value = fsValue.Sqrt(m_A.Value/m_loverb.Value);
        }

        #endregion
    }
}

