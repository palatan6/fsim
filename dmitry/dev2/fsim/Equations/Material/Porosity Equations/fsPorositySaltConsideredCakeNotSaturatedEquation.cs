using Parameters;
using Value;

namespace Equations
{
    public class fsPorositySaltConsideredCakeNotSaturatedEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_area;
        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_dryMass;
        private readonly IEquationParameter m_liquidDensity;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_solidsConcentration;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_wetMass;

        #endregion

        public fsPorositySaltConsideredCakeNotSaturatedEquation(
            IEquationParameter porosity,
            IEquationParameter dryMass,
            IEquationParameter wetMass,
            IEquationParameter solidsConcentration,
            IEquationParameter solidsDensity,
            IEquationParameter liquidDensity,
            IEquationParameter area,
            IEquationParameter cakeHeight)
            : base(porosity, dryMass, wetMass, solidsConcentration, solidsDensity, liquidDensity, area, cakeHeight)
        {
            m_porosity = porosity;
            m_dryMass = dryMass;
            m_wetMass = wetMass;
            m_solidsConcentration = solidsConcentration;
            m_solidsDensity = solidsDensity;
            m_liquidDensity = liquidDensity;
            m_area = area;
            m_cakeHeight = cakeHeight;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_porosity, PorosityFormula);
        }

        #region Formulas

        private void PorosityFormula()
        {
            fsValue nom = m_dryMass.Value -
                          (m_wetMass.Value - m_dryMass.Value) * m_solidsConcentration.Value / m_liquidDensity.Value;
            fsValue den = m_solidsDensity.Value * m_area.Value * m_cakeHeight.Value;
            m_porosity.Value = 1 - nom / den;
        }

        #endregion
    }
}