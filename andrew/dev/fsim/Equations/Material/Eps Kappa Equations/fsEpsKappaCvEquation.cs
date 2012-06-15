using Parameters;

namespace Equations
{
    public class fsEpsKappaCvEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_volumeConcentration;

        #endregion

        public fsEpsKappaCvEquation(
            IEquationParameter porosity,
            IEquationParameter kappa,
            IEquationParameter volumeConcentration)
            : base(
                porosity,
                kappa,
                volumeConcentration)
        {
            m_porosity = porosity;
            m_kappa = kappa;
            m_volumeConcentration = volumeConcentration;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_porosity, PorosityFormula);
            AddFormula(m_kappa, KappaFormula);
        }

        #region Formulas

        private void KappaFormula()
        {
            m_kappa.Value = m_volumeConcentration.Value / (1 - m_porosity.Value - m_volumeConcentration.Value);
        }

        private void PorosityFormula()
        {
            m_porosity.Value = 1 - m_volumeConcentration.Value * (m_kappa.Value + 1) / m_kappa.Value;
        }

        #endregion
    }
}