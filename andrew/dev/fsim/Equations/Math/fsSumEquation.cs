using Parameters;

namespace Equations
{
    public class fsSumEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_firstElement;
        private readonly IEquationParameter m_secondElement;
        private readonly IEquationParameter m_sum;

        #endregion

        // sum = first + second

        public fsSumEquation(
            IEquationParameter sum,
            IEquationParameter firstElement,
            IEquationParameter secondElement)
            : base(sum, firstElement, secondElement)
        {
            m_sum = sum;
            m_firstElement = firstElement;
            m_secondElement = secondElement;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_sum, SumFormula);
            AddFormula(m_firstElement, FirstElementFormula);
            AddFormula(m_secondElement, SecondElementFormula);
        }

        #region Formulas

        private void SecondElementFormula()
        {
            m_secondElement.Value = m_sum.Value - m_firstElement.Value;
        }

        private void FirstElementFormula()
        {
            m_firstElement.Value = m_sum.Value - m_secondElement.Value;
        }

        private void SumFormula()
        {
            m_sum.Value = m_firstElement.Value + m_secondElement.Value;
        }

        #endregion
    }
}