using Parameters;
using Value;

namespace Equations
{
    public class fsConcentrationFromPhEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_concentration;
        private readonly IEquationParameter m_liquidDensity;
        private readonly IEquationParameter m_pH;

        #endregion

        public fsConcentrationFromPhEquation(
            IEquationParameter concentration,
            IEquationParameter pH,
            IEquationParameter liquidDensity)
            : base(concentration, pH, liquidDensity)
        {
            m_concentration = concentration;
            m_pH = pH;
            m_liquidDensity = liquidDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_concentration, ConcentrationFormula);
        }

        #region Formulas

        private void ConcentrationFormula()
        {
            m_concentration.Value = fsValue.Pow(new fsValue(10), -m_pH.Value) / m_liquidDensity.Value;
        }

        #endregion
    }
}