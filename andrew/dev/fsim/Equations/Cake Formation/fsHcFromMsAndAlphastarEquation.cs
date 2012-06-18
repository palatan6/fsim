using Parameters;
using Value;

namespace Equations
{
    public class fsHcFromMsAndAlphastarEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alphastar;
        private readonly IEquationParameter eta;

        #endregion

        public fsHcFromMsAndAlphastarEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePorosityAlphaStar,
            IEquationParameter Viscosity)
            : base(
                CakeHeight,
                SolidsMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePorosityAlphaStar,
                Viscosity)
        {
            hc = CakeHeight;
            ms = SolidsMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alphastar = CakePorosityAlphaStar;
            eta = Viscosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = ms.Value / (A.Value * rho_s.Value * cv.Value)- ((2 * A.Value * Dp.Value * tf.Value) / (eta.Value * ms.Value * alphastar.Value));
        }

        #endregion
    }
}