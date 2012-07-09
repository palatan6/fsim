using Equations;
using Parameters;
using Equations.Material.Cake_Moisture_Content_Rf_Equations;
using Equations.Material.Eps_Kappa_Equations;
using Value;

namespace StepCalculators
{
    /*
     * It's a general solution for calculating of
     * Pc0, rc0, alpha0, Pc, rc, alpha fromm Dp and nc
     * 
     * */

    public class fsPorosityCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter ne = calculator.AddVariable(fsParameterIdentifier.Ne);
            IEquationParameter pressureDifference = calculator.AddVariable(fsParameterIdentifier.PressureDifference);
            IEquationParameter volumeConcentration = calculator.AddVariable(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter filtrateDensity = calculator.AddVariable(fsParameterIdentifier.MotherLiquidDensity);
            IEquationParameter solidsMass = calculator.AddVariable(fsParameterIdentifier.SolidsMass);
            IEquationParameter filtrationArea = calculator.AddVariable(fsParameterIdentifier.FilterArea);
            IEquationParameter cakeHight = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter fitrationTime = calculator.AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter qf = calculator.AddVariable(fsParameterIdentifier.qf);
            IEquationParameter mf = calculator.AddVariable(fsParameterIdentifier.FiltrateMass);
            IEquationParameter cakeMass = calculator.AddVariable(fsParameterIdentifier.CakeMass);

            IEquationParameter porosity0 = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter kappa0 = calculator.AddVariable(fsParameterIdentifier.Kappa0);
            IEquationParameter cakeDrySolidsDensity0 = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity0);

            IEquationParameter porosity = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter kappa = calculator.AddVariable(fsParameterIdentifier.Kappa);
            IEquationParameter cakeDrySolidsDensity = calculator.AddVariable(fsParameterIdentifier.DryCakeDensity);

            IEquationParameter cakeWetDensity0 = calculator.AddVariable(fsParameterIdentifier.CakeWetDensity0);
            IEquationParameter cakeMoistureContentRf0 = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf0);
            IEquationParameter cakeWetMassSolidsFractionRs0 = calculator.AddVariable(fsParameterIdentifier.CakeWetMassSolidsFractionRs0);

            IEquationParameter cakeWetDensity = calculator.AddVariable(fsParameterIdentifier.CakeWetDensity);
            IEquationParameter cakeMoistureContentRf = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
            IEquationParameter cakeWetMassSolidsFractionRs = calculator.AddVariable(fsParameterIdentifier.CakeWetMassSolidsFractionRs);

            #endregion

            #region Equations Initialization

            var one = new fsCalculatorConstant(new fsParameterIdentifier("one")) { Value = fsValue.One };
            
            calculator.AddEquation(new fsEpsKappaCvEquation(porosity0, kappa0, volumeConcentration));
            calculator.AddEquation(new fsCakeDrySolidsDensityEquation(cakeDrySolidsDensity0, porosity0, solidsDensity));

            calculator.AddEquation(new fsEpsKappaCvEquation(porosity, kappa, volumeConcentration));
            calculator.AddEquation(new fsCakeDrySolidsDensityEquation(cakeDrySolidsDensity, porosity, solidsDensity));

            calculator.AddEquation(new fsFrom0AndDpEquation(porosity, porosity0, pressureDifference, ne));

            calculator.AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(cakeMoistureContentRf0, porosity0, filtrateDensity, solidsDensity));
            calculator.AddEquation(new fsSumEquation(one, cakeMoistureContentRf0, cakeWetMassSolidsFractionRs0));
            calculator.AddEquation(new fsCakeWetDensityFromRhofRhosPorosityEquation(cakeWetDensity0, filtrateDensity, solidsDensity, porosity0));

            calculator.AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(cakeMoistureContentRf, porosity, filtrateDensity, solidsDensity));
            calculator.AddEquation(new fsSumEquation(one, cakeMoistureContentRf, cakeWetMassSolidsFractionRs));
            calculator.AddEquation(new fsCakeWetDensityFromRhofRhosPorosityEquation(cakeWetDensity, filtrateDensity, solidsDensity, porosity));

            calculator.AddEquation(new fsEpsFromMsAndHcEquation(porosity, solidsMass, filtrationArea, solidsDensity, cakeHight));
            calculator.AddEquation(new fsEpsFromMsAndQfEquation(porosity, volumeConcentration, solidsMass, filtrationArea, solidsDensity, fitrationTime, qf));
            calculator.AddEquation(new fsEpsFromMsAndMfEquation(porosity, volumeConcentration, solidsMass, filtrateDensity, solidsDensity, mf));
            calculator.AddEquation(new fsEpsFromMcAndQfEquation(porosity, volumeConcentration, cakeMass, filtrationArea, filtrateDensity, solidsDensity, fitrationTime, qf));

            #endregion
        }
    }

}
