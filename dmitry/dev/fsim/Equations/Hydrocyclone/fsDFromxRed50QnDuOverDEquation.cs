using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using fsNumericalMethods;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsDFromxRed50QnDuOverDEquation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation (*eq*):
         * 
         *   2 * xRed50^2 * (rhoS - rhoF) * Q           /   4 * rhoF * Q    \^beta2                               /                                                 /         /    4 * rhoF * Q   \^beta2                    \ \^alpha2
         *   -------------------------------- * beta1 * | ----------------- |       * exp(-beta3 * cv) = alpha1 * | - ln(gamma1) - gamma2 * ln(DuOverD) + gamma3* ln| beta1 * | ----------------- |       * exp(-beta3 * cv) | |       * exp(alpha3 * cv)
         *       9 * pi * etaF * n * D^3                \ pi * etaF * n * D /                                     \                                                 \         \ pi * etaF * n * D /                          / /
         * 
         * with respect to the parameter D given as input parameters 
         * xRed50, etaF, rhoS, rhoF, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3, DuOverD, n, Q
         * 
         *                      / 4 * rhoF * Q  \^beta2
         * Put     A =  beta1 * | ------------- |       * exp(-beta3 * cv) ;
         *                      \ pi * etaF * n /
         *
         *                     2 * xRed50^2 * (rhoS - rhoF) * Q  
         *         B = --------------------------------------------- ;
         *             9 * pi * etaF * n * alpha1 * exp(alpha3 * cv)
         *             
         *             gamma3 * beta2 * alpha2 
         *         C = ----------------------- ;
         *                    3 + beta2
         *                    
         *         x = (ln(gamma1) + gamma2 * ln(DuOverD) - gamma3 * ln(A)) / C ,
         *         y = (A * B)^(1 / alpha2) / C ,
         *         z = D^((-3 - beta2) / alpha2) .
         *         
         * Then the equation (*eq*) is equivalent to the equation
         * 
         *               ln(z) = y * z + x                     (*lln*)
         *               
         * with respect to z given fixed x, y; 
         * so we can apply the function Lln (see fsSpecialFunctions.cs)
         */

        #region Parameters

        private readonly IEquationParameter m_D;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_Q;
        private readonly IEquationParameter m_n;
        private readonly IEquationParameter m_DuOverD;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_etaF;
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

        public fsDFromxRed50QnDuOverDEquation(
            IEquationParameter D,
            IEquationParameter xRed50,
            IEquationParameter Q,
            IEquationParameter n,
            IEquationParameter DuOverD,
            IEquationParameter rhoS,
            IEquationParameter rhoF,
            IEquationParameter etaF,
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
            : base(D, xRed50, Q, n, DuOverD, rhoS, rhoF, etaF, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3)
        {
            m_D = D;
            m_xRed50 = xRed50;
            m_Q = Q;
            m_n = n;
            m_DuOverD = DuOverD;
            m_rhoS = rhoS;
            m_rhoF = rhoF;
            m_etaF = etaF;
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
            AddFormula(m_D, DFormula);
        }

        #region Formulas

        private void DFormula()
        {
            fsValue A = m_beta1.Value * fsValue.Exp(-m_beta3.Value * m_cv.Value) *
                        fsValue.Pow(4 * m_rhoF.Value * m_Q.Value / (Math.PI * m_etaF.Value * m_n.Value),
                                    m_beta2.Value);
            fsValue B = 2 * m_xRed50.Value * m_xRed50.Value * (m_rhoS.Value - m_rhoF.Value) * m_Q.Value /
                        (9 * Math.PI * m_etaF.Value * m_n.Value * m_alpha1.Value *
                         fsValue.Exp(m_alpha3.Value * m_cv.Value));
            fsValue C = m_gamma3.Value * m_beta2.Value * m_alpha2.Value / (3 + m_beta2.Value);
            fsValue x = (fsValue.Log(m_gamma1.Value) + m_gamma2.Value * fsValue.Log(m_DuOverD.Value) -
                         m_gamma3.Value * fsValue.Log(A)) / C;
            fsValue y = fsValue.Pow(A * B, 1 / m_alpha2.Value) / C;
            m_D.Value = fsValue.Pow(fsSpecialFunctions.Lln(x, y), -m_alpha2.Value / (3 + m_beta2.Value));
        }

        #endregion
    }
}
