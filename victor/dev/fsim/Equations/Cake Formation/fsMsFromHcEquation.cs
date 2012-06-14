using Parameters;

namespace Equations
{
    public class fsMsFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter eps;

        #endregion

        //m_s = A * h_c * rho_s * (1 - eps)

        public fsMsFromHcEquation(
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter SolidsDensity,
            IEquationParameter CakePorosity)
            : base(
                SolidsMass,
                FilterArea,
                CakeHeight,
                SolidsDensity,
                CakePorosity)
        {
            ms = SolidsMass;
            A = FilterArea;
            hc = CakeHeight;
            rhos = SolidsDensity;
            eps = CakePorosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(ms, msFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void msFormula()
        {
            ms.Value = A.Value * hc.Value * rhos.Value * (1 - eps.Value);
        }

        private void hcFormula()
        {
            hc.Value = ms.Value / (A.Value * rhos.Value * (1 - eps.Value));
        }

        #endregion
    }
}
