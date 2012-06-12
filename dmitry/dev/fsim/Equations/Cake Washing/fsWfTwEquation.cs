using System;
using Parameters;
using Value;

namespace Equations.CakeWashing
{
    public class fsWfTwEquation : fsCalculatorEquation
    {
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

