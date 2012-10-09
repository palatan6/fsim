using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations
{
    public class fsCakeDrySolidsDensityEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_cakeDrySolidsDensity;
        private readonly IEquationParameter m_porosity;
        private readonly IEquationParameter m_solidsDensity;

        #endregion

        public fsCakeDrySolidsDensityEquation(
            IEquationParameter cakeDrySolidsDensity,
            IEquationParameter porosity,
            IEquationParameter solidsDensity)
            : base(
                cakeDrySolidsDensity,
                porosity,
                solidsDensity)
        {
            m_cakeDrySolidsDensity = cakeDrySolidsDensity;
            m_porosity = porosity;
            m_solidsDensity = solidsDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_cakeDrySolidsDensity, CakeDrySolidsDensityFormula);
            AddFormula(m_porosity, PorosityFormula);
        }

        #region Formulas

        private void CakeDrySolidsDensityFormula()
        {
            m_cakeDrySolidsDensity.Value = (1 - m_porosity.Value) * m_solidsDensity.Value;
        }

        private void PorosityFormula()
        {
            m_porosity.Value = 1 - m_cakeDrySolidsDensity.Value / m_solidsDensity.Value;
        }

        #endregion
    }
}
