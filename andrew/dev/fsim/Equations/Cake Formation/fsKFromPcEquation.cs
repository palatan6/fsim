using Parameters;

namespace Equations
{
    public class fsKFromPcEquation: fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter K;
        private readonly IEquationParameter hc;
        private readonly IEquationParameter pc;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce0;

        #endregion

        //K = (hc*pc)/(eta*(hc+ hce0))

        public fsKFromPcEquation(
            IEquationParameter PracticalCakePermeability,
            IEquationParameter CakeHeight,
            IEquationParameter CakePermeability,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter FilterMediumResistanceHce0)
            : base(
                PracticalCakePermeability,
                CakeHeight,
                CakePermeability,
                MotherLiquidDensity,
                FilterMediumResistanceHce0)
        {
            K = PracticalCakePermeability;
            hc = CakeHeight;
            pc = CakePermeability;
            eta = MotherLiquidDensity;
            hce0 = FilterMediumResistanceHce0;
        }

        protected override void InitFormulas()
        {
            AddFormula(K, KFormula);
            AddFormula(pc, PcFormula);
        }

        private void KFormula()
        {
            K.Value = (hc.Value * pc.Value) / (eta.Value * (hc.Value + hce0.Value));
        }

        private void PcFormula()
        {
            pc.Value = (K.Value * eta.Value * (hc.Value + hce0.Value)) / hc.Value;
        }

    }
}
