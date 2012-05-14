using System;
using Parameters;

namespace Equations
{
    public class fsCylinderAreaEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_area;
        private readonly IEquationParameter m_diameter;
        private readonly IEquationParameter m_height;

        #endregion

        public fsCylinderAreaEquation(
            IEquationParameter area,
            IEquationParameter diameter,
            IEquationParameter height)
            : base(
                area,
                diameter,
                height)
        {
            m_area = area;
            m_diameter = diameter;
            m_height = height;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_area, AreaFormula);
            AddFormula(m_diameter, DiameterFormula);
            AddFormula(m_height, HeightFormula);
        }

        #region Formulas

        private void AreaFormula()
        {
            m_area.Value = Math.PI * m_diameter.Value * m_height.Value;
        }

        private void DiameterFormula()
        {
            m_diameter.Value = m_area.Value / (Math.PI * m_height.Value);
        }

        private void HeightFormula()
        {
            m_height.Value = m_area.Value / (Math.PI * m_diameter.Value);
        }

        #endregion
    }
}