using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;

namespace Equations.Hydrocyclone
{
    public class fsvDuOverDxRed50Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_v;
        private readonly IEquationParameter m_DuOverD;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_etaF;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_alpha1;
        private readonly IEquationParameter m_alpha2;
        private readonly IEquationParameter m_alpha3;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;
        private readonly IEquationParameter m_gamma1;
        private readonly IEquationParameter m_gamma2;
        private readonly IEquationParameter m_gamma3;

        #endregion

        public fsvDuOverDxRed50Equation(
            IEquationParameter v,
            IEquationParameter DuOverD,
            IEquationParameter xRed50,
            IEquationParameter rhoS,
            IEquationParameter rhoF,
            IEquationParameter etaF,
            IEquationParameter Dp,
            IEquationParameter cv,
            IEquationParameter alpha1,
            IEquationParameter alpha2,
            IEquationParameter alpha3,
            IEquationParameter beta1,
            IEquationParameter beta2,
            IEquationParameter beta3,
            IEquationParameter gamma1,
            IEquationParameter gamma2,
            IEquationParameter gamma3)
            : base(v,DuOverD,xRed50,rhoS,rhoF,etaF,Dp,cv,alpha1,alpha2,alpha3,beta1,beta2,beta3,gamma1,gamma2,gamma3)
        {
            m_v = v;
            m_DuOverD = DuOverD;
            m_xRed50 = xRed50;
            m_rhoS = rhoS;
            m_rhoF = rhoF;
            m_etaF = etaF;
            m_Dp = Dp;
            m_cv = cv;
            m_alpha1 = alpha1;
            m_alpha2 = alpha2;
            m_alpha3 = alpha3;
            m_beta1 = beta1;
            m_beta2 = beta2;
            m_beta3 = beta3;
            m_gamma1 = gamma1;
            m_gamma2 = gamma2;
            m_gamma3 = gamma3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_v, vFormula);
        }

        #region Formulas

        #region Help Equation Class

        class Equation : fsFunction
        {
            #region Parameters

            private readonly fsValue m_DuOverD;
            private readonly fsValue m_xRed50;
            private readonly fsValue m_rhoS;
            private readonly fsValue m_rhoF;
            private readonly fsValue m_etaF;
            private readonly fsValue m_Dp;
            private readonly fsValue m_cv;
            private readonly fsValue m_alpha1;
            private readonly fsValue m_alpha2;
            private readonly fsValue m_alpha3;
            private readonly fsValue m_beta1;
            private readonly fsValue m_beta2;
            private readonly fsValue m_beta3;
            private readonly fsValue m_gamma1;
            private readonly fsValue m_gamma2;
            private readonly fsValue m_gamma3;

            #endregion

            public Equation(
                fsValue DuOverD,
                fsValue xRed50,
                fsValue rhoS,
                fsValue rhoF,
                fsValue etaF,
                fsValue Dp,
                fsValue cv,
                fsValue alpha1,
                fsValue alpha2,
                fsValue alpha3,
                fsValue beta1,
                fsValue beta2,
                fsValue beta3,
                fsValue gamma1,
                fsValue gamma2,
                fsValue gamma3)
            {
                m_DuOverD = DuOverD;
                m_xRed50 = xRed50;
                m_rhoS = rhoS;
                m_rhoF = rhoF;
                m_etaF = etaF;
                m_Dp = Dp;
                m_cv = cv;
                m_alpha1 = alpha1;
                m_alpha2 = alpha2;
                m_alpha3 = alpha3;
                m_beta1 = beta1;
                m_beta2 = beta2;
                m_beta3 = beta3;
                m_gamma1 = gamma1;
                m_gamma2 = gamma2;
                m_gamma3 = gamma3;
            }

            public override fsValue Eval(fsValue v)
            {
                fsValue D = m_etaF / (m_rhoF * v) * fsValue.Pow(2 * m_Dp / (m_rhoF * v * v * m_beta1 * fsValue.Exp(-m_beta3 * m_cv)), 1 / m_beta2);
                fsValue Stk = m_xRed50 * m_xRed50 * (m_rhoS - m_rhoF) * v / (18 * m_etaF * D);
                fsValue Eu = 2 * m_Dp / (m_rhoF * v * v);
                fsValue A = Stk * Eu;
                fsValue S1 = fsValue.Log(1 / m_gamma1);
                fsValue S2 = m_gamma2 * fsValue.Log(m_DuOverD);
                fsValue S3 = m_gamma3 * fsValue.Log(Eu);
                fsValue B = m_alpha1 * fsValue.Pow(S1 - S2 + S3, m_alpha2) * fsValue.Exp(m_alpha3 * m_cv);
                return A - B;
            }
        }

        #endregion

        private void vFormula()
        {
            var f = new Equation(m_DuOverD.Value, m_xRed50.Value, m_rhoS.Value, m_rhoF.Value, m_etaF.Value, m_Dp.Value, m_cv.Value, m_alpha1.Value, m_alpha2.Value, m_alpha3.Value, m_beta1.Value, m_beta2.Value, m_beta3.Value, m_gamma1.Value, m_gamma2.Value, m_gamma3.Value);
            fsValue upperBound = new fsValue(15); // 15 m/s we assume as maximal possible speed
            m_v.Value = fsBisectionMethod.FindRoot(f, fsValue.Zero, upperBound, 60);
        }

        #endregion
    }
}
