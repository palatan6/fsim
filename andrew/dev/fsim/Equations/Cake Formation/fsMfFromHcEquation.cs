using Parameters;
namespace Equations
{
    public class fsMfFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter mf;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter kappa;

        #endregion

        //m_f = (rho * A * h_c) /  kappa

        public fsMfFromHcEquation(
            IEquationParameter FiltrateMass,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter Kappa)
            : base(
                FiltrateMass,
                MotherLiquidDensity,
                FilterArea,
                CakeHeight,
                Kappa)
        {
            mf = FiltrateMass;
            rho = MotherLiquidDensity;
            A = FilterArea;
            hc = CakeHeight;
            kappa = Kappa;
        }

        protected override void InitFormulas()
        {
            AddFormula(mf, mfFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void mfFormula()
        {
            mf.Value = (rho.Value * A.Value * hc.Value) / kappa.Value;
        }

        private void hcFormula()
        {
            hc.Value = (mf.Value * kappa.Value) / (rho.Value * A.Value);
        }

        #endregion
    }
}
