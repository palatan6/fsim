using Parameters;
using Value;

namespace Equations
{
    public class fsHcFromMcAndPcstarEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter pcstar;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter eta;

        //hc = (mc  / (2 * A * (rho  + cv  * (rho_s  - rho )))) + Sqrt(fsValue.Sqr(mc  / (2 * A  * (rho  + cv  * (rho_s  - rho )))) - ((2 * (rho_s  - rho ) * cv  * Dp  * tf  * pcstar ) / (eta  * (rho  + cv  * (rho_s  - rho )))))

        #endregion

        public fsHcFromMcAndPcstarEquation(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakePorosityStar,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter Viscosity)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakePorosityStar,
                MotherLiquidDensity,
                Viscosity)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            pcstar = CakePorosityStar;
            rho = MotherLiquidDensity;
            eta = Viscosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = (mc.Value / (2 * A.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value)))) + fsValue.Sqrt(fsValue.Sqr(mc.Value / (2 * A.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value)))) - ((2 * (rho_s.Value - rho.Value) * cv.Value * Dp.Value * tf.Value * pcstar.Value) / (eta.Value * (rho.Value + cv.Value * (rho_s.Value - rho.Value)))));
        }

        #endregion
    }
}
