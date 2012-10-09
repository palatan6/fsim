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
            IEquationParameter kappa0 = AddConstant(fsParameterIdentifier.Kappa0);
            IEquationParameter area = AddConstant(fsParameterIdentifier.FilterArea);
            IEquationParameter rhosus = AddConstant(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            IEquationParameter solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            IEquationParameter cm = AddConstant(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter kappa = AddConstant(fsParameterIdentifier.Kappa);

            IEquationParameter pc0 = AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter rc0 = AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter alpha0 = AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter pc = AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc = AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter nc = AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter Dp = AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter hc = AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter formationTime = AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter solidsMass = AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            IEquationParameter liquidMass = AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            IEquationParameter suspensionMass = AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Rm = AddVariable(fsParameterIdentifier.FilterMediumResistanceRm);
            IEquationParameter Mf = AddVariable(fsParameterIdentifier.FiltrateMass);
            IEquationParameter mc = AddVariable(fsParameterIdentifier.CakeMass);
            IEquationParameter ms = AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter qmf = AddVariable(fsParameterIdentifier.qmf);
            IEquationParameter vsus = AddVariable(fsParameterIdentifier.SuspensionVolume);
            IEquationParameter vf = AddVariable(fsParameterIdentifier.FiltrateVolume);
            IEquationParameter vc = AddVariable(fsParameterIdentifier.CakeVolume);
            IEquationParameter vs = AddVariable(fsParameterIdentifier.SolidsVolume);
            IEquationParameter qf = AddVariable(fsParameterIdentifier.qf);
            IEquationParameter hce = AddVariable(fsParameterIdentifier.FilterMediumResistanceHce);
            IEquationParameter K = AddVariable(fsParameterIdentifier.PracticalCakePermeability);

            #endregion

            #region Help Parameters
            
            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };

            IEquationParameter onePlusKappa = AddVariable(new fsParameterIdentifier("1 + kappa"));
            Equations.Add(new fsSumEquation(onePlusKappa, constantOne, kappa));
            IEquationParameter oneMinusEps = AddVariable(new fsParameterIdentifier("1 - eps"));
            Equations.Add(new fsSumEquation(constantOne, oneMinusEps, eps));
            IEquationParameter oneMinusEps0 = AddVariable(new fsParameterIdentifier("1 - eps0"));
            Equations.Add(new fsSumEquation(constantOne, oneMinusEps0, eps0));            
            IEquationParameter hcPlusTwoTimesHce = AddVariable(new fsParameterIdentifier("hc+ 2*hce"));
            Equations.Add(new fsSumsEquation(
                new[] { hcPlusTwoTimesHce },
                new[] { hc, hce, hce }));

            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            AddEquation(new fsCakeHeightFromDpTf(hc, hce, pc, kappa0, Dp, formationTime, viscosity));
            
            Equations.Add(new fsProductsEquation(
                new[] { oneMinusEps0, solidsDensity, area, hc },
                new[] { cm, suspensionMass }));

            AddEquation(new fsProductEquation(solidsMass, suspensionMass, cm));
            AddEquation(new fsSumEquation(suspensionMass, solidsMass, liquidMass));
            AddEquation(new fsMcFromHcEquation(mc, area, hc, solidsDensity, eps, rho));
            Equations.Add(new fsProductsEquation(
               new[] { qmf, viscosity, hcPlusTwoTimesHce },
               new[] { rho, constantTwo, pc, Dp }));
            Equations.Add(new fsProductsEquation(
               new[] { qf, viscosity, hcPlusTwoTimesHce },
               new[] { constantTwo, pc, Dp }));
            Equations.Add(new fsProductsEquation(
               new[] { hc, hc },
               new[] { constantTwo, kappa, Dp, formationTime, K }));
            Equations.Add(new fsProductsEquation(
                new[] { K, viscosity, hcPlusTwoTimesHce },
                new[] { hc, pc }));
            AddEquation(new fsKfromPcAndRmEquation(K, hc, pc, viscosity, Rm));
            
            Equations.Add(new fsProductsEquation(
                new[] { area, hc, onePlusKappa },
                new[] { vsus, kappa }));
            Equations.Add(new fsProductEquation(hce, pc, Rm));
            Equations.Add(new fsProductsEquation(
                new[] { suspensionMass, kappa},
                new[] { rhosus, area, hc, onePlusKappa }));
            Equations.Add(new fsProductsEquation(
                new[] { Mf, kappa },
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

            Equations.Add(new fsProductsEquation(
                new[] { hc, qf },
                new[] { constantTwo, Dp, K }));
            Equations.Add(new fsProductsEquation(
                new[] { hc, qmf },
                new[] { constantTwo, Dp, K, rho }));

            AddEquation(new fsDivisionInverseEquation(pc0, rc0));
            AddEquation(new fsAlphaPcEquation(alpha0, pc0, eps0, solidsDensity));
            AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            AddEquation(new fsDivisionInverseEquation(pc, rc));
            AddEquation(new fsAlphaPcEquation(alpha, pc, eps, solidsDensity));
            
            #endregion
        }
    }
}

