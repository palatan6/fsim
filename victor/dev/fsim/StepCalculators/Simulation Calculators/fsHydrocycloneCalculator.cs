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
    public class fsHydrocycloneCalculator : fsCalculator
    {
        public fsHydrocycloneCalculator()
        {
            #region Parameters Initialization

            IEquationParameter etaf = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter rhoS = AddConstant(fsParameterIdentifier.SolidsDensity);
            IEquationParameter rhoF = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter rhoSus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter cv = AddConstant(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);

            IEquationParameter xG = AddVariable(fsParameterIdentifier.xg);
            IEquationParameter sigmaG = AddVariable(fsParameterIdentifier.sigma_g);
            IEquationParameter sigmaS = AddVariable(fsParameterIdentifier.sigma_s);

            IEquationParameter rf = AddVariable(fsParameterIdentifier.rf);
            IEquationParameter DuOverD = AddVariable(fsParameterIdentifier.DuOverD);
            IEquationParameter xRed50 = AddVariable(fsParameterIdentifier.ReducedCutSize);
            IEquationParameter D = AddVariable(fsParameterIdentifier.MachineDiameter);

            IEquationParameter numberOfCyclones = AddVariable(fsParameterIdentifier.NumberOfCyclones);
            IEquationParameter Dp = AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter Q = AddVariable(fsParameterIdentifier.FeedVolumeFlowRate);
            IEquationParameter Qm = AddVariable(fsParameterIdentifier.FeedSolidsMassFlowRate);
            IEquationParameter Qms = AddVariable(fsParameterIdentifier.Qms);

            IEquationParameter Cu = AddVariable(fsParameterIdentifier.UnderflowSolidsConcentration);
            IEquationParameter Co = AddVariable(fsParameterIdentifier.OverflowSolidsConcentration);
            IEquationParameter C = AddConstant(fsParameterIdentifier.SuspensionSolidsConcentration);

            IEquationParameter ReducedTotalEfficiency = AddVariable(fsParameterIdentifier.ReducedTotalEfficiency);
            IEquationParameter TotalEfficiency = AddVariable(fsParameterIdentifier.TotalEfficiency);

            IEquationParameter StokesNumber = AddVariable(fsParameterIdentifier.StokesNumber);
            IEquationParameter EulerNumber = AddVariable(fsParameterIdentifier.EulerNumber);
            IEquationParameter ReynoldsNumber = AddVariable(fsParameterIdentifier.ReynoldsNumber);
            IEquationParameter v = AddVariable(fsParameterIdentifier.AverageVelocity);

            
            IEquationParameter alpha1 = AddVariable(fsParameterIdentifier.Alpha1);
            IEquationParameter alpha2 = AddVariable(fsParameterIdentifier.Alpha2);
            IEquationParameter alpha3 = AddVariable(fsParameterIdentifier.Alpha3);
            IEquationParameter beta1 = AddVariable(fsParameterIdentifier.Beta1);
            IEquationParameter beta2 = AddVariable(fsParameterIdentifier.Beta2);
            IEquationParameter beta3 = AddVariable(fsParameterIdentifier.Beta3);
            IEquationParameter gamma1 = AddVariable(fsParameterIdentifier.Gamma1);
            IEquationParameter gamma2 = AddVariable(fsParameterIdentifier.Gamma2);
            IEquationParameter gamma3 = AddVariable(fsParameterIdentifier.Gamma3);

            IEquationParameter LOverD = AddVariable(fsParameterIdentifier.bigLOverD);
            IEquationParameter lOverD = AddVariable(fsParameterIdentifier.smallLOverD);
            IEquationParameter DiOverD = AddVariable(fsParameterIdentifier.DiOverD);
            IEquationParameter DoOverD = AddVariable(fsParameterIdentifier.DoOverD);

            IEquationParameter L = AddVariable(fsParameterIdentifier.CycloneLength);
            IEquationParameter l = AddVariable(fsParameterIdentifier.LengthOfCylindricalPart);
            IEquationParameter Di = AddVariable(fsParameterIdentifier.InletDiameter);
            IEquationParameter Do = AddVariable(fsParameterIdentifier.OutletDiameter);
            IEquationParameter Du = AddVariable(fsParameterIdentifier.UnderflowDiameter);
            IEquationParameter Qo = AddVariable(fsParameterIdentifier.OverflowVolumeFlowRate);
            IEquationParameter Qu = AddVariable(fsParameterIdentifier.UnderflowVolumeFlowRate);
            IEquationParameter Qmo = AddVariable(fsParameterIdentifier.OverflowMassFlowRate);
            IEquationParameter Qmu = AddVariable(fsParameterIdentifier.UnderflowMassFlowRate);
            IEquationParameter Qso = AddVariable(fsParameterIdentifier.OverflowSolidsMassFlowRate);
            IEquationParameter Qsu = AddVariable(fsParameterIdentifier.UnderflowSolidsMassFlowRate);
            IEquationParameter cmo = AddVariable(fsParameterIdentifier.OverflowSolidsMassFraction);
            IEquationParameter cmu = AddVariable(fsParameterIdentifier.UnderflowSolidsMassFraction);
            IEquationParameter cvo = AddVariable(fsParameterIdentifier.OverflowSolidsVolumeFraction);
            IEquationParameter cvu = AddVariable(fsParameterIdentifier.UnderflowSolidsVolumeFraction);
            
            #region Help Parameters and Constants

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };
            var constantFour = new fsCalculatorConstant(new fsParameterIdentifier("4")) { Value = new fsValue(4) };
            var constantEighteen = new fsCalculatorConstant(new fsParameterIdentifier("18")) { Value = new fsValue(18) };
            var constantPi = new fsCalculatorConstant(new fsParameterIdentifier("Pi")) { Value = new fsValue(Math.PI) };

            IEquationParameter rhoSMinusRhoF = AddVariable(new fsParameterIdentifier("rho_s - rho_f"));
            Equations.Add(new fsSumEquation(rhoS, rhoF, rhoSMinusRhoF));

            IEquationParameter rhoSusOverflow = AddVariable(new fsParameterIdentifier("rho_sus_overflow"));
            IEquationParameter rhoSusUnderflow = AddVariable(new fsParameterIdentifier("rho_sus_underflow"));

            #endregion

            #endregion

            #region Equations Initialization

            Equations.Add(new fsProductEquation(Qm, Q, rhoSus));
            Equations.Add(new fsProductEquation(Qms, Qm, cm));

            Equations.Add(new fsReducedTotalEfficiencyEquation(ReducedTotalEfficiency, xG, xRed50, sigmaG, sigmaS));
            Equations.Add(new fsTotalEfficiencyEquation(TotalEfficiency, rf, ReducedTotalEfficiency));

            Equations.Add(new fsProductEquation(Qu, rf, Q));
            Equations.Add(new fsCUnderflowEquation(Cu, C, rf, ReducedTotalEfficiency));
            Equations.Add(new fsCOverflowEquation(Co, C, ReducedTotalEfficiency));

            Equations.Add(new fsDFromxRed50QnDuOverDEquation(D, xRed50, Q, numberOfCyclones, DuOverD, rhoS, rhoF, etaf, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3));
            Equations.Add(new fsProductsEquation(
                new[] { v, constantPi, D, D, numberOfCyclones },
                new[] { constantFour, Q }));
            Equations.Add(new fsProductsEquation(
                new[] { StokesNumber, constantEighteen, etaf, D },
                new[] { xRed50, xRed50, rhoSMinusRhoF, v }));
            Equations.Add(new fsProductsEquation(
                new[] { EulerNumber, rhoF, v, v },
                new[] { constantTwo, Dp }));
            Equations.Add(new fsProductsEquation(
                new[] { ReynoldsNumber, etaf },
                new[] { rhoF, D, v }));
            Equations.Add(new fsDFromxRed50QnRfEquation(D, xRed50, rhoS, rhoF, Q, etaf, numberOfCyclones, cv, rf, alpha1, alpha2, alpha3, beta1, beta2, beta3));
            Equations.Add(new fsDuOverDrfEuEquation(DuOverD, rf, EulerNumber, gamma1, gamma2, gamma3));
            Equations.Add(new fsEulerReynoldsConnectionEquation(EulerNumber, ReynoldsNumber, cv, beta1, beta2, beta3));
            Equations.Add(new fsReynoldsFromXRed50Equation(ReynoldsNumber, xRed50, rhoS, rhoF, etaf, Dp, rf, cv, alpha1, alpha2, alpha3));
            Equations.Add(new fsvDuOverDxRed50Equation(v, DuOverD, xRed50, rhoS, rhoF, etaf, Dp, cv, alpha1, alpha2, alpha3, beta1, beta2, beta3, gamma1, gamma2, gamma3));
            Equations.Add(new fsQnDpDEquation(Q, Dp, numberOfCyclones, D, rhoF, etaf, cv, beta1, beta2, beta3));

            Equations.Add(new fsProductEquation(L, LOverD, D));
            Equations.Add(new fsProductEquation(l, lOverD, D));
            Equations.Add(new fsProductEquation(Di, DiOverD, D));
            Equations.Add(new fsProductEquation(Do, DoOverD, D));
            Equations.Add(new fsProductEquation(Du, DuOverD, D));
            Equations.Add(new fsProductEquation(Qu, rf, Q));
            Equations.Add(new fsSumEquation(Q, Qo, Qu));
            Equations.Add(new fsQmFromQCEquation(Qmo, Qo, Co, rhoF, rhoS));
            Equations.Add(new fsQmFromQCEquation(Qmu, Qu, Cu, rhoF, rhoS));
            Equations.Add(new fsProductEquation(Qsu, Qu, Cu));
            Equations.Add(new fsProductEquation(Qso, Qo, Co));
            Equations.Add(new fsConcentrationEquation(Co, rhoF, rhoS, rhoSusOverflow));
            Equations.Add(new fsConcentrationEquation(Cu, rhoF, rhoS, rhoSusUnderflow));
            Equations.Add(new fsMassConcentrationEquation(cmu, rhoF, rhoS, rhoSusUnderflow));
            Equations.Add(new fsMassConcentrationEquation(cmo, rhoF, rhoS, rhoSusOverflow));
            Equations.Add(new fsVolumeConcentrationEquation(cvu, rhoF, rhoS, rhoSusUnderflow));
            Equations.Add(new fsVolumeConcentrationEquation(cvo, rhoF, rhoS, rhoSusOverflow));

            #endregion
        }
    }
}
