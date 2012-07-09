using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsCakeFormationDpConstCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter suspensionDensity = calculator.AddVariable(fsParameterIdentifier.SuspensionDensity);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter etaf = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter hce0 = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);
            IEquationParameter porosity0 = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);

            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter ne = calculator.AddVariable(fsParameterIdentifier.Ne);
            IEquationParameter nc = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter volumeConcentration = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);

            IEquationParameter filterArea = calculator.AddVariable(fsParameterIdentifier.FilterArea);

            IEquationParameter pressure = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter cycleTime = calculator.AddVariable(fsParameterIdentifier.CycleTime);
            IEquationParameter rotationalSpeed = calculator.AddVariable(fsParameterIdentifier.RotationalSpeed);

            IEquationParameter formationRelativeTime = calculator.AddVariable(fsParameterIdentifier.SpecificFiltrationTime);
            IEquationParameter formationTime = calculator.AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter cakeHeight = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter suspensionMass = calculator.AddVariable(fsParameterIdentifier.SuspensionMass);
            IEquationParameter suspensionVolume = calculator.AddVariable(fsParameterIdentifier.SuspensionVolume);
            IEquationParameter suspensionMassFlowrate = calculator.AddVariable(fsParameterIdentifier.SuspensionMassFlowrate);

            IEquationParameter porosity = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsDivisionInverseEquation(cycleTime, rotationalSpeed));
            calculator.AddEquation(new fsDivisionInverseEquation(rotationalSpeed, cycleTime));
            calculator.AddEquation(new fsProductEquation(formationTime, formationRelativeTime, cycleTime));
            calculator.AddEquation(new fsCakeHeightFromDpTf(cakeHeight, hce0, pc, kappa, pressure, formationTime, etaf));
            calculator.AddEquation(new fsVsusFromAreaAndCakeHeightEquation(suspensionVolume, filterArea, cakeHeight, kappa));
            calculator.AddEquation(new fsProductEquation(suspensionMass, suspensionDensity, suspensionVolume));
            calculator.AddEquation(new fsFrom0AndDpEquation(porosity, porosity0, pressure, ne));
            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressure, nc));
            calculator.AddEquation(new fsEpsKappaCvEquation(porosity, kappa, volumeConcentration));
            calculator.AddEquation(new fsAlphaPcEquation(alpha, pc, porosity, solidsDensity));
            calculator.AddEquation(new fsDivisionInverseEquation(rc, pc));
            calculator.AddEquation(new fsProductEquation(suspensionMass, suspensionMassFlowrate, cycleTime));

            #endregion
        }
    }
}
