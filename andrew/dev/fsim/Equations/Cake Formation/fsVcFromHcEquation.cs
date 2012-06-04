using Parameters;

namespace Equations
{
    public class fsVcFromHcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter vc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter hc;
        
        #endregion

        //v_c = A * h_c

        public fsVcFromHcEquation(
            IEquationParameter CakeVolume,
            IEquationParameter FilterArea,
            IEquationParameter CakeHeight)
            : base(
                CakeVolume,
                FilterArea,
                CakeHeight)
        {
            vc = CakeVolume;
            A = FilterArea;
            hc = CakeHeight;
        }

        protected override void InitFormulas()
        {
            AddFormula(vc, vcFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void vcFormula()
        {
            vc.Value = A.Value * hc.Value;
        }

        private void hcFormula()
        {
            hc.Value = vc.Value / A.Value;
        }

        #endregion
    }
}