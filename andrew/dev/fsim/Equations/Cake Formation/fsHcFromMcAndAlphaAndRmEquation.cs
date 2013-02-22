using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using fsNumericalMethods;
using Value;

namespace Equations
{
    public class fsHcFromMcAndAlphaAndRmEquation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alpha;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter Rm;
        private readonly IEquationParameter rho;

        #endregion

        public fsHcFromMcAndAlphaAndRmEquation(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceRm,
            IEquationParameter MotherLiquidDensity)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakeResistanceAlpha,
                Viscosity,
                FilterMediumResistanceRm,
                MotherLiquidDensity)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alpha = CakeResistanceAlpha;
            eta = Viscosity;
            Rm = FilterMediumResistanceRm;
            rho = MotherLiquidDensity;
        }

        protected override void InitFormulas()
        {
            AddFormula(hc, CakeHeightFormula);
        }

        #region Formulas

        #region Help Equation Class

        class Equation : fsFunction
        {
            #region Parameters

            private readonly fsValue m_mc;
            private readonly fsValue m_A;
            private readonly fsValue m_rho_s;
            private readonly fsValue m_cv;
            private readonly fsValue m_Dp;
            private readonly fsValue m_tf;
            private readonly fsValue m_alpha;
            private readonly fsValue m_eta;
            private readonly fsValue m_Rm;
            private readonly fsValue m_rho;
            #endregion

            public Equation(
            fsValue m_CakeMass,
            fsValue m_FilterArea,
            fsValue m_SolidsDensity,
            fsValue m_SuspensionSolidsVolumeFraction,
            fsValue m_PresureDifference,
            fsValue m_FiltrationTime,
            fsValue m_CakeResistanceAlpha,
            fsValue m_Viscosity,
            fsValue m_FilterMediumResistanceRm,
            fsValue m_MotherLiquidDensity)
            {
                m_mc = m_CakeMass;
                m_A = m_FilterArea;
                m_rho_s = m_SolidsDensity;
                m_cv = m_SuspensionSolidsVolumeFraction;
                m_Dp = m_PresureDifference;
                m_tf = m_FiltrationTime;
                m_alpha = m_CakeResistanceAlpha;
                m_eta = m_Viscosity;
                m_Rm = m_FilterMediumResistanceRm;
                m_rho = m_MotherLiquidDensity;
            }

            public override fsValue Eval(fsValue CakeHeight)
            {
                fsValue E = (m_rho_s * m_A * CakeHeight - m_mc) / (m_A * CakeHeight * (m_rho_s - m_rho));
                fsValue K = m_cv / (1 - E - m_cv);
                fsValue P = 1 / (m_alpha * (1 - E) * m_rho_s);
                fsValue Hce = m_Rm * P;
                fsValue S = fsValue.Sqrt(fsValue.Sqr(Hce) + (2 * P * m_Dp * K * m_tf) / m_eta) - Hce - CakeHeight;

                return S;
            }
        }
        #endregion

        private void CakeHeightFormula()
        {
            var f = new Equation(mc.Value, A.Value, rho_s.Value, cv.Value, Dp.Value, tf.Value, alpha.Value, eta.Value, Rm.Value, rho.Value);
            var eps = new fsValue(1e-8);
            fsValue RightBorder = -mc.Value / (A.Value * (-rho.Value - cv.Value * (rho_s.Value - rho.Value))) - eps;
            hc.Value = fsBisectionMethod.FindRoot(f, new fsValue(1e-6), RightBorder, 30);
        }

        #endregion
    }
}
