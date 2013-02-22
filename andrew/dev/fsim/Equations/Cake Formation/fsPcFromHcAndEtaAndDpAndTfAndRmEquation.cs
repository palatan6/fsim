using Parameters;
using Value;

namespace Equations
{
    public class fsPcFromHcAndEtaAndDpAndTfAndRmEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_etaf;
        private readonly IEquationParameter m_formationTime;
        private readonly IEquationParameter m_Rm;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_pressure;

        #endregion

        public fsPcFromHcAndEtaAndDpAndTfAndRmEquation(
            IEquationParameter cakeHeight,
            IEquationParameter Rm,
            IEquationParameter pc,
            IEquationParameter kappa,
            IEquationParameter pressure,
            IEquationParameter formationTime,
            IEquationParameter etaf)
            : base(
                cakeHeight,
                Rm,
                pc,
                kappa,
                pressure,
                formationTime,
                etaf)
        {
            m_cakeHeight = cakeHeight;
            m_Rm = Rm;
            m_pc = pc;
            m_kappa = kappa;
            m_pressure = pressure;
            m_formationTime = formationTime;
            m_etaf = etaf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_pc, PcFormula);
        }

        #region Formulas

        private void PcFormula()
        {
            m_pc.Value = m_etaf.Value * fsValue.Sqr(m_cakeHeight.Value) / (2 * (m_pressure.Value * m_kappa.Value * m_formationTime.Value - m_etaf.Value * m_cakeHeight.Value * m_Rm.Value));
        }
        #endregion
    }
}