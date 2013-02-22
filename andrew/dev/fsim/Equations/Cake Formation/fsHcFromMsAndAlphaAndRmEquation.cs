using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndAlphaAndRmEquation : fsCalculatorEquation
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
        private readonly IEquationParameter Rm;

        //hc  = -(-eta  * alpha  *  Sqr(ms ) - 2 * ms  * eta  * Rm  * A  + 2 *  Sqr(A ) * Dp  * cv  * rho_s  * tf ) / (eta  * cv  * A  * rho_s  * (alpha  * ms  + 2 * Rm  * A ));

        #endregion

        public fsHcFromMsAndAlphaAndRmEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceRm)
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
                FilterMediumResistanceRm)
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
            Rm = FilterMediumResistanceRm;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = -(-eta.Value * alpha.Value * fsValue.Sqr(ms.Value) - 2 * ms.Value * eta.Value * Rm.Value * A.Value + 2 * fsValue.Sqr(A.Value) * Dp.Value * cv.Value * rho_s.Value * tf.Value) / (eta.Value * cv.Value * A.Value * rho_s.Value * (alpha.Value * ms.Value + 2 * Rm.Value * A.Value));
        }

        #endregion
    }
}
