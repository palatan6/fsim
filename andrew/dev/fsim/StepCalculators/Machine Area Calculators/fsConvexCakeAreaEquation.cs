using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsConvexCakeAreaEquation : fsCalculatorEquation
    {
        // cakeArea = area * (1 + cakeHeight / diameter)

        #region Parameters
        
        readonly IEquationParameter m_cakeArea;
        readonly IEquationParameter m_area;
        readonly IEquationParameter m_cakeHeight;
        readonly IEquationParameter m_diameter;
        
        #endregion

        public fsConvexCakeAreaEquation(
            IEquationParameter cakeArea,
            IEquationParameter area,
            IEquationParameter cakeHeight,
            IEquationParameter diameter)
            : base(cakeArea, area, cakeHeight, diameter)
        {
            m_cakeArea = cakeArea;
            m_area = area;
            m_cakeHeight = cakeHeight;
            m_diameter = diameter;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_cakeArea, CakeAreaFormula);
        }

        #region Formulas

        private void CakeAreaFormula()
        {
            m_cakeArea.Value = m_area.Value * (1 + m_cakeHeight.Value / m_diameter.Value);
        }

        #endregion
    }
}
