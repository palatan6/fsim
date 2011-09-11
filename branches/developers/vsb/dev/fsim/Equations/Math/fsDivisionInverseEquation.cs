﻿using Parameters;

namespace Equations
{
    public class fsDivisionInverseEquation : fsCalculatorEquation
    {
        // first * second = 1

        #region Parameters

        readonly IEquationParameter m_first;
        readonly IEquationParameter m_second;

        #endregion

        public fsDivisionInverseEquation(
            IEquationParameter first,
            IEquationParameter second)
            : base(first, second)
        {
            m_first = first;
            m_second = second;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_first, FirstFormula);
            AddFormula(m_second, SecondFormula);
        }

        #region Formulas

        private void FirstFormula()
        {
            m_first.Value = 1 / m_second.Value;
        }

        private void SecondFormula()
        {
            m_second.Value = 1 / m_first.Value;
        }

        #endregion
    }
}
