using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsReducedTotalEfficiencyEquation : fsCalculatorEquation
    {
        /*
         *              /        /           ln(xg) - ln(x'50)               \ \   
         *  E'T = 0.5 * | 1 + erf| ----------------------------------------- | | 
         *              \        \ (2 * (ln(sigmaG)^2 + ln(sigmaS)^2))^(1/2) / / 
         */ 
        
        #region Parameters

        private readonly IEquationParameter m_ReducedTotalEfficiency;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_xRed50;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_sigmaS;

        #endregion

        public fsReducedTotalEfficiencyEquation(
                        IEquationParameter ReducedTotalEfficiency,
                        IEquationParameter xG,
                        IEquationParameter xRed50,
                        IEquationParameter sigmaG,
                        IEquationParameter sigmaS)
            : base(ReducedTotalEfficiency, xG, xRed50, sigmaG, sigmaS)
        {
            m_ReducedTotalEfficiency = ReducedTotalEfficiency;
            m_xG = xG;
            m_xRed50 = xRed50;
            m_sigmaG = sigmaG;
            m_sigmaS = sigmaS;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ReducedTotalEfficiency, ReducedTotalEfficiencyFormula);
            AddFormula(m_xRed50, xRed50Formula);
        }

        #region Formulas

        private void ReducedTotalEfficiencyFormula()
        {
            fsValue A = fsValue.Log(m_xG.Value / m_xRed50.Value);
            fsValue B = fsValue.Sqrt(2 * (fsValue.Sqr(fsValue.Log(m_sigmaG.Value)) + fsValue.Sqr(fsValue.Log(m_sigmaS.Value))));
            m_ReducedTotalEfficiency.Value = 0.5 * (1 + fsValue.Erf(A / B));
        }

        private void xRed50Formula()
        {
            fsValue B = -fsValue.Sqrt(2 * (fsValue.Sqr(fsValue.Log(m_sigmaG.Value)) + fsValue.Sqr(fsValue.Log(m_sigmaS.Value))));
            m_xRed50.Value = m_xG.Value * fsValue.Exp(B * fsValue.InvErf(2 * m_ReducedTotalEfficiency.Value - 1));
        }

        #endregion
    }
}
