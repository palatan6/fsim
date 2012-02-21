﻿using Equations;
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

            IEquationParameter etaf = AddConstant(fsParameterIdentifier.ViscosityFiltrate);
            IEquationParameter rho_s = AddConstant(fsParameterIdentifier.SolidsDensity);
            IEquationParameter rho_f = AddConstant(fsParameterIdentifier.FiltrateDensity);
            IEquationParameter rho_sus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter cv = AddConstant(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            IEquationParameter Pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            IEquationParameter ne = AddConstant(fsParameterIdentifier.Ne);
            IEquationParameter nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter hce0 = AddConstant(fsParameterIdentifier.FilterMediumResistanceHce0);
            IEquationParameter ttech0 = AddConstant(fsParameterIdentifier.ttech0);
            IEquationParameter lambda = AddConstant(fsParameterIdentifier.lambda);

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

            IEquationParameter Dp = AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter eps = AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter rho_cd = AddVariable(fsParameterIdentifier.CakeDrySolidsDensity);
            IEquationParameter kappa = AddVariable(fsParameterIdentifier.Kappa);
            IEquationParameter Pc = AddVariable(fsParameterIdentifier.CakePermeability);

            IEquationParameter ttech = AddVariable(fsParameterIdentifier.TechnicalTime);

            IEquationParameter filterArea = AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter As = AddVariable(fsParameterIdentifier.As);
            IEquationParameter machineWidth = AddVariable(fsParameterIdentifier.MachineWidth);
            IEquationParameter filterLength = AddVariable(fsParameterIdentifier.FilterLength);

            IEquationParameter qft = AddVariable(fsParameterIdentifier.qft);
            IEquationParameter qmft = AddVariable(fsParameterIdentifier.qmft);
            
            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) {Value = fsValue.One};
            
            #endregion

            #region Equations Initialization

            Equations.Add(new fsAreaOfBeltWithReversibleTraysEquation(filterArea, lsOverB, ns, Qms, rho_cd, u, cakeHeigth));

            Equations.Add(new fsProductEquation(Qms, rho_s, Qs));
            Equations.Add(new fsProductEquation(Qmsus, rho_sus, Qsus));
            Equations.Add(new fsProductEquation(Qs, Qsus, cv));
            Equations.Add(new fsProductsEquation(
                new IEquationParameter[] {ls, rho_cd, u, cakeHeigth},
                new IEquationParameter[] {lsOverB, Qms}));
            Equations.Add(new fsProductEquation(lOverB, ns, lsOverB));
            Equations.Add(new fsProductEquation(nsf, ns, sf));
            Equations.Add(new fsFrom0AndDpEquation(eps, eps0, Dp, ne));
            Equations.Add(new fsFrom0AndDpEquation(Pc, Pc0, Dp, nc));
            Equations.Add(new fsEpsKappaCvEquation(eps, kappa, cv));
            Equations.Add(new fsCakeDrySolidsDensityEquation(rho_cd, eps, rho_s));
            Equations.Add(new fsTechnicalTimeFrom0Equation(ttech, ttech0, As, lambda));
            Equations.Add(new fsProductsEquation(
                new IEquationParameter[] { As, lsOverB },
                new IEquationParameter[] { ls, ls }));
            Equations.Add(new fsProductEquation(filterLength, ns, ls));
            Equations.Add(new fsProductEquation(filterLength, tc, u));
            Equations.Add(new fsDivisionInverseEquation(tc, n));
            Equations.Add(new fsCakeHeightFromDpTf(cakeHeigth, hce0, Pc, kappa, Dp, filtrationTime, etaf));
            Equations.Add(new fsSumEquation(ns, nsf, nsr));
            Equations.Add(new fsSumEquation(tc, tr, filtrationTime));
            Equations.Add(new fsSumEquation(constantOne, sr, sf));
            Equations.Add(new fsProductEquation(tr, tc, sr));
            Equations.Add(new fsProductEquation(filtrationTime, tc, sf));
            Equations.Add(new fsProductEquation(ls, lsOverB, machineWidth));
            Equations.Add(new fsSfFromEtafHcHceKappaPcDpNsLsUTtechEquation(
                sf, etaf, cakeHeigth, hce0, kappa, Pc, Dp, ns, ls, u, ttech));
            Equations.Add(new fsUFromLsOverBQmsHcDpTtech0LambdaNsfMaterialEquation(
                u, lambda, nsf, lsOverB, Qms, rho_cd, cakeHeigth, etaf, hce0, kappa, Pc, Dp, ttech0));
            
            Equations.Add(new fsProductsEquation(
                new IEquationParameter[] { qft, kappa, filtrationTime },
                new IEquationParameter[] { cakeHeigth }));
            Equations.Add(new fsProductEquation(qmft, qft, rho_f));

            #endregion
        }
    }
}
