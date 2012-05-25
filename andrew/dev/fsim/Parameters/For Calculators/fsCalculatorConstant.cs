using System;

namespace Parameters
{
    public class fsCalculatorConstant : fsCalculatorParameter, IEquationParameter
    {
        public fsCalculatorConstant(fsParameterIdentifier identifier)
            : base(identifier)
        {
        }

        #region IEquationParameter Members

        public bool IsProcessed
        {
            get { return true; }
            set { throw new Exception("Set is processed is restricted for Consts"); }
        }

        #endregion
    }
}