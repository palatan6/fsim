using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using fsNumericalMethods;
using Value;

namespace Equations
{
    public class fsHcFromMcAndAlpha0Equation : fsCalculatorEquation
    {
        #region Parameters

        private readonly IEquationParameter hc;
        private readonly IEquationParameter mc;
        private readonly IEquationParameter A;
        private readonly IEquationParameter rho_s;
        private readonly IEquationParameter cv;
        private readonly IEquationParameter Dp;
        private readonly IEquationParameter tf;
        private readonly IEquationParameter alpha0;
        private readonly IEquationParameter eta;
        private readonly IEquationParameter hce;
        private readonly IEquationParameter rho;
        private readonly IEquationParameter nc;

        #endregion

        public fsHcFromMcAndAlpha0Equation(
            IEquationParameter CakeHeight,
            IEquationParameter CakeMass,
            IEquationParameter FilterArea,
            IEquationParameter SolidsDensity,
            IEquationParameter SuspensionSolidsVolumeFraction,
            IEquationParameter PresureDifference,
            IEquationParameter FiltrationTime,
            IEquationParameter CakeResistanceAlpha0,
            IEquationParameter Viscosity,
            IEquationParameter FilterMediumResistanceHce,
            IEquationParameter MotherLiquidDensity,
            IEquationParameter CakeCompressibility)
            : base(
                CakeHeight,
                CakeMass,
                FilterArea,
                SolidsDensity,
                SuspensionSolidsVolumeFraction,
                PresureDifference,
                FiltrationTime,
                CakeResistanceAlpha0,
                Viscosity,
                FilterMediumResistanceHce,
                MotherLiquidDensity,
                CakeCompressibility)
        {
            hc = CakeHeight;
            mc = CakeMass;
            A = FilterArea;
            rho_s = SolidsDensity;
            cv = SuspensionSolidsVolumeFraction;
            Dp = PresureDifference;
            tf = FiltrationTime;
            alpha0 = CakeResistanceAlpha0;
            eta = Viscosity;
            hce = FilterMediumResistanceHce;
            rho = MotherLiquidDensity;
            nc = CakeCompressibility;
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

            private readonly fsValue mc;
            private readonly fsValue A;
            private readonly fsValue rho_s;
            private readonly fsValue cv;
            private readonly fsValue Dp;
            private readonly fsValue tf;
            private readonly fsValue alpha0;
            private readonly fsValue eta;
            private readonly fsValue hce;
            private readonly fsValue rho;
            private readonly fsValue nc;
            #endregion

            public Equation(
            fsValue CakeMass,
            fsValue FilterArea,
            fsValue SolidsDensity,
            fsValue SuspensionSolidsVolumeFraction,
            fsValue PresureDifference,
            fsValue FiltrationTime,
            fsValue CakeResistanceAlpha0,
            fsValue Viscosity,
            fsValue FilterMediumResistanceHce,
            fsValue MotherLiquidDensity,
            fsValue CakeCompressibility)
            {
                mc = CakeMass;
                A = FilterArea;
                rho_s = SolidsDensity;
                cv = SuspensionSolidsVolumeFraction;
                Dp = PresureDifference;
                tf = FiltrationTime;
                alpha0 = CakeResistanceAlpha0;
                eta = Viscosity;
                hce = FilterMediumResistanceHce;
                rho = MotherLiquidDensity;
                nc = CakeCompressibility;
            }

            public override fsValue Eval(fsValue CakeHeight)
            {
                double Dp0 = 1e5;
                fsValue E = (rho_s * A * CakeHeight - mc) / (A * CakeHeight * (rho_s - rho));
                fsValue K = cv / (1 - E - cv);
                fsValue P0 = 1 / (alpha0 * (1 - E) * rho_s);
                fsValue P = P0 * fsValue.Pow(Dp / Dp0, -nc);
                fsValue S = fsValue.Sqrt(fsValue.Sqr(hce) + (2 * P * Dp * K * tf) / eta) - hce - CakeHeight;

                return S;
            }
        }
        #endregion

        private void CakeHeightFormula()
        {
            var f = new Equation(mc.Value, A.Value, rho_s.Value, cv.Value, Dp.Value, tf.Value, alpha0.Value, eta.Value, hce.Value, rho.Value, nc.Value);
            var eps = new fsValue(1e-8);
            fsValue RightBorder = -mc.Value / (A.Value * (-rho.Value - cv.Value * (rho_s.Value - rho.Value))) - eps;
            hc.Value = fsBisectionMethod.FindRoot(f, new fsValue(1e-6), RightBorder, 30);
        }

        #endregion
    }
}
