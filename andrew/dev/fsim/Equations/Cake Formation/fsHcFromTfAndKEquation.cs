using Parameters;
using Value;

namespace Equations
{
    public class fsHcFromTfAndKEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter kappa;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter K;
        
        #endregion

        public fsHcFromTfAndKEquation(
            IEquationParameter CakeHeight,
            IEquationParameter Kappa,
            IEquationParameter PressureDifference,
            IEquationParameter FiltrtionTime,
            IEquationParameter PracticalCakePermeability)
            : base(
                CakeHeight,
                Kappa,
                PressureDifference,
                FiltrtionTime,
                PracticalCakePermeability)
        {
            hc = CakeHeight;
            kappa = Kappa;
            Dp = PressureDifference;
            tf = FiltrtionTime;
            K = PracticalCakePermeability;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);            
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            hc.Value = fsValue.Sqrt(2 * kappa.Value * Dp.Value * tf.Value * K.Value);
        }

        #endregion
    }
}