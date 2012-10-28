using System;
using Parameters;
using Value;
using fsNumericalMethods;


namespace Equations.Hydrocyclone
{
    public class fsXRed50Equation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation
         * 
         *             /                             /   /                                                                   \ \^alpha2 \ ^(1/2)
         *            /                              |   |                                                                   | |         \
         *            |                              |   |                             cu / c - 1                            | |         |
         *            | 9 * pi * etaF * n * alpha1 * | ln| 1 + ------------------------------------------------------------- | |         | 
         *            |                              |   |           /       /        ln(xg) - ln(xRed50)                \ \ | |         |
         *            |                              |   |     0.5 * |1 + erf| ----------------------------------------- | | | |         |         
         *            |                              \   \           \       \ (2 * (ln(sigmaG)^2 + ln(sigmaS)^2))^(1/2) / / / /         |
         *   xRed50 = | ---------------------------------------------------------------------------------------------------------------- |  * D^((3 + beta2) / 2)                      
         *            |                                            / 4 * rhoF * Q  \^beta2                                               |
         *            \            2 * (rhoS - rhoF) * Q * beta1 * | ------------- |       * exp(-(alpha3 + beta3) * cv)                 /
         *             \                                           \ pi * etaF * n /                                                    /
         * 
         * with respect to the parameter xRed50 given as an input the parameters 
         * etaF, rhoS, rhoF, cu, c, cv , xg, sigmaG, sigmaS, alpha1, alpha2, alpha3, beta1, beta2, beta3, n, D, Q
         * 
         * This equation can be reduced to the form
         * 
         *                  /                B                \^(alpha2 / 2)
         *   xRed50 = A * ln| 1 + --------------------------- |                   (eq.2)
         *                  \     1 + erf(C - ln(xRed50) / E) /
         * 
         * where
         * 
         *       /   9 * pi * etaF * n * alpha1 * exp((alpha3 + beta3) * cv)      \^(1/2)
         *   A = | -------------------------------------------------------------- |      * D^((3 + beta2) / 2) ;
         *       |                                 / 4 * rhoF * Q  \^beta2        | 
         *       \ 2 * (rhoS - rhoF) * Q * beta1 * | ------------- |              /
         *        \                                \ pi * etaF * n /             /
         * 
         *   B = 2 * (cu / c - 1);  E = (2 * (ln(sigmaG)^2 + ln(sigmaS)^2))^(1/2).          
         *    
         *  Taking the logarithm of (eq.2) we obtain
         * 
         *                                         /   /                     B                  \ \
         *   ln(xRed50) = ln(A) + (alpha2 / 2) * ln| ln| 1 + ---------------------------------- | |       (eq.3)
         *                                         \   \     1 + erf((ln(xg) - ln(xRed50)) / E) / /
         * 
         * Put x = (ln(xg) - ln(xRed50)) / E. Then (eq.3) can be written in the form
         * 
         *                   /   /         a3     \ \
         *   x = a1 - a2 * ln| ln| 1 + ---------- | |        (eq.4)
         *                   \   \     1 + erf(x) / /
         * 
         * where a1 = ln(xg / A) / E;   a2 = alpha2 / 2 / E >= 0;  a3 = B >= 0 (since cu >= c) 
         */

        #region Parameters

        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_D;
        private readonly IEquationParameter m_Q;
        private readonly IEquationParameter m_n;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_etaF;
        private readonly IEquationParameter m_c;
        private readonly IEquationParameter m_cu;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_sigmaS;
        private readonly IEquationParameter m_alpha1;
        private readonly IEquationParameter m_alpha2;
        private readonly IEquationParameter m_alpha3;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;

        #endregion

