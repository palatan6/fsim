using Parameters;

namespace Equations
{
    public class fsQftFromHcEtaDpEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter qft;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter hce0;
        
        #endregion

        //qft = (2 * pc * Dp) / (eta * (hc + 2 * hce0))

        public fsQftFromHcEtaDpEquation(
            IEquationParameter Qft,
            IEquationParameter CakePermeability,
            IEquationParameter PressureDifference,
            IEquationParameter MotherLiquidViscosity,
            IEquationParameter CakeHeight,
            IEquationParameter FilterMediumResistanceHce0)
            : base(
                Qft,
                CakePermeability,    
                PressureDifference,
                MotherLiquidViscosity,
                CakeHeight,
                FilterMediumResistanceHce0)
        {
            qft = Qft;
            pc = CakePermeability;    
            Dp = PressureDifference;
            eta = MotherLiquidViscosity;
            hc = CakeHeight;
            hce0 = FilterMediumResistanceHce0;
        }

        protected override void InitFormulas()
        {
            AddFormula(qft, qftFormula);
            AddFormula(hc, hcFormula);
        }

        #region Formulas

        private void qftFormula()
        {
            qft.Value = (2 * pc.Value * Dp.Value) / (eta.Value * (hc.Value + 2 * hce0.Value));
        }

        private void hcFormula()
        {
            hc.Value = 2 * (((pc.Value * Dp.Value) / (eta.Value * qft.Value)) - hce0.Value);
        }

        #endregion
    }
}
    