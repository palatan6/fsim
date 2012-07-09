using Parameters;
using Equations.Material;

namespace StepCalculators
{
    public class fsRfFromCakeSaturationCalculator : fsCalculatorEquationsList
    {
        public override void AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter liquidDensity = calculator.AddVariable(fsParameterIdentifier.LiquidDensity);
            IEquationParameter solidsDensity = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter cakePorosity = calculator.AddVariable(fsParameterIdentifier.CakePorosity);
            IEquationParameter cakeSaturation = calculator.AddVariable(fsParameterIdentifier.CakeSaturation);
            IEquationParameter cakeMoistureContent = calculator.AddVariable(fsParameterIdentifier.CakeMoistureContentRf);

            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsMoistureContentFromCakeSaturationEquation(liquidDensity, solidsDensity, cakePorosity, cakeMoistureContent, cakeSaturation));

            #endregion
        }
    }
}
