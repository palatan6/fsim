using System;
using Parameters;
using Value;

namespace Equations.CakeWashing
{
    public class fsIfMoreOrLessThenOneEquation : fsCalculatorEquation
    {
        /*
         *     { c,                    if v < 0  
         * x = | v * a + (1 - v) * b,  if 0 <= v < 1
         *     { e + d*v,              if v >= 1 
         */

        #region Parameters

        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_v;
        private readonly IEquationParameter m_a;
        private readonly IEquationParameter m_b;
        private readonly IEquationParameter m_c;
        private readonly IEquationParameter m_d;
        private readonly IEquationParameter m_e;

        #endregion

        public fsIfMoreOrLessThenOneEquation(
            IEquationParameter x,
            IEquationParameter v,
            IEquationParameter a,
            IEquationParameter b,
            IEquationParameter c,
            IEquationParameter d,
            IEquationParameter e)
            : base(x, v, a, b, c, d, e)
        {
            m_x = x;
            m_v = v;
            m_a = a;
            m_b = b;
            m_c = c;
            m_d = d;
            m_e = e;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_x, xFormula);
        }

        #region Formulas

        private void xFormula()
        {
            if (fsValue.Less(m_v.Value, fsValue.Zero) && m_v.Value.Defined)
            {
                m_x.Value = m_c.Value;
            }
            else
            {
                if (fsValue.Less(m_v.Value, fsValue.One))
                {
                    m_x.Value = m_v.Value * m_a.Value + (1 - m_v.Value) * m_b.Value;
                }
                else
                {
                    m_x.Value = m_e.Value + m_d.Value * m_v.Value;
                }
            }
        }

        #endregion
    }
}
