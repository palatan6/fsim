using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Parameters;
using Value;

namespace StepCalculators.Simulation_Calculators.Cake_Formation
{
    public class fsContinuousNonModularBeltFilterCalculator : fsCalculatorEquationsList
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
            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter nc = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter hce0 = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);

            IEquationParameter rhoCd = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);

            IEquationParameter Qms = calculator.AddVariable(fsParameterIdentifier.Qms);
            IEquationParameter Qs = calculator.AddVariable(fsParameterIdentifier.Qs);
            IEquationParameter Qmsus = calculator.AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);
            IEquationParameter Qsus = calculator.AddVariable(fsParameterIdentifier.Qsus);
            
            IEquationParameter ls = calculator.AddVariable(fsParameterIdentifier.ls);
            IEquationParameter lOverB = calculator.AddVariable(fsParameterIdentifier.l_over_b);
            IEquationParameter lsOverB = calculator.AddVariable(fsParameterIdentifier.ls_over_b);

            IEquationParameter u = calculator.AddVariable(fsParameterIdentifier.u);
            IEquationParameter n = calculator.AddVariable(fsParameterIdentifier.RotationalSpeed);
            IEquationParameter tc = calculator.AddVariable(fsParameterIdentifier.CycleTime);
            IEquationParameter nsf = calculator.AddVariable(fsParameterIdentifier.nsf);
            IEquationParameter nsr = calculator.AddVariable(fsParameterIdentifier.nsr);
            IEquationParameter sf = calculator.AddVariable(fsParameterIdentifier.SpecificFiltrationTime);
            IEquationParameter sr = calculator.AddVariable(fsParameterIdentifier.SpecificResidualTime);
            IEquationParameter tr = calculator.AddVariable(fsParameterIdentifier.ResidualTime);

            IEquationParameter cakeHeigth = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter filtrationTime = calculator.AddVariable(fsParameterIdentifier.FiltrationTime);

            IEquationParameter pressureDifference = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);

            IEquationParameter filterArea = calculator.AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter filterAreaAs = calculator.AddVariable(fsParameterIdentifier.As);
            IEquationParameter machineWidth = calculator.AddVariable(fsParameterIdentifier.MachineWidth);
            IEquationParameter filterLength = calculator.AddVariable(fsParameterIdentifier.FilterLength);

            IEquationParameter qft = calculator.AddVariable(fsParameterIdentifier.qft);
            IEquationParameter qmft = calculator.AddVariable(fsParameterIdentifier.qmft);

            #region Help Parameters and Constants

            var constantZero = new fsCalculatorConstant(new fsParameterIdentifier("0")) { Value = fsValue.Zero };
            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) {Value = fsValue.One};
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("1")) {Value = new fsValue(2)};
            
            IEquationParameter hcAddHce = calculator.AddVariable(new fsParameterIdentifier("hc + hce"));
            calculator.AddEquation(new fsSumEquation(hcAddHce, cakeHeigth, hce0));
            
            IEquationParameter hcAdd2Hce = calculator.AddVariable(new fsParameterIdentifier("hc + 2 hce"));
            calculator.AddEquation(new fsSumEquation(hcAdd2Hce, hcAddHce, hce0));

            IEquationParameter oneMinusEps = calculator.AddVariable(new fsParameterIdentifier("1 - eps"));
            calculator.AddEquation(new fsSumEquation(constantOne, eps, oneMinusEps));
                        
            #endregion

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsAreaOfBeltWithReversibleTraysEquation(filterArea, lsOverB, constantOne, Qms, rhoCd, u, cakeHeigth));

            calculator.AddEquation(new fsProductEquation(filtrationTime, tc, sf));
            calculator.AddEquation(new fsProductEquation(Qms, rhoS, Qs));
            calculator.AddEquation(new fsProductEquation(Qmsus, rhoSus, Qsus));
            calculator.AddEquation(new fsProductEquation(Qs, Qsus, cv));
            calculator.AddEquation(new fsProductsEquation(
                new[] {ls, rhoCd, u, cakeHeigth},
                new[] {lsOverB, Qms}));
            calculator.AddEquation(new fsProductsEquation(
                new[] { Qms, tc },
                new[] { rhoCd, filterArea, cakeHeigth }));
            calculator.AddEquation(new fsProductEquation(lOverB, constantOne, lsOverB));
            calculator.AddEquation(new fsProductEquation(nsf, constantOne, sf));
            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressureDifference, nc));
            calculator.AddEquation(new fsProductEquation(filterArea, filterLength, machineWidth));
            calculator.AddEquation(new fsProductsEquation(
                new[] { filterAreaAs, lsOverB },
                new[] { ls, ls }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { filterArea },
                new[] { lOverB, machineWidth, machineWidth }));
            calculator.AddEquation(new fsProductEquation(filterLength, constantOne, ls));
            calculator.AddEquation(new fsProductEquation(filterLength, tc, u));
            calculator.AddEquation(new fsDivisionInverseEquation(tc, n));
            calculator.AddEquation(new fsCakeHeightFromDpTf(cakeHeigth, hce0, pc, kappa, pressureDifference, filtrationTime, etaf));
            calculator.AddEquation(new fsSumEquation(constantOne, nsf, nsr));
            calculator.AddEquation(new fsSumEquation(tc, tr, filtrationTime));
            calculator.AddEquation(new fsSumEquation(constantOne, sr, sf));
            calculator.AddEquation(new fsTrSrTtechNsTcEquation(sr, tr, tc, constantOne, constantZero));
            calculator.AddEquation(new fsTfSfTtechNsTcEquation(sf, filtrationTime, tc, constantOne, constantZero));
            calculator.AddEquation(new fsProductEquation(ls, lsOverB, machineWidth));
            calculator.AddEquation(new fsSfFromEtafHcHceKappaPcDpNsLsUTtechEquation(
                sf, etaf, cakeHeigth, hce0, kappa, pc, pressureDifference, constantOne, ls, u, constantZero));
            calculator.AddEquation(new fsUFromLsOverBQmsHcDpTtech0LambdaNsfMaterialEquation(
                u, constantOne, nsf, lsOverB, Qms, rhoCd, cakeHeigth, etaf, hce0, kappa, pc, pressureDifference, constantZero));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qft, etaf, hcAdd2Hce },
                new[] { constantTwo, pc, pressureDifference }));
            calculator.AddEquation(new fsProductEquation(qmft, qft, rhoF));
            calculator.AddEquation(new fsHcQmsEquation(cakeHeigth, Qms, filterArea, rhoCd, hce0, constantOne, constantZero, kappa, pc, pressureDifference, sf, etaf));

            #region Only Calculated Parameters

            IEquationParameter meanHeightRate = calculator.AddVariable(fsParameterIdentifier.MeanHeightRate);
            IEquationParameter hcOverTc = calculator.AddVariable(fsParameterIdentifier.HcOverTc);
            IEquationParameter diffHeightrate = calculator.AddVariable(fsParameterIdentifier.DiffHeightRate);
            calculator.AddEquation(new fsProductEquation(cakeHeigth, filtrationTime, meanHeightRate));
            calculator.AddEquation(new fsProductEquation(cakeHeigth, tc, hcOverTc));
            calculator.AddEquation(new fsProductsEquation(
                new[] { diffHeightrate, etaf, hcAddHce },
                new[] { kappa, pressureDifference, pc }));

            IEquationParameter Ms = calculator.AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter Vs = calculator.AddVariable(fsParameterIdentifier.SolidsVolume);
            IEquationParameter Msus = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Vsus = calculator.AddVariable(fsParameterIdentifier.SuspensionVolume);
            calculator.AddEquation(new fsProductsEquation(
                new[] { Ms },
                new[] { rhoCd, filterArea, cakeHeigth }));
            calculator.AddEquation(new fsProductEquation(Ms, Msus, cm));
            calculator.AddEquation(new fsProductEquation(Ms, Vs, rhoS));
            calculator.AddEquation(new fsProductEquation(Vs, Vsus, cv));

            IEquationParameter msus = calculator.AddVariable(fsParameterIdentifier.SpecificSuspensionMass);
            IEquationParameter vsus = calculator.AddVariable(fsParameterIdentifier.SpecificSuspensionVolume);
            calculator.AddEquation(new fsProductEquation(Msus, msus, filterArea));
            calculator.AddEquation(new fsProductEquation(Vsus, vsus, filterArea));

            IEquationParameter qmsusd = calculator.AddVariable(fsParameterIdentifier.qmsusd);
            IEquationParameter qsusd = calculator.AddVariable(fsParameterIdentifier.qsusd);
            IEquationParameter Qmsusd = calculator.AddVariable(fsParameterIdentifier.Qmsusd);
            IEquationParameter Qsusd = calculator.AddVariable(fsParameterIdentifier.Qsusd);
            calculator.AddEquation(new fsProductsEquation(
                new[] { qsusd, cv },
                new[] { oneMinusEps, diffHeightrate }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qmsusd, cv },
                new[] { rhoCd, diffHeightrate }));
            calculator.AddEquation(new fsProductEquation(Qmsusd, qmsusd, filterArea));
            calculator.AddEquation(new fsProductEquation(Qsusd, qsusd, filterArea));

            IEquationParameter qmsust = calculator.AddVariable(fsParameterIdentifier.qmsust);
            IEquationParameter qsust = calculator.AddVariable(fsParameterIdentifier.qsust);
            IEquationParameter Qmsust = calculator.AddVariable(fsParameterIdentifier.Qmsust);
            IEquationParameter Qsust = calculator.AddVariable(fsParameterIdentifier.Qsust);
            calculator.AddEquation(new fsProductsEquation(
                new[] { qmsust, cv },
                new[] { oneMinusEps, meanHeightRate }));
            calculator.AddEquation(new fsProductEquation(qmsust, rhoSus, qsust));
            calculator.AddEquation(new fsProductEquation(Qmsust, qmsust, filterArea));
            calculator.AddEquation(new fsProductEquation(Qsust, qsust, filterArea));
            
            #endregion

            #endregion
        }
    }
}
