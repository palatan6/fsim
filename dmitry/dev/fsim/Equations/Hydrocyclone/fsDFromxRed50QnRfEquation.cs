using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsDFromxRed50QnRfEquation : fsCalculatorEquation
    {
        /*
         *  Equation
         *  
         *     /   2      xRed50^2 * (rhoS-rhoF) * Q * beta1 * (4 rhoF Q / Pi / etaF / n)^beta2 * exp(-beta3 * cv)  \^(1 / (3 + beta3))
         * D = | ----- * ------------------------------------------------------------------------------------------ |
         *     \  9Pi                 etaF * n * alpha1 * pow(ln(1 / rf), alpha2) * exp(alpha3 * cv)                /
         * 
         * 
         * D = pow(C1 * xRed50^2 * (Q / n)^(1 + beta2) / pow(ln(1 / rf), alpha2), P1)
         * 
         * where
         *   P1 = 1 / (3 + beta3);
         *   
         *   C1 = K1 * K2 * K3 * K4 * K5;
         *      K1 = 2 / (9 * Pi);
         *      K2 = (rhoS - rhoF) / etaF;
         *      K3 = beta1 / alpha1;
         *      K4 = (4 * rhoF / (Q * Pi * etaF))^beta2;
         *      K5 = exp(-(beta3 + alpha3) * cv), 
         *                                    
         * */

        #region Parameters

        private readonly IEquationParameter m_D;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;
        private readonly IEquationParameter m_Q;
        private readonly IEquationParameter m_etaF;
        private readonly IEquationParameter m_numberOfCyclones;
        private readonly IEquationParameter m_cv;
        private readonly IEquationParameter m_rf;
        private readonly IEquationParameter m_alpha1;
        private readonly IEquationParameter m_alpha2;
        private readonly IEquationParameter m_alpha3;
        private readonly IEquationParameter m_beta1;
        private readonly IEquationParameter m_beta2;
        private readonly IEquationParameter m_beta3;

        #endregion

        public fsDFromxRed50QnRfEquation(
                        IEquationParameter D,
                        IEquationParameter xRed50,
                        IEquationParameter rhoS,
                        IEquationParameter rhoF,
                        IEquationParameter Q,
                        IEquationParameter etaF,
                        IEquationParameter numberOfCyclones,
                        IEquationParameter cv,
                        IEquationParameter rf,
                        IEquationParameter alpha1,
                        IEquationParameter alpha2,
                        IEquationParameter alpha3,
                        IEquationParameter beta1,
                        IEquationParameter beta2,
                        IEquationParameter beta3)
            : base(D, xRed50, rhoS, rhoF, Q, etaF, numberOfCyclones, cv, rf, alpha1, alpha2, alpha3, beta1, beta2, beta3)
        {
            m_D = D;
            m_xRed50 = xRed50;
            m_rhoS = rhoS;
            m_rhoF = rhoF;
            m_Q = Q;
            m_etaF = etaF;
            m_numberOfCyclones = numberOfCyclones;
            m_cv = cv;
            m_rf = rf;
            m_alpha1 = alpha1;
            m_alpha2 = alpha2;
            m_alpha3 = alpha3;
            m_beta1 = beta1;
            m_beta2 = beta2;
            m_beta3 = beta3;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_D, DFormula);
            AddFormula(m_xRed50, xRed50Formula);
        }

        #region Formulas

        private void DFormula()
        {
            m_D.Value = fsValue.Pow(GetC1() * fsValue.Sqr(m_xRed50.Value) * fsValue.Pow(m_Q.Value / m_numberOfCyclones.Value, 1 + m_beta2.Value) / fsValue.Pow(-fsValue.Log(m_rf.Value), m_alpha2.Value), GetP1());
        }

        private void xRed50Formula()
        {
            m_xRed50.Value = fsValue.Sqrt(fsValue.Pow(m_D.Value, 1 / GetP1()) * fsValue.Pow(-fsValue.Log(m_rf.Value), m_alpha2.Value) / (GetC1() * fsValue.Pow(m_Q.Value / m_numberOfCyclones.Value, 1 + m_beta2.Value)));
        }

        #region Help Functions

        private fsValue GetP1()
        {
            return 1 / (3 + m_beta2.Value);
        }

        private fsValue GetC1()
        {
            fsValue K1 = new fsValue(2 / (9 * Math.PI));
            fsValue K2 = (m_rhoS.Value - m_rhoF.Value) / m_etaF.Value;
            fsValue K3 = m_beta1.Value / m_alpha1.Value;
            fsValue K4 = fsValue.Pow(4 * m_rhoF.Value / (Math.PI * m_etaF.Value), m_beta2.Value);
            fsValue K5 = fsValue.Exp(-(m_beta3.Value + m_alpha3.Value) * m_cv.Value);
            return K1 * K2 * K3 * K4 * K5;
        }

        #endregion

        #endregion
    }
}
