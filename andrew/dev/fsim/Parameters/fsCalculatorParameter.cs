using Value;

namespace Parameters
{
    /*
     * This class is used in Calculators, 
     * where we need value and isInput flag
     * 
     * */

    public class fsCalculatorParameter : fsNamedValueParameter
    {
        public fsCalculatorParameter(fsParameterIdentifier identifier)
            : base(identifier)
        {
            IsInput = false;
        }

        public fsCalculatorParameter(fsParameterIdentifier identifier, bool isInput)
            : base(identifier)
        {
            IsInput = isInput;
        }

        public fsCalculatorParameter(fsParameterIdentifier identifier, fsValue value)
            : base(identifier, value)
        {
            IsInput = false;
        }

        public fsCalculatorParameter(fsParameterIdentifier identifier, bool isInput, fsValue value)
            : base(identifier, value)
        {
            IsInput = isInput;
        }

        public bool IsInput { get; set; }

        public string ValueToStringWithCurrentUnits()
        {
            return (Value / Identifier.MeasurementCharacteristic.CurrentUnit.Coefficient).ToString();
        }

        public override string ToString()
        {
            return Identifier.Name + "(" + (IsInput ? "Inputed" : "Calculated") + ")" + " = " + Value;
        }
    }
}