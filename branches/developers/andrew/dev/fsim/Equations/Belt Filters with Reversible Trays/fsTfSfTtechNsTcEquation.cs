using Parameters;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsTfSfTtechNsTcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_sf;
        private readonly IEquationParameter m_tf;
        private readonly IEquationParameter m_tc;
        private readonly IEquationParameter m_ns;
        private readonly IEquationParameter m_ttech;
        
        #endregion

        public fsTfSfTtechNsTcEquation(
            IEquationParameter sf,
            IEquationParameter tf,
            IEquationParameter tc,
            IEquationParameter ns,
            IEquationParameter ttech)
            : base(sf, tf, tc, ns, ttech)
        {
            m_sf = sf;
            m_tf = tf;
            m_tc = tc;
            m_ns = ns;
            m_ttech = ttech;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_sf, SfFormula);
            AddFormula(m_tf, TfFormula);
            AddFormula(m_tc, TcFormula);
        }

        #region Formulas

        private void SfFormula()
        {
            m_sf.Value = m_tf.Value / (m_tc.Value - m_ns.Value * m_ttech.Value);
        }

        private void TfFormula()
        {
            m_tf.Value = m_sf.Value * (m_tc.Value - m_ns.Value * m_ttech.Value);
        }

        private void TcFormula()
        {
            m_tc.Value = m_tf.Value / m_sf.Value + m_ns.Value * m_ttech.Value;
        }

        #endregion
    }
}
