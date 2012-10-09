using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsTechnicalTimeFrom0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_ttech;
        private readonly IEquationParameter m_ttech0;
        private readonly IEquationParameter m_As;
        private readonly IEquationParameter m_lambda;

        #endregion

        public fsTechnicalTimeFrom0Equation(
                IEquationParameter ttech,
                IEquationParameter ttech0,
                IEquationParameter As,
                IEquationParameter lambda)
            : base(ttech, ttech0, As, lambda)
        {
            m_ttech = ttech;
            m_ttech0 = ttech0;
            m_As = As;
            m_lambda = lambda;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ttech, TTechFormula);
            AddFormula(m_ttech0, TTech0Formula);
        }

        #region Formulas

        private void TTechFormula()
        {
            var Ast = new fsValue(1.0);
            m_ttech.Value = m_ttech0.Value * fsValue.Pow(m_As.Value / Ast, m_lambda.Value);
        }

        private void TTech0Formula()
        {
            var Ast = new fsValue(1.0);
            m_ttech0.Value = m_ttech.Value / fsValue.Pow(m_As.Value / Ast, m_lambda.Value);
        }

        #endregion
    }
}
