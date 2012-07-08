using Parameters;
using Value;

namespace Equations
{
    public class fsMassConcentrationEquation : fsCalculatorEquation
    {
        // Cm * rhoSus * (rhoF - rhoS) = rhoS * (rhoF - rhoSus)

        #region Parameters

        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_solidsMassFraction;
        private readonly IEquationParameter m_suspensionDensity;

        #endregion

        public fsMassConcentrationEquation(
            IEquationParameter solidsMassFraction,
            IEquationParameter filtrateDensity,
            IEquationParameter solidsDensity,
            IEquationParameter suspensionDensity)
            : base(
                solidsMassFraction,
                filtrateDensity,
                solidsDensity,
                suspensionDensity)
        {
            m_solidsMassFraction = solidsMassFraction;
            m_filtrateDensity = filtrateDensity;
            m_solidsDensity = solidsDensity;
            m_suspensionDensity = suspensionDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_solidsMassFraction, MassConcentrationFormula);
            AddFormula(m_filtrateDensity, FiltrateDensityFormula);
            AddFormula(m_solidsDensity, SolidsDensityFormula);
            AddFormula(m_suspensionDensity, SuspensionDensityFormula);
        }

        #region Formulas

        private void MassConcentrationFormula()
        {
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsMassFraction.Value = rhoS * (rhoF - rhoSus) / rhoSus / (rhoF - rhoS);
        }

        private void FiltrateDensityFormula()
        {
            fsValue cm = m_solidsMassFraction.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_filtrateDensity.Value = rhoSus * (1 - cm) / (1 - cm * rhoSus / rhoS);
        }

        private void SolidsDensityFormula()
        {
            fsValue cm = m_solidsMassFraction.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsDensity.Value = cm * rhoSus / (1 - rhoSus / rhoF * (1 - cm));
        }

        private void SuspensionDensityFormula()
        {
            fsValue cm = m_solidsMassFraction.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            m_suspensionDensity.Value = rhoF / (1 + cm * (rhoF / rhoS - 1));
        }

        #endregion
    }
}