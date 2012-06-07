using Parameters;

namespace Equations
{
    public class fsMcFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter rhos;
        private readonly IEquationParameter eps;
        private readonly IEquationParameter rho;
        
        #endregion

        //m_c = (A * h_c) * (rho_s * (1 - eps) + rho * eps)

        public fsMcFromHcEquation(
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter SolidsDensity,
            IEquationParameter CakePorosity,
            IEquationParameter MotherLiquidDensity)
            : base(
                CakeMass,
                FilterArea,
                CakeHeight,
                SolidsDensity,
                CakePorosity,
                MotherLiquidDensity)
        {
            mc = CakeMass;
            A = FilterArea;
            hc = CakeHeight;
            rhos = SolidsDensity;
            eps = CakePorosity;
            rho = MotherLiquidDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(mc, mcFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void mcFormula()
        {
            mc.Value = A.Value * hc.Value * (rhos.Value * (1 - eps.Value) + rho.Value * eps.Value);
        }

        private void hcFormula()
        {
            hc.Value = mc.Value / (A.Value * (rhos.Value * (1 - eps.Value) + rho.Value * eps.Value));
        }

        #endregion
    }
}

