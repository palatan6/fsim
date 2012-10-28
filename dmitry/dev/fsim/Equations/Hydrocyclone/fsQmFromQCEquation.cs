using System;
using Parameters;

namespace Equations.Hydrocyclone
{
    public class fsQmFromQCEquation : fsCalculatorEquation
    {
        #region Parameters

        // Qm[u|o] = Q[u|o] * (C[u|o] + (rho_s - C[u|o]) * rho_f / rho_s)

        private readonly IEquationParameter m_Qm;
        private readonly IEquationParameter m_Q;
        private readonly IEquationParameter m_C;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_rhoS;


        #endregion

        public fsQmFromQCEquation(
                        IEquationParameter Qm,
                        IEquationParameter Q,
                        IEquationParameter C,
                        IEquationParameter rhoF,
                        IEquationParameter rhoS)
            : base(Qm, Q, C, rhoF, rhoS)
        {
            m_Qm = Qm;
            m_Q = Q;
            m_C = C;
            m_rhoF = rhoF;
            m_rhoS = rhoS;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Qm, QmFormula);
        }

        #region Formulas

        private void QmFormula()
        {
            m_Qm.Value = m_Q.Value * (m_C.Value + (m_rhoS.Value - m_C.Value) * m_rhoF.Value / m_rhoS.Value);
        }

        #endregion
    }
}
