using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndAlphaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alpha;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;

        //hc = (2 * Sqr(A) * cv * rho_s * Dp * tf - alpha * eta * (Sqr(ms) - 2 * ms * cv * rho_s * A * hce) - Sqrt(Sqr(2 * Sqr(A) * cv * rho_s * Dp * tf + alpha * Sqr(ms) * eta + 2 * alpha * ms * eta * cv * rho_s * A * hce) - 8 * Sqr(A) * cv * rho_s * Dp * tf * alpha * Sqr(ms) * eta)) / (-2 * ms * alpha * eta * cv * rho_s * A);

        #endregion

        public fsHcFromMsAndAlphaEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceHce)
            : base(
                CakeHeight,
                SolidsMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakeResistanceAlpha,
                Viscosity,
                FilterMediumResistanceHce)
        {
            hc = CakeHeight;
            ms = SolidsMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alpha = CakeResistanceAlpha;
            eta = Viscosity;
            hce = FilterMediumResistanceHce;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = (2 * fsValue.Sqr(A.Value) * cv.Value * rho_s.Value * Dp.Value * tf.Value - alpha.Value * eta.Value * (fsValue.Sqr(ms.Value) - 2 * ms.Value * cv.Value * rho_s.Value * A.Value * hce.Value) - fsValue.Sqrt(fsValue.Sqr(2 * fsValue.Sqr(A.Value) * cv.Value * rho_s.Value * Dp.Value * tf.Value + alpha.Value * fsValue.Sqr(ms.Value) * eta.Value + 2 * alpha.Value * ms.Value * eta.Value * cv.Value * rho_s.Value * A.Value * hce.Value) - 8 * fsValue.Sqr(A.Value) * cv.Value * rho_s.Value * Dp.Value * tf.Value * alpha.Value * fsValue.Sqr(ms.Value) * eta.Value)) / (-2 * ms.Value * alpha.Value * eta.Value * cv.Value * rho_s.Value * A.Value);
        }

        #endregion
    }
}