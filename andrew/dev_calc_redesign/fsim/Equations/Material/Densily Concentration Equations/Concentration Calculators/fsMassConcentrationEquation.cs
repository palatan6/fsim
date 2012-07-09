using Parameters;
using Value;

namespace Equations
{
    public class fsMassConcentrationEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_solidsMassFraction;
        private readonly IEquationParameter m_suspensionDensity;

        #endregion

        // Cm * rhoSus * (rhoF - rhoS) = rhoS * (rhoF - rhoSus)

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
            m_filtrateDensity.Value = rhoS * rhoSus * (1 - cm) / (rhoS - cm * rhoSus);
        }

        private void SolidsDensityFormula()
        {
            fsValue cm = m_solidsMassFraction.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsDensity.Value = cm * rhoF * rhoSus / (rhoF - rhoSus * (1 - cm));
        }

        private void SuspensionDensityFormula()
        {
            fsValue cm = m_solidsMassFraction.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            m_suspensionDensity.Value = rhoS * rhoF / (rhoS * (1 - cm) + cm * rhoF);
        }

        #endregion
    }
}