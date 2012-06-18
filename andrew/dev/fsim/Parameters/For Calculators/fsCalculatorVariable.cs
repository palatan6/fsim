namespace Parameters
{
    public class fsCalculatorVariable : fsCalculatorParameter, IEquationParameter
    {
        public fsCalculatorVariable(fsParameterIdentifier identifier)
            : base(identifier)
        {
            IsProcessed = false;
        }

        public fsCalculatorVariable(fsParameterIdentifier identifier, bool isInput)
            : base(identifier, isInput)
        {
            IsProcessed = false;
        }

        #region IEquationParameter Members

        public bool IsProcessed { get; set; }

        #endregion
    }
}