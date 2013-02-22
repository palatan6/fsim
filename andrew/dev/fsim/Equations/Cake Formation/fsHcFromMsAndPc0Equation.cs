using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndPc0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pc0;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;
        private readonly IEquationParameter nc;

        //hc = (-eta * ms + 2 * eta * cv * A * rho_s * hce + Sqrt(Sqr(eta * ms + 2 * eta * cv * A * rho_s * hce) - 8 * eta * Sqr(cv * A * rho_s) * pc0 * Pow(Dp / Dp0, -nc) * Dp * tf)) / (-2 * eta * cv * A * rho_s);

        #endregion

        public fsHcFromMsAndPc0Equation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePermeability0,
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
                CakePermeability0,
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
            pc0 = CakePermeability0;
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
            hc.Value = (-eta.Value * ms.Value + 2 * eta.Value * cv.Value * A.Value * rho_s.Value * hce.Value + fsValue.Sqrt(fsValue.Sqr(eta.Value * ms.Value + 2 * eta.Value * cv.Value * A.Value * rho_s.Value * hce.Value) - 8 * eta.Value * fsValue.Sqr(cv.Value * A.Value * rho_s.Value) * pc0.Value * fsValue.Pow(Dp.Value / Dp0, -nc.Value) * Dp.Value * tf.Value)) / (-2 * eta.Value * cv.Value * A.Value * rho_s.Value);
        }

        #endregion
    }
}
