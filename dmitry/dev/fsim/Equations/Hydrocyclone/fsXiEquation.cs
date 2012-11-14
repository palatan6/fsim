using System;
using Parameters;
using Value;
using fsNumericalMethods;

namespace Equations.Hydrocyclone
{
    public class fsXiEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter m_xi;
        private readonly IEquationParameter m_sigmaG;
        private readonly IEquationParameter m_xG;
        private readonly IEquationParameter m_i;

        #endregion

        public fsXiEquation(
            IEquationParameter xi, 
            IEquationParameter sigmaG, 
            IEquationParameter xG, 
            IEquationParameter i)
            : base(new IEquationParameter[] { xi, sigmaG, xG, i })
        {
            m_xi = xi;
            m_sigmaG = sigmaG;
            m_xG = xG;
            m_i = i;
        }

        protected override void InitFormulas()
        {
           base.AddFormula(m_xi, new fsCalculatorEquation.fsFormula(xiFormula));
        }
        private void xiFormula()
        {
            if (m_i.Value == fsValue.Zero)
            {
                m_xi.Value = fsValue.Zero;
                return;
            }
            if (m_i.Value >= fsValue.One)
            {
                m_xi.Value = new fsValue();
                return;
            }
            if (m_i.Value.Value == 0.5)
            {
                m_xi.Value = m_xG.Value;
                return;
            }
            fsValue lnSigmaG = fsValue.Log(m_sigmaG.Value);
            if (lnSigmaG == fsValue.Zero)
            {
                m_xi.Value = new fsValue();
                return;
            }

            fsValue z = fsSpecialFunctions.InvErf(2 * m_i.Value - 1);
            m_xi.Value = m_xG.Value * fsValue.Exp(Math.Sqrt(2) * lnSigmaG * z);
        }
    }
}