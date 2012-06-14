using Value;

namespace Parameters
{
    public class fsNamedValueParameter : fsJustValueParameter
    {
        public fsParameterIdentifier Identifier { get; set; }

        #region Constructors

        public fsNamedValueParameter(fsNamedValueParameter other)
            : base(other)
        {
            Identifier = other.Identifier;
        }

        public fsNamedValueParameter(fsParameterIdentifier identifier)
        {
            Identifier = identifier;
        }

        public fsNamedValueParameter(fsParameterIdentifier identifier, fsValue value)
            : base(value)
        {
            Identifier = identifier;
        }

        #endregion

        public override string ToString()
        {
            return Identifier.Name + " = " + Value;
        }
    }
}