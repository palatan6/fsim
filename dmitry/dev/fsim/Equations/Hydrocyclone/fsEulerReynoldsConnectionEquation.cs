using System;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsEulerReynoldsConnectionEquation : fsCalculatorEquation
    {
        // Eu = beta1 * Re^beta2 * exp(-beta3 * cv)

        #region Parameters

        private readonly IEquationParameter m_Eu;
        private readonly IEquationParameter m_Re;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;

        #endregion

        public fsEulerReynoldsConnectionEquation(
                        IEquationParameter EulerNumber,
                        IEquationParameter ReynoldsNumber,
                        IEquationParameter cv,
                        IEquationParameter beta1,
                        IEquationParameter beta2,
                        IEquationParameter beta3)
            : base(EulerNumber, ReynoldsNumber, cv, beta1, beta2, beta3)
        {
            m_Eu = EulerNumber;
            m_Re = ReynoldsNumber;
            m_cv = cv;
            m_beta1 = beta1;
            m_beta2 = beta2;
            m_beta3 = beta3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Eu, EuFormula);
        }

        #region Formulas

        private void EuFormula()
        {
            m_Eu.Value = m_beta1.Value * fsValue.Pow(m_Re.Value, m_beta2.Value) * fsValue.Exp(-m_beta3.Value * m_cv.Value);
        }

        #endregion
    }
}
