using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;
using Equations;
using Equations.Hydrocyclone;

namespace StepCalculators.Simulation_Calculators
{
    public class fsHydrocycloneCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter etaf = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter rhoS = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter rhoF = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter rhoSus = calculator.AddVariable(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter cv = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter cm = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);

            IEquationParameter xG = calculator.AddVariable(fsParameterIdentifier.xg);
            IEquationParameter sigmaG = calculator.AddVariable(fsParameterIdentifier.sigma_g);
            IEquationParameter sigmaS = calculator.AddVariable(fsParameterIdentifier.sigma_s);

            IEquationParameter rf = calculator.AddVariable(fsParameterIdentifier.rf);
            IEquationParameter DuOverD = calculator.AddVariable(fsParameterIdentifier.DuOverD);
            IEquationParameter xRed50 = calculator.AddVariable(fsParameterIdentifier.ReducedCutSize);
            IEquationParameter D = calculator.AddVariable(fsParameterIdentifier.MachineDiameter);

            IEquationParameter numberOfCyclones = calculator.AddVariable(fsParameterIdentifier.NumberOfCyclones);
            IEquationParameter Dp = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter Q = calculator.AddVariable(fsParameterIdentifier.FeedVolumeFlowRate);
            IEquationParameter Qm = calculator.AddVariable(fsParameterIdentifier.FeedSolidsMassFlowRate);
            IEquationParameter Qms = calculator.AddVariable(fsParameterIdentifier.Qms);

