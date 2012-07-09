using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsMsusHcPlainAreaCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter filtrateDensity = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter suspensionDensity = calculator.AddVariable(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter solidsMassFraction = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsMassFraction);
            IEquationParameter solidsVolumeFraction = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter solidsConcentration = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsConcentration);

            IEquationParameter porosity = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);

            IEquationParameter filterArea = calculator.AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter cakeHeight = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter suspensionVolume = calculator.AddVariable(fsParameterIdentifier.SuspensionVolume);

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsMassConcentrationEquation(solidsMassFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsVolumeConcentrationEquation(solidsVolumeFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsConcentrationEquation(solidsConcentration, filtrateDensity, solidsDensity, suspensionDensity));

            calculator.AddEquation(new fsEpsKappaCvEquation(porosity, kappa, solidsVolumeFraction));

            calculator.AddEquation(new fsSuspensionMassFromHcEpsPlainAreaEquation(suspensionMass, porosity, solidsDensity, filterArea, cakeHeight, solidsMassFraction));
            calculator.AddEquation(new fsSuspensionVolumeFromHcEpsPlainAreaEquation(suspensionVolume, porosity, filterArea, cakeHeight, solidsVolumeFraction));
            calculator.AddEquation(new fsSuspensionVolumeFromHcKappaPlainAreaEquation(suspensionVolume, kappa, filterArea, cakeHeight));

            calculator.AddEquation(new fsProductEquation(suspensionMass, suspensionVolume, suspensionDensity));
            
            #endregion
        }
    }
}
