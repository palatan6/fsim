using Parameters;
namespace Equations
{
    public class fsVsusFromHcAKappaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter vsus;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter kappa;
        
        #endregion

        //vsus = A * h_c * ((1 + kappa) / kappa)

        public fsVsusFromHcAKappaEquation(
            IEquationParameter SuspensionVolume,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter Kappa)
            : base(
                SuspensionVolume,
                FilterArea,
                CakeHeight,
                Kappa)
        {
            vsus = SuspensionVolume;
            A = FilterArea;
            hc = CakeHeight;
            kappa = Kappa;
        }

        protected override void InitFormulas()
        {
            AddFormula(vsus, vsusFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void vsusFormula()
        {
            vsus.Value = A.Value * hc.Value * ((1 + kappa.Value) / kappa.Value);
        }

        private void hcFormula()
        {
            hc.Value = (vsus.Value * kappa.Value) / (A.Value * (1 + kappa.Value));
        }

        #endregion
    }
}