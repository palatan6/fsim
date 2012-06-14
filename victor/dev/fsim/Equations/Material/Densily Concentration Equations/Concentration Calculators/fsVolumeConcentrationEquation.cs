using Parameters;
using Value;

namespace Equations
{
    public class fsVolumeConcentrationEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_solidsVolumeFractionConcentration;
        private readonly IEquationParameter m_suspensionDensity;

        #endregion

        public fsVolumeConcentrationEquation(
            IEquationParameter solidsVolumeFractionConcentration,
            IEquationParameter filtrateDensity,
            IEquationParameter solidsDensity,
            IEquationParameter suspensionDensity)
            : base(
                solidsVolumeFractionConcentration,
                filtrateDensity,
                solidsDensity,
                suspensionDensity)
        {
            m_solidsVolumeFractionConcentration = solidsVolumeFractionConcentration;
            m_filtrateDensity = filtrateDensity;
            m_solidsDensity = solidsDensity;
            m_suspensionDensity = suspensionDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_solidsVolumeFractionConcentration, SolidsVolumeFractionFormula);
            AddFormula(m_filtrateDensity, FiltrateDensityFormula);
            AddFormula(m_solidsDensity, SolidsDensityFormula);
            AddFormula(m_suspensionDensity, SuspensionDensityFormula);
        }

        #region Formulas

        private void SolidsVolumeFractionFormula()
        {
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsVolumeFractionConcentration.Value = (rhoF - rhoSus) / (rhoF - rhoS);
        }

        private void FiltrateDensityFormula()
        {
            fsValue cv = m_solidsVolumeFractionConcentration.Value;
            fsValue rhoS = m_solidsDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_filtrateDensity.Value = (rhoSus - cv * rhoS) / (1 - cv);
        }

        private void SolidsDensityFormula()
        {
            fsValue cv = m_solidsVolumeFractionConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoSus = m_suspensionDensity.Value;
            m_solidsDensity.Value = (rhoSus - (1 - cv) * rhoF) / cv;
        }

        private void SuspensionDensityFormula()
        {
            fsValue cv = m_solidsVolumeFractionConcentration.Value;
            fsValue rhoF = m_filtrateDensity.Value;
            fsValue rhoS = m_solidsDensity.Value;
            m_suspensionDensity.Value = rhoF * (1 - cv) + cv * rhoS;
        }

        #endregion
    }
}