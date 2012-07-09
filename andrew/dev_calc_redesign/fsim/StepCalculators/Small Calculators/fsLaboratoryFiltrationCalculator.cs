using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsLaboratoryFiltrationCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter rho = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter viscosity = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter kappa0 = calculator.AddVariable(fsParameterIdentifier.Kappa0);
            IEquationParameter area = calculator.AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter rhosus = calculator.AddVariable(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter eps0 = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter cm = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);

            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter rc0 = calculator.AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter alpha0 = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter nc = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter Dp = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter hc = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter formationTime = calculator.AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter solidsMass = calculator.AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            IEquationParameter liquidMass = calculator.AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            IEquationParameter suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Rm = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceRm);
            IEquationParameter Mf = calculator.AddVariable(fsParameterIdentifier.FiltrateMass);
            IEquationParameter mc = calculator.AddVariable(fsParameterIdentifier.CakeMass);
            IEquationParameter ms = calculator.AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter qmf = calculator.AddVariable(fsParameterIdentifier.qmf);
            IEquationParameter vsus = calculator.AddVariable(fsParameterIdentifier.SuspensionVolume);
            IEquationParameter vf = calculator.AddVariable(fsParameterIdentifier.FiltrateVolume);
            IEquationParameter vc = calculator.AddVariable(fsParameterIdentifier.CakeVolume);
            IEquationParameter vs = calculator.AddVariable(fsParameterIdentifier.SolidsVolume);
            IEquationParameter qf = calculator.AddVariable(fsParameterIdentifier.qf);
            IEquationParameter hce = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce);
            IEquationParameter K = calculator.AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            #endregion

            #region Help Parameters
            
            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };

            IEquationParameter onePlusKappa = calculator.AddVariable(new fsParameterIdentifier("1 + kappa"));
            calculator.AddEquation(new fsSumEquation(onePlusKappa, constantOne, kappa));
            IEquationParameter oneMinusEps = calculator.AddVariable(new fsParameterIdentifier("1 - eps"));
            calculator.AddEquation(new fsSumEquation(constantOne, oneMinusEps, eps));
            IEquationParameter oneMinusEps0 = calculator.AddVariable(new fsParameterIdentifier("1 - eps0"));
            calculator.AddEquation(new fsSumEquation(constantOne, oneMinusEps0, eps0));            
            IEquationParameter hcPlusTwoTimesHce = calculator.AddVariable(new fsParameterIdentifier("hc+ 2*hce"));
            calculator.AddEquation(new fsSumsEquation(
                new[] { hcPlusTwoTimesHce },
                new[] { hc, hce, hce }));

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            calculator.AddEquation(new fsCakeHeightFromDpTf(hc, hce, pc, kappa0, Dp, formationTime, viscosity));
            
            calculator.AddEquation(new fsProductsEquation(
                new[] { oneMinusEps0, solidsDensity, area, hc },
                new[] { cm, suspensionMass }));

            calculator.AddEquation(new fsProductEquation(solidsMass, suspensionMass, cm));
            calculator.AddEquation(new fsSumEquation(suspensionMass, solidsMass, liquidMass));
            calculator.AddEquation(new fsMcFromHcEquation(mc, area, hc, solidsDensity, eps, rho));
            calculator.AddEquation(new fsProductsEquation(
               new[] { qmf, viscosity, hcPlusTwoTimesHce },
               new[] { rho, constantTwo, pc, Dp }));
            calculator.AddEquation(new fsProductsEquation(
               new[] { qf, viscosity, hcPlusTwoTimesHce },
               new[] { constantTwo, pc, Dp }));
            calculator.AddEquation(new fsProductsEquation(
               new[] { hc, hc },
               new[] { constantTwo, kappa, Dp, formationTime, K }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { K, viscosity, hcPlusTwoTimesHce },
                new[] { hc, pc }));
            calculator.AddEquation(new fsKfromPcAndRmEquation(K, hc, pc, viscosity, Rm));
            
            calculator.AddEquation(new fsProductsEquation(
                new[] { area, hc, onePlusKappa },
                new[] { vsus, kappa }));
            calculator.AddEquation(new fsProductEquation(hce, pc, Rm));
            calculator.AddEquation(new fsProductsEquation(
                new[] { suspensionMass, kappa},
                new[] { rhosus, area, hc, onePlusKappa }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { Mf, kappa },
                new[] { rho, area, hc }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { ms },
                new[] { area, hc, solidsDensity, oneMinusEps }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { vf, kappa },
                new[] { area, hc }));
            calculator.AddEquation(new fsProductEquation(vc, area, hc));
            calculator.AddEquation(new fsProductsEquation(
                new[] { vs },
                new[] { area, hc, oneMinusEps }));

            calculator.AddEquation(new fsDivisionInverseEquation(pc0, rc0));
            calculator.AddEquation(new fsAlphaPcEquation(alpha0, pc0, eps0, solidsDensity));
            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            calculator.AddEquation(new fsDivisionInverseEquation(pc, rc));
            calculator.AddEquation(new fsAlphaPcEquation(alpha, pc, eps, solidsDensity));
            
            #endregion
        }
    }
}

