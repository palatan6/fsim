using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsSfFromEtafHcHceKappaPcDpNsLsUTtechEquation : fsCalculatorEquation
    {
        //               etaf * hc * (hc + 2 * hce)
        // sf = ---------------------------------------------
        //       2 * kappa * pc * Dp * ns * (ls / u - ttech)

        #region Parameters

        private readonly IEquationParameter m_sf;
        private readonly IEquationParameter m_etaf;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_Pc;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_ls;
        private readonly IEquationParameter m_u;
        private readonly IEquationParameter m_ttech;

        #endregion

        public fsSfFromEtafHcHceKappaPcDpNsLsUTtechEquation(
                IEquationParameter sf,
                IEquationParameter etaf,
                IEquationParameter hc,
                IEquationParameter hce,
                IEquationParameter kappa,
                IEquationParameter Pc,
                IEquationParameter Dp,
                IEquationParameter ns,
                IEquationParameter ls,
                IEquationParameter u,
                IEquationParameter ttech)
            : base(sf, etaf, hc, hce, kappa, Pc, Dp, ns, ls, u, ttech)
        {
            m_sf = sf;
            m_etaf = etaf;
            m_hc = hc;
            m_hce = hce;
            m_kappa = kappa;
            m_Pc = Pc;
            m_Dp = Dp;
            m_ns = ns;
            m_ls = ls;
            m_u = u;
            m_ttech = ttech;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_sf, SfFormula);
            AddFormula(m_u, UFormula);
        }

        #region Formulas

        private void SfFormula()
        {
            fsValue num = m_etaf.Value * m_hc.Value * (m_hc.Value + 2 * m_hce.Value);
            fsValue den = 2 * m_kappa.Value * m_Pc.Value * m_Dp.Value * m_ns.Value *
                          (m_ls.Value / m_u.Value - m_ttech.Value);
            m_sf.Value = num / den;
        }

        private void UFormula()
        {
            fsValue num = m_etaf.Value * m_hc.Value * (m_hc.Value + 2 * m_hce.Value);
            fsValue den = 2 * m_sf.Value * m_kappa.Value * m_Pc.Value * m_Dp.Value * m_ns.Value;
            fsValue lsOverU = m_ttech.Value + num / den;
            m_u.Value = m_ls.Value / lsOverU;
        }

        #endregion
    }
}
