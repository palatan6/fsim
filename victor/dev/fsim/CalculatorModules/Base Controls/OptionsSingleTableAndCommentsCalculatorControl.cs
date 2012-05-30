namespace CalculatorModules.Base_Controls
{
    public partial class fsOptionsSingleTableAndCommentsCalculatorControl : fsOptionsAndCommentsCalculatorControl
    {
        public fsOptionsSingleTableAndCommentsCalculatorControl()
        {
            InitializeComponent();
        }

        protected internal override void StopGridsEdit()
        {
            dataGrid.EndEdit();
        }
    }
}
