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
        /*
         *              wf      
         *              /
         *              |
         * x*(Dn, wf) = | c*(Dn, u) du ,      (1)
         *              |
         *              /
         *              0
         * where
         * c*(Dn, u) = 1 - 1/2*( erfc(Dn^(1/2)/2 * (1 - u)/u^(1/2)) + exp(Dn) * erfc(Dn^(1/2)/2 * (1 + u)/u^(1/2)) )
         * 
         * The integral (1) can be calculated analytically!!! Precisely:
         * x*(Dn, wf) = 1/2 *
         *              ( (1 - wf) * (2 - erfc(Dn^(1/2)/2 * (1 - wf)/wf^(1/2))) + 
         *                 exp(Dn) * (1 + wf) * erfc(Dn^(1/2)/2 * (1 + wf)/wf^(1/2))
         *              ) 
         * Denote v = Dn^(1/2)/2 * (1 - wf)/wf^(1/2). 
         * Then Dn^(1/2)/2 * (1 + wf)/wf^(1/2) = (v^2 + Dn)^(1/2). 
         * We use this relashionship for finding unknown wf when x* is known (see wfFormula())              
         */

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
                return 1 / (x + x2a) *
                       (fsValue.Exp(m_a) * x2a * fsSpecialFunctions.Erfc(x2a) + x * (2 - fsSpecialFunctions.Erfc(x))) - m_u;
               
            }
        }

        #endregion

       private void wfFormula()
        {
            if (m_Dn.Value == fsValue.Zero)
            {
                if (m_xStar.Value == fsValue.One)
                {
                    m_wf.Value = fsValue.Zero;
                    return;
                }
                else
                {
                    m_wf.Value = new fsValue();
                    return;
                }
            }
            if (fsValue.One == m_xStar.Value)
            {
                m_wf.Value = fsValue.Zero;
                return;
            }
            bool condEmpty = fsValue.Less(m_Dn.Value, fsValue.Zero) ||
                             fsValue.Less(fsValue.One, m_xStar.Value);
            if (condEmpty)
            {
                m_wf.Value = new fsValue();
            }
            else
            {
                fsValue u = m_xStar.Value;
                var f = new wfCalculationFunction(m_Dn.Value, u);
                fsValue lowerBound;
                fsValue upperBound;
                if (fsValue.Greater(u, fsValue.Exp(m_Dn.Value) * fsSpecialFunctions.Erfc(fsValue.Sqrt(m_Dn.Value))))
                {
                    lowerBound = fsValue.Zero;
                    upperBound = fsValue.Sqrt(m_Dn.Value / (fsValue.Sqr(2/u - 1) - 1));
                }
                else
                {
                    lowerBound = -1.0 * fsValue.Max(fsValue.One, 
                                                    fsSpecialFunctions.InvErf(1.0 - u / (1.0 + fsValue.Sqrt(1.0 + m_Dn.Value)))
                                        );                                               
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

