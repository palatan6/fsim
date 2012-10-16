using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;
using AGLibrary;
using ErfExpIntBoundsCalculator;
using ErfExpIntCalculator;

namespace Equations.Hydrocyclone
{
    public class fsXoiXred50Equation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation
         * 
         *          ErfcExpInt(b, zRed50, zoi) = 2 * i * erfc(a * zRed50)         (*eq*)
         *       
         * with respect to zoi or zRed50 where
         * 
         *          a = ln(sigmaS) / ( ln(sigmaS)^2 + ln(sigmaG)^2)^(1/2) ),
         *          b = ln(sigmaG) / ln(sigmaS), 
         *          zRed50 = (ln(xG) - ln(xRed50) ) / ( 2^(1/2) * ln(sigmaS) )    (*Red50*)
         *          
         * Getting calculated zoi or zRed50 we then can calculate xoi and xRed50 by (*Red50*) and the relation:
         * 
         *          xoi = xG * exp(2^(1/2) * zoi * ln(sigmaG))                    (*oi*)
         *          
         * The equation (*eq*) (under relations (*oi*), (*Red50*)) is equivalent to the equation 
         * 
         *          Fo(xoi) = i       (*xoi*)
         *  
         * (i in (*xoi*) is dimensionless, 0 <= i <= 1) because of the equality
         * 
         *          
         *                            ErfcExpInt(b, zRed50, zoi)
         *          Fo(xoi) =  0.5 * --------------------------
         *                                 erfc(a * zRed50)
         */

        #region Parameters

        private readonly IEquationParameter m_i;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_sigmaS;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_xoi;
        private readonly IEquationParameter m_xRed50;

        #endregion

        public fsXoiXred50Equation(
            IEquationParameter xRed50, 
            IEquationParameter xoi, 
            IEquationParameter sigmaG, 
            IEquationParameter sigmaS, 
            IEquationParameter xG, 
            IEquationParameter i)
            : base(new IEquationParameter[] { xRed50, xoi, sigmaG, sigmaS, xG, i })
        {
            m_xRed50 = xRed50;
            m_xoi = xoi;
            m_sigmaG = sigmaG;
            m_sigmaS = sigmaS;
            m_xG = xG;
            m_i = i;
        }

        protected override void InitFormulas()
        {
            base.AddFormula(m_xRed50, new fsCalculatorEquation.fsFormula(xRed50Formula));
            base.AddFormula(m_xoi, new fsCalculatorEquation.fsFormula(xoiFormula));
        }

        #region Formulas

        #region Help Equation Classes

        // The function for fast zoi-estimating
        private class fzoi : fsFunction
        {
            private readonly fsValue m_h;

            public fzoi(fsValue h)
            {
                m_h = h;
            }

            public override fsValue Eval(fsValue zoi)
            {
                return m_h;
            }
        }

        // The function for fast zRed50-estimating
        private class fzRed50 : fsFunction
        {
            private readonly fsValue m_a;
            private readonly fsValue m_i;

            public fzRed50(fsValue a, fsValue i)
            {
                m_a = a;
                m_i = i;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                return (2.0 * m_i) * fsSpecialFunctions.Erfc(m_a * zRed50);
            }
        }

        private class zoiCalculationFunction : fsFunction
        {
            private readonly fsValue m_b;
            private readonly fsValue m_zRed50;
            private readonly fsValue m_h;

            public zoiCalculationFunction(fsValue b, fsValue zRed50, fsValue h)
            {
                m_b = b;
                m_zRed50 = zRed50;
                m_h = h;
            }

            public override fsValue Eval(fsValue zoi)
            {
                return fsSpecialFunctions.ErfcExpInt(m_b, m_zRed50, zoi) - m_h;
            }
        }

        #endregion

        private void xoiFormula()
        {
            fsValue lnSigmaS = fsValue.Log(m_sigmaS.Value);
            if (lnSigmaS == fsValue.Zero)
            {
                m_xoi.Value = new fsValue();
            }
            else
            {
                fsValue lnSigmaG = fsValue.Log(m_sigmaG.Value);
                if (lnSigmaG == fsValue.Zero)
                {
                    m_xoi.Value = m_xG.Value;
                }
                else
                {
                    fsValue a = lnSigmaS / fsValue.Sqrt(fsValue.Sqr(lnSigmaG) + fsValue.Sqr(lnSigmaS));
                    fsValue b = lnSigmaG / lnSigmaS;
                    fsValue zRed50 = (fsValue.Log(m_xG.Value) - fsValue.Log(m_xRed50.Value)) / (Math.Sqrt(2.0) * lnSigmaS);
                    fsFunction f = new fzoi(2.0 * m_i.Value * fsSpecialFunctions.Erfc(a * zRed50));
                    double[] bounds = fsErfExpIntBoundsCalculator.getInterv(20, 1e-5, -9.8, 9.8, b.Value, zRed50.Value, f);
                    int n;
                    if (bounds[0] == 1.0)
                        n = 25;
                    else if (bounds[0] == 0.0)
                        n = 50;
                    else
                        n = 45;
                    zoiCalculationFunction function = new zoiCalculationFunction(b, zRed50, (2.0 * m_i.Value) * fsSpecialFunctions.Erfc(a * zRed50));
                    fsValue zoi = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), n, new fsValue(1e-6));
                    m_xoi.Value = m_xG.Value * fsValue.Exp((zoi * Math.Sqrt(2.0)) * lnSigmaG);
                }
            }
        }

        private void xRed50Formula()
        {
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
                    fsValue z = (fsValue.Log(m_xoi.Value) - lnXG) / (Math.Sqrt(2.0) * lnSigmaG);
                    fsFunction f = new fzRed50(a, m_i.Value);
                    double neigh = fsErfExpIntBoundsCalculator.getRootNeighborSecondArg(20, 1e-5, -9.8, 9.8, b.Value, z.Value, f);
                    if (neigh == 1000000.0)
                    {
                        m_xRed50.Value = new fsValue();
                    }
                    else
                    {
                        fsValue zRed50 = new fsValue(neigh);
                        fsValue two = new fsValue(2.0);
                        fsValue erfcExpInt;
                        fsValue val;
                        for (int i = 0; i < 30; i++)
                        {
                            erfcExpInt = fsSpecialFunctions.ErfcExpInt(b, zRed50, z);
                            val = (0.5 * erfcExpInt) / m_i.Value;
                            if (fsValue.Less(fsValue.Zero, val) && fsValue.Less(val, two))
                                zRed50 = fsSpecialFunctions.InvErfc(val) / a;
                            else
                                break;
                        }
                        m_xRed50.Value = m_xG.Value * fsValue.Exp((-zRed50 * Math.Sqrt(2.0)) * lnSigmaS);
                    }
                }
            }
        }

        #endregion  
    }
}    
 
