using Parameters;

namespace Equations
{
    public class fsKfromPcAndRmEquation: fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter K;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter Rm;

        #endregion

        //K = (hc*pc)/(eta*(hc+ 2*Rm*pc))

        public fsKfromPcAndRmEquation(
            IEquationParameter PracticalCakePermeability,
            IEquationParameter CakeHeight,
            IEquationParameter CakePermeability,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter FilterMediumResistanceRm)
            : base(
                PracticalCakePermeability,
                CakeHeight,
                CakePermeability,
                MotherLiquidDensity,
                FilterMediumResistanceRm)
        {
            K = PracticalCakePermeability;
            hc = CakeHeight;
            pc = CakePermeability;
            eta = MotherLiquidDensity;
            Rm = FilterMediumResistanceRm;
        }

        protected override void InitFormulas()
        {
            AddFormula(K, KFormula);
            AddFormula(pc, PcFormula);
        }

        private void KFormula()
        {
            K.Value = (hc.Value * pc.Value) / (eta.Value * (hc.Value + 2 * Rm.Value * pc.Value));
        }

        private void PcFormula()
        {
            pc.Value = (K.Value * eta.Value * hc.Value) /(hc.Value - 2 * eta.Value * Rm.Value * K.Value);
        }

    }
}
