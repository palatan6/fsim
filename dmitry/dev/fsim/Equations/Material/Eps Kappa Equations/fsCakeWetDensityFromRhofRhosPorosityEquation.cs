using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations.Material.Eps_Kappa_Equations
{
    public class fsCakeWetDensityFromRhofRhosPorosityEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeWetDensity;
        private readonly IEquationParameter m_filtrateDensity;
        private readonly IEquationParameter m_solidsDensity;
        private readonly IEquationParameter m_porosity;
        

        #endregion

        public fsCakeWetDensityFromRhofRhosPorosityEquation(
            IEquationParameter cakeWetDensity,
            IEquationParameter filtrateDensity,
            IEquationParameter solidsDensity,
            IEquationParameter porosity)
            : base(cakeWetDensity, filtrateDensity, solidsDensity, porosity)
        {
            m_cakeWetDensity = cakeWetDensity;
            m_filtrateDensity = filtrateDensity;
            m_solidsDensity = solidsDensity;
            m_porosity = porosity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_cakeWetDensity, CakeWetDensityFormula);
            AddFormula(m_porosity, PorosityFormula);
        }

        #region Formulas

        private void CakeWetDensityFormula()
        {
            m_cakeWetDensity.Value = m_solidsDensity.Value - m_porosity.Value * (m_solidsDensity.Value - m_filtrateDensity.Value);
        }

        private void PorosityFormula()
        {
            m_porosity.Value = (m_solidsDensity.Value - m_cakeWetDensity.Value) / (m_solidsDensity.Value - m_filtrateDensity.Value);
        }

        #endregion
    }
}


