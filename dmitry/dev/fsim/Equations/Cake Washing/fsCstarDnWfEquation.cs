using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;


namespace Equations.CakeWashing
{
    public class fsCstarDnWfEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cStar;
        private readonly IEquationParameter m_Dn;
        private readonly IEquationParameter m_wf;

        #endregion

        public fsCstarDnWfEquation(
            IEquationParameter cStar,
            IEquationParameter Dn,
            IEquationParameter wf)
            : base(cStar, Dn, wf)
        {
            m_cStar = cStar;
            m_Dn = Dn;
            m_wf = wf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_cStar, cStarFormula);
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
                if (m_a == fsValue.Zero)
                {
                    return 2 * fsValue.Erfc(x) - m_u;
                }
                else 
                {
                    return fsValue.Erfc(x) +
                           fsValue.Exp(m_a) * fsValue.Erfc(fsValue.Sqrt(fsValue.Sqr(x) + m_a)) - m_u;
                }
                
            }
        }

        #endregion

        private void cStarFormula()
        {
            fsValue sqrt = 2 * fsValue.Sqrt(m_wf.Value / m_Dn.Value);
            m_cStar.Value = 1 - 0.5 *
                           (fsValue.Erfc((1 - m_wf.Value) / sqrt) +
                            fsValue.Exp(m_Dn.Value) * fsValue.Erfc((1 + m_wf.Value) / sqrt)
                           );
        }

        private void wfFormula()
        {
            bool condEmpty = fsValue.Less(m_Dn.Value, fsValue.Zero) ||
                             fsValue.Less(fsValue.One, m_cStar.Value) || 
                             fsValue.One == m_cStar.Value  ||
                             fsValue.Less(fsValue.Exp(m_Dn.Value) * fsValue.Erfc(fsValue.Sqrt(m_Dn.Value)),
                                          1 - 2 * m_cStar.Value
                             );
            if (condEmpty)
            {
                m_wf.Value = new fsValue();
            }
            else 
            {
                fsValue u = 2 * (1 - m_cStar.Value);
                var f = new Equation(m_Dn.Value, u);
                fsValue upperBound = fsValue.Max(fsValue.One,
                                                 fsValue.Sqrt(fsValue.Log(2 / (u *Math.Sqrt(Math.PI))))
                                     );
                fsValue x = fsBisectionMethod.FindRoot(f, fsValue.Zero, upperBound, 60);
                x = 2 * x / fsValue.Sqrt(m_Dn.Value);
                m_wf.Value = 1 + 0.5 * x * (x - fsValue.Sqrt(fsValue.Sqr(x) + 4));
            }          
        }

        #endregion
    }
}

