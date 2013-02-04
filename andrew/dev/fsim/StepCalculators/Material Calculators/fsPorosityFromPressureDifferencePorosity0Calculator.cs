using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Equations;
using Parameters;

namespace StepCalculators.Material_Calculators
{
    public class fsPorosityFromPressureDifferencePorosity0Calculator : fsCalculator
    {
        public fsPorosityFromPressureDifferencePorosity0Calculator()
        {
            #region Parameters Initialization

            fsCalculatorVariable porosity = AddVariable(fsParameterIdentifier.CakePorosity);
            fsCalculatorVariable porosity0 = AddVariable(fsParameterIdentifier.CakePorosity0);
            fsCalculatorVariable ne = AddVariable(fsParameterIdentifier.Ne);
            fsCalculatorConstant pressureDifference = AddConstant(fsParameterIdentifier.PressureDifference);

            #endregion

            #region Equations Initialization

            AddEquation(new fsFrom0AndDpEquation(porosity, porosity0, pressureDifference, ne));

            #endregion
        }
    }
}
