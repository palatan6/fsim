using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace Equations.Hydrocyclone
{
    public class fsReducedTotalEfficiencyFromCmoEquation : fsCalculatorEquation
    {
        /*
         *  A solution of the general equation for cmo:
         *  
         *                            c * (1 - ERedT) 
         *      cmo =  --------------------------------------------------
         *                    /       c * (1 - ERedT)   /  rhoS       \ \
         *             rhoF * | 1 + ----------------- * | -----  -  1 | |
         *                    \            rhoS         \  rhoF       / /
         * 
         * with respect to ERedT is
         * 
         *                         cmo * rhoS * rhoF          
         *     ERedT =  1 -  ------------------------------ 
         *                   c * (cmo * (rhoF - rhoS) + rhoS)
         */

        #region Parameters

        private readonly IEquationParameter m_ReducedTotalEfficiency;
        private readonly IEquationParameter m_cmo;
        private readonly IEquationParameter m_C;
        private readonly IEquationParameter m_rhoS;
        private readonly IEquationParameter m_rhoF;

        #endregion

        public fsReducedTotalEfficiencyFromCmoEquation(
                        IEquationParameter ReducedTotalEfficiency,
                        IEquationParameter cmo,
                        IEquationParameter C,
                        IEquationParameter rhoS,
                        IEquationParameter rhoF)
            : base(ReducedTotalEfficiency, cmo, C, rhoS, rhoF)
        {
            m_ReducedTotalEfficiency = ReducedTotalEfficiency;
            m_cmo = cmo;
            m_C = C;
            m_rhoS = rhoS;
            m_rhoF = rhoF;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_ReducedTotalEfficiency, ReducedTotalEfficiencyFormula);
        }

        #region Formulas

        private void ReducedTotalEfficiencyFormula()
        {
            m_ReducedTotalEfficiency.Value =  1 - m_cmo.Value * m_rhoS.Value * m_rhoF.Value / m_C.Value /
                                              (m_cmo.Value * (m_rhoF.Value - m_rhoS.Value) + m_rhoS.Value);
        }

        #endregion
    }
}
