using Parameters;
namespace Equations
{
    public class fsMsusFromHcRhosusAKappaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter msus;
        private readonly IEquationParameter rhosus;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter kappa;
        
        #endregion

        //msus = rho_sus * A * h_c * ((1 + kappa) / kappa)

        public fsMsusFromHcRhosusAKappaEquation(
            IEquationParameter SuspensionMass,
            IEquationParameter SuspensionDensity,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter Kappa)
            : base(
                SuspensionMass,
                SuspensionDensity,
                FilterArea,
                CakeHeight,
                Kappa)
        {
            msus = SuspensionMass;
            rhosus = SuspensionDensity;
            A = FilterArea;
            hc = CakeHeight;
            kappa = Kappa;
        }

        protected override void InitFormulas()
        {
            AddFormula(msus, msusFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void msusFormula()
        {
            msus.Value = rhosus.Value * A.Value * hc.Value * ((1 + kappa.Value) / kappa.Value);
        }

        private void hcFormula()
        {
            hc.Value = (msus.Value * kappa.Value) / (rhosus.Value * A.Value * (1 + kappa.Value));
        }

        #endregion
    }
}
