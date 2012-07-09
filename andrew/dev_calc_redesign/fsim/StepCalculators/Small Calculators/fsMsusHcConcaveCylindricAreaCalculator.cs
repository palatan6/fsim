using Equations;
using Parameters;

namespace StepCalculators
{
    public class fsMsusHcConcaveCylindricAreaCalculator : fsCalculatorEquationsList
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
            IEquationParameter filterDiameter = calculator.AddVariable(fsParameterIdentifier.MachineDiameter);
            IEquationParameter filterB = calculator.AddVariable(fsParameterIdentifier.MachineWidth);
            IEquationParameter filterBOverDiameter = calculator.AddVariable(fsParameterIdentifier.WidthOverDiameterRatio);

            IEquationParameter cakeHeight = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter suspensionVolume = calculator.AddVariable(fsParameterIdentifier.SuspensionVolume);

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsMassConcentrationEquation(solidsMassFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsVolumeConcentrationEquation(solidsVolumeFraction, filtrateDensity, solidsDensity, suspensionDensity));
            calculator.AddEquation(new fsConcentrationEquation(solidsConcentration, filtrateDensity, solidsDensity, suspensionDensity));

            calculator.AddEquation(new fsEpsKappaCvEquation(porosity, kappa, solidsVolumeFraction));
            calculator.AddEquation(new fsCylinderAreaEquation(filterArea, filterDiameter, filterB));
            calculator.AddEquation(new fsProductEquation(filterB, filterDiameter, filterBOverDiameter));

            calculator.AddEquation(new fsSuspensionMassFromHcEpsConcaveCylindircAreaEquation(suspensionMass, porosity, solidsDensity, filterArea, filterDiameter, cakeHeight, solidsMassFraction));
            //calculator.AddEquation(new fsSuspensionVolumeFromHcEpsConvexCylindircAreaEquation(suspensionVolume, porosity, filterArea, cakeHeight, solidsVolumeFraction));
            //calculator.AddEquation(new fsSuspensionVolumeFromHcKappaConvexCylindircAreaEquation(suspensionVolume, kappa, filterArea, cakeHeight));

            calculator.AddEquation(new fsProductEquation(suspensionMass, suspensionVolume, suspensionDensity));
            
            #endregion
        }
    }
}
