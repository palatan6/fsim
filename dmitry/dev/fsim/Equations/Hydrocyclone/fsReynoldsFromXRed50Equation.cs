using System;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsReynoldsFromXRed50Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_Re;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_etaF;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_alpha1;
        private readonly IEquationParameter m_alpha2;
        private readonly IEquationParameter m_alpha3;

        #endregion

        public fsReynoldsFromXRed50Equation(
            IEquationParameter Re,
            IEquationParameter xRed50,
            IEquationParameter rhoS,
            IEquationParameter rhoF,
            IEquationParameter etaF,
            IEquationParameter Dp,
            IEquationParameter rf,
            IEquationParameter cv,
            IEquationParameter alpha1,
            IEquationParameter alpha2,
            IEquationParameter alpha3)
            : base(Re,xRed50,rhoS,rhoF,etaF,Dp,rf,cv,alpha1,alpha2,alpha3)
        {
            m_Re = Re;
            m_xRed50 = xRed50;
            m_rhoS = rhoS;
            m_rhoF = rhoF;
            m_etaF = etaF;
            m_Dp = Dp;
            m_rf = rf;
            m_cv = cv;
            m_alpha1 = alpha1;
            m_alpha2 = alpha2;
            m_alpha3 = alpha3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Re, ReFormula);
        }

        #region Formulas

        private void ReFormula()
        {
            fsValue Num = m_xRed50.Value * m_xRed50.Value * (m_rhoS.Value - m_rhoF.Value) * m_Dp.Value;
            fsValue Den = 9 * m_etaF.Value * m_etaF.Value * m_alpha1.Value * fsValue.Pow(fsValue.Log(1 / m_rf.Value), m_alpha2.Value) * fsValue.Exp(m_alpha3.Value * m_cv.Value);
            m_Re.Value = Num / Den;
        }

        #endregion
    }
}
