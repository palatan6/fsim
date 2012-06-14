using Parameters;

namespace Equations
{
    public class fsSuspensionSolidsMassFractionEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_dryMass;
        private readonly IEquationParameter m_saltMassFractionInTheMotherLiquid;
        private readonly IEquationParameter m_suspensionMass;
        private readonly IEquationParameter m_suspensionSolidsMassFraction;

        #endregion

        public fsSuspensionSolidsMassFractionEquation(
            IEquationParameter suspensionSolidsMassFraction,
            IEquationParameter dryMass,
            IEquationParameter suspensionMass,
            IEquationParameter saltMassFractionInTheMotherLiquid)
            : base(suspensionSolidsMassFraction, dryMass, suspensionMass, saltMassFractionInTheMotherLiquid)
        {
            m_suspensionSolidsMassFraction = suspensionSolidsMassFraction;
            m_dryMass = dryMass;
            m_suspensionMass = suspensionMass;
            m_saltMassFractionInTheMotherLiquid = saltMassFractionInTheMotherLiquid;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_suspensionSolidsMassFraction, SuspensionSolidsMassFractionFormula);
        }

        #region Formulas

        private void SuspensionSolidsMassFractionFormula()
        {
            m_suspensionSolidsMassFraction.Value = m_dryMass.Value / m_suspensionMass.Value -
                                                   m_saltMassFractionInTheMotherLiquid.Value *
                                                   (1 - m_dryMass.Value / m_suspensionMass.Value);
        }

        #endregion
    }
}