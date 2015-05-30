using Parameters;

namespace Equations
{
    public class fsVSusFromEpsCvVc : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_specSusVolume;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_vc;

        #endregion

        // vsus = (1- eps)* vc/cv

        public fsVSusFromEpsCvVc(
            IEquationParameter vsus,
            IEquationParameter eps,
            IEquationParameter cv,
            IEquationParameter vc)
            : base(
                vsus,
                eps,
                cv,
                vc)
        {
            m_specSusVolume = vsus;
            m_eps = eps;
            m_cv = cv;
            m_vc = vc;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_specSusVolume, SuspensionVolumeFormula);
            AddFormula(m_cv, CvFormula);
        }

        #region Formulas

        private void SuspensionVolumeFormula()
        {
            m_specSusVolume.Value = ((1 - m_eps.Value)*m_vc.Value)/m_cv.Value;
        }

        private void CvFormula()
        {
            m_cv.Value = ((1 - m_eps.Value) * m_vc.Value) / m_specSusVolume.Value;
        }

        #endregion
    }
}
