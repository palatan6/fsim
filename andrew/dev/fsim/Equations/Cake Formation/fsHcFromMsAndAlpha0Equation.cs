using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndAlpha0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alpha0;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;
        private readonly IEquationParameter nc;

        //hc = (2 * Sqr(A) * Pow(Dp / Dp0, -nc) * Dp * cv * rho_s * tf - alpha0 * Sqr(ms) * eta + 2 * alpha0 * ms * eta * cv * A * rho_s * hce - Sqrt(Sqr(2 * Sqr(A) * Pow(Dp / Dp0, -nc) * Dp * cv * rho_s * tf + alpha0 * eta * Sqr(ms) + 2 * alpha0 * ms * eta * cv * A * rho_s * hce) - 8 * Sqr(A) * Pow(Dp / Dp0, -nc) * Dp * cv * rho_s * tf * alpha0 * Sqr(ms) * eta)) / (-2 * eta * ms * alpha0 * cv * A * rho_s);

        #endregion

        public fsHcFromMsAndAlpha0Equation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha0,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceHce,
            IEquationParameter CakeCompressibility)
            : base(
                CakeHeight,
                SolidsMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakeResistanceAlpha0,
                Viscosity,
                FilterMediumResistanceHce,
                CakeCompressibility)
        {
            hc = CakeHeight;
            ms = SolidsMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alpha0 = CakeResistanceAlpha0;
            eta = Viscosity;
            hce = FilterMediumResistanceHce;
            nc = CakeCompressibility;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            double Dp0 = 1e5;
            hc.Value = (2 * fsValue.Sqr(A.Value) * fsValue.Pow(Dp.Value / Dp0, -nc.Value) * Dp.Value * cv.Value * rho_s.Value * tf.Value - alpha0.Value * fsValue.Sqr(ms.Value) * eta.Value + 2 * alpha0.Value * ms.Value * eta.Value * cv.Value * A.Value * rho_s.Value * hce.Value - fsValue.Sqrt(fsValue.Sqr(2 * fsValue.Sqr(A.Value) * fsValue.Pow(Dp.Value / Dp0, -nc.Value) * Dp.Value * cv.Value * rho_s.Value * tf.Value + alpha0.Value * eta.Value * fsValue.Sqr(ms.Value) + 2 * alpha0.Value * ms.Value * eta.Value * cv.Value * A.Value * rho_s.Value * hce.Value) - 8 * fsValue.Sqr(A.Value) * fsValue.Pow(Dp.Value / Dp0, -nc.Value) * Dp.Value * cv.Value * rho_s.Value * tf.Value * alpha0.Value * fsValue.Sqr(ms.Value) * eta.Value)) / (-2 * eta.Value * ms.Value * alpha0.Value * cv.Value * A.Value * rho_s.Value);
        }

        #endregion
    }
}