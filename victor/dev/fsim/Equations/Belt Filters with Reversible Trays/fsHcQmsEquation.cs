using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsHcQmsEquation : fsCalculatorEquation
    {
        /*
         * 
         * hc = C1 + sqrt(C1^2 + C2)
         * 
         * where,
         *     C1 = K * A * rhoCd / Qms - hce
         *     C2 = K * 2 * ns * ttech
         * and
         *     K = kappa * pc * Dp * sf / etaF
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
            fsValue K = m_kappa.Value * m_pc.Value * m_Dp.Value * m_sf.Value / m_etaF.Value;
            fsValue C1 = K * m_A.Value * m_rhoCd.Value / m_Qms.Value - m_hce.Value;
            fsValue C2 = K * 2 * m_ns.Value * m_ttech.Value;
            m_hc.Value = C1 + fsValue.Sqrt(C1 * C1 + C2);
        }

        

        #endregion
    }
}
