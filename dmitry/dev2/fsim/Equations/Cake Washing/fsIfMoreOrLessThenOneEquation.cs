using System;
using Parameters;
using Value;

namespace Equations.CakeWashing
{
    public class fsIfMoreOrLessThenOneEquation : fsCalculatorEquation
    {
        /*
         *     { v * a + (1 - v) * b,  if v < 1 
         * x = | 
         *     { a,                    if v >= 1 
         */ 

        #region Parameters

        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_v;
        private readonly IEquationParameter m_a;
        private readonly IEquationParameter m_b;

        #endregion

        public fsIfMoreOrLessThenOneEquation(
            IEquationParameter x,
            IEquationParameter v,
            IEquationParameter a,
            IEquationParameter b)
            : base(x, v, a, b)
        {
            m_x = x;
            m_v = v;
            m_a = a;
            m_b = b;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_x, xFormula);
        }

        #region Formulas

        private void xFormula()
        {
            if (fsValue.Less(m_v.Value, fsValue.One))
            {
                m_x.Value = m_v.Value * m_a.Value + (1 - m_v.Value) * m_b.Value;
            }
            else
            {
                m_x.Value = m_a.Value;
            }
        }

        #endregion
    }
}
