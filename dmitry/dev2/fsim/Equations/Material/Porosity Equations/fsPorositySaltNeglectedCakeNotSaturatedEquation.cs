using Parameters;

namespace Equations
{
    public class fsPorositySaltNeglectedCakeNotSaturatedEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_area;
        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_dryMass;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_solidsDensity;

        #endregion

        public fsPorositySaltNeglectedCakeNotSaturatedEquation(
            IEquationParameter porosity,
            IEquationParameter dryMass,
            IEquationParameter solidsDensity,
            IEquationParameter area,
            IEquationParameter cakeHeight)
            : base(porosity, dryMass, solidsDensity, area, cakeHeight)
        {
            m_porosity = porosity;
            m_dryMass = dryMass;
            m_solidsDensity = solidsDensity;
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
            m_porosity.Value = 1 - m_dryMass.Value / (m_solidsDensity.Value * m_area.Value * m_cakeHeight.Value);
        }

        #endregion
    }
}