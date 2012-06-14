using Parameters;
namespace Equations
{
    public class fsVfFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter vf;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter kappa;

        #endregion

        //v_f = (A * h_c) /  kappa

        public fsVfFromHcEquation(
            IEquationParameter FiltrateVolume,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter Kappa)
            : base(
                FiltrateVolume,
                FilterArea,
                CakeHeight,
                Kappa)
        {
            vf = FiltrateVolume;
            A = FilterArea;
            hc = CakeHeight;
            kappa = Kappa;
        }

        protected override void InitFormulas()
        {
            AddFormula(vf, vfFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void vfFormula()
        {
            vf.Value = (A.Value * hc.Value) / kappa.Value;
        }

        private void hcFormula()
        {
            hc.Value = (vf.Value * kappa.Value) /A.Value;
        }

        #endregion
    }
}