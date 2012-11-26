using Parameters;
using ParametersIdentifiers.Ranges;
using Units;
using Value;

namespace ParametersIdentifiers
{
    /*
     * This class is used in Calculator modules,
     * where we need to know parameter Value, Unit and Range
     * 
     * */

    public class fsSimulationModuleParameter : fsNamedValueParameter
    {
        public fsUnit Unit { get; set; }
        public fsRange Range { get; set; }

        #region Constructors

        public fsSimulationModuleParameter(fsSimulationModuleParameter other)
            : base(other)
        {
            Unit = other.Unit;
            Range = new fsRange(new fsValue(0), new fsValue(100));
        }

        public fsSimulationModuleParameter(fsParameterIdentifier identifier)
            : base(identifier)
        {
            Unit = identifier.MeasurementCharacteristic.CurrentUnit;
            Range = new fsRange(new fsValue(0), new fsValue(100));
        }

        public fsSimulationModuleParameter(fsParameterIdentifier identifier, fsValue value)
            : base(identifier, value)
        {
            Unit = identifier.MeasurementCharacteristic.CurrentUnit;
            Range = new fsRange(new fsValue(0), new fsValue(100));
        }

        public fsSimulationModuleParameter(fsParameterIdentifier identifier, fsValue value, fsRange range)
            : base(identifier, value)
        {
            Unit = identifier.MeasurementCharacteristic.CurrentUnit;
            Range = range;
        }

        #endregion

        public fsValue GetValueInUnits()
        {
            return Value / Unit.Coefficient;
        }

        public void SetValueInUnits(fsValue valueInUnits)
        {
            Value = valueInUnits * Unit.Coefficient;
        }

        public override string ToString()
        {
            return Identifier + " [" + Unit.Name + "]";
        }
    }
}