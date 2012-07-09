using System.Collections.Generic;
using Parameters;
using ParametersIdentifiers;
using StepCalculators;

namespace CalculatorModules
{
    public static class fsCalculationProcessor
    {
        public static void ProcessCalculatorParameters(
            Dictionary<fsParameterIdentifier, fsSimulationModuleParameter> values,
            Dictionary<fsParameterIdentifier, fsParametersGroup> parameterToGroup,
            fsCalculator calculator)
        {
            var localValues = new Dictionary<fsParameterIdentifier, fsCalculatorParameter>();

            foreach (fsParameterIdentifier parameter in values.Keys)
            {
                fsParametersGroup group = parameterToGroup[parameter];
                localValues[parameter] = group.GetIsInputFlag() && parameter == group.Representator
                                             ? new fsCalculatorParameter(parameter, true, values[parameter].Value)
                                             : new fsCalculatorParameter(parameter);
            }

            calculator.ReadDataFromStorage(localValues);
            calculator.Calculate();
            calculator.CopyValuesToStorage(localValues);

            foreach (fsParameterIdentifier parameter in values.Keys)
            {
                values[parameter].Value = localValues[parameter].Value;
            }
        }
    }
}