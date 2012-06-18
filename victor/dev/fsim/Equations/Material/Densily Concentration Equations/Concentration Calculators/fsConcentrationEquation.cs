using Parameters;
using Value;

namespace Equations
{
    public class fsConcentrationEquation : fsCalculatorEquation
    {
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
            m_solidsConcentration.Value = rhoS * (rhoF - rhoSus) / (rhoF - rhoS);
        }

        private void FiltrateDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_filtrateDensity.Value = rhoS * (c - rhoSus) / (c - rhoS);
        }

        private void SolidsDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsDensity.Value = c * rhoF / (c + rhoF - rhoSus);
        }

        private void SuspensionDensityFormula()
        {
            fsValue c = m_solidsConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            m_suspensionDensity.Value = (rhoS * rhoF + c * rhoS - c * rhoF) / rhoS;
        }

        #endregion
    }
}