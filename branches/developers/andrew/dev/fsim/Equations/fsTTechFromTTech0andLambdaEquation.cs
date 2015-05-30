using Parameters;
using Value;

namespace Equations
{
    public class fsTTechFromTTech0andLambdaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_ttech;
        private readonly IEquationParameter m_ttech0;
        private readonly IEquationParameter m_b;
        private readonly IEquationParameter m_lambda;

        #endregion

        public fsTTechFromTTech0andLambdaEquation(
            IEquationParameter ttech,
            IEquationParameter ttech0,
            IEquationParameter b,
            IEquationParameter lambda)
            : base(
                ttech, ttech0, b, lambda)
        {
            m_ttech=ttech;
            m_ttech0 = ttech0;
            m_b = b;
            m_lambda = lambda;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ttech, TtechFormula);
            AddFormula(m_ttech0, Ttech0Formula);
        }

        #region Formulas

        private void TtechFormula()
        {
            m_ttech.Value = m_ttech0.Value*fsValue.Pow(m_b.Value, m_lambda.Value);
        }

        private void Ttech0Formula()
        {
            m_ttech0.Value = m_ttech.Value * fsValue.Pow(m_b.Value, -m_lambda.Value);
        }

        #endregion
    }
}