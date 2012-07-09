using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsDensityConcentrationCalculator : fsCalculatorEquationsList
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

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsMassConcentrationEquation(solidsMassFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsVolumeConcentrationEquation(solidsVolumeFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsConcentrationEquation(solidsConcentration, filtrateDensity, solidsDensity, suspensionDensity));

            #endregion
        }
    }
}
