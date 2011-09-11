﻿using Parameters;
using Value;

namespace Equations
{
    public class fsCakeHeightFromDpTf : fsCalculatorEquation
    {
        #region Parameters

        readonly IEquationParameter m_cakeHeight;
        readonly IEquationParameter m_hce;
        readonly IEquationParameter m_pc;
        readonly IEquationParameter m_kappa;
        readonly IEquationParameter m_pressure;
        readonly IEquationParameter m_formationTime;
        readonly IEquationParameter m_etaf;

        #endregion

        public fsCakeHeightFromDpTf(
            IEquationParameter cakeHeight,
            IEquationParameter hce,
            IEquationParameter pc,
            IEquationParameter kappa,
            IEquationParameter pressure,
            IEquationParameter formationTime,
            IEquationParameter etaf)
            : base(
                cakeHeight, 
                hce, 
                pc, 
                kappa, 
                pressure, 
                formationTime, 
                etaf)
        {
            m_cakeHeight = cakeHeight;
            m_hce = hce;
            m_pc = pc;
            m_kappa = kappa;
            m_pressure = pressure;
            m_formationTime = formationTime;
            m_etaf = etaf;

        }

        protected override void InitFormulas()
        {
            AddFormula(m_cakeHeight, CakeHeightFormula);
            AddFormula(m_formationTime, CakeFormationTime);
        }

        #region Formulas

        private void CakeHeightFormula()
        {
            m_cakeHeight.Value = fsValue.Sqrt(m_hce.Value * m_hce.Value + 2 * m_pc.Value * m_kappa.Value * m_pressure.Value * m_formationTime.Value / m_etaf.Value) - m_hce.Value;
        }

        private void CakeFormationTime()
        {
            m_formationTime.Value = m_etaf.Value
                * (m_cakeHeight.Value * m_cakeHeight.Value + 2 * m_cakeHeight.Value * m_hce.Value)
                / (2 * m_pc.Value * m_kappa.Value * m_pressure.Value);
        }

        #endregion
    }
}
