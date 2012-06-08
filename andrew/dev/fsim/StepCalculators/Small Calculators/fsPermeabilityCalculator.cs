using Parameters;
using Equations;

namespace StepCalculators
{
    public class fsPermeabilityCalculator : fsCalculator
    {
        public fsPermeabilityCalculator()
        {
            #region Parameters Initialization

            fsCalculatorConstant rhoS = AddConstant(fsParameterIdentifier.SolidsDensity);
            fsCalculatorConstant eps0 = AddConstant(fsParameterIdentifier.CakePorosity0);
            fsCalculatorConstant eps = AddConstant(fsParameterIdentifier.CakePorosity);

            fsCalculatorConstant hc = AddConstant(fsParameterIdentifier.CakeHeight);
            fsCalculatorConstant eta = AddConstant(fsParameterIdentifier.MotherLiquidViscosity);
            fsCalculatorConstant hce0 = AddConstant(fsParameterIdentifier.FilterMediumResistanceHce0);
                        
            fsCalculatorVariable nc = AddVariable(fsParameterIdentifier.CakeCompressibility);
            fsCalculatorVariable pressure = AddVariable(fsParameterIdentifier.PressureDifference);

            fsCalculatorVariable pc0 = AddVariable(fsParameterIdentifier.CakePermeability0);
            fsCalculatorVariable rc0 = AddVariable(fsParameterIdentifier.CakeResistance0);
            fsCalculatorVariable alpha0 = AddVariable(fsParameterIdentifier.CakeResistanceAlpha0);

            fsCalculatorVariable pc = AddVariable(fsParameterIdentifier.CakePermeability);
            fsCalculatorVariable rc = AddVariable(fsParameterIdentifier.CakeResistance);
            fsCalculatorVariable alpha = AddVariable(fsParameterIdentifier.CakeResistanceAlpha);
            
            #endregion

            #region Equations Initialization

            AddEquation(new fsDivisionInverseEquation(pc0, rc0));
            AddEquation(new fsAlphaPcEquation(alpha0, pc0, eps0, rhoS));
            AddEquation(new fsFrom0AndDpEquation(pc, pc0, pressure, nc));
            AddEquation(new fsDivisionInverseEquation(pc, rc));
            AddEquation(new fsAlphaPcEquation(alpha, pc, eps, rhoS));
            
            #endregion
        }
    }
}
