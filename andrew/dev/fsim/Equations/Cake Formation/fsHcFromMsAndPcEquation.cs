using Parameters;
using Value;
using System.Numeric;

namespace Equations
{
    public class fsHcFromMsAndPcEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter ms;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;

        //hc = (ms - .Sqrt(.Sqr(ms + 2 * cv * rho_s * A * hce) - 8 * .Sqr(cv * rho_s * A) * pc * Dp * tf / eta )) / (2* cv * rho_s * A) - hce;            

        #endregion

        public fsHcFromMsAndPcEquation(
            IEquationParameter CakeHeight,
            IEquationParameter SolidsMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePermeability,
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
                CakePermeability,
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
            pc = CakePermeability;
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
            hc.Value = (ms.Value - fsValue.Sqrt(fsValue.Sqr(ms.Value + 2 * cv.Value * rho_s.Value * A.Value * hce.Value) - 8 * fsValue.Sqr(cv.Value * rho_s.Value * A.Value) * pc.Value * Dp.Value * tf.Value / eta.Value)) / (2 * cv.Value * rho_s.Value * A.Value) - hce.Value;
        }

        #endregion
    }
}