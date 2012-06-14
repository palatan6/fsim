using Parameters;

namespace Equations
{
    public class fsSuspensionMassFromHcEpsPlainAreaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_filterArea;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_solidsMassFraction;
        private readonly IEquationParameter m_suspensionMass;

        #endregion

        // Msus = (1 - eps) * rho_s * A * hc / Cm;

        public fsSuspensionMassFromHcEpsPlainAreaEquation(
            IEquationParameter suspensionMass,
            IEquationParameter porosity,
            IEquationParameter solidsDensity,
            IEquationParameter filterArea,
            IEquationParameter cakeHeight,
            IEquationParameter solidsMassFraction)
            : base(
                suspensionMass,
                porosity,
                solidsDensity,
                filterArea,
                cakeHeight,
                solidsMassFraction)
        {
            m_suspensionMass = suspensionMass;
            m_porosity = porosity;
            m_solidsDensity = solidsDensity;
            m_filterArea = filterArea;
            m_cakeHeight = cakeHeight;
            m_solidsMassFraction = solidsMassFraction;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_suspensionMass, SuspensionMassFormula);
            AddFormula(m_cakeHeight, CakeHeightFormula);
            AddFormula(m_filterArea, FilterAreaFormula);
            AddFormula(m_solidsDensity, SolidsDensityFormula);
            AddFormula(m_porosity, PorosityFormula);
            AddFormula(m_solidsMassFraction, SolidsMassFractionFormula);
        }

        #region Formulas

        private void SuspensionMassFormula()
        {
            m_suspensionMass.Value = (1 - m_porosity.Value) * m_solidsDensity.Value * m_filterArea.Value *
                                     m_cakeHeight.Value / m_solidsMassFraction.Value;
        }

        private void CakeHeightFormula()
        {
            m_cakeHeight.Value = m_suspensionMass.Value * m_solidsMassFraction.Value /
                                 ((1 - m_porosity.Value) * m_solidsDensity.Value *
                                  m_filterArea.Value);
        }

        private void FilterAreaFormula()
        {
            m_filterArea.Value = m_suspensionMass.Value * m_solidsMassFraction.Value /
                                 ((1 - m_porosity.Value) * m_solidsDensity.Value * m_cakeHeight.Value);
        }

        private void SolidsDensityFormula()
        {
            m_solidsDensity.Value = m_suspensionMass.Value * m_solidsMassFraction.Value /
                                    ((1 - m_porosity.Value) * m_filterArea.Value * m_cakeHeight.Value);
        }

        private void PorosityFormula()
        {
            m_porosity.Value = 1 - m_suspensionMass.Value * m_solidsMassFraction.Value
                               / (m_solidsDensity.Value * m_filterArea.Value * m_cakeHeight.Value);
        }

        private void SolidsMassFractionFormula()
        {
            m_solidsMassFraction.Value = (1 - m_porosity.Value) * m_solidsDensity.Value *
                                         m_filterArea.Value * m_cakeHeight.Value / m_suspensionMass.Value;
        }

        #endregion
    }
}