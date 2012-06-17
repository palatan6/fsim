using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsAreaOfBeltWithReversibleTraysEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_filterArea;
        private readonly IEquationParameter m_ls_over_b;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_Qms;
        private readonly IEquationParameter m_rho_cs;
        private readonly IEquationParameter m_u;
        private readonly IEquationParameter m_cakeHeight;

        #endregion

        public fsAreaOfBeltWithReversibleTraysEquation(
                        IEquationParameter filterArea,
                        IEquationParameter ls_over_b,
                        IEquationParameter ns,
                        IEquationParameter Qms,
                        IEquationParameter rho_cs,
                        IEquationParameter u,
                        IEquationParameter cakeHeight)
            : base(filterArea, ls_over_b, ns, Qms, rho_cs, u, cakeHeight)
        {
            m_filterArea = filterArea;
            m_ls_over_b = ls_over_b;
            m_ns = ns;
            m_Qms = Qms;
            m_rho_cs = rho_cs;
            m_u = u;
            m_cakeHeight = cakeHeight;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_filterArea, FilterAreaFormula);
        }

        #region Formulas

        private void FilterAreaFormula()
        {
            fsValue den = fsValue.Sqr(m_rho_cs.Value * m_u.Value * m_cakeHeight.Value);
            m_filterArea.Value = m_ns.Value * fsValue.Sqr(m_Qms.Value) * m_ls_over_b.Value / den;
        }

        #endregion
    }
}
