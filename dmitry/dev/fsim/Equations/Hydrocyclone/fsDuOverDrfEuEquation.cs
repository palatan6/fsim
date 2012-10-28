using System;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsDuOverDrfEuEquation : fsCalculatorEquation
    {
        // rf = gamma1 * (Du/D)^gamma2 * Eu^(-gamma3)

        #region Parameters

        private readonly IEquationParameter m_DuOverD;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_Eu;
        private readonly IEquationParameter m_gamma1;
        private readonly IEquationParameter m_gamma2;
        private readonly IEquationParameter m_gamma3;

        #endregion

        public fsDuOverDrfEuEquation(
                        IEquationParameter DuOverD,
                        IEquationParameter rf,
                        IEquationParameter EulerNumber,
                        IEquationParameter gamma1,
                        IEquationParameter gamma2,
                        IEquationParameter gamma3)
            : base(DuOverD, rf, EulerNumber, gamma1, gamma2, gamma3)
        {
            m_DuOverD = DuOverD;
            m_rf = rf;
            m_Eu = EulerNumber;
            m_gamma1 = gamma1;
            m_gamma2 = gamma2;
            m_gamma3 = gamma3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_DuOverD, DuOverDFormula);
            AddFormula(m_rf, rfFormula);
        }

        #region Formulas

        private void DuOverDFormula()
        {
            m_DuOverD.Value = fsValue.Pow(m_rf.Value / (m_gamma1.Value * fsValue.Pow(m_Eu.Value, -m_gamma3.Value)), 1 / m_gamma2.Value);
        }

        private void rfFormula()
        {
            m_rf.Value = fsValue.Pow(m_DuOverD.Value, m_gamma2.Value) * m_gamma1.Value * fsValue.Pow(m_Eu.Value, -m_gamma3.Value); 
        }

        #endregion
    }
}
