using Parameters;
using Value;

namespace Equations
{
    public class fsHcFromMcAndAlphastarEquations : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alphastar;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter rho;

        #endregion

        public fsHcFromMcAndAlphastarEquations(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePorosityAlphaStar,
            IEquationParameter Viscosity,
            IEquationParameter MotherLiquidDensity)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePorosityAlphaStar,
                Viscosity,
                MotherLiquidDensity)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alphastar = CakePorosityAlphaStar;
            eta = Viscosity;
            rho = MotherLiquidDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = mc.Value * (2 * rho.Value + cv.Value * (rho_s.Value - rho.Value)) / (2 * rho.Value * A.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value))) - fsValue.Sqrt(fsValue.Sqr(mc.Value * (2 * rho.Value + cv.Value * (rho_s.Value - rho.Value)) / (2 * rho.Value * A.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value)))) - (eta.Value * rho_s.Value * fsValue.Sqr(mc.Value) * alphastar.Value - 2 * fsValue.Sqr(rho_s.Value - rho.Value) * cv.Value * fsValue.Sqr(A.Value) * Dp.Value * tf.Value) / (eta.Value * rho.Value * fsValue.Sqr(A.Value) * (rho.Value + cv.Value * (rho_s.Value - rho.Value)) * rho_s.Value * alphastar.Value));
        }

        #endregion
    }
}