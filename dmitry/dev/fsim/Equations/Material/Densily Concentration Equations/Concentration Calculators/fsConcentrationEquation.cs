using Parameters;
using Value;

namespace Equations
{
    public class fsConcentrationEquation : fsCalculatorEquation
    {
        //  rhoSus = rhoF + c * (1 - rhoF / rhoS)

        #region Parameters

        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsConcentration;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_suspensionDensity;

        #endregion

        public fsConcentrationEquation(
            IEquationParameter solidsConcentration,
            IEquationParameter filtrateDensity,
            IEquationParameter solidsDensity,
            IEquationParameter suspensionDensity)
            : base(
                solidsConcentration,
                filtrateDensity,
                solidsDensity,
                suspensionDensity)
        {
            m_solidsConcentration = solidsConcentration;
            m_filtrateDensity = filtrateDensity;
            m_solidsDensity = solidsDensity;
            m_suspensionDensity = suspensionDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_solidsConcentration, ConcentrationFormula);
            AddFormula(m_filtrateDensity, FiltrateDensityFormula);
            AddFormula(m_solidsDensity, SolidsDensityFormula);
            AddFormula(m_suspensionDensity, SuspensionDensityFormula);
        }

        #region Formulas

        private void ConcentrationFormula()
        {
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsConcentration.Value = (rhoF - rhoSus) / (rhoF / rhoS - 1);
        }

        private void FiltrateDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_filtrateDensity.Value = (c - rhoSus) / (c / rhoS - 1);
        }

        private void SolidsDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsDensity.Value = c / (1 + (c - rhoSus) / rhoF);
        }

        private void SuspensionDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            m_suspensionDensity.Value = rhoF + c * (1 - rhoF / rhoS);
        }

        #endregion
    }
}