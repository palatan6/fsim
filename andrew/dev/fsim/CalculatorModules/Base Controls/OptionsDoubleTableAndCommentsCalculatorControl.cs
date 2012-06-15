namespace CalculatorModules.Base_Controls
{
    public partial class fsOptionsDoubleTableAndCommentsCalculatorControl : fsOptionsAndCommentsCalculatorControl
    {
        public fsOptionsDoubleTableAndCommentsCalculatorControl()
        {
            InitializeComponent();
        }

        protected internal override void StopGridsEdit()
        {
            dataGrid.EndEdit();
            materialParametersDataGrid.EndEdit();
        }
    }
}
