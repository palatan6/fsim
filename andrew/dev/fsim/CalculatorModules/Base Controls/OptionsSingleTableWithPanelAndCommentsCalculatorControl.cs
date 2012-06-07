namespace CalculatorModules
{
    public partial class fsOptionsSingleTableWithPanelAndCommentsCalculatorControl : fsOptionsAndCommentsCalculatorControl
    {
        public fsOptionsSingleTableWithPanelAndCommentsCalculatorControl()
        {
            InitializeComponent();
        }

        protected internal override void StopGridsEdit()
        {
            dataGrid.EndEdit();
        }
    }
}
