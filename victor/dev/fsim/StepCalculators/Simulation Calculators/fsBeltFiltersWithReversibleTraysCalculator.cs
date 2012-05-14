using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Parameters;
using Value;

namespace StepCalculators.Simulation_Calculators
{
    public class fsBeltFiltersWithReversibleTraysCalculator : fsCalculator
    {
        public fsBeltFiltersWithReversibleTraysCalculator()
        {
            #region Parameters Initialization

            IEquationParameter etaf = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter rhoS = AddConstant(fsParameterIdentifier.SolidsDensity);
            IEquationParameter rhoF = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter rhoSus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter cv = AddConstant(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            IEquationParameter nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter hce0 = AddConstant(fsParameterIdentifier.FilterMediumResistanceHce0);
            IEquationParameter lambda = AddConstant(fsParameterIdentifier.lambda);

            IEquationParameter rhoCd = AddConstant(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter eps = AddConstant(fsParameterIdentifier.CakePorosity);
            IEquationParameter kappa = AddConstant(fsParameterIdentifier.Kappa);

            IEquationParameter ns = AddVariable(fsParameterIdentifier.ns);
            IEquationParameter Qms = AddVariable(fsParameterIdentifier.Qms);
            IEquationParameter Qs = AddVariable(fsParameterIdentifier.Qs);
            IEquationParameter Qmsus = AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);
            IEquationParameter Qsus = AddVariable(fsParameterIdentifier.Qsus);
            
            IEquationParameter ls = AddVariable(fsParameterIdentifier.ls);
            IEquationParameter lOverB = AddVariable(fsParameterIdentifier.l_over_b);
            IEquationParameter lsOverB = AddVariable(fsParameterIdentifier.ls_over_b);

            IEquationParameter u = AddVariable(fsParameterIdentifier.u);
            IEquationParameter n = AddVariable(fsParameterIdentifier.RotationalSpeed);
            IEquationParameter tc = AddVariable(fsParameterIdentifier.CycleTime);
            IEquationParameter nsf = AddVariable(fsParameterIdentifier.nsf);
            IEquationParameter nsr = AddVariable(fsParameterIdentifier.nsr);
            IEquationParameter sf = AddVariable(fsParameterIdentifier.SpecificFiltrationTime);
            IEquationParameter sr = AddVariable(fsParameterIdentifier.SpecificResidualTime);
            IEquationParameter tr = AddVariable(fsParameterIdentifier.ResidualTime);

            IEquationParameter cakeHeigth = AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter filtrationTime = AddVariable(fsParameterIdentifier.FiltrationTime);

            IEquationParameter pressureDifference = AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter pc = AddVariable(fsParameterIdentifier.CakePermeability);

            IEquationParameter ttech0 = AddVariable(fsParameterIdentifier.StandardTechnicalTime);
            IEquationParameter ttech = AddVariable(fsParameterIdentifier.TechnicalTime);

            IEquationParameter filterArea = AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter filterAreaAs = AddVariable(fsParameterIdentifier.As);
            IEquationParameter machineWidth = AddVariable(fsParameterIdentifier.MachineWidth);
            IEquationParameter filterLength = AddVariable(fsParameterIdentifier.FilterLength);

            IEquationParameter qft = AddVariable(fsParameterIdentifier.qft);
            IEquationParameter qmft = AddVariable(fsParameterIdentifier.qmft);

            #region Help Parameters and Constants

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) {Value = fsValue.One};
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("1")) {Value = new fsValue(2)};
            
            IEquationParameter hcAddHce = AddVariable(new fsParameterIdentifier("hc + hce"));
            Equations.Add(new fsSumEquation(hcAddHce, cakeHeigth, hce0));
            
            IEquationParameter hcAdd2Hce = AddVariable(new fsParameterIdentifier("hc + 2 hce"));
            Equations.Add(new fsSumEquation(hcAdd2Hce, hcAddHce, hce0));

            IEquationParameter oneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
            Equations.Add(new fsSumEquation(constantOne, eps, oneMinusEps));
                        
            #endregion

            #endregion

            #region Equations Initialization

            Equations.Add(new fsAreaOfBeltWithReversibleTraysEquation(filterArea, lsOverB, ns, Qms, rhoCd, u, cakeHeigth));

            Equations.Add(new fsProductEquation(Qms, rhoS, Qs));
            Equations.Add(new fsProductEquation(Qmsus, rhoSus, Qsus));
            Equations.Add(new fsProductEquation(Qs, Qsus, cv));
            Equations.Add(new fsProductsEquation(
                new[] {ls, rhoCd, u, cakeHeigth},
                new[] {lsOverB, Qms}));
            Equations.Add(new fsProductsEquation(
                new[] { Qms, tc },
                new[] { rhoCd, filterArea, cakeHeigth }));
            Equations.Add(new fsProductEquation(lOverB, ns, lsOverB));
            Equations.Add(new fsProductEquation(nsf, ns, sf));
            Equations.Add(new fsFrom0AndDpEquation(pc, pc0, pressureDifference, nc));
            Equations.Add(new fsTechnicalTimeFrom0Equation(ttech, ttech0, filterAreaAs, lambda));
            Equations.Add(new fsProductEquation(filterArea, filterLength, machineWidth));
            Equations.Add(new fsProductsEquation(
                new[] { filterAreaAs, lsOverB },
                new[] { ls, ls }));
            Equations.Add(new fsProductsEquation(
                new[] { filterArea },
                new[] { lOverB, machineWidth, machineWidth }));
            Equations.Add(new fsProductEquation(filterLength, ns, ls));
            Equations.Add(new fsProductEquation(filterLength, tc, u));
            Equations.Add(new fsDivisionInverseEquation(tc, n));
            Equations.Add(new fsCakeHeightFromDpTf(cakeHeigth, hce0, pc, kappa, pressureDifference, filtrationTime, etaf));
            Equations.Add(new fsSumEquation(ns, nsf, nsr));
            Equations.Add(new fsSumEquation(tc, tr, filtrationTime));
            Equations.Add(new fsSumEquation(constantOne, sr, sf));
            Equations.Add(new fsTrSrTtechNsTcEquation(sr, tr, tc, ns, ttech));
            Equations.Add(new fsTfSfTtechNsTcEquation(sf, filtrationTime, tc, ns, ttech));
            Equations.Add(new fsProductEquation(ls, lsOverB, machineWidth));
            Equations.Add(new fsSfFromEtafHcHceKappaPcDpNsLsUTtechEquation(
                sf, etaf, cakeHeigth, hce0, kappa, pc, pressureDifference, ns, ls, u, ttech));
            Equations.Add(new fsUFromLsOverBQmsHcDpTtech0LambdaNsfMaterialEquation(
                u, lambda, nsf, lsOverB, Qms, rhoCd, cakeHeigth, etaf, hce0, kappa, pc, pressureDifference, ttech0));
            Equations.Add(new fsProductsEquation(
                new[] { qft, etaf, hcAdd2Hce },
                new[] { constantTwo, pc, pressureDifference }));
            Equations.Add(new fsProductEquation(qmft, qft, rhoF));

            #region Only Calculated Parameters

            IEquationParameter meanHeightRate = AddVariable(fsParameterIdentifier.MeanHeightRate);
            IEquationParameter hcOverTc = AddVariable(fsParameterIdentifier.HcOverTc);
            IEquationParameter diffHeightrate = AddVariable(fsParameterIdentifier.DiffHeightRate);
            Equations.Add(new fsProductEquation(cakeHeigth, filtrationTime, meanHeightRate));
            Equations.Add(new fsProductEquation(cakeHeigth, tc, hcOverTc));
            Equations.Add(new fsProductsEquation(
                new[] { diffHeightrate, etaf, hcAddHce },
                new[] { kappa, pressureDifference, pc }));

            IEquationParameter Ms = AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter Vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            IEquationParameter Msus = AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            Equations.Add(new fsProductsEquation(
                new[] { Ms },
                new[] { rhoCd, filterArea, cakeHeigth }));
            Equations.Add(new fsProductEquation(Ms, Msus, cm));
            Equations.Add(new fsProductEquation(Ms, Vs, rhoS));
            Equations.Add(new fsProductEquation(Vs, Vsus, cv));

            IEquationParameter msus = AddVariable(fsParameterIdentifier.SpecificSuspensionMass);
            IEquationParameter vsus = AddVariable(fsParameterIdentifier.SpecificSuspensionVolume);
            Equations.Add(new fsProductEquation(Msus, msus, filterArea));
            Equations.Add(new fsProductEquation(Vsus, vsus, filterArea));

            IEquationParameter qmsusd = AddVariable(fsParameterIdentifier.qmsusd);
            IEquationParameter qsusd = AddVariable(fsParameterIdentifier.qsusd);
            IEquationParameter Qmsusd = AddVariable(fsParameterIdentifier.Qmsusd);
            IEquationParameter Qsusd = AddVariable(fsParameterIdentifier.Qsusd);
            Equations.Add(new fsProductsEquation(
                new[] { qsusd, cv },
                new[] { oneMinusEps, diffHeightrate }));
            Equations.Add(new fsProductsEquation(
                new[] { qmsusd, cv },
                new[] { rhoCd, diffHeightrate }));
            Equations.Add(new fsProductEquation(Qmsusd, qmsusd, filterArea));
            Equations.Add(new fsProductEquation(Qsusd, qsusd, filterArea));

            IEquationParameter qmsust = AddVariable(fsParameterIdentifier.qmsust);
            IEquationParameter qsust = AddVariable(fsParameterIdentifier.qsust);
            IEquationParameter Qmsust = AddVariable(fsParameterIdentifier.Qmsust);
            IEquationParameter Qsust = AddVariable(fsParameterIdentifier.Qsust);
            Equations.Add(new fsProductsEquation(
                new[] { qmsust, cv },
                new[] { oneMinusEps, meanHeightRate }));
            Equations.Add(new fsProductEquation(qmsust, rhoSus, qsust));
            Equations.Add(new fsProductEquation(Qmsust, qmsust, filterArea));
            Equations.Add(new fsProductEquation(Qsust, qsust, filterArea));
            
            #endregion

            #endregion
        }
    }
}
