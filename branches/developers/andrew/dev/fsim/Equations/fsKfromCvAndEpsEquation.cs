using Parameters;

namespace Equations
{
    public class fsKfromCvAndEpsEquation: fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter K;
        private readonly IEquationParameter Cv;
        private readonly IEquationParameter eps;

        #endregion

        //K = (hc*pc)/(eta*(hc+ 2*Rm*pc))

        public fsKfromCvAndEpsEquation(
            IEquationParameter PracticalCakePermeability,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter CakePorosity)
            : base(
                PracticalCakePermeability,
                SuspensionSolidsVolumeFraction,
                CakePorosity)
        {
            K = PracticalCakePermeability;
            Cv = SuspensionSolidsVolumeFraction;
            eps = CakePorosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(K, KFormula);
        }

        private void KFormula()
        {
            K.Value = Cv.Value/(1 - eps.Value - Cv.Value);
        }
    }
}

