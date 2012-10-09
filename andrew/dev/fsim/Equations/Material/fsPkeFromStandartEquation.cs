using Parameters;
using Value;

namespace Equations
{
    public class fsPkeFromStandartEquation : fsCalculatorEquation
    {
        /*
         *               sigma        /  pc0  \
         * pke = pke0 * -------- sqrt | ----- |
         *               sigma0       \  pcd  /
         * */

        #region Parameters

        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_pke;
        private readonly IEquationParameter m_pkest;
        private readonly IEquationParameter m_sigma;

        #endregion

        public fsPkeFromStandartEquation(
            IEquationParameter pke,
            IEquationParameter pkeSt,
            IEquationParameter sigma,
            IEquationParameter pc)
            : base(pke, pkeSt, sigma, pc)
        {
            m_pke = pke;
            m_pkest = pkeSt;
            m_sigma = sigma;
            m_pc = pc;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_pke, PkeFormula);
        }

        #region Formulas

        private void PkeFormula()
        {
            const double sigmaSt = 72e-3;
            const double PcSt = 1e-13;
            m_pke.Value = m_pkest.Value * m_sigma.Value / sigmaSt * fsValue.Sqrt(PcSt / m_pc.Value);
        }

        #endregion
    }
}