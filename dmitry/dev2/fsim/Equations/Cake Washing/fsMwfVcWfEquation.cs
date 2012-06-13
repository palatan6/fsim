using System;
using Parameters;
using Value;


namespace Equations.CakeWashing
{
    public class fsMwfVcWfEquation : fsCalculatorEquation
    {
        /*
         *        { eps*Vc * ((rhow - rho)/2 * wf^2 + rho * wf), if wf < 1
         *  Mwf = |
         *        { eps*Vc * (rhow * (wf - 1/2) + rho/2),        if wf >= 1 
         */

        #region Parameters

        private readonly IEquationParameter m_Mwf;
        private readonly IEquationParameter m_eps;
        private readonly IEquationParameter m_Vc;
        private readonly IEquationParameter m_rhow;
        private readonly IEquationParameter m_rho;
        private readonly IEquationParameter m_wf;

        #endregion

        public fsMwfVcWfEquation(
            IEquationParameter Mwf,
            IEquationParameter eps,
            IEquationParameter Vc,
            IEquationParameter rhow,
            IEquationParameter rho,
            IEquationParameter wf)
            : base(Mwf, eps, Vc, rhow, rho, wf)
        {
            m_Mwf  = Mwf;
            m_eps  = eps;
            m_Vc   = Vc;
            m_rhow = rhow;
            m_rho  = rho;
            m_wf   = wf;           
        }

        protected override void InitFormulas()
        {
            AddFormula(m_Mwf, MwfFormula);
        }

        #region Formulas

        private void MwfFormula()
        {           
            if (fsValue.Less(m_wf.Value, fsValue.One))
            {
                m_Mwf.Value = m_eps.Value * m_Vc.Value * 
                            ((m_rhow.Value - m_rho.Value)/2 *  fsValue.Sqr(m_wf.Value) + 
                              m_rho.Value * m_wf.Value);
            }
            else
            {              
                m_Mwf.Value = m_eps.Value * m_Vc.Value *
                             (m_rhow.Value * (m_wf.Value - 0.5) +  m_rho.Value/2);
            }
        }

        #endregion
    }
}

