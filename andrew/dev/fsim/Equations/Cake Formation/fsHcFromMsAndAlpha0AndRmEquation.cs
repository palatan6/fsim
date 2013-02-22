using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndAlpha0AndRmEquation : fsCalculatorEquation
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
        private readonly IEquationParameter Rm;
        private readonly IEquationParameter nc;

        //hc  = -(-Pow((Dp  / Dp0), nc ) * eta  * alpha0  *  Sqr(ms ) - 2 * ms  * eta  * Rm  * A  + 2 *  Sqr(A ) * Dp  * cv  * rho_s  * tf ) / (eta  * cv  * A  * rho_s  * (alpha0  * ms  *  Pow((Dp  / Dp0), nc ) + 2 * Rm  * A ));

        #endregion

        public fsHcFromMsAndAlpha0AndRmEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha0,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceRm,
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
                FilterMediumResistanceRm,
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
            Rm = FilterMediumResistanceRm;
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
            hc.Value = -(-fsValue.Pow((Dp.Value / Dp0), nc.Value) * eta.Value * alpha0.Value * fsValue.Sqr(ms.Value) - 2 * ms.Value * eta.Value * Rm.Value * A.Value + 2 * fsValue.Sqr(A.Value) * Dp.Value * cv.Value * rho_s.Value * tf.Value) / (eta.Value * cv.Value * A.Value * rho_s.Value * (alpha0.Value * ms.Value * fsValue.Pow((Dp.Value / Dp0), nc.Value) + 2 * Rm.Value * A.Value));
        }

        #endregion
    }
}
