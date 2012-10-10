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
        /*
         * c* = 1 - 1/2*( erfc(Dn^(1/2)/2 * (1 - wf)/wf^(1/2)) + exp(Dn) * erfc(Dn^(1/2)/2 * (1 + wf)/wf^(1/2)) )
         * 
         * Denote v = Dn^(1/2)/2 * (1 - wf)/wf^(1/2). 
         * Then Dn^(1/2)/2 * (1 + wf)/wf^(1/2) = (v^2 + Dn)^(1/2). 
         * We use this relashionship for finding unknown wf when c* is known (see wfFormula())
         */

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
                if (m_a == fsValue.Zero)
                {
                    return 2 * fsSpecialFunctions.Erfc(x) - m_u;
                }
                else 
                {
                    return fsSpecialFunctions.Erfc(x) +
                           fsValue.Exp(m_a) * fsSpecialFunctions.Erfc(fsValue.Sqrt(fsValue.Sqr(x) + m_a)) - m_u;
                }
                
            }
        }

        #endregion

        private void cStarFormula()
        {
            if (m_Dn.Value == fsValue.Zero)
            {
                m_cStar.Value = fsValue.Zero;
                return;
            }
            if ((fsValue.Less(m_wf.Value, fsValue.Zero) || m_wf.Value == fsValue.Zero) &&
                  m_wf.Value.Defined
               )
            {
                m_cStar.Value = fsValue.One;
            }
            else
            {
                fsValue sqrt = 2 * fsValue.Sqrt(m_wf.Value / m_Dn.Value);
                m_cStar.Value = 1 - 0.5 *
                               (fsSpecialFunctions.Erfc((1 - m_wf.Value) / sqrt) +
                                fsValue.Exp(m_Dn.Value) * fsSpecialFunctions.Erfc((1 + m_wf.Value) / sqrt)
                               );
            }
        }

        private void wfFormula()
        {
            if (fsValue.One == m_cStar.Value)
            {
                m_wf.Value = fsValue.Zero;
                return;
            }
            bool condEmpty = fsValue.Less(m_Dn.Value, fsValue.Zero) ||
                             fsValue.Less(fsValue.One, m_cStar.Value);
            if (condEmpty)
            {
                m_wf.Value = new fsValue();
            }
            else 
            {
                fsValue u = 2 * (1 - m_cStar.Value);
                var f = new wfCalculationFunction(m_Dn.Value, u);
                fsValue lowerBound;
                fsValue upperBound;
                if (fsValue.Less(u, 1.0 + fsValue.Exp(m_Dn.Value) * fsSpecialFunctions.Erfc(fsValue.Sqrt(m_Dn.Value))))
                {
                    lowerBound = fsValue.Zero;
                    upperBound = fsValue.Max(fsValue.One,
                                             fsValue.Sqrt(fsValue.Log(2 / (u * Math.Sqrt(Math.PI))))
                                 );
                }
                else
                {
                    lowerBound = -1.0 * fsSpecialFunctions.InvErf(u - 1.0); 
                    upperBound = fsValue.Zero;
                }

                fsValue x = fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 60, new fsValue(1e-8));
                x = 2 * x / fsValue.Sqrt(m_Dn.Value);
                m_wf.Value = 1 + 0.5 * x * (x - fsValue.Sqrt(fsValue.Sqr(x) + 4));
            }          
        }

        #endregion
    }
}

