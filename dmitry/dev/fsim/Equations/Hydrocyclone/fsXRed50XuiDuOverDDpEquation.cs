using System;
using Parameters;
using Value;
using fsNumericalMethods;
using ExpLinCalculator;

namespace Equations.Hydrocyclone
{
    public class fsXRed50XuiDuOverDDpEquation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation (*eq*):
         * 
         *          1 + erf(zui) + rfFrac * ErfExpInt(b, zRed50, zui) = 2 * i * ( 1 + rfFrac * erf(a * zRed50) )         (*eq*)
         *       
         * with respect to zRed50 where
         * 
         *          zui = (ln(xui) - ln(xG)) / (2^(1/2) * ln(sigmaG)),
         *          a = ln(sigmaS) / ( ln(sigmaS)^2 + ln(sigmaG)^2)^(1/2) ),
         *          b = ln(sigmaG) / ln(sigmaS),
         *          rfFrac = (1 - rf) / (1 + rf),       where
         *          
         *                        /          -1                                          \
         *          rf = ExpLinNeg| ----------------------- * A^( 1 / (alpha2 * beta2) ) | ,
         *                        \  alpha2 * beta2 * gamma3                             /
         *                                
         *          xRed50 = xG * exp(-zRed50 * 2^(1/2) * ln(sigmaS))                     (*Red50*)
         *                                
         *          A = A1 * xRed50^(2 * beta2),
         *          
         *          A1 = ( gamma1 * (DuOverD^gamma2) )^(-1/gamma3) * beta1 * 
         *               ( (rhoS - rho) * Dp * exp(-alpha3 * cv) / (9 * eta^2 * alpha1) )^beta2 * exp(-beta3 * cv),
         *               
         * Getting calculated zRed50 we then can calculate xRed50 by (*Red50*).
         * The equation (*eq*) (under the relation (*Red50*)) is equivalent to the equation 
         * 
         *          Fu(xRed50) = i       (*xRed50*)
         *  
         * (i in (*xRed50*) is dimensionless, 0 <= i <= 1) because of the equality
         * 
         *          
         *                                1 + erf(zui) + rfFrac(zRed50) * ErfcExpInt(b, zRed50, zui)
         *          Fu(xRed50) =  0.5 *   ----------------------------------------------------------
         *                                         1 + rfFrac(zRed50) * erfc(a * zRed50)
         */

        #region Parameters

        private readonly IEquationParameter m_DuOverD;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_alpha1;
        private readonly IEquationParameter m_alpha2;
        private readonly IEquationParameter m_alpha3;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;
        private readonly IEquationParameter m_gamma1;
        private readonly IEquationParameter m_gamma2;
        private readonly IEquationParameter m_gamma3;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_rho;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_eta;
        private readonly IEquationParameter m_sigmaS;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_xui;
        private readonly IEquationParameter m_i;

        #endregion

        public fsXRed50XuiDuOverDDpEquation(
            IEquationParameter DuOverD,
            IEquationParameter Dp,
            IEquationParameter xRed50,
            IEquationParameter alpha1,
            IEquationParameter alpha2,
            IEquationParameter alpha3,
            IEquationParameter beta1,
            IEquationParameter beta2,
            IEquationParameter beta3,
            IEquationParameter gamma1,
            IEquationParameter gamma2,
            IEquationParameter gamma3,
            IEquationParameter cv,
            IEquationParameter rho,
            IEquationParameter rhoS,
            IEquationParameter eta,
            IEquationParameter sigmaS,
            IEquationParameter sigmaG,
            IEquationParameter xG,
            IEquationParameter xui,
            IEquationParameter i)
            : base(new IEquationParameter[] { DuOverD, Dp, xRed50, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3, cv, rho, rhoS, eta, sigmaS, sigmaG, xG, xui, i })
        {
            m_DuOverD = DuOverD;
            m_Dp = Dp;
            m_xRed50 = xRed50;
            m_alpha1 = alpha1;
            m_alpha2 = alpha2;
            m_alpha3 = alpha3;
            m_beta1 = beta1;
            m_beta2 = beta2;
            m_beta3 = beta3;
            m_gamma1 = gamma1;
            m_gamma2 = gamma2;
            m_gamma3 = gamma3;
            m_cv = cv;
            m_rho = rho;
            m_rhoS = rhoS;
            m_eta = eta;
            m_sigmaS = sigmaS;
            m_sigmaG = sigmaG;
            m_xG = xG;
            m_xui = xui;
            m_i = i;
        }

        protected override void InitFormulas()
        {
            base.AddFormula(m_xRed50, new fsCalculatorEquation.fsFormula(xRed50Formula));
        }

        #region Formulas

        #region Help Equation Classes and Functions

        private static fsValue getA1(fsValue alpha1, fsValue alpha3, fsValue beta1, fsValue beta2, fsValue beta3,
                                     fsValue gamma1, fsValue gamma2, fsValue gamma3, fsValue cv, fsValue DuOverD,
                                     fsValue Dp, fsValue rho, fsValue rhoS, fsValue eta)
        {
            fsValue a = (rhoS - rho) * Dp * fsValue.Exp(-alpha3 * cv)/ (9 * alpha1 * fsValue.Sqr(eta));
            fsValue b = fsValue.Pow(gamma1 * fsValue.Pow(DuOverD, gamma2), -1 / gamma3) *
                        beta1 * fsValue.Exp(-beta3 * cv);
            return b * fsValue.Pow(a, beta2);
        }

        private static fsValue getRf(fsValue xG, fsValue zRed50, fsValue A1, fsValue lnsigmaS2, fsValue deg0, fsValue deg1, fsValue deg2)
        {
            fsValue xRed50 = xG * fsValue.Exp(zRed50 * lnsigmaS2);
            fsValue A = A1 * fsValue.Pow(xRed50, deg0);
            fsValue x = deg2 * fsValue.Pow(A, deg1);
            fsValue z = fsSpecialFunctions.ExpLinNeg(x);
            return fsValue.Exp(-z / deg2);
        }

        private static fsValue getRfFast(fsValue xG, fsValue zRed50, fsValue A1, fsValue lnsigmaS2, fsValue deg0, fsValue deg1, fsValue deg2)
        {
            fsValue xRed50 = xG * fsValue.Exp(zRed50 * lnsigmaS2);
            fsValue A = A1 * fsValue.Pow(xRed50, deg0);
            fsValue x = deg2 * fsValue.Pow(A, deg1);
            double z = fsExpLinCalculator.ExpLinNegSpline(x.Value);
            return fsValue.Exp(-z / deg2);
        }

        // The function for fast zRed50-estimating
        private class fzRed50 : fsFunction
        {
            private readonly fsValue m_xG;
            private readonly fsValue m_a;
            private readonly fsValue m_i2;
            private readonly fsValue m_erf;
            private readonly fsValue m_A1;
            private readonly fsValue m_lnsigmaS2;
            private readonly fsValue m_deg0;
            private readonly fsValue m_deg1;
            private readonly fsValue m_deg2;

            public fzRed50(fsValue xG, fsValue a, fsValue i2, fsValue erf, fsValue A1, fsValue lnsigmaS2, fsValue deg0, fsValue deg1, fsValue deg2)
            {
                m_xG = xG;
                m_a = a;
                m_i2 = i2;
                m_erf = erf;
                m_A1 = A1;
                m_lnsigmaS2 = lnsigmaS2;
                m_deg0 = deg0;
                m_deg1 = deg1;
                m_deg2 = deg2;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                fsValue rf = getRfFast(m_xG, zRed50, m_A1, m_lnsigmaS2, m_deg0, m_deg1, m_deg2);
                fsValue rfFrac = (1 + rf) / (1 - rf);
                return (1 + rfFrac) * m_erf - m_i2 * (rfFrac + fsSpecialFunctions.Erf(m_a * zRed50));
            }
        }

        private class zRed50CalculationFunction : fsFunction
        {
            private readonly fsValue m_xG;
            private readonly fsValue m_i2;
            private readonly fsValue m_b;
            private readonly fsValue m_a;
            private readonly fsValue m_zui;
            private readonly fsValue m_erfui;
            private readonly fsValue m_A1;
            private readonly fsValue m_lnsigmaS2;
            private readonly fsValue m_deg0;
            private readonly fsValue m_deg1;
            private readonly fsValue m_deg2;

            public zRed50CalculationFunction(fsValue xG, fsValue i2, fsValue b, fsValue a, fsValue zui,
                                             fsValue erfui, fsValue A1, fsValue lnsigmaS2,
                                             fsValue deg0, fsValue deg1, fsValue deg2)
            {
                m_xG = xG;
                m_i2 = i2;
                m_b = b;
                m_a = a;
                m_zui = zui;
                m_erfui = erfui;
                m_A1 = A1;
                m_lnsigmaS2 = lnsigmaS2;
                m_deg0 = deg0;
                m_deg1 = deg1;
                m_deg2 = deg2;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                fsValue rf = getRf(m_xG, zRed50, m_A1, m_lnsigmaS2, m_deg0, m_deg1, m_deg2);
                fsValue rfFrac = (1 - rf) / (1 + rf);
                return m_erfui + rfFrac * fsSpecialFunctions.ErfExpInt(m_b, zRed50, m_zui) -
                       m_i2 * (1 + rfFrac * fsSpecialFunctions.Erf(m_a * zRed50));
            }
        }

        #endregion

        private void xRed50Formula()
        {
            if (m_i.Value == fsValue.Zero)
            {
                m_xRed50.Value = new fsValue();
                return;
            }
            fsValue lnSigmaS = fsValue.Log(m_sigmaS.Value);
            if (lnSigmaS == fsValue.Zero)
            {
                m_xRed50.Value = m_xG.Value;
            }
            else
            {
                fsValue lnSigmaG = fsValue.Log(m_sigmaG.Value);
                if (lnSigmaG == fsValue.Zero)
                {
                    m_xRed50.Value = new fsValue();
                }
                else
                {
                    fsValue lnXG = fsValue.Log(m_xG.Value);
                    fsValue a = lnSigmaS / fsValue.Sqrt(fsValue.Sqr(lnSigmaG) + fsValue.Sqr(lnSigmaS));
                    fsValue b = lnSigmaG / lnSigmaS;
                    fsValue zui = (fsValue.Log(m_xui.Value) - lnXG) / (Math.Sqrt(2.0) * lnSigmaG);
                    if (!zui.Defined)
                    {
                        m_xRed50.Value = new fsValue();
                        return;
                    }

                    double[] bounds = fsiRedboundsFunction.Reduced(-9.8, 9.8, m_xG.Value.Value, m_sigmaS.Value.Value);
                    fsValue erfui = 1 + fsSpecialFunctions.Erf(zui);
                    fsValue A1 = getA1(m_alpha1.Value, m_alpha3.Value,
                                       m_beta1.Value, m_beta2.Value, m_beta3.Value,
                                       m_gamma1.Value, m_gamma2.Value, m_gamma3.Value,
                                       m_cv.Value, m_DuOverD.Value, m_Dp.Value,
                                       m_rho.Value, m_rhoS.Value, m_eta.Value);
                    fsValue lnsigmaS2 = -Math.Sqrt(2.0) * lnSigmaS;
                    fsValue deg0 = 2 * m_beta2.Value;
                    fsValue deg1 = 1 / (m_alpha2.Value * m_beta2.Value);
                    fsValue deg2 = -deg1 / m_gamma3.Value;

                    fzRed50 f = new fzRed50(m_xG.Value, a, 2 * m_i.Value, erfui, A1, lnsigmaS2, deg0, deg1, deg2);
                    bounds = fsTryBoundsFunction.Second(20, 1e-5, bounds[0], bounds[1], b.Value, zui.Value, f);
                    if (bounds[0] == 0.0)
                    {
                        m_xRed50.Value = new fsValue();
                        return;
                    }
                    int n;
                    if (bounds[0] == 1.0)
                        n = 25;
                    else
                        n = 35;

                    zRed50CalculationFunction function = new zRed50CalculationFunction(m_xG.Value, 2 * m_i.Value, b, a, zui, erfui, A1, lnsigmaS2, deg0, deg1, deg2);
                    fsValue zRed50 = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), n, new fsValue(1e-8));
                    m_xRed50.Value = m_xG.Value * fsValue.Exp(zRed50 * lnsigmaS2);
                }
            }

        }

        #endregion
    }
}