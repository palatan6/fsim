using Parameters;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsTrSrTtechNsTcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_sr;
        private readonly IEquationParameter m_tr;
        private readonly IEquationParameter m_tc;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_ttech;
        
        #endregion

        public fsTrSrTtechNsTcEquation(
            IEquationParameter sr,
            IEquationParameter tr,
            IEquationParameter tc,
            IEquationParameter ns,
            IEquationParameter ttech)
            : base(sr, tr, tc, ns, ttech)
        {
            m_sr = sr;
            m_tr = tr;
            m_tc = tc;
            m_ns = ns;
            m_ttech = ttech;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_sr, SrFormula);
            AddFormula(m_tr, TrFormula);
        }

        #region Formulas

        private void SrFormula()
        {
            m_sr.Value = (m_tr.Value - m_ns.Value * m_ttech.Value) / (m_tc.Value - m_ns.Value * m_ttech.Value);
        }

        private void TrFormula()
        {
            m_tr.Value = m_sr.Value * (m_tc.Value - m_ns.Value * m_ttech.Value) + m_ns.Value * m_ttech.Value;
        }

        #endregion
    }
}
