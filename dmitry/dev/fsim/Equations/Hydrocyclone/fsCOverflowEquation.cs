using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;

namespace Equations.Hydrocyclone
{
    public class fsCOverflowEquation : fsCalculatorEquation
    {
        // Co = C * (1 - E'T)

        #region Parameters

        private readonly IEquationParameter m_Co;
        private readonly IEquationParameter m_C;
        private readonly IEquationParameter m_ReducedTotalEfficiency;

        #endregion

        public fsCOverflowEquation(
                        IEquationParameter Cu,
                        IEquationParameter C,
                        IEquationParameter ReducedTotalEfficiency)
            : base(Cu, C, ReducedTotalEfficiency)
        {
            m_Co = Cu;
            m_C = C;
            m_ReducedTotalEfficiency = ReducedTotalEfficiency;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Co, CoFormula);
        }

        #region Formulas

        private void CoFormula()
        {
            m_Co.Value = m_C.Value * (1 - m_ReducedTotalEfficiency.Value);
        }

        #endregion
    }
}
