using Parameters;

namespace Equations
{
    public class fsEpsKappaMCvEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_kappaM;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_volumeConcentration;
        private readonly IEquationParameter m_solidsDensity;

        #endregion

        public fsEpsKappaMCvEquation(
            IEquationParameter porosity,
            IEquationParameter kappaM,
            IEquationParameter volumeConcentration,
            IEquationParameter solidsDensity)
            : base(
                porosity,
                kappaM,
                volumeConcentration,
                solidsDensity)
        {
            m_porosity = porosity;
            m_kappaM = kappaM;
            m_volumeConcentration = volumeConcentration;
            m_solidsDensity = solidsDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_porosity, PorosityFormula);
            AddFormula(m_kappaM, KappaFormula);
        }

        #region Formulas

        private void KappaFormula()
        {
            m_kappaM.Value = (m_volumeConcentration.Value / (1 - m_porosity.Value - m_volumeConcentration.Value)) * (1 - m_porosity.Value)* m_solidsDensity.Value;
        }

        private void PorosityFormula()
        {
            m_porosity.Value = 1 -
                               m_volumeConcentration.Value*(m_kappaM.Value)/
                               (m_kappaM.Value - m_volumeConcentration.Value*m_solidsDensity.Value);
        }

        #endregion
    }
}