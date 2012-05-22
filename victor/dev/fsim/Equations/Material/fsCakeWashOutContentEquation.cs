using Parameters;

namespace Equations
{
    public class fsCakeWashOutContentEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeWashOut;
        private readonly IEquationParameter m_concentration;
        private readonly IEquationParameter m_dryMass;
        private readonly IEquationParameter m_liquidMass;
        private readonly IEquationParameter m_wetMass;

        #endregion

        public fsCakeWashOutContentEquation(
            IEquationParameter cakeWashOut,
            IEquationParameter dryMass,
            IEquationParameter wetMass,
            IEquationParameter liquidMass,
            IEquationParameter concentration)
            : base(cakeWashOut, dryMass, wetMass, liquidMass, concentration)
        {
            m_cakeWashOut = cakeWashOut;
            m_dryMass = dryMass;
            m_wetMass = wetMass;
            m_liquidMass = liquidMass;
            m_concentration = concentration;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_cakeWashOut, CakeWashOutContentFormula);
        }

        #region Formulas

        private void CakeWashOutContentFormula()
        {
            m_cakeWashOut.Value = (m_wetMass.Value / m_dryMass.Value - 1) *
                                  (1 + m_liquidMass.Value / (m_wetMass.Value - m_dryMass.Value)) * m_concentration.Value;
        }

        #endregion
    }
}