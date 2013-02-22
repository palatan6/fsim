using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMcAndPcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;
        private readonly IEquationParameter rho;

        //hc = (-eta * (2 * A * hce * (rho + cv * (rho_s - rho)) - mc) + .Sqrt(.Sqr(2 * A * rho * hce + mc + 2 * A * hce * cv * (rho_s - rho)) * .Sqr(eta) - 8 * eta * .Sqr(A) * Dp * tf * pc * cv * (cv * .Sqr(rho_s - rho) + rho * (rho_s - rho)))) / (-2 * eta * A * (-rho - cv * rho_s + cv * rho));

        #endregion

        public fsHcFromMcAndPcEquation(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePermeability,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceHce,
            IEquationParameter MotherLiquidDensity)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePermeability,
                Viscosity,
                FilterMediumResistanceHce,
                MotherLiquidDensity)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            pc = CakePermeability;
            eta = Viscosity;
            hce = FilterMediumResistanceHce;
            rho = MotherLiquidDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = (-eta.Value * (2 * A.Value * hce.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value)) - mc.Value) + fsValue.Sqrt(fsValue.Sqr(2 * A.Value * rho.Value * hce.Value + mc.Value + 2 * A.Value * hce.Value * cv.Value * (rho_s.Value - rho.Value)) * fsValue.Sqr(eta.Value) - 8 * eta.Value * fsValue.Sqr(A.Value) * Dp.Value * tf.Value * pc.Value * cv.Value * (cv.Value * fsValue.Sqr(rho_s.Value - rho.Value) + rho.Value * (rho_s.Value - rho.Value)))) / (-2 * eta.Value * A.Value * (-rho.Value - cv.Value * rho_s.Value + cv.Value * rho.Value));
        }

        #endregion
    }
}