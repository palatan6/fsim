using Parameters;
using Value;

namespace Equations
{
    public class fsPcFromHcAndEtaAndDpAndTfAndKEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_pc;
        private readonly IEquationParameter m_cakeHeight;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_etaf;
        private readonly IEquationParameter m_formationTime;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_pressure;

        #endregion

        public fsPcFromHcAndEtaAndDpAndTfAndKEquation(
            IEquationParameter pc,
            IEquationParameter cakeHeight,
            IEquationParameter hce,
            IEquationParameter kappa,
            IEquationParameter pressure,
            IEquationParameter formationTime,
            IEquationParameter etaf)
            : base(
                pc,
                cakeHeight,
                hce,
                kappa,
                pressure,
                formationTime,
                etaf)
        {
            m_pc = pc;
            m_cakeHeight = cakeHeight;
            m_hce = hce;
            m_kappa = kappa;
            m_pressure = pressure;
            m_formationTime = formationTime;
            m_etaf = etaf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_pc, PcFormula);
            AddFormula(m_formationTime, TfFormula);
        }

        #region Formulas

        private void PcFormula()
        {
            m_pc.Value = m_etaf.Value * (fsValue.Sqr(m_cakeHeight.Value)+2 * m_cakeHeight.Value* m_hce.Value) / (2 * m_pressure.Value * m_kappa.Value * m_formationTime.Value);
        }

        private void TfFormula()
        {
            m_formationTime.Value = m_etaf.Value * (fsValue.Sqr(m_cakeHeight.Value) + 2 * m_cakeHeight.Value * m_hce.Value) / (2 * m_pressure.Value * m_kappa.Value * m_pc.Value);
        }
        #endregion
    }
}