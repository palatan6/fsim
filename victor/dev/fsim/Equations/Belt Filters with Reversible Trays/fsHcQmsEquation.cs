using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsHcQmsEquation : fsCalculatorEquation
    {
        /*
         * 
         * a * hc^2 + b * hc + c = 0
         * 
         * a = Qms
         * b = 2 * hce * Qms - rhoCd * sf * C1 * A
         * c = sf * ns * ttech * C1
         * C1 = 2 * pc * kappa * Dp / etaF
         * 
         *   */

        #region Parameters

        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_Qms;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_rhoCd;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_ttech;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_sf;
        private readonly IEquationParameter m_etaF;

        #endregion

        public fsHcQmsEquation(
            IEquationParameter hc,
            IEquationParameter Qms,
            IEquationParameter A,
            IEquationParameter rhoCd,
            IEquationParameter hce,
            IEquationParameter ns,
            IEquationParameter ttech,
            IEquationParameter kappa,
            IEquationParameter pc,
            IEquationParameter Dp,
            IEquationParameter sf,
            IEquationParameter etaF)
            : base(hc,Qms,A,rhoCd,hce,ns,ttech,kappa,pc,Dp,sf,etaF)
        {

            m_hc = hc;
            m_Qms = Qms;
            m_A = A;
            m_rhoCd = rhoCd;
            m_hce = hce;
            m_ns = ns;
            m_ttech = ttech;
            m_kappa = kappa;
            m_pc = pc;
            m_Dp = Dp;
            m_sf = sf;
            m_etaF = etaF;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_hc, HcFormula);
        }

        #region Formulas

        private void HcFormula()
        {
            fsValue C1 = 2 * m_pc.Value * m_kappa.Value * m_Dp.Value / m_etaF.Value;
            fsValue a = m_Qms.Value;
            fsValue b = 2 * m_hce.Value * m_Qms.Value - m_rhoCd.Value * m_sf.Value * C1 * m_A.Value;
            fsValue c = m_Qms.Value * m_sf.Value * m_ns.Value * m_ttech.Value * C1;
            fsValue x1, x2;
            if (fsQuadraticEquation.Solve(a, b, c, out x1, out x2))
            {
                if (x1.Value > x2.Value)
                {
                    fsValue temp = x1;
                    x1 = x2;
                    x2 = temp;
                }
                // we select the biggest one solution
                m_hc.Value = x2.Value > 0 ? x2 : x1;
            }
            else
            {
                m_hc.Value = new fsValue();
            }
        }

        

        #endregion
    }
}
