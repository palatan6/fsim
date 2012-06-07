using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations.Hydrocyclone
{
    public class fsCUnderflowEquation : fsCalculatorEquation
    {
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
        }

        #region Formulas

        private void CuFormula()
        {
            m_Cu.Value = m_C.Value * (1 + (1 - m_rf.Value) / m_rf.Value * m_ReducedTotalEfficiency.Value);
        }

        #endregion
    }
}
