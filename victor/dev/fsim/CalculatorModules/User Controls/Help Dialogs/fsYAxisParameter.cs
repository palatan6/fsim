using Parameters;

namespace CalculatorModules.User_Controls.Help_Dialogs
{
    public class fsYAxisParameter
    {
        public enum fsYParameterKind
        {
            InputParameter,
            CalculatedConstantParameter,
            CalculatedVariableParameter
        }

        public fsParameterIdentifier Identifier { get; private set; }
        public fsYParameterKind Kind { get; private set; }

        public fsYAxisParameter(fsParameterIdentifier identifier, fsYParameterKind kind)
        {
            Identifier = identifier;
            Kind = kind;
        }

        public fsYAxisParameter(fsYAxisParameter other)
        {
            Identifier = other.Identifier;
            Kind = other.Kind;
        }
    }
}
