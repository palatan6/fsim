using Parameters;

namespace Equations
{
    public class fsvcFromMsusAndOthers : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_specCakeVolume;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_A;
        private readonly IEquationParameter m_rho_sus;
        private readonly IEquationParameter m_M_sus;

        #endregion

        // vc = cv*Msus / ((1-eps) *A* rho_sus)

        public fsvcFromMsusAndOthers(
            IEquationParameter vc,
            IEquationParameter eps,
            IEquationParameter cv,
            IEquationParameter A,
            IEquationParameter rho_sus,
            IEquationParameter M_sus)
            : base(
                vc,
                eps,
                cv,
                A,
                rho_sus,
                M_sus)
        {
            m_specCakeVolume = vc;
            m_eps = eps;
            m_cv = cv;
            m_A = A;
            m_rho_sus = rho_sus;
            m_M_sus = M_sus;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_specCakeVolume, SuspensionVolumeFormula);
            AddFormula(m_M_sus, MsusFormula);
        }

        #region Formulas

        private void SuspensionVolumeFormula()
        {
            m_specCakeVolume.Value = (m_cv.Value*m_M_sus.Value)/((1 - m_eps.Value)*m_A.Value*m_rho_sus.Value);
        }

        private void MsusFormula()
        {
            m_M_sus.Value = ((1 - m_eps.Value) * m_A.Value * m_rho_sus.Value * m_specCakeVolume.Value) / m_cv.Value;
        }

        #endregion
    }
}
