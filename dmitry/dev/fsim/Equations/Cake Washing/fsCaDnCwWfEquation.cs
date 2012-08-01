using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;


namespace Equations.CakeWashing
{
    public class fsCaDnCwWfEquation : fsCalculatorEquation
    {
        /*
         * ca(Dn, wf) = 1/wf * (cw * wf + (c0 - cw) * x*(Dn, wf)),
         * where
         * x*(Dn, wf) = 1/2 *
         *              ( (1 - wf) * (2 - erfc(Dn^(1/2)/2 * (1 - wf)/wf^(1/2))) + 
         *                 exp(Dn) * (1 + wf) * erfc(Dn^(1/2)/2 * (1 + wf)/wf^(1/2))
         *              )
         * Denote v = Dn^(1/2)/2 * (1 - wf)/wf^(1/2). 
         * Then Dn^(1/2)/2 * (1 + wf)/wf^(1/2) = (v^2 + Dn)^(1/2). 
         * We use this relashionship for finding unknown wf when ca is known (see wfFormula())              
         */

        #region Parameters

        private readonly IEquationParameter m_ca;
        private readonly IEquationParameter m_cw;
        private readonly IEquationParameter m_c0;
        private readonly IEquationParameter m_Dn;
        private readonly IEquationParameter m_wf;

        #endregion

        public fsCaDnCwWfEquation(
            IEquationParameter ca,
            IEquationParameter cw,
            IEquationParameter c0,
            IEquationParameter Dn,
            IEquationParameter wf)
            : base(ca, cw, c0, Dn, wf)
        {
            m_ca = ca;
            m_cw = cw;
            m_c0 = c0;
            m_Dn = Dn;
            m_wf = wf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ca, caFormula);
            AddFormula(m_wf, wfFormula);
        }

        #region Formulas

        #region Help Equation Class

        class wfCalculationFunction : fsFunction
        {
            #region Parameters

            private readonly fsValue m_a;
            private readonly fsValue m_u;

            #endregion

            public wfCalculationFunction(
                fsValue a,
                fsValue u)
            {
                m_a = a;
                m_u = u;
            }

            public override fsValue Eval(fsValue x)
            {
                fsValue x2a = fsValue.Sqrt(fsValue.Sqr(x) + m_a);
                return 1 / m_a * (x + x2a) *
                      (fsValue.Exp(m_a) * x2a * fsSpecialFunctions.Erfc(x2a) - x * fsSpecialFunctions.Erfc(x)) - m_u;
            }
        }

        #endregion

        private void caFormula()
        {
            if (m_Dn.Value == fsValue.Zero)
            {
                m_ca.Value = m_cw.Value;
                return;
            }
            if ((fsValue.Less(m_wf.Value, fsValue.Zero) || m_wf.Value == fsValue.Zero) &&
                  m_wf.Value.Defined
               )
            {
                m_ca.Value = m_c0.Value;
            }
            else
            {
                fsValue sqrt = 2 * fsValue.Sqrt(m_wf.Value / m_Dn.Value);
                m_ca.Value = (m_c0.Value - m_cw.Value) / (2 * m_wf.Value) *
                             ((1 - m_wf.Value) * fsSpecialFunctions.Erfc((1 - m_wf.Value) / sqrt) -
                               fsValue.Exp(m_Dn.Value) *
                              (1 + m_wf.Value) * fsSpecialFunctions.Erfc((1 + m_wf.Value) / sqrt)
                             ) + m_c0.Value;
            }
        }

        private void wfFormula()
        {
            bool cwEqualsCa = (m_cw.Value == m_ca.Value);
            if (m_Dn.Value == fsValue.Zero)
            {
                if (cwEqualsCa)
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
            if (m_cw.Value == m_c0.Value)
            {
                if (cwEqualsCa)
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

            fsValue u = (m_ca.Value - m_c0.Value) / (m_cw.Value - m_c0.Value);
            bool condEmpty = fsValue.Less(m_Dn.Value, fsValue.Zero) ||
                             fsValue.Less(u, fsValue.Zero) ||
                             u == fsValue.Zero ||
                             fsValue.Less(fsValue.One, u) ||
                             u == fsValue.One;
            if (condEmpty)
            {
                m_wf.Value = new fsValue();
            }
            else
            {
                var f = new wfCalculationFunction(m_Dn.Value, u);
                fsValue lowerBound;
                fsValue upperBound;
                if (fsValue.Less(u, fsValue.Exp(m_Dn.Value) * fsSpecialFunctions.Erfc(fsValue.Sqrt(m_Dn.Value))))
                {
                    lowerBound = fsValue.Zero;
                    fsValue sqrt = 1 / fsValue.Sqrt(m_Dn.Value);
                    fsValue b = sqrt * (1 + sqrt) / Math.Sqrt(Math.PI);
                    upperBound = 1 + fsValue.Sqrt(fsValue.Log(b / u));
                }
                else
                {
                    lowerBound = -1.0 * fsValue.Sqrt(m_Dn.Value / (fsValue.Sqr(2 / u - 1) - 1));
                    upperBound = fsValue.Zero;
                }

                fsValue x = fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 60);
                x = 2 * x / fsValue.Sqrt(m_Dn.Value);
                m_wf.Value = 1 + 0.5 * x * (x - fsValue.Sqrt(fsValue.Sqr(x) + 4));
            }
        }

        #endregion
    }
}

