using System;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsCUnderflowEquation : fsCalculatorEquation
    {
        // Cu = C * (1 + (1 - rf) / rf * E'T)

        #region Parameters

        private readonly IEquationParameter m_Cu;
        private readonly IEquationParameter m_C;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_ReducedTotalEfficiency;

        #endregion

        public fsCUnderflowEquation(
                        IEquationParameter Cu,
                        IEquationParameter C,
                        IEquationParameter rf,
                        IEquationParameter ReducedTotalEfficiency)
            : base(Cu, C, rf, ReducedTotalEfficiency)
        {
            m_Cu = Cu;
            m_C = C;
            m_rf = rf;
            m_ReducedTotalEfficiency = ReducedTotalEfficiency;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Cu, CuFormula);
            AddFormula(m_rf, rfFormula);
        }

        #region Formulas

        private void CuFormula()
        {
            m_Cu.Value = m_C.Value * (1 + (1 - m_rf.Value) / m_rf.Value * m_ReducedTotalEfficiency.Value);
        }

        private void rfFormula()
        {
            if (m_ReducedTotalEfficiency.Value == fsValue.Zero)
                m_rf.Value = fsValue.Zero;
            else  
                m_rf.Value = 1 / (1 +  (m_Cu.Value / m_C.Value - 1) / m_ReducedTotalEfficiency.Value);
        }

        #endregion
    }
}
