using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsLaboratoryFiltrationCalculator : fsCalculator
    {
        public fsLaboratoryFiltrationCalculator()
        {
            #region Parameters Initialization

            fsCalculatorConstant rho = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            fsCalculatorConstant eps = AddConstant(fsParameterIdentifier.CakePorosity);
            fsCalculatorConstant viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            fsCalculatorConstant pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            fsCalculatorConstant kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            fsCalculatorConstant pressure = AddConstant(fsParameterIdentifier.PressureDifference);
            IEquationParameter area = AddConstant(fsParameterIdentifier.FilterArea);
            fsCalculatorConstant nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
            fsCalculatorConstant rhosus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            fsCalculatorConstant eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            fsCalculatorConstant solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            fsCalculatorConstant cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter kappa = AddConstant(fsParameterIdentifier.Kappa);
            fsCalculatorVariable pc = AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter hc = AddVariable(fsParameterIdentifier.CakeHeight);
            fsCalculatorVariable formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            fsCalculatorVariable solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            fsCalculatorVariable liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            fsCalculatorVariable suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            fsCalculatorVariable Rm0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm0);
            fsCalculatorVariable mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            fsCalculatorVariable mc = AddVariable(fsParameterIdentifier.CakeMass);
            fsCalculatorVariable ms = AddVariable(fsParameterIdentifier.SolidsMass);
            fsCalculatorVariable qmft = AddVariable(fsParameterIdentifier.qmft);
            fsCalculatorVariable Dp = AddVariable(fsParameterIdentifier.PressureDifference);
            IEquationParameter vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            fsCalculatorVariable vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            fsCalculatorVariable vc = AddVariable(fsParameterIdentifier.CakeVolume);
            fsCalculatorVariable vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            fsCalculatorVariable qft = AddVariable(fsParameterIdentifier.qft);
            fsCalculatorVariable hce0 = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            fsCalculatorVariable K = AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };

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
            //AddEquation(new fsRm0FromHcePc0Equation(Rm0, hce0, pc0));
            //AddEquation(new fsMsusFromHcRhosusAKappaEquation(suspensionMass, rhosus, area, hc, kappa));
            //AddEquation(new fsMfFromHcEquation(mf, rho, area, hc, kappa));
            AddEquation(new fsMcFromHcEquation(mc, area, hc, solidsDensity, eps, rho));
            //AddEquation(new fsMsFromHcEquation(ms, area, hc, solidsDensity, eps));
            AddEquation(new fsQmftFromHcRhoEtaDpEquation(qmft, rho, pc, Dp, viscosity, hc, hce0));
            //AddEquation(new fsVsusFromHcAKappaEquation(vsus, area, hc, kappa));
            //AddEquation(new fsVfFromHcEquation(vf, area, hc, kappa));
            //AddEquation(new fsVcFromHcEquation(vc, area, hc));
            AddEquation(new fsVsFromHcEquation(vs, area, hc, eps));
            AddEquation(new fsQftFromHcEtaDpEquation(qft, pc, Dp, viscosity, hc, hce0));
            AddEquation(new fsKFromPcEquation(K, hc, pc, viscosity, hce0));

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
            
            #endregion
        }
    }
}

