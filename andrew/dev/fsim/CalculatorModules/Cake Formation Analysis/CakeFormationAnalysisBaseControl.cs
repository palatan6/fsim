using System;
using System.Drawing;
using System.Windows.Forms;
using CalculatorModules.Base_Controls;
using StepCalculators;

namespace CalculatorModules.Cake_Formation_Analysis
{
    public partial class CakeFormationAnalysisBaseControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        public CakeFormationAnalysisBaseControl()
        {
            InitializeComponent();
        }

        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsAnalysisFiltrationCalculator());
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(simulationBox.Items, typeof(fsCalculationOptions.fsSimulationsOption));
            EstablishCalculationOption(fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsSimulationsOption), simulationBox);
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // this control has only one equation
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control hasn't calculation options
        }

        private void simulationBox_DropDown(object sender, EventArgs e)
        {
            //ComboBox's DropDown menu fitting
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        public Point GetFilterTypesComboBoxPosition()
        {
            int x = simulationBox.Left;
            int y = simulationBox.Top - (label2.Top- label1.Top);

            Point p = new Point(x,y);

            return p;
        }
    }
}
