using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsQnDpDEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Q;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_n;
        private readonly IEquationParameter m_D;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_etaF;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;

        #endregion

        public fsQnDpDEquation(
            IEquationParameter Q,
            IEquationParameter Dp,
            IEquationParameter numberOfCyclones,
            IEquationParameter D,
            IEquationParameter rhoF,
            IEquationParameter etaF,
            IEquationParameter cv,
            IEquationParameter beta1,
            IEquationParameter beta2,
            IEquationParameter beta3)
            : base(Q,Dp,numberOfCyclones,D,rhoF,etaF,cv,beta1,beta2,beta3)
        {
            m_Q = Q;
            m_Dp = Dp;
            m_n = numberOfCyclones;
            m_D = D;
            m_rhoF = rhoF;
            m_etaF = etaF;
            m_cv = cv;
            m_beta1 = beta1;
            m_beta2 = beta2;
            m_beta3 = beta3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Q, QFormula);
        }

        #region Formulas

        private void QFormula()
        {
            fsValue Num = m_Dp.Value * fsValue.Sqr(Math.PI * m_n.Value * m_D.Value * m_D.Value);
            fsValue Den = 8 * m_rhoF.Value * m_beta1.Value * fsValue.Pow(4 * m_rhoF.Value / (Math.PI * m_etaF.Value * m_n.Value * m_D.Value), m_beta2.Value) * fsValue.Exp(-m_beta3.Value * m_cv.Value);
            m_Q.Value = fsValue.Pow(Num / Den, 1 / (2 + m_beta2.Value));
        }

        #endregion
    }
}
