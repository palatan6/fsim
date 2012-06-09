using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsAnalysisFiltrationCalculator : fsCalculator
    {
        public fsAnalysisFiltrationCalculator()
        {
            #region Parameters Initialization

            IEquationParameter rho_cd = AddConstant(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter c = AddConstant(fsParameterIdentifier.SuspensionSolidsConcentration);
            IEquationParameter tc = AddConstant(fsParameterIdentifier.CycleTime);

            IEquationParameter rho = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter eps = AddConstant(fsParameterIdentifier.CakePorosity);
            IEquationParameter viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            IEquationParameter area = AddConstant(fsParameterIdentifier.FilterArea);
            IEquationParameter rhosus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            IEquationParameter solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            IEquationParameter cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter kappa = AddConstant(fsParameterIdentifier.Kappa);
            IEquationParameter pc = AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter hc = AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            IEquationParameter liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            IEquationParameter suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Rm0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            IEquationParameter mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            IEquationParameter mc = AddVariable(fsParameterIdentifier.CakeMass);
            IEquationParameter ms = AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter qmft = AddVariable(fsParameterIdentifier.qmft);
            IEquationParameter Dp = AddVariable(fsParameterIdentifier.PressureDifference);
            IEquationParameter vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            IEquationParameter vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            IEquationParameter vc = AddVariable(fsParameterIdentifier.CakeVolume);
            IEquationParameter vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            IEquationParameter qft = AddVariable(fsParameterIdentifier.qft);
            IEquationParameter hce0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            IEquationParameter K = AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            IEquationParameter rc0 = AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter pc0 = AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter alpha0 = AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter rc = AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter nc = AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter pressure = AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter Qms = AddVariable(fsParameterIdentifier.Qms);
            IEquationParameter Qsus = AddVariable(fsParameterIdentifier.Qsus);
            IEquationParameter Qmsus = AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);
            IEquationParameter pcstar = AddVariable(fsParameterIdentifier.CakePlusMediumPermeability);
            IEquationParameter rcstar = AddVariable(fsParameterIdentifier.CakePlusMediumResistance);
            IEquationParameter alphastar = AddVariable(fsParameterIdentifier.CakePlusMediumResistanceAlpha);

            #endregion

            #region Help Parameters

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };

            IEquationParameter onePlusKappa = AddVariable(new fsParameterIdentifier("1 + kappa"));
            Equations.Add(new fsSumEquation(onePlusKappa, constantOne, kappa));
            IEquationParameter oneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
            Equations.Add(new fsSumEquation(constantOne, oneMinusEps, eps));
            
            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressure, nc));
            AddEquation(new fsCakeHeightFromDpTf(hc, hce0, pc, kappa0, pressure, formationTime, viscosity));
            AddEquation(new fsSuspensionMassFromHcEpsPlainAreaEquation(suspensionMass, eps0, solidsDensity, area, hc, cm));
            AddEquation(new fsProductEquation(solidsMass, suspensionMass, cm));
            AddEquation(new fsSumEquation(suspensionMass, solidsMass, liquidMass));
            AddEquation(new fsMcFromHcEquation(mc, area, hc, solidsDensity, eps, rho));
            AddEquation(new fsQmftFromHcRhoEtaDpEquation(qmft, rho, pc, Dp, viscosity, hc, hce0));
            AddEquation(new fsQftFromHcEtaDpEquation(qft, pc, Dp, viscosity, hc, hce0));
           
            Equations.Add(new fsProductsEquation(
                new[] { area, hc, onePlusKappa },
                new[] { vsus, kappa }));
            Equations.Add(new fsProductEquation(hce0, pc0, Rm0));
            Equations.Add(new fsProductsEquation(
                new[] { suspensionMass, kappa},
                new[] { rhosus, area, hc, onePlusKappa }));
            Equations.Add(new fsProductsEquation(
                new[] { mf, kappa },
                new[] { rho, area, hc }));
            Equations.Add(new fsProductsEquation(
                new[] { ms },
                new[] { area, hc, solidsDensity, oneMinusEps }));
            Equations.Add(new fsProductsEquation(
                new[] { vf, kappa },
                new[] { area, hc }));
            Equations.Add(new fsProductEquation(vc, area, hc));
            Equations.Add(new fsProductsEquation(
                new[] { vs },
                new[] { area, hc, oneMinusEps }));

            AddEquation(new fsDivisionInverseEquation(pc0, rc0));
            AddEquation(new fsAlphaPcEquation(alpha0, pc0, eps0, solidsDensity));
            AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressure, nc));
            AddEquation(new fsDivisionInverseEquation(pc, rc));
            AddEquation(new fsAlphaPcEquation(alpha, pc, eps, solidsDensity));

            AddEquation(new fsProductEquation(ms, c, vsus));
            AddEquation(new fsProductEquation(ms, cm, suspensionMass));
            AddEquation(new fsProductEquation(ms, solidsDensity, vs));
            AddEquation(new fsProductEquation(ms, tc, Qms));
            Equations.Add(new fsProductsEquation(
                new[] { ms },
                new[] { tc, c, Qsus }));
            Equations.Add(new fsProductsEquation(
                new[] { ms },
                new[] { tc, cm, Qmsus }));
            Equations.Add(new fsProductsEquation(
                new[] { qft, kappa, formationTime },
                new[] { hc }));
            Equations.Add(new fsProductEquation(qmft, rho, qft));
            Equations.Add(new fsProductsEquation(
                new[] { rho, hc, area },
                new[] { kappa, mf }));

            Equations.Add(new fsProductsEquation(
                new[] { formationTime, constantTwo, pressure },
                new[] { viscosity, kappa, formationTime, qft, qft }));
            Equations.Add(new fsProductsEquation(
                new[] { pcstar, constantTwo, pressure, kappa, formationTime },
                new[] { viscosity, hc, hc }));

            Equations.Add(new fsProductEquation(constantOne, rcstar, pcstar));
            Equations.Add(new fsProductsEquation(
                new[] { alphastar, rho_cd, pcstar },
                new[] { constantOne }));
            AddEquation(new fsPcFromPcstarEquation(pc, hc, hce0, pcstar));
            Equations.Add(new fsProductEquation(pcstar, K, viscosity));
            
            #endregion
        }
    }
}

