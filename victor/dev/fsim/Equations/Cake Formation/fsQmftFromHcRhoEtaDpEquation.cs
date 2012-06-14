using Parameters;

namespace Equations
{
    public class fsQmftFromHcRhoEtaDpEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter qmft;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter hce0;
        
        #endregion

        //qmft = (rho * 2 * pc * Dp) / (eta * (hc + 2 * hce0))

        public fsQmftFromHcRhoEtaDpEquation(
            IEquationParameter Qmft,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter CakePermeability,
            IEquationParameter PressureDifference,
            IEquationParameter MotherLiquidViscosity,
            IEquationParameter CakeHeight,
            IEquationParameter FilterMediumResistanceHce0)
            : base(
                Qmft,
                MotherLiquidDensity,
                CakePermeability,    
                PressureDifference,
                MotherLiquidViscosity,
                CakeHeight,
                FilterMediumResistanceHce0)
        {
            qmft = Qmft;
            rho = MotherLiquidDensity;
            pc = CakePermeability;    
            Dp = PressureDifference;
            eta = MotherLiquidViscosity;
            hc = CakeHeight;
            hce0 = FilterMediumResistanceHce0;
        }

        protected override void InitFormulas()
        {
            AddFormula(qmft, qmftFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void qmftFormula()
        {
            qmft.Value = (rho.Value * 2 * pc.Value * Dp.Value) / (eta.Value * (hc.Value + 2 * hce0.Value));
        }

        private void hcFormula()
        {
            hc.Value = 2 * (((pc.Value * Dp.Value * rho.Value) / (eta.Value * qmft.Value)) - hce0.Value);
        }

        #endregion
    }
}