            IEquationParameter Cu = calculator.AddVariable(fsParameterIdentifier.UnderflowSolidsConcentration);
            IEquationParameter Co = calculator.AddVariable(fsParameterIdentifier.OverflowSolidsConcentration);
            IEquationParameter C = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsConcentration);

            IEquationParameter ReducedTotalEfficiency = calculator.AddVariable(fsParameterIdentifier.ReducedTotalEfficiency);
            IEquationParameter TotalEfficiency = calculator.AddVariable(fsParameterIdentifier.TotalEfficiency);

            IEquationParameter StokesNumber = calculator.AddVariable(fsParameterIdentifier.StokesNumber);
            IEquationParameter EulerNumber = calculator.AddVariable(fsParameterIdentifier.EulerNumber);
            IEquationParameter ReynoldsNumber = calculator.AddVariable(fsParameterIdentifier.ReynoldsNumber);
            IEquationParameter v = calculator.AddVariable(fsParameterIdentifier.AverageVelocity);

            
            IEquationParameter alpha1 = calculator.AddVariable(fsParameterIdentifier.Alpha1);
            IEquationParameter alpha2 = calculator.AddVariable(fsParameterIdentifier.Alpha2);
            IEquationParameter alpha3 = calculator.AddVariable(fsParameterIdentifier.Alpha3);
            IEquationParameter beta1 = calculator.AddVariable(fsParameterIdentifier.Beta1);
            IEquationParameter beta2 = calculator.AddVariable(fsParameterIdentifier.Beta2);
            IEquationParameter beta3 = calculator.AddVariable(fsParameterIdentifier.Beta3);
            IEquationParameter gamma1 = calculator.AddVariable(fsParameterIdentifier.Gamma1);
            IEquationParameter gamma2 = calculator.AddVariable(fsParameterIdentifier.Gamma2);
            IEquationParameter gamma3 = calculator.AddVariable(fsParameterIdentifier.Gamma3);

            IEquationParameter LOverD = calculator.AddVariable(fsParameterIdentifier.bigLOverD);
            IEquationParameter lOverD = calculator.AddVariable(fsParameterIdentifier.smallLOverD);
            IEquationParameter DiOverD = calculator.AddVariable(fsParameterIdentifier.DiOverD);
            IEquationParameter DoOverD = calculator.AddVariable(fsParameterIdentifier.DoOverD);

            IEquationParameter L = calculator.AddVariable(fsParameterIdentifier.CycloneLength);
            IEquationParameter l = calculator.AddVariable(fsParameterIdentifier.LengthOfCylindricalPart);
            IEquationParameter Di = calculator.AddVariable(fsParameterIdentifier.InletDiameter);
            IEquationParameter Do = calculator.AddVariable(fsParameterIdentifier.OutletDiameter);
            IEquationParameter Du = calculator.AddVariable(fsParameterIdentifier.UnderflowDiameter);
            IEquationParameter Qo = calculator.AddVariable(fsParameterIdentifier.OverflowVolumeFlowRate);
            IEquationParameter Qu = calculator.AddVariable(fsParameterIdentifier.UnderflowVolumeFlowRate);
            IEquationParameter Qmo = calculator.AddVariable(fsParameterIdentifier.OverflowMassFlowRate);
            IEquationParameter Qmu = calculator.AddVariable(fsParameterIdentifier.UnderflowMassFlowRate);
            IEquationParameter Qso = calculator.AddVariable(fsParameterIdentifier.OverflowSolidsMassFlowRate);
            IEquationParameter Qsu = calculator.AddVariable(fsParameterIdentifier.UnderflowSolidsMassFlowRate);
            IEquationParameter cmo = calculator.AddVariable(fsParameterIdentifier.OverflowSolidsMassFraction);
            IEquationParameter cmu = calculator.AddVariable(fsParameterIdentifier.UnderflowSolidsMassFraction);
            IEquationParameter cvo = calculator.AddVariable(fsParameterIdentifier.OverflowSolidsVolumeFraction);
            IEquationParameter cvu = calculator.AddVariable(fsParameterIdentifier.UnderflowSolidsVolumeFraction);
            
            #region Help Parameters and Constants

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };
            var constantFour = new fsCalculatorConstant(new fsParameterIdentifier("4")) { Value = new fsValue(4) };
            var constantEighteen = new fsCalculatorConstant(new fsParameterIdentifier("18")) { Value = new fsValue(18) };
            var constantPi = new fsCalculatorConstant(new fsParameterIdentifier("Pi")) { Value = new fsValue(Math.PI) };

            IEquationParameter rhoSMinusRhoF = calculator.AddVariable(new fsParameterIdentifier("rho_s - rho_f"));
            calculator.AddEquation(new fsSumEquation(rhoS, rhoF, rhoSMinusRhoF));

            IEquationParameter rhoSusOverflow = calculator.AddVariable(new fsParameterIdentifier("rho_sus_overflow"));
            IEquationParameter rhoSusUnderflow = calculator.AddVariable(new fsParameterIdentifier("rho_sus_underflow"));

            #endregion

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsProductEquation(Qm, Q, rhoSus));
            calculator.AddEquation(new fsProductEquation(Qms, Qm, cm));

            calculator.AddEquation(new fsReducedTotalEfficiencyEquation(ReducedTotalEfficiency, xG, xRed50, sigmaG, sigmaS));
            calculator.AddEquation(new fsTotalEfficiencyEquation(TotalEfficiency, rf, ReducedTotalEfficiency));

            calculator.AddEquation(new fsProductEquation(Qu, rf, Q));
            calculator.AddEquation(new fsCUnderflowEquation(Cu, C, rf, ReducedTotalEfficiency));
            calculator.AddEquation(new fsCOverflowEquation(Co, C, ReducedTotalEfficiency));

            calculator.AddEquation(new fsDFromxRed50QnDuOverDEquation(D, xRed50, Q, numberOfCyclones, DuOverD, rhoS, rhoF, etaf, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3));
            calculator.AddEquation(new fsProductsEquation(
                new[] { v, constantPi, D, D, numberOfCyclones },
                new[] { constantFour, Q }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { StokesNumber, constantEighteen, etaf, D },
                new[] { xRed50, xRed50, rhoSMinusRhoF, v }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { EulerNumber, rhoF, v, v },
                new[] { constantTwo, Dp }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { ReynoldsNumber, etaf },
                new[] { rhoF, D, v }));
            calculator.AddEquation(new fsDFromxRed50QnRfEquation(D, xRed50, rhoS, rhoF, Q, etaf, numberOfCyclones, cv, rf, alpha1, alpha2, alpha3, beta1, beta2, beta3));
            calculator.AddEquation(new fsDuOverDrfEuEquation(DuOverD, rf, EulerNumber, gamma1, gamma2, gamma3));
            calculator.AddEquation(new fsEulerReynoldsConnectionEquation(EulerNumber, ReynoldsNumber, cv, beta1, beta2, beta3));
            calculator.AddEquation(new fsReynoldsFromXRed50Equation(ReynoldsNumber, xRed50, rhoS, rhoF, etaf, Dp, rf, cv, alpha1, alpha2, alpha3));
            calculator.AddEquation(new fsvDuOverDxRed50Equation(v, DuOverD, xRed50, rhoS, rhoF, etaf, Dp, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3));
            calculator.AddEquation(new fsQnDpDEquation(Q, Dp, numberOfCyclones, D, rhoF, etaf, cv, beta1, beta2, beta3));

            calculator.AddEquation(new fsProductEquation(L, LOverD, D));
            calculator.AddEquation(new fsProductEquation(l, lOverD, D));
            calculator.AddEquation(new fsProductEquation(Di, DiOverD, D));
            calculator.AddEquation(new fsProductEquation(Do, DoOverD, D));
            calculator.AddEquation(new fsProductEquation(Du, DuOverD, D));
            calculator.AddEquation(new fsProductEquation(Qu, rf, Q));
            calculator.AddEquation(new fsSumEquation(Q, Qo, Qu));
            calculator.AddEquation(new fsQmFromQCEquation(Qmo, Qo, Co, rhoF, rhoS));
            calculator.AddEquation(new fsQmFromQCEquation(Qmu, Qu, Cu, rhoF, rhoS));
            calculator.AddEquation(new fsProductEquation(Qsu, Qu, Cu));
            calculator.AddEquation(new fsProductEquation(Qso, Qo, Co));
            calculator.AddEquation(new fsConcentrationEquation(Co, rhoF, rhoS, rhoSusOverflow));
            calculator.AddEquation(new fsConcentrationEquation(Cu, rhoF, rhoS, rhoSusUnderflow));
            calculator.AddEquation(new fsMassConcentrationEquation(cmu, rhoF, rhoS, rhoSusUnderflow));
            calculator.AddEquation(new fsMassConcentrationEquation(cmo, rhoF, rhoS, rhoSusOverflow));
            calculator.AddEquation(new fsVolumeConcentrationEquation(cvu, rhoF, rhoS, rhoSusUnderflow));
            calculator.AddEquation(new fsVolumeConcentrationEquation(cvo, rhoF, rhoS, rhoSusOverflow));

            #endregion
        }
    }
}
