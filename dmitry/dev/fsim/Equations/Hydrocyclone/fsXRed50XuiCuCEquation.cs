using System;
using Parameters;
using Value;
using fsNumericalMethods;
using ErfExpIntBoundsCalculator;

namespace Equations.Hydrocyclone 
{
    public class fsXRed50XuiCuCEquation : fsCalculatorEquation
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
         *                           1           
         *          rf =  ------------------------ ,
         *                1 + (cu / c - 1) / E'T
         *                                
         *          E'T = 0.5 * (1 + erf(a * zRed50))
         *               
         * Getting calculated zRed50 we then can calculate xRed50 by the formula
         * 
         *            xRed50 = xG * exp(-zRed50 * 2^(1/2) * ln(sigmaS))                     (*Red50*)
         *                                
         *          
         * The equation (*eq*) (under the relation (*Red50*)) is equivalent to the equation 
         * 
         *             Fu(xRed50) = i       (*xRed50*)
         *  
         * (i in (*xRed50*) is dimensionless, 0 <= i <= 1) because of the equality
         * 
         *          
         *                                1 + erf(zui) + rfFrac(zRed50) * ErfExpInt(b, zRed50, zui)
         *          Fu(xRed50) =  0.5 *   ----------------------------------------------------------
         *                                         1 + rfFrac(zRed50) * erf(a * zRed50)
         */

        #region Parameters

        private readonly IEquationParameter m_cu;
        private readonly IEquationParameter m_c;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_sigmaS;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_xui;
        private readonly IEquationParameter m_i;

        #endregion

        public fsXRed50XuiCuCEquation(
            IEquationParameter cu,
            IEquationParameter c,
            IEquationParameter xRed50,
            IEquationParameter sigmaS,
            IEquationParameter sigmaG,
            IEquationParameter xG,
            IEquationParameter xui,
            IEquationParameter i)
            : base(new IEquationParameter[] { cu, c, xRed50, sigmaS, sigmaG, xG, xui, i })
        {
            m_cu = cu;
            m_c = c;
            m_xRed50 = xRed50;
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

        #region Help Equation Classes

        private class fzRed50 : fsFunction
        {
            private readonly fsValue m_a;
            private readonly fsValue m_i2;
            private readonly fsValue m_erf;
            private readonly fsValue m_CuC2Minus1;

            public fzRed50(fsValue a, fsValue i2, fsValue erf, fsValue CuC2Minus1)
            {
                m_a = a;
                m_i2 = i2;
                m_erf = erf;
                m_CuC2Minus1 = CuC2Minus1;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                double erfAzRed50 = fsErfExpIntBoundsCalculator.erf((m_a * zRed50).Value);
                fsValue rf = 1 / (1 + m_CuC2Minus1 / (1 + erfAzRed50));
                fsValue rfFrac = (1 + rf) / (1 - rf);
                return (1 + rfFrac) * m_erf - m_i2 * (rfFrac + erfAzRed50);
            }
        }

        private class zRed50CalculationFunction : fsFunction
        {
            private readonly fsValue m_i2;
            private readonly fsValue m_b;
            private readonly fsValue m_a;
            private readonly fsValue m_zui;
            private readonly fsValue m_erfui; 
            private readonly fsValue m_CuC2Minus1;

            public zRed50CalculationFunction(fsValue i2, fsValue b, fsValue a, fsValue zui, fsValue erfui, fsValue CuC2Minus1)
            {
                m_i2 = i2;
                m_b = b;
                m_a = a;
                m_zui = zui;
                m_erfui = erfui;
                m_CuC2Minus1 = CuC2Minus1;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                fsValue erfAzRed50 = fsSpecialFunctions.Erf(m_a * zRed50);
                fsValue rf = 1 / (1 + m_CuC2Minus1 / (1 + erfAzRed50));
                fsValue rfFrac = (1 - rf) / (1 + rf);
                return m_erfui + rfFrac * fsSpecialFunctions.ErfExpInt(m_b, zRed50, m_zui) -
                       m_i2 * (1 + rfFrac * erfAzRed50);
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
                    fsValue CuC2Minus1 = 2 * m_cu.Value / m_c.Value - 1;

                    fzRed50 f = new fzRed50(a, 2 * m_i.Value, erfui, CuC2Minus1);
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

                    zRed50CalculationFunction function = new zRed50CalculationFunction(2 * m_i.Value, b, a, zui, erfui, CuC2Minus1);
                    fsValue zRed50 = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), n, new fsValue(1e-8));
                    m_xRed50.Value = m_xG.Value * fsValue.Exp(zRed50 * Math.Sqrt(2.0) * lnSigmaS);
                }
            }

        }

        #endregion
    }
}