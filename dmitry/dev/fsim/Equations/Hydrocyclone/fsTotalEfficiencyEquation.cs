using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations.Hydrocyclone
{
    public class fsTotalEfficiencyEquation : fsCalculatorEquation
    {
        // ET = (1 - rf) * E'T + rf
        
        #region Parameters

        private readonly IEquationParameter m_TotalEfficiency;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_ReducedTotalEfficiency;

        #endregion

        public fsTotalEfficiencyEquation(
                        IEquationParameter TotalEfficiency,
                        IEquationParameter rf,
                        IEquationParameter ReducedTotalEfficiency)
            : base(TotalEfficiency, rf, ReducedTotalEfficiency)
        {
            m_TotalEfficiency = TotalEfficiency;
            m_rf = rf;
            m_ReducedTotalEfficiency = ReducedTotalEfficiency;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_TotalEfficiency, TotalEfficiencyFormula);
        }

        #region Formulas

        private void TotalEfficiencyFormula()
        {
            m_TotalEfficiency.Value = (1 - m_rf.Value) * m_ReducedTotalEfficiency.Value + m_rf.Value;
        }

        #endregion
    }
}