        public fsXRed50Equation(
            IEquationParameter xRed50,
            IEquationParameter D,
            IEquationParameter Q,
            IEquationParameter n,
            IEquationParameter rhoS,
            IEquationParameter rhoF,
            IEquationParameter etaF,
            IEquationParameter c,
            IEquationParameter cu,
            IEquationParameter cv,
            IEquationParameter xG,
            IEquationParameter sigmaG,
            IEquationParameter sigmaS,
            IEquationParameter alpha1,
            IEquationParameter alpha2,
            IEquationParameter alpha3,
            IEquationParameter beta1,
            IEquationParameter beta2,
            IEquationParameter beta3)
            : base(xRed50, D, Q, n, rhoS, rhoF, etaF, c, cu, cv, xG, sigmaG, sigmaS, alpha1, alpha2, alpha3, beta1, beta2, beta3)
        {
            m_xRed50 = xRed50;
            m_D      = D;
            m_Q      = Q;
            m_n      = n;
            m_rhoS   = rhoS;
            m_rhoF   = rhoF;
            m_etaF   = etaF;
            m_c      = c;
            m_cu     = cu;
            m_cv     = cv;
            m_xG     = xG;
            m_sigmaG = sigmaG;
            m_sigmaS = sigmaS;
            m_alpha1 = alpha1;
            m_alpha2 = alpha2;
            m_alpha3 = alpha3;
            m_beta1  = beta1;
            m_beta2  = beta2;
            m_beta3  = beta3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_xRed50, xRed50Formula);
        }

        #region Formulas

        #region Help Equation Class

        class xCalculationFunction : fsFunction
        {
            #region Parameters

            private readonly fsValue m_a1;
            private readonly fsValue m_a2;
            private readonly fsValue m_a3;

            #endregion

            public xCalculationFunction(
                fsValue a1,
                fsValue a2,
                fsValue a3)
            {
                m_a1 = a1;
                m_a2 = a2;
                m_a3 = a3;
            }

            /*
             * As a rule in math software 
             *        erf(x) exactly equals  - 1  (!!!)
             * when x < = -8 or -9 or -10 (depending on demanded accuracy).
             * So we will get an error trying to calculate ln(ln(1 + a/(1 + erf(x)))) for such x (division by zero).
             * But as it well-known 
             *    1 + erf(x) = erfc(|x|) = O(exp(-|x|^2 / |x|),  x -> -infinity
             * hence we have the asymptotic
             *    ln(ln(1 + a/(1 + erf(x))) = O(ln(|x|)),  x -> -infinity !!!
             * To calculate appropriately the above function for a negative x < -7
             * we use continued fraction expansion for erfc 
             * (see, for example, the article on error functions erf, erfc in Wikipedia)
             */
            private fsValue lnlnErf(fsValue a, fsValue x)
            { 
                if (fsValue.Greater(x, new fsValue(-7)))
                {
                    return fsValue.Log(fsValue.Log(1 + a / (1 + fsSpecialFunctions.Erf(x))));
                }
                {
                    x = -x;
                    fsValue x2 = x * x;
                    return fsValue.Log(x2 + fsValue.Log(a * Math.Sqrt(Math.PI) * 
                                                   (x + 0.5 / 
                                                        (x + x / (x2 + 1.5 / 
                                                                          (1 + 2 / (x2 + 2.5 / 
                                                                                           (1 + 3 / (x2 + 4)
                                                                                           )
                                                                                    )
                                                                           )
                                                                  )
                                                         )
                                                    )
                                       )
                           ); 
                }
            }

            // see the above equation (eq.4) (in comments to fsXRed50Equation)
            public override fsValue Eval(fsValue x)
            {
                return m_a1 - m_a2 * lnlnErf(m_a3, x) - x;
            }
        }

        #endregion

        private void xRed50Formula()
        {
            fsValue lnSigmaS = fsValue.Log(m_sigmaS.Value);
            if (lnSigmaS == fsValue.Zero)
            {
                m_xRed50.Value = m_xG.Value;
                return;
            }
            fsValue lnSigmaG = fsValue.Log(m_sigmaG.Value);
            if (lnSigmaG == fsValue.Zero)
            {
                m_xRed50.Value = new fsValue();
                return;
            }

            fsValue P0 = Math.PI * m_etaF.Value * m_n.Value;
            fsValue P1 = fsValue.Pow(4 * m_rhoF.Value * m_Q.Value / P0, m_beta2.Value);
            fsValue P2 = fsValue.Pow(m_D.Value, 3 + m_beta2.Value);
            fsValue P3 = fsValue.Exp((m_alpha3.Value + m_beta3.Value) * m_cv.Value);
            fsValue Num = 9 * P0 * m_alpha1.Value * P3 * P2;
            fsValue Den = 2 * (m_rhoS.Value - m_rhoF.Value) * m_Q.Value * m_beta1.Value * P1;
            fsValue A = fsValue.Sqrt(Num / Den);
            fsValue B = 2 * (m_cu.Value / m_c.Value - 1);
            fsValue E = fsValue.Sqrt(2 * (fsValue.Sqr(lnSigmaG) + fsValue.Sqr(lnSigmaS)));

            fsValue a1 = fsValue.Log(m_xG.Value / A) / E;
            fsValue a2 = m_alpha2.Value / 2 / E;
            fsValue a3 = B;
            if (!(a1.Defined && a2.Defined && a3.Defined))
            {
                m_xRed50.Value = new fsValue();
                return;
            }

            fsValue alpha = Math.Sqrt(2) * lnSigmaS / E;
            fsValue upperBound = a1 - a2 * fsValue.Log(fsValue.Log(1 + a3 / 2));

            double[] bounds = fsiRedboundsFunction.Reduced(-9.8, (upperBound / alpha).Value, m_xG.Value.Value, m_sigmaS.Value.Value);

            var f = new xCalculationFunction(a1, a2, a3);
            fsValue x = fsBisectionMethod.FindRoot(f, alpha * bounds[0], alpha * bounds[1], 35, new fsValue(1e-8));

            m_xRed50.Value = m_xG.Value * fsValue.Exp(-x * E);
            
        }

        #endregion
    }
}

