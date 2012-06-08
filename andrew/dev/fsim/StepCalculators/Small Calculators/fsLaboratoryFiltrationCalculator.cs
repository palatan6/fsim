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

            IEquationParameter rho = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter eps = AddConstant(fsParameterIdentifier.CakePorosity);
            IEquationParameter viscosity = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter pc0 = AddConstant(fsParameterIdentifier.CakePermeability0);
            IEquationParameter kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            IEquationParameter pressure = AddConstant(fsParameterIdentifier.PressureDifference);
            IEquationParameter area = AddConstant(fsParameterIdentifier.FilterArea);
            IEquationParameter nc = AddConstant(fsParameterIdentifier.CakeCompressibility);
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

            #endregion

            #region Help Parameters

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
            AddEquation(new fsMcFromHcEquation(mc, area, hc, solidsDensity, eps, rho));
            AddEquation(new fsQmftFromHcRhoEtaDpEquation(qmft, rho, pc, Dp, viscosity, hc, hce0));
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
            Equations.Add(new fsProductsEquation(
                new[] { vs },
                new[] { area, hc, oneMinusEps }));
            
            #endregion
        }
    }
}

