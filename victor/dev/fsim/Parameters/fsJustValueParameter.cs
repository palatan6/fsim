using Value;

namespace Parameters
{
    public class fsJustValueParameter
    {
        public fsValue Value { get; set; }

        #region Constructors

        public fsJustValueParameter()
        {
            Value = new fsValue();
        }

        public fsJustValueParameter(fsJustValueParameter other)
        {
            Value = other.Value;
        }

        public fsJustValueParameter(fsValue value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        #endregion
    }
}