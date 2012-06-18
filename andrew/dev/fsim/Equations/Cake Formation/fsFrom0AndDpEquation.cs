using Parameters;
using Value;

namespace Equations
{
    public class fsFrom0AndDpEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_degree;
        private readonly IEquationParameter m_pressure;
        private readonly IEquationParameter m_x;
        private readonly IEquationParameter m_x0;

        #endregion

        public fsFrom0AndDpEquation(
            IEquationParameter x,
            IEquationParameter x0,
            IEquationParameter pressure,
            IEquationParameter degree)
            : base(
                x,
                x0,
                pressure,
                degree)
        {
            m_x = x;
            m_x0 = x0;
            m_pressure = pressure;
            m_degree = degree;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_x, XFormula);
            AddFormula(m_x0, X0Formula);
            AddFormula(m_pressure, PressureFormula);
            AddFormula(m_degree, DegreeFormula);
        }

        #region Formulas

        private void XFormula()
        {
            m_x.Value = m_x0.Value * fsValue.Pow(m_pressure.Value / 1e5, -m_degree.Value);
        }

        private void X0Formula()
        {
            m_x0.Value = m_x.Value / fsValue.Pow(m_pressure.Value / 1e5, -m_degree.Value);
        }

        private void PressureFormula()
        {
            m_pressure.Value = 1e5 / fsValue.Pow(m_x.Value / m_x0.Value, 1 / m_degree.Value);
        }

        private void DegreeFormula()
        {
            m_degree.Value = fsValue.Log(m_x.Value / m_x0.Value) / fsValue.Log(1e5 / m_pressure.Value);
        }

        #endregion
    }
}