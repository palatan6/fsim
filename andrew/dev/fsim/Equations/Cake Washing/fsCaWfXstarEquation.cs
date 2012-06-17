using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using fsNumericalMethods;


namespace Equations.CakeWashing
{
    public class fsCaWfXstarEquation : fsCalculatorEquation
    {
        /*
         * ca * wf = cw * wf + (c0 - cw) * (1 - x*)
         */

        #region Parameters

        private readonly IEquationParameter m_ca;
        private readonly IEquationParameter m_xStar;
        private readonly IEquationParameter m_cw;
        private readonly IEquationParameter m_c0;
        private readonly IEquationParameter m_wf;

        #endregion

        public fsCaWfXstarEquation(
            IEquationParameter ca,
            IEquationParameter xStar,
            IEquationParameter cw,
            IEquationParameter c0,
            IEquationParameter wf)
            : base(ca, xStar, cw, c0, wf)
        {
            m_ca    = ca;
            m_xStar = xStar;
            m_cw    = cw;
            m_c0    = c0;
            m_wf    = wf;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ca, caFormula);
            AddFormula(m_xStar, xStarFormula);
        }

        #region Formulas

        private void caFormula()
        {
            if ((fsValue.Less(m_wf.Value, fsValue.Zero) || m_wf.Value == fsValue.Zero) &&
                  m_wf.Value.Defined && m_xStar.Value == fsValue.One
               )
            {
                m_ca.Value = m_c0.Value;
            }
            else
            {
                m_ca.Value = 1 / m_wf.Value *
                             (m_cw.Value * m_wf.Value + (m_c0.Value - m_cw.Value) * (1 - m_xStar.Value));
            }
        }

        private void xStarFormula()
        {
            if ((fsValue.Less(m_wf.Value, fsValue.Zero) || m_wf.Value == fsValue.Zero) &&
                  m_wf.Value.Defined && m_ca.Value == m_c0.Value
               )
            {
                m_xStar.Value = fsValue.One;
            }
            else
            {
                m_xStar.Value = 1 - m_wf.Value * (m_ca.Value - m_cw.Value) / (m_c0.Value - m_cw.Value);
            }
        }

        #endregion
    }
}

