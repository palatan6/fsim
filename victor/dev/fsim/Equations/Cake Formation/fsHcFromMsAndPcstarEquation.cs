using Parameters;
using Value;

namespace Equations
{
    public class fsHcFromMsAndPcstarEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pcstar;
        private readonly IEquationParameter eta;

        //hc = (ms / (2 * A * rho_s * cv)) * (1 - Sqrt(1 - (8 * A * A * rho_s * rho_s * cv * cv * Dp * tf * pcstar / (eta * ms * ms))));

        #endregion

        public fsHcFromMsAndPcstarEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePorosityStar,
            IEquationParameter Viscosity)
            : base(
                CakeHeight,
                SolidsMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePorosityStar,
                Viscosity)
        {
            hc = CakeHeight;
            ms = SolidsMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            pcstar = CakePorosityStar;
            eta = Viscosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = (ms.Value / (2 * A.Value * rho_s.Value * cv.Value)) * (1 - fsValue.Sqrt(1 - (8 * A.Value * A.Value * rho_s.Value * rho_s.Value * cv.Value * cv.Value * Dp.Value * tf.Value * pcstar.Value / (eta.Value * ms.Value * ms.Value))));
        }

        #endregion
    }
}