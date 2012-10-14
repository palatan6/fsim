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
         *          ErfcExpInt(b, zRed50, zoi) = 2 * i * erfc(a * zRed50)      (eq)
         *       
         * where  
         *          a = ln(sigmaS) / (ln(sigmaS)^2 + ln(sigmaG)^2)^(1/2),
         *          b = ln(sigmaG) / ln(sigmaS)
         * Getting calculated zRed50 or zoi we then can calculate xRed50 and xoi by the formulae:
         *          xoi = xG * exp(2^(1/2) * zoi * ln(sigmaG)),
         *          xRed50 = xG * exp(-2^(1/2) * zRed50 * ln(sigmaS))
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

        // Function for fast estimating for zoi
        private class fzoi : fsFunction
        {
            private readonly fsValue m_a;
            private readonly fsValue m_i;
            private readonly fsValue m_zRed50;

            public fzoi(fsValue a, fsValue zRed50, fsValue i)
            {
                m_a = a;
                m_zRed50 = zRed50;
                m_i = i;
            }

            public override fsValue Eval(fsValue zoi)
            {
                return (2.0 * m_i) * fsSpecialFunctions.Erfc(m_a * m_zRed50);
            }
        }

        // Function for fast estimating for zRed50
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

        // ------------------------------\\

        private class zoiCalculationFunction : fsFunction
        {
            private readonly fsValue m_a;
            private readonly fsValue m_b;
            private readonly fsValue m_i;
            private readonly fsValue m_zRed50;

            public zoiCalculationFunction(fsValue a, fsValue b, fsValue zRed50, fsValue i)
            {
                m_a = a;
                m_b = b;
                m_zRed50 = zRed50;
                m_i = i;
            }

            public override fsValue Eval(fsValue zoi)
            {
                return fsSpecialFunctions.ErfcExpInt(m_b, m_zRed50, zoi) - (2.0 * m_i) * fsSpecialFunctions.Erfc(m_a * m_zRed50);
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
                    fsValue zRed50 = (fsValue)((fsValue.Log(m_xG.Value) - fsValue.Log(m_xRed50.Value)) / (Math.Sqrt(2.0) * lnSigmaS));
                    fsFunction f = new fzoi(a, zRed50, m_i.Value);
                    double[] bounds = fsErfExpIntBoundsCalculator.getInterv(20, 1e-20, -9.8, 9.8, b.Value, zRed50.Value, f);
                    if (bounds[0] == 0.0)
                    {
                        m_xoi.Value = new fsValue();
                    }
                    else
                    {
                        int n;
                        if (bounds[0] == 1.0)
                            n = 25;
                        else
                            n = 50;
                        zoiCalculationFunction function = new zoiCalculationFunction(a, b, zRed50, m_i.Value);
                        fsValue zoi = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), n, new fsValue(1e-8));
                        m_xoi.Value = m_xG.Value * fsValue.Exp((zoi * Math.Sqrt(2.0)) * lnSigmaG);
                    }
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
                    double neigh = fsErfExpIntBoundsCalculator.getRootNeighbor(20, 1e-20, -9.8, 9.8, b.Value, z.Value, f);
                    if (neigh == 1000000.0)
                    {
                        m_xRed50.Value = new fsValue();
                    }
                    else
                    {
                        fsValue zRed50 = new fsValue(neigh);
                        fsValue two = new fsValue(2.0);
                        for (int i = 0; i < 30; i++)
                        {
                            fsValue erfcExpInt = fsSpecialFunctions.ErfcExpInt(b, zRed50, z);
                            fsValue val = (0.5 * erfcExpInt) / m_i.Value;
                            if (fsValue.Less(fsValue.Zero, val) && fsValue.Less(val, two))
                            {
                                zRed50 = fsSpecialFunctions.InvErfc(val) / a;
                            }
                            else
                            {
                                m_xRed50.Value = m_xG.Value * fsValue.Exp((-zRed50 * Math.Sqrt(2.0)) * lnSigmaS);
                                return;
                            }
                        }
                        m_xRed50.Value = m_xG.Value * fsValue.Exp((-zRed50 * Math.Sqrt(2.0)) * lnSigmaS);
                    }
                }
            }
        }

        #endregion  
    }
}    
 
