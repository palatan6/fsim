using Equations;
using Equations.Belt_Filters_with_Reversible_Trays;
using Equations.Material.Cake_Moisture_Content_Rf_Equations;
using Equations.Material.Eps_Kappa_Equations;
using Parameters;
using Value;

namespace StepCalculators
{
    public class fsAnalysisFiltrationCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter cv = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);

            IEquationParameter c = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsConcentration);
            IEquationParameter tc = calculator.AddVariable(fsParameterIdentifier.CycleTime);

            IEquationParameter rho = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter viscosity = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter area = calculator.AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter rhosus = calculator.AddVariable(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter cm = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter hc = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter formationTime = calculator.AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter solidsMass = calculator.AddVariable(fsParameterIdentifier.SolidsMassInSuspension);
            IEquationParameter liquidMass = calculator.AddVariable(fsParameterIdentifier.LiquidMassInSuspension);
            IEquationParameter suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter Rm = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceRm);
            IEquationParameter mf = calculator.AddVariable(fsParameterIdentifier.FiltrateMass);
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

            IEquationParameter rc0 = calculator.AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter alpha0 = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);
            IEquationParameter rc = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter nc = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter Dp = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter Qms = calculator.AddVariable(fsParameterIdentifier.Qms);
            IEquationParameter Qsus = calculator.AddVariable(fsParameterIdentifier.Qsus);
            IEquationParameter Qmsus = calculator.AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);
            IEquationParameter pcstar = calculator.AddVariable(fsParameterIdentifier.CakePlusMediumPermeability);
            IEquationParameter rcstar = calculator.AddVariable(fsParameterIdentifier.CakePlusMediumResistance);
            IEquationParameter alphastar = calculator.AddVariable(fsParameterIdentifier.CakePlusMediumResistanceAlpha);

            IEquationParameter ne = calculator.AddVariable(fsParameterIdentifier.Ne);

            IEquationParameter eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter eps0 = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);
            IEquationParameter kappa0 = calculator.AddVariable(fsParameterIdentifier.Kappa0);
            IEquationParameter rho_cd = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity);
            IEquationParameter rho_cd0 = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity0);
            IEquationParameter Rf = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
            IEquationParameter Rf0 = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf0);

            IEquationParameter n = calculator.AddVariable(fsParameterIdentifier.RotationalSpeed);
            IEquationParameter tr = calculator.AddVariable(fsParameterIdentifier.ResidualTime);
            IEquationParameter sf = calculator.AddVariable(fsParameterIdentifier.SpecificFiltrationTime);
            
            #endregion

            #region Help Parameters

            var constantOne = new fsCalculatorConstant(new fsParameterIdentifier("1")) { Value = fsValue.One };
            var constantTwo = new fsCalculatorConstant(new fsParameterIdentifier("2")) { Value = new fsValue(2) };
            
            IEquationParameter onePlusKappa = calculator.AddVariable(new fsParameterIdentifier("1 + kappa"));
            calculator.AddEquation(new fsSumEquation(onePlusKappa, constantOne, kappa));
            IEquationParameter oneMinusEps = calculator.AddVariable(new fsParameterIdentifier("1 - eps"));
            calculator.AddEquation(new fsSumEquation(constantOne, oneMinusEps, eps));
            IEquationParameter hcPlusTwoTimesHce = calculator.AddVariable(new fsParameterIdentifier("hc+ 2*hce"));
            calculator.AddEquation(new fsSumsEquation(
                new[] { hcPlusTwoTimesHce },
                new[] { hc, hce, hce }));
            
            #endregion

            #region Equations Initialization

            #region Porosity Equations

            calculator.AddEquation(new fsEpsKappaCvEquation(eps0, kappa0, cv));
            calculator.AddEquation(new fsCakeDrySolidsDensityEquation(rho_cd0, eps0, solidsDensity));

            calculator.AddEquation(new fsEpsKappaCvEquation(eps, kappa, cv));
            calculator.AddEquation(new fsCakeDrySolidsDensityEquation(rho_cd, eps, solidsDensity));

            calculator.AddEquation(new fsFrom0AndDpEquation(eps, eps0, Dp, ne));

            calculator.AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(Rf0, eps0, rho, solidsDensity));
                        
            calculator.AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(Rf, eps, rho, solidsDensity));
                       
            calculator.AddEquation(new fsEpsFromMsAndHcEquation(eps, solidsMass, area, solidsDensity, hc));
            calculator.AddEquation(new fsEpsFromMsAndQfEquation(eps, cv, solidsMass, area, solidsDensity, formationTime, qf));
            calculator.AddEquation(new fsEpsFromMsAndMfEquation(eps, cv, solidsMass, formationTime, solidsDensity, mf));
            calculator.AddEquation(new fsEpsFromMcAndQfEquation(eps, cv, mc, area, rho, solidsDensity, formationTime, qf));

            #endregion

            #region Cake Hight Equations

            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, Dp, nc));
            calculator.AddEquation(new fsCakeHeightFromDpTf(hc, hce, pc, kappa0, Dp, formationTime, viscosity));
            calculator.AddEquation(new fsHcFromMsAndPcstarEquation(hc, ms, area, solidsDensity, cv, Dp, formationTime, pcstar, viscosity));
            calculator.AddEquation(new fsHcFromMcAndPcstarEquation(hc, mc, area, solidsDensity, cv, Dp, formationTime, pcstar, rho, viscosity));
            calculator.AddEquation(new fsHcFromMsAndAlphastarEquation(hc, ms, area, solidsDensity, cv, Dp, formationTime, alphastar, viscosity));
            calculator.AddEquation(new fsHcFromMcAndAlphastarEquations(hc, mc, area, solidsDensity, cv, Dp, formationTime, alphastar, viscosity, rho));

            calculator.AddEquation(new fsProductsEquation(
                new[] { oneMinusEps, solidsDensity, area, hc },
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
                new[] { area, hc, onePlusKappa },
                new[] { vsus, kappa }));
            calculator.AddEquation(new fsProductEquation(hce, pc, Rm));
            calculator.AddEquation(new fsProductsEquation(
                new[] { suspensionMass, kappa},
                new[] { rhosus, area, hc, onePlusKappa }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { mf, kappa },
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

            calculator.AddEquation(new fsProductEquation(ms, c, vsus));
            calculator.AddEquation(new fsProductEquation(ms, cm, suspensionMass));
            calculator.AddEquation(new fsProductEquation(ms, solidsDensity, vs));
            calculator.AddEquation(new fsProductEquation(ms, tc, Qms));
            calculator.AddEquation(new fsProductsEquation(
                new[] { ms },
                new[] { tc, c, Qsus }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { ms },
                new[] { tc, cm, Qmsus }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qf, kappa, formationTime },
                new[] { hc }));
            calculator.AddEquation(new fsProductEquation(qmf, rho, qf));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qf, area, rho, formationTime },
                new[] { mf }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qmf, area, formationTime },
                new[] { mf }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qf, area, formationTime },
                new[] { vf }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { qmf, area, formationTime },
                new[] { vf, rho }));
            
            calculator.AddEquation(new fsProductsEquation(
                new[] { pcstar, constantTwo, Dp },
                new[] { viscosity, kappa, formationTime, qf, qf }));
            calculator.AddEquation(new fsProductsEquation(
                new[] { pcstar, constantTwo, Dp, kappa, formationTime },
                new[] { viscosity, hc, hc }));

            calculator.AddEquation(new fsProductEquation(constantOne, rcstar, pcstar));
            calculator.AddEquation(new fsProductsEquation(
                new[] { alphastar, rho_cd, pcstar },
                new[] { constantOne }));
            calculator.AddEquation(new fsPcFromPcstarEquation(pc, hc, hce, pcstar));
            calculator.AddEquation(new fsProductEquation(pcstar, K, viscosity));
            
            #endregion

            #region Time Equations 

            calculator.AddEquation(new fsDivisionInverseEquation(n, tc));
            calculator.AddEquation(new fsSumEquation(tc, tr, formationTime));
            calculator.AddEquation(new fsProductEquation(formationTime, tc, sf));

            #endregion

            #endregion
        }
    }
}

