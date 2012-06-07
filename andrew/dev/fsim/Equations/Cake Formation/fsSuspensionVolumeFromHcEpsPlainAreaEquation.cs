using Parameters;

namespace Equations
{
    public class fsSuspensionVolumeFromHcEpsPlainAreaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_filterArea;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_solidsVolumeFraction;
        private readonly IEquationParameter m_suspensionVolume;

        #endregion

        // Vsus = (1 - eps) * A * hc / Cv;

        public fsSuspensionVolumeFromHcEpsPlainAreaEquation(
            IEquationParameter suspensionVolume,
            IEquationParameter porosity,
            IEquationParameter filterArea,
            IEquationParameter cakeHeight,
            IEquationParameter solidsVolumeFraction)
            : base(
                suspensionVolume,
                porosity,
                filterArea,
                cakeHeight,
                solidsVolumeFraction)
        {
            m_suspensionVolume = suspensionVolume;
            m_porosity = porosity;
            m_filterArea = filterArea;
            m_cakeHeight = cakeHeight;
            m_solidsVolumeFraction = solidsVolumeFraction;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_suspensionVolume, SuspensionVolumeFormula);
            AddFormula(m_cakeHeight, CakeHeightFormula);
            AddFormula(m_filterArea, FilterAreaFormula);
            AddFormula(m_porosity, PorosityFormula);
            AddFormula(m_solidsVolumeFraction, SolidsVolumeFractionFormula);
        }

        #region Formulas

        private void SuspensionVolumeFormula()
        {
            m_suspensionVolume.Value = (1 - m_porosity.Value) * m_filterArea.Value * m_cakeHeight.Value /
                                       m_solidsVolumeFraction.Value;
        }

        private void CakeHeightFormula()
        {
            m_cakeHeight.Value = m_suspensionVolume.Value * m_solidsVolumeFraction.Value /
                                 ((1 - m_porosity.Value) * m_filterArea.Value);
        }

        private void FilterAreaFormula()
        {
            m_filterArea.Value = m_suspensionVolume.Value * m_solidsVolumeFraction.Value /
                                 ((1 - m_porosity.Value) * m_cakeHeight.Value);
        }

        private void PorosityFormula()
        {
            m_porosity.Value = 1 - m_suspensionVolume.Value * m_solidsVolumeFraction.Value
                               / (m_filterArea.Value * m_cakeHeight.Value);
        }

        private void SolidsVolumeFractionFormula()
        {
            m_solidsVolumeFraction.Value = (1 - m_porosity.Value) * m_filterArea.Value * m_cakeHeight.Value /
                                           m_suspensionVolume.Value;
        }

        #endregion
    }
}