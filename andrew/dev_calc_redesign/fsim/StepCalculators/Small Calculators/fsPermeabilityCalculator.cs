using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsPermeabilityCalculator : fsCalculatorEquationsList
    {
        public override void  AddToCalculator(fsCalculator calculator)
        {
            #region Parameters Initialization

            IEquationParameter rhoS = calculator.AddVariable(fsParameterIdentifier.SolidsDensity);
            IEquationParameter eps0 = calculator.AddVariable(fsParameterIdentifier.CakePorosity0);
            IEquationParameter eps = calculator.AddVariable(fsParameterIdentifier.CakePorosity);

            IEquationParameter hc = calculator.AddVariable(fsParameterIdentifier.CakeHeight);
            IEquationParameter eta = calculator.AddVariable(fsParameterIdentifier.MotherLiquidViscosity);
            IEquationParameter hce0 = calculator.AddVariable(fsParameterIdentifier.FilterMediumResistanceHce0);

            IEquationParameter nc = calculator.AddVariable(fsParameterIdentifier.CakeCompressibility);
            IEquationParameter pressure = calculator.AddVariable(fsParameterIdentifier.PressureDifference);

            IEquationParameter pc0 = calculator.AddVariable(fsParameterIdentifier.CakePermeability0);
            IEquationParameter rc0 = calculator.AddVariable(fsParameterIdentifier.CakeResistance0);
            IEquationParameter alpha0 = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);

            IEquationParameter pc = calculator.AddVariable(fsParameterIdentifier.CakePermeability);
            IEquationParameter rc = calculator.AddVariable(fsParameterIdentifier.CakeResistance);
            IEquationParameter alpha = calculator.AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            
            #endregion

            #region Equations Initialization

            calculator.AddEquation(new fsDivisionInverseEquation(pc0, rc0));
            calculator.AddEquation(new fsAlphaPcEquation(alpha0, pc0, eps0, rhoS));
            calculator.AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressure, nc));
            calculator.AddEquation(new fsDivisionInverseEquation(pc, rc));
            calculator.AddEquation(new fsAlphaPcEquation(alpha, pc, eps, rhoS));
            
            #endregion
        }
    }
}
