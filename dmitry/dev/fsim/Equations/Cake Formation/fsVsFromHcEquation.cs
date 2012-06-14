using Parameters;

namespace Equations
{
    public class fsVsFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter vs;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter eps;

        #endregion

        //v_s = A * h_c * (1 - eps)

        public fsVsFromHcEquation(
            IEquationParameter SolidsVolume,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight,
            IEquationParameter CakePorosity)
            : base(
                SolidsVolume,
                FilterArea,
                CakeHeight,
                CakePorosity)
        {
            vs = SolidsVolume;
            A = FilterArea;
            hc = CakeHeight;
            eps = CakePorosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(vs, vsFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void vsFormula()
        {
            vs.Value = A.Value * hc.Value * (1 - eps.Value);
        }

        private void hcFormula()
        {
            hc.Value = vs.Value / (A.Value * (1 - eps.Value));
        }

        #endregion
    }
}
