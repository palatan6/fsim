using System.Drawing;
using CalculatorModules.Base_Controls;
using StepCalculators;

namespace CalculatorModules.Cake_Formation_Analysis
{
    public partial class CakePorossityBaseControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        public CakePorossityBaseControl()
        {
            InitializeComponent();
        }

        protected readonly fsCakePorosityCalculator m_calculator = new fsCakePorosityCalculator();


        protected override void InitializeCalculators()
        {
            Calculators.Add(m_calculator);
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(saturationComboBox.Items, typeof(fsCalculationOptions.fsSimulationsOption));
            EstablishCalculationOption(fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsSimulationsOption), saturationComboBox);
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // this control has only one equation
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control hasn't calculation options
        }

        public Point GetFilterTypesComboBoxPosition()
        {
            int x = saturationComboBox.Left;
            int y = (saltContentComboBox.Top - saturationComboBox.Top) / 2 + 4;

            Point p = new Point(x,y);

            return p;
        }
    }
}
