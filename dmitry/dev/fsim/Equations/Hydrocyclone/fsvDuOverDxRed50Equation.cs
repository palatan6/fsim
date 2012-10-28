using System;
using Parameters;
using Value;
using fsNumericalMethods;

namespace Equations.Hydrocyclone
{
    public class fsvDuOverDxRed50Equation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation (*eq*):
         *   
         *                     xRed50^2 * (rhoS - rhoF) * Dp                              /                                                 /   2 * Dp   \ \^alpha2
         *    ---------------------------------------------------------------- = alpha1 * | - ln(gamma1) - gamma2 * ln(DuOverD) + gamma3* ln| ---------- | |       * exp(alpha3 * cv)
         *                 /               2 * Dp                  \^(1/beta2)            \                                                 \ rhoF * v^2 / /
         *    9 * etaF^2 * | ------------------------------------  |
         *                 \ rhoF * v^2 * beta1 * exp(-beta3 * cv) /
         *                 
         * with respect to the parameter v given as input parameters 
         * xRed50, etaF, rhoS, rhoF, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3, DuOverD, Dp
         * 
         *              
         * Put     A = 2 * Dp / rhoF ; 
         *              
         *                                xRed50^2 * (rhoS - rhoF) * Dp
         *         B = ------------------------------------------------------------------------------ ;
         *                          /            A             \^(1/beta2)
         *             9 * etaF^2 * | ------------------------ |          * alpha1 * exp(alpha3 * cv)
         *                          \ beta1 * exp(-beta3 * cv) /
         *                          
         *         C = -ln(gamma1) - gamma2 * ln(DuOverD) + gamma3 * ln(A);
         *         D = 1 / (alpha2 * beta2 * gamma3);
         *         E = exp(C * D);
         *         
         * Let connect z and v by the formula
         * 
         *         v = (E * exp(z))^(alpha2 * beta2 / 2)
         *         
         * and put
         * 
         *         x = - B^(1 / alpha2) * D * E.
         *         
         * Then the equation (*eq*) is equivalent to the equation
         * 
         *               z = x * exp(z)
         *               
         * with respect to z given fixed x < 0; 
         * so we can apply the function ExpLinNeg (see fsSpecialFunctions.cs) 
         */

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

        private void vFormula()
        {
            fsValue A = 2 * m_Dp.Value / m_rhoF.Value;
            fsValue B = m_xRed50.Value * m_xRed50.Value * (m_rhoS.Value - m_rhoF.Value) * m_Dp.Value /
                        (9 * m_etaF.Value * m_etaF.Value * m_alpha1.Value * fsValue.Exp(m_alpha3.Value * m_cv.Value) *
                         fsValue.Pow(A * fsValue.Exp(m_beta3.Value * m_cv.Value) / m_beta1.Value, 1 / m_beta2.Value));
            fsValue C = (m_gamma3.Value * fsValue.Log(A) - fsValue.Log(m_gamma1.Value) -
                         m_gamma2.Value * fsValue.Log(m_DuOverD.Value));
            fsValue D = 1 / (m_alpha2.Value * m_beta2.Value * m_gamma3.Value);
            fsValue E = fsValue.Exp(D * C);
            fsValue x = -(D * fsValue.Pow(B, 1 / m_alpha2.Value) * E);
            fsValue z = fsSpecialFunctions.ExpLinNeg(x);
            m_v.Value = fsValue.Pow(E * fsValue.Exp(z), 0.5 * m_alpha2.Value * m_beta2.Value);
        }

        #endregion
    }
}
