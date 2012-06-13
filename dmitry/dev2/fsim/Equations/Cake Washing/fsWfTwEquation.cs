using System;
using Parameters;
using Value;

namespace Equations.CakeWashing
{
    public class fsWfTwEquation : fsCalculatorEquation
    {
        /*
         * A solution of the differential equation 
         * dwf/dt *((etaw - eta)*wf + eta) = c3, wf = 0 at t = 0
         * (etaw > 0, eta > 0, c3 > 0)
         * can be represented either in the form:
         *       { (etaw - eta)/(2*c3) * wf^2 + eta/c3 * wf,   if wf < 1 
         *   t = |  
         *       { (etaw * (2*wf - 1) + eta)/(2*c3),           if wf >= 1
         * as a dependence wf -> t  (see twFormula())
         * or in the form:
         *        { 1/(etaw - eta)* (-eta + (eta^2 + 2*c3*(etaw - eta)*t)^(1/2)), if etaw - eta <> 0 and t < t1
         *   wf = | c3/eta * t,                                                   if etaw - eta = 0  and t < t1  
         *        { 1/etaw * (c3*t + 1/2 *(etaw - eta)),                          if t >= t1,
         * where t1 = (etaw + eta)/(2*c3),
         * as a dependence t -> wf  (see wfFormula())
         */
        #region Parameters

        private readonly IEquationParameter m_wf;
        private readonly IEquationParameter m_tw;
        private readonly IEquationParameter m_c3;
        private readonly IEquationParameter m_etaw;
        private readonly IEquationParameter m_eta;

       #endregion

        public fsWfTwEquation(
            IEquationParameter wf,
            IEquationParameter tw,
            IEquationParameter c3,
            IEquationParameter etaw,
            IEquationParameter eta)
            : base(wf, tw, c3, etaw, eta)
        {
            m_wf   = wf;
            m_tw   = tw;
            m_c3   = c3;
            m_etaw = etaw;
            m_eta  = eta;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_wf, wfFormula);
            AddFormula(m_tw, twFormula);
        }

        #region Formulas

        private void wfFormula()
        {
            fsValue diff = m_etaw.Value - m_eta.Value;
            fsValue t1   = (m_etaw.Value + m_eta.Value) / (2 * m_c3.Value);
            if (fsValue.Less(m_tw.Value, t1))
            {
              if (diff != fsValue.Zero)
              {
                  m_wf.Value = 1 / diff *
                              (-m_eta.Value +
                                fsValue.Sqrt(fsValue.Sqr(m_eta.Value) + 2 * m_c3.Value * diff * m_tw.Value
                                ));  
              }
              else
              {
                  m_wf.Value = m_c3.Value / m_eta.Value * m_tw.Value;
              }
            }
            else
            {
                m_wf.Value = 1 / m_etaw.Value * (m_c3.Value * m_tw.Value + 1 / 2 * diff);    
            }
        }

        private void twFormula()
        {
            fsValue diff = m_etaw.Value - m_eta.Value;
            if (fsValue.Less(m_wf.Value, fsValue.One))
            {
               m_tw.Value = diff / (2 * m_c3.Value) * fsValue.Sqr(m_wf.Value) +
                             m_eta.Value / m_c3.Value * m_wf.Value;
            }
            else
            {
                m_tw.Value = (m_etaw.Value * (2 * m_wf.Value - 1) + m_eta.Value) / (2 * m_c3.Value);
            }
        }

        #endregion
    }
}

