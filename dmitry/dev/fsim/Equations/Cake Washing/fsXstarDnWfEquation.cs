using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;


namespace Equations.CakeWashing
{
    public class fsXstarDnWfEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_xStar;
        private readonly IEquationParameter m_Dn;
        private readonly IEquationParameter m_wf;

        #endregion

        public fsXstarDnWfEquation(
            IEquationParameter xStar,
            IEquationParameter Dn,
            IEquationParameter wf)
            : base(xStar, Dn, wf)
        {
            m_xStar = xStar;
            m_Dn = Dn;
            m_wf = wf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_wf, wfFormula);
        }

        #region Formulas

        #region Help Equation Class

        class Equation : fsFunction
        {
            #region Parameters

            private readonly fsValue m_a;
            private readonly fsValue m_u;

            #endregion

            public Equation(
                fsValue a,
                fsValue u)
            {
                m_a = a;
                m_u = u;
            }

            public override fsValue Eval(fsValue x)
            {
                fsValue x2a = fsValue.Sqrt(fsValue.Sqr(x) + m_a);
                return 1 / (x + x2a) *
                       (fsValue.Exp(m_a) * x2a * fsValue.Erfc(x2a) + x * (2 - fsValue.Erfc(x))) - m_u;
               
            }
        }

        #endregion

       private void wfFormula()
        {
            if (m_Dn.Value == fsValue.Zero)
            {
                if (m_xStar.Value == fsValue.One)
                {
                    m_wf.Value = fsValue.One;
                    return;
                }
                else
                {
                    m_wf.Value = new fsValue();
                    return;
                }
            }
            bool condEmpty = fsValue.Less(m_Dn.Value, fsValue.Zero) ||
                             fsValue.Less(fsValue.One, m_xStar.Value) ||
                             fsValue.One == m_xStar.Value ||
                             fsValue.Less(m_xStar.Value,
                                          fsValue.Exp(m_Dn.Value) * fsValue.Erfc(fsValue.Sqrt(m_Dn.Value))
                             );
            if (condEmpty)
            {
                m_wf.Value = new fsValue();
            }
            else   // 
            {
                var f = new Equation(m_Dn.Value, m_xStar.Value);
                fsValue upperBound = fsValue.Sqrt(m_Dn.Value / (fsValue.Sqr(2/m_xStar.Value - 1) - 1));
                fsValue x = fsBisectionMethod.FindRoot(f, fsValue.Zero, upperBound, 60);
                x = 2 * x / fsValue.Sqrt(m_Dn.Value);
                m_wf.Value = 1 + 0.5 * x * (x - fsValue.Sqrt(fsValue.Sqr(x) + 4));
            }
        }

        #endregion
    }
}

