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
    public class fsXuiXred50RfEquation : fsCalculatorEquation
    {
        /*
         * We have to solve the transcendental equation
         * 
         *          1 + erf(zui) + rfFrac * ErfExpInt(b, zRed50, zui) = 2 * i * ( 1 + rfFrac * erf(a * zRed50) )         (*eq*)
         *       
         * with respect to zui where
         * 
         *          rfFrac = (1 - rf) / (1 + rf),
         *          a = ln(sigmaS) / ( ln(sigmaS)^2 + ln(sigmaG)^2)^(1/2) ),
         *          b = ln(sigmaG) / ln(sigmaS), 
         *          zRed50 = (ln(xG) - ln(xRed50) ) / ( 2^(1/2) * ln(sigmaS) )    (*Red50*)
         *          
         * Getting calculated zui we then can calculate xui by the relation:
         * 
         *          xui = xG * exp(2^(1/2) * zui * ln(sigmaG))                    (*ui*)
         *          
         * The equation (*eq*) (under relations (*ui*), (*Red50*)) is equivalent to the equation 
         * 
         *          Fu(xui) = i       (*xui*)
         *  
         * (i in (*xui*) is dimensionless, 0 <= i <= 1) because of the equality
         * 
         *          
         *                            1 + erf(zui) + rfFrac * ErfcExpInt(b, zRed50, zui)
         *          Fu(xui) =  0.5 * ----------------------------------------------------
         *                                  1 + rfFrac * erfc(a * zRed50)
         */

        #region Parameters

        private readonly IEquationParameter m_i;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_sigmaS;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_xui;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_rf;

        #endregion

        public fsXuiXred50RfEquation(
            IEquationParameter xRed50, 
            IEquationParameter xui, 
            IEquationParameter sigmaG, 
            IEquationParameter sigmaS, 
            IEquationParameter xG, 
            IEquationParameter i,
            IEquationParameter rf)
            : base(new IEquationParameter[] { xRed50, xui, sigmaG, sigmaS, xG, i, rf })
        {
            m_xRed50 = xRed50;
            m_xui = xui;
            m_sigmaG = sigmaG;
            m_sigmaS = sigmaS;
            m_xG = xG;
            m_i = i;
            m_rf = rf;
        }

        protected override void InitFormulas()
        {
            base.AddFormula(m_xui, new fsCalculatorEquation.fsFormula(xuiFormula));
            base.AddFormula(m_xRed50, new fsCalculatorEquation.fsFormula(xRed50Formula));
        }

        #region Formulas

        #region Help Equation Classes

        // The function for fast zui-estimating
        private class fzui : fsFunction
        {
            private readonly fsValue m_rfFac;
            private readonly fsValue m_h;

            public fzui(fsValue rfFac, fsValue h)
            {
                m_rfFac = rfFac;
                m_h = h;
            }

            public override fsValue Eval(fsValue zui)
            {
                return m_rfFac * (1 + fsSpecialFunctions.Erf(zui)) - m_h;
            }
        }

        // The function for fast zRed50-estimating
        private class fzRed50 : fsFunction
        {
            private readonly fsValue m_a;
            private readonly fsValue m_i2;
            private readonly fsValue m_erf;
            private readonly fsValue m_rfFrac;

            public fzRed50(fsValue a, fsValue i2, fsValue erf, fsValue rfFrac)
            {
                m_a = a;
                m_i2 = i2;
                m_erf = erf;
                m_rfFrac = rfFrac;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                return m_erf - m_i2 * (m_rfFrac + fsSpecialFunctions.Erf(m_a * zRed50));
            }
        }

        private class zuiCalculationFunction : fsFunction
        { 
            private readonly fsValue m_rfFrac;
            private readonly fsValue m_mult;
            private readonly fsValue m_b;
            private readonly fsValue m_zRed50;

            public zuiCalculationFunction(fsValue rfFrac, fsValue mult, fsValue b, fsValue zRed50)
            {
                m_rfFrac = rfFrac;
                m_mult = mult;
                m_b = b;
                m_zRed50 = zRed50;
            }

            public override fsValue Eval(fsValue zui)
            {
                return m_rfFrac * (1 + fsSpecialFunctions.Erf(zui)) + fsSpecialFunctions.ErfExpInt(m_b, m_zRed50, zui) - m_mult;
            }
        }

        private class zRed50CalculationFunction : fsFunction
        {
            private readonly fsValue m_rfFrac;
            private readonly fsValue m_i2;
            private readonly fsValue m_b;
            private readonly fsValue m_a;
            private readonly fsValue m_zui;
            private readonly fsValue m_add;

            public zRed50CalculationFunction(fsValue rfFrac, fsValue i2, fsValue b, fsValue a, fsValue zui, fsValue add)
            {
                m_rfFrac = rfFrac;
                m_i2 = i2;
                m_b = b;
                m_a = a;
                m_zui = zui;
                m_add = add;
            }

            public override fsValue Eval(fsValue zRed50)
            {
                return m_add + m_rfFrac * fsSpecialFunctions.ErfExpInt(m_b, zRed50, m_zui) -
                       m_i2 * (1 + m_rfFrac * fsSpecialFunctions.Erf(m_a * zRed50));
            }
        }

        #endregion

        private void xuiFormula()
        {
            if (m_i.Value == fsValue.Zero)
            {
                m_xui.Value = fsValue.Zero;
                return;
            }
            fsValue lnSigmaG = fsValue.Log(m_sigmaG.Value);
            if (lnSigmaG == fsValue.Zero)
            {
                m_xui.Value = m_xG.Value;
                return;
            }
            fsValue lnSigmaS = fsValue.Log(m_sigmaS.Value);
            if (lnSigmaS == fsValue.Zero)
            {
                m_xui.Value = new fsValue();
                return;
            }
            if (m_rf.Value.Value > 1.0)
            {
                m_xui.Value = new fsValue();
                return;
            }
            fsValue zui;
            if (m_rf.Value == fsValue.One)
            {
                zui = fsSpecialFunctions.InvErf(2 * m_i.Value - 1);
            }
            else
            {
                fsValue a = lnSigmaS / fsValue.Sqrt(fsValue.Sqr(lnSigmaG) + fsValue.Sqr(lnSigmaS));
                fsValue b = lnSigmaG / lnSigmaS;
                fsValue zRed50 = (fsValue.Log(m_xG.Value) - fsValue.Log(m_xRed50.Value)) / (Math.Sqrt(2.0) * lnSigmaS);
                fsValue rfFrac = (1 + m_rf.Value) / (1 - m_rf.Value);
                fsValue erfAz50 = 2 * m_i.Value * (rfFrac + fsSpecialFunctions.Erf(a * zRed50));
                fsFunction f = new fzui(1 + rfFrac, erfAz50);
                double left = Math.Max(-9.8, (fsSpecialFunctions.InvErf(erfAz50 / (1 + rfFrac) - 1)).Value);
                double right = (erfAz50 / (rfFrac - 1) - 1).Value;
                if (right < 1.0)
                    right = Math.Min(9.8, normaldistr.inverf(right));
                else
                    right = 9.8;
                double[] bounds = fsiRedboundsFunction.i(left, right, m_xG.Value.Value, m_sigmaG.Value.Value);
                bounds =  fsErfExpIntBoundsCalculator.getIntervThirdArg(20, 1e-6, bounds[0], bounds[1], b.Value, zRed50.Value, f);
                int n;
                if (bounds[0] == 1.0)
                    n = 25;
                else if (bounds[0] == 0.0)
                    n = 50;
                else
                    n = 45;
                zuiCalculationFunction function = new zuiCalculationFunction(rfFrac, erfAz50, b, zRed50);
                zui = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), n, new fsValue(1e-6));
            }
            m_xui.Value = m_xG.Value * fsValue.Exp((zui * Math.Sqrt(2.0)) * lnSigmaG);            
        }

        private void xRed50Formula()
        {
            if (m_rf.Value == fsValue.One)
            {
                m_xRed50.Value = new fsValue();
                return;
            }
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

                    fsValue rfFrac = (1 - m_rf.Value) / (1 + m_rf.Value);
                    fsValue add = 1 + fsSpecialFunctions.Erf(zui);
                    double[] bounds = fsiRedboundsFunction.Reduced(-9.8, 9.8, m_xG.Value.Value, m_sigmaS.Value.Value);
                    fzRed50 f = new fzRed50(a, 2 * m_i.Value, (1 + rfFrac) * (1 + fsSpecialFunctions.Erf(zui)), rfFrac); // new
                    bounds = fsErfExpIntBoundsCalculator.getIntervSecondArg(20, 1e-5, bounds[0], bounds[1], b.Value, zui.Value, f); // new
                    zRed50CalculationFunction function = new zRed50CalculationFunction(rfFrac, 2 * m_i.Value, b, a, zui, add);
                    fsValue zRed50 = fsBisectionMethod.FindRoot(function, new fsValue(bounds[1]), new fsValue(bounds[2]), 25, new fsValue(1e-8));
                    m_xRed50.Value = m_xG.Value * fsValue.Exp((-zRed50 * Math.Sqrt(2.0)) * lnSigmaS);
                }
            }
        }

       #endregion
    }
}