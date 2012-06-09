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

    public class fsPorosityCalculator : fsCalculator
    {
        public fsPorosityCalculator()
        {
            #region Parameters Initialization

            fsCalculatorConstant ne = AddConstant(fsParameterIdentifier.Ne);
            fsCalculatorConstant pressureDifference = AddConstant(fsParameterIdentifier.PressureDifference);
            fsCalculatorConstant volumeConcentration = AddConstant(fsParameterIdentifier.SuspensionSolidsVolumeFraction);
            fsCalculatorConstant solidsDensity = AddConstant(fsParameterIdentifier.SolidsDensity);
            fsCalculatorConstant filtrateDensity = AddConstant(fsParameterIdentifier.MotherLiquidDensity);
            fsCalculatorConstant solidsMass = AddConstant(fsParameterIdentifier.SolidsMass);
            fsCalculatorConstant filtrationArea = AddConstant(fsParameterIdentifier.FilterArea);
            fsCalculatorConstant cakeHight = AddConstant(fsParameterIdentifier.CakeHeight);
            fsCalculatorConstant fitrationTime = AddConstant(fsParameterIdentifier.FiltrationTime);
            fsCalculatorConstant qft = AddConstant(fsParameterIdentifier.qft);
            fsCalculatorConstant mf = AddConstant(fsParameterIdentifier.FiltrateMass);
            fsCalculatorConstant cakeMass = AddConstant(fsParameterIdentifier.CakeMass);

            fsCalculatorVariable porosity0 = AddVariable(fsParameterIdentifier.CakePorosity0);
            fsCalculatorVariable kappa0 = AddVariable(fsParameterIdentifier.Kappa0);
            fsCalculatorVariable cakeDrySolidsDensity0 = AddVariable(fsParameterIdentifier.DryCakeDensity0);

            fsCalculatorVariable porosity = AddVariable(fsParameterIdentifier.CakePorosity);
            fsCalculatorVariable kappa = AddVariable(fsParameterIdentifier.Kappa);
            fsCalculatorVariable cakeDrySolidsDensity = AddVariable(fsParameterIdentifier.DryCakeDensity);

            fsCalculatorVariable cakeWetDensity0 = AddVariable(fsParameterIdentifier.CakeWetDensity0);
            fsCalculatorVariable cakeMoistureContentRf0 = AddVariable(fsParameterIdentifier.CakeMoistureContentRf0);
            fsCalculatorVariable cakeWetMassSolidsFractionRs0 = AddVariable(fsParameterIdentifier.CakeWetMassSolidsFractionRs0);

            fsCalculatorVariable cakeWetDensity = AddVariable(fsParameterIdentifier.CakeWetDensity);
            fsCalculatorVariable cakeMoistureContentRf = AddVariable(fsParameterIdentifier.CakeMoistureContentRf);
            fsCalculatorVariable cakeWetMassSolidsFractionRs = AddVariable(fsParameterIdentifier.CakeWetMassSolidsFractionRs);

            #endregion

            #region Equations Initialization

            var one = new fsCalculatorConstant(new fsParameterIdentifier("one")) { Value = fsValue.One };
            
            AddEquation(new fsEpsKappaCvEquation(porosity0, kappa0, volumeConcentration));
            AddEquation(new fsCakeDrySolidsDensityEquation(cakeDrySolidsDensity0, porosity0, solidsDensity));

            AddEquation(new fsEpsKappaCvEquation(porosity, kappa, volumeConcentration));
            AddEquation(new fsCakeDrySolidsDensityEquation(cakeDrySolidsDensity, porosity, solidsDensity));

            AddEquation(new fsFrom0AndDpEquation(porosity, porosity0, pressureDifference, ne));

            AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(cakeMoistureContentRf0, porosity0, filtrateDensity, solidsDensity));
            AddEquation(new fsSumEquation(one, cakeMoistureContentRf0, cakeWetMassSolidsFractionRs0));
            AddEquation(new fsCakeWetDensityFromRhofRhosPorosityEquation(cakeWetDensity0, filtrateDensity, solidsDensity, porosity0));

            AddEquation(new fsMoistureContentFromDensitiesAndPorosityEquation(cakeMoistureContentRf, porosity, filtrateDensity, solidsDensity));
            AddEquation(new fsSumEquation(one, cakeMoistureContentRf, cakeWetMassSolidsFractionRs));
            AddEquation(new fsCakeWetDensityFromRhofRhosPorosityEquation(cakeWetDensity, filtrateDensity, solidsDensity, porosity));

            AddEquation(new fsEpsFromMsAndHcEquation(porosity, solidsMass, filtrationArea, solidsDensity, cakeHight));
            AddEquation(new fsEpsFromMsAndQfEquation(porosity, volumeConcentration, solidsMass, filtrationArea, solidsDensity, fitrationTime, qft));
            AddEquation(new fsEpsFromMsAndMfEquation(porosity, volumeConcentration, solidsMass, filtrateDensity, solidsDensity, mf));
            AddEquation(new fsEpsFromMcAndQfEquation(porosity, volumeConcentration, cakeMass, filtrationArea, filtrateDensity, solidsDensity, fitrationTime, qft));

            #endregion
        }
    }

}
