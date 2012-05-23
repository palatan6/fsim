using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fmCalculationLibrary.Equations;
using Value;
using Parameters;
using fsNumericalMethods;

namespace Equations.Belt_Filters_with_Reversible_Trays
{
    public class fsUFromLsOverBQmsHcDpTtech0LambdaNsfMaterialEquation : fsCalculatorEquation
    {
        /*
         *      u
         *  --------------------------------------------------------- = sqrt(f1 / f2 * nsf)
         *   sqrt(1 - f3 * f1^(2*lambda - 1) / u^(2 * (lambda - 1)))
         *   
         *   */

        #region Parameters

        private readonly IEquationParameter m_u;
        private readonly IEquationParameter m_lambda;
        private readonly IEquationParameter m_nsf;
        private readonly IEquationParameter m_lsOverB;
        private readonly IEquationParameter m_Qms;
        private readonly IEquationParameter m_rho_cd;
        private readonly IEquationParameter m_hc;
        private readonly IEquationParameter m_etaf;
        private readonly IEquationParameter m_hce;
        private readonly IEquationParameter m_kappa;
        private readonly IEquationParameter m_Pc;
        private readonly IEquationParameter m_Dp;
        private readonly IEquationParameter m_ttech0;

        #endregion

        public fsUFromLsOverBQmsHcDpTtech0LambdaNsfMaterialEquation(
            IEquationParameter u,
            IEquationParameter lambda,
            IEquationParameter nsf,
            IEquationParameter lsOverB,
            IEquationParameter Qms,
            IEquationParameter rho_cd,
            IEquationParameter hc,
            IEquationParameter etaf,
            IEquationParameter hce,
            IEquationParameter kappa,
            IEquationParameter Pc,
            IEquationParameter Dp,
            IEquationParameter ttech0)
            : base(u,lambda,nsf,lsOverB,Qms,rho_cd,hc,etaf,hce,kappa,Pc,Dp,ttech0)
        {
            m_u = u;
            m_lambda = lambda;
            m_nsf = nsf;
            m_lsOverB = lsOverB;
            m_Qms = Qms;
            m_rho_cd = rho_cd;
            m_hc = hc;
            m_etaf = etaf;
            m_hce = hce;
            m_kappa = kappa;
            m_Pc = Pc;
            m_Dp = Dp;
            m_ttech0 = ttech0;
        }

        protected override void InitFormulas()
        {
            AddFormula(m_u, UFormula);
        }

        #region Formulas

        private void UFormula()
        {
            fsValue f1 = m_lsOverB.Value * m_Qms.Value / (m_rho_cd.Value * m_hc.Value);
            fsValue f2 = m_etaf.Value * m_hc.Value * (m_hc.Value + 2 * m_hce.Value) / (2 * m_kappa.Value * m_Pc.Value * m_Dp.Value);
            fsValue Ast = fsValue.One;
            fsValue f3 = m_ttech0.Value / fsValue.Pow(m_lsOverB.Value * Ast, m_lambda.Value);
            fsValue c1 = f2;
            fsValue p1 = 2 * m_lambda.Value;
            fsValue c2 = -m_nsf.Value * f1;
            fsValue p2 = 2 * m_lambda.Value - 2;
            fsValue c3 = m_nsf.Value * f3 * fsValue.Pow(f1, 2 * m_lambda.Value);
            List<fsValue> solutions = fsMathEquations.SolveC1Xp1C2Xp2C3(c1, p1, c2, p2, c3, new fsValue(100));
            m_u.Value = solutions[0];
        }

        

        #endregion
    }
}
