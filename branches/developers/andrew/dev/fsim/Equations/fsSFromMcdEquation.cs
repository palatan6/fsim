using Parameters;

namespace Equations
{
    public class fsSFromMcdEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_S;
        private readonly IEquationParameter m_Mcd;
        private readonly IEquationParameter m_Ms;
        private readonly IEquationParameter m_rhof;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_Vc;

        #endregion

        public fsSFromMcdEquation(
            IEquationParameter S,
            IEquationParameter Mcd,
            IEquationParameter Ms,
            IEquationParameter rhof,
            IEquationParameter eps,
            IEquationParameter Vc)
            : base(
                S,
                Mcd,
                Ms,
                rhof,
                eps,
                Vc)
        {
            m_S = S;
            m_Mcd = Mcd;
            m_Ms = Ms;
            m_rhof = rhof;
            m_eps = eps;
            m_Vc = Vc;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_S, SFormula);
            AddFormula(m_Mcd, McdFormula);
        }

        #region Formulas

        private void SFormula()
        {
            m_S.Value = (m_Mcd.Value - m_Ms.Value)/(m_rhof.Value*m_eps.Value*m_Vc.Value);
        }

        private void McdFormula()
        {
            m_Mcd.Value = m_rhof.Value*m_eps.Value*m_Vc.Value*m_S.Value + m_Ms.Value;
        }
        #endregion
    }
}