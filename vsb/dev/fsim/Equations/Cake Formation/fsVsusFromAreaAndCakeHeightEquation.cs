﻿using Parameters;

namespace Equations
{
    public class fsVsusFromAreaAndCakeHeightEquation : fsCalculatorEquation
    {
        #region Parameters

        readonly IEquationParameter m_suspensionVolume;
        readonly IEquationParameter m_area;
        readonly IEquationParameter m_cakeHeight;
        readonly IEquationParameter m_kappa;

        #endregion

        public fsVsusFromAreaAndCakeHeightEquation(
            IEquationParameter suspensionVolume,
            IEquationParameter area,
            IEquationParameter cakeHeight,
            IEquationParameter kappa)
            : base(
                suspensionVolume, 
                area, 
                cakeHeight, 
                kappa)
        {
            m_suspensionVolume = suspensionVolume;
            m_area = area;
            m_cakeHeight = cakeHeight;
            m_kappa = kappa;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_suspensionVolume, SuspensionVolumeFormula);
        }

        #region Formulas

        private void SuspensionVolumeFormula()
        {
            m_suspensionVolume.Value = m_area.Value * m_cakeHeight.Value * (1 + m_kappa.Value) / m_kappa.Value;
        }

        #endregion
    }
}