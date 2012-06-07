using Parameters;

namespace CalculatorModules.User_Controls.Help_Dialogs
{
    public class fsYAxisParameterWithChecking : fsYAxisParameter
    {
        public bool IsChecked { get; set; }

        public fsYAxisParameterWithChecking(
            fsParameterIdentifier identifier,
            fsYParameterKind kind,
            bool isChecked)
                : base(identifier, kind)
        {
            IsChecked = isChecked;
        }

        public fsYAxisParameterWithChecking(fsYAxisParameterWithChecking other)
            : base(other)
        {
            IsChecked = other.IsChecked;
        }
    }
}
