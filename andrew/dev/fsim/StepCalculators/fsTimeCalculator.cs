using Parameters;
using Equations;
using Value;

namespace StepCalculators
{
    public class fsTimeCalculator : fsCalculator
    {
        public fsTimeCalculator()
        {
            #region Parameters Initialization

            IEquationParameter n = AddVariable(fsParameterIdentifier.NumberOfCyclones);
            IEquationParameter tc = AddVariable(fsParameterIdentifier.CycleTime);
            IEquationParameter tr = AddVariable(fsParameterIdentifier.ResidualTime);
            IEquationParameter tf = AddVariable(fsParameterIdentifier.FiltrationTime);
            IEquationParameter sf = AddVariable(fsParameterIdentifier.SpecificFiltrationTime);

            #endregion

            #region Equations Initialization

            Equations.Add(new fsDivisionInverseEquation(n, tc));
            Equations.Add(new fsSumEquation(tc, tr, tf));
            Equations.Add(new fsProductEquation(tf, tc, sf));

            #endregion
        }
    }
}
