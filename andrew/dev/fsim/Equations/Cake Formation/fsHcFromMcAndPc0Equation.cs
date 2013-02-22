using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMcAndPc0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pc0;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter nc;
        double Dp0 = 1e5;

        //hc = (-eta * (2 * A * rho * hce - mc + cv * A * hce * (rho_s - rho)) + Sqrt(Sqr(2 * eta * A * rho * hce + eta * mc + 2 * eta * cv * A * hce * (rho_s - rho)) - 8 * eta * A * pc0 * Pow(Dp / Dp0, -nc) * rho * Dp * cv * tf * (rho_s - rho + cv * Sqr(rho_s - rho)))) / (2 * eta * A * (rho + cv * (rho_s * -rho)));

        #endregion

        public fsHcFromMcAndPc0Equation(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePermeability0,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceHce,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter CakeCompressibility)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePermeability0,
                Viscosity,
                FilterMediumResistanceHce,
                MotherLiquidDensity,
                CakeCompressibility)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            pc0 = CakePermeability0;
            eta = Viscosity;
            hce = FilterMediumResistanceHce;
            rho = MotherLiquidDensity;
            nc = CakeCompressibility;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = (-eta.Value * (2 * A.Value * rho.Value * hce.Value - mc.Value + cv.Value * A.Value * hce.Value * (rho_s.Value - rho.Value)) + fsValue.Sqrt(fsValue.Sqr(2 * eta.Value * A.Value * rho.Value * hce.Value + eta.Value * mc.Value + 2 * eta.Value * cv.Value * A.Value * hce.Value * (rho_s.Value - rho.Value)) - 8 * eta.Value * A.Value * pc0.Value * fsValue.Pow(Dp.Value / Dp0, -nc.Value) * rho.Value * Dp.Value * cv.Value * tf.Value * (rho_s.Value - rho.Value + cv.Value * fsValue.Sqr(rho_s.Value - rho.Value)))) / (2 * eta.Value * A.Value * (rho.Value + cv.Value * (rho_s.Value * -rho.Value)));
        }

        #endregion
    }
}