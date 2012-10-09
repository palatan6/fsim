using Parameters;
using Value;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsUFromSfMaterialHcDpNsTtechLsOverBQmsEquation : fsCalculatorEquation
    {
        // using two equations:
        //
        //
        //               etaf * hc * (hc + 2 * hce)
        // sf = ---------------------------------------------
        //       2 * kappa * pc * Dp * ns * (ls / u - ttech)
        //
        //
        //
        // and
        //
        //
        //        lsOverB * Qms
        // ls = ------------------
        //       rho_cd0 * u * hc
        //
        //
        //
        // we can calculate u
        //

        #region Parameters

        private readonly IEquationParameter m_sf;
        private readonly IEquationParameter m_etaf;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_Pc;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_ttech;
        private readonly IEquationParameter m_lsOverB;
        private readonly IEquationParameter m_Qms;
        private readonly IEquationParameter m_rhoCd;
        private readonly IEquationParameter m_u;

        #endregion

        public fsUFromSfMaterialHcDpNsTtechLsOverBQmsEquation(
                IEquationParameter sf,
                IEquationParameter etaf,
                IEquationParameter hc,
                IEquationParameter hce,
                IEquationParameter kappa,
                IEquationParameter Pc,
                IEquationParameter Dp,
                IEquationParameter ns,
                IEquationParameter ttech,
                IEquationParameter lsOverB,
                IEquationParameter Qms,
                IEquationParameter rhoCd,
                IEquationParameter u)
            : base(sf, etaf, hc, hce, kappa, Pc, Dp, ns, ttech, lsOverB, Qms, rhoCd, u)
        {
            m_sf = sf;
            m_etaf = etaf;
            m_hc = hc;
            m_hce = hce;
            m_kappa = kappa;
            m_Pc = Pc;
            m_Dp = Dp;
            m_ns = ns;
            m_ttech = ttech;
            m_lsOverB = lsOverB;
            m_Qms = Qms;
            m_rhoCd = rhoCd;
            m_u = u;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_u, UFormula);
        }

        #region Formulas

        private void UFormula()
        {
            fsValue lsOverU = CalculateLsOverU();
            fsValue lsTimesU = CalculateLsTimesU();
            m_u.Value = fsValue.Sqrt(lsTimesU / lsOverU);
        }

        #endregion

        #region Help Calculations

        private fsValue CalculateLsOverU()
        {
            fsValue lsOverUMinusTtech = m_sf.Value * 2 * m_kappa.Value * m_Pc.Value * m_Dp.Value * m_ns.Value /
                                        (m_etaf.Value * m_hc.Value * (m_hc.Value + 2 * m_hce.Value));
            return lsOverUMinusTtech + m_ttech.Value;
        }

        private fsValue CalculateLsTimesU()
        {
            return m_lsOverB.Value * m_Qms.Value / (m_rhoCd.Value * m_hce.Value);
        }

        #endregion
    }
}
