using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules.BeltFiltersWithReversibleTrays;
using Parameters;
using Units;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCakeFormationOvermoduleControl : fsCalculatorControl
    {
        private readonly Dictionary<string, fsCakeFormationBaseControl> m_moduleNameToControl = new Dictionary<string, fsCakeFormationBaseControl>();
        private fsCakeFormationBaseControl m_currentCalculatorControl;

        public fsCakeFormationOvermoduleControl()
        {
            InitializeComponent();

            AddCalculatorControl(new fsContinuousNonModularBeltFilterControl(), "Continuous Belt Filters (Non Modular)");
            AddCalculatorControl(new fsContinuousModularBeltFilterControl(), "Continuous Belt Filters (Modular)");
            AddCalculatorControl(new fsBeltFilterWithReversibleTrayControl(), "Belt Filter With Reversible Tray");
            AddCalculatorControl(new fsCommonCakeFormationControl(),
                "Drum Filters",
                "Disc Filters",
                "Pan Filters",
                "Rotary Pressure Filters",
                "Nutsche Filters",
                "Pressure Leaf Filters",
                "Filter Presses",
                "Filter Press Automats",
                "Pneuma Press",
                "Laboratory Pressure Nutsche Filter",
                "Laboratory Vacuum Filter");
            ChangeAndShowCurrentCalculatorControl();
        }

        public override Control ControlToResizeForExpanding
        {
            get { return base.ControlToResizeForExpanding; }
            set
            {
                base.ControlToResizeForExpanding = value;
                foreach (fsCakeFormationBaseControl calculatorControl in m_moduleNameToControl.Values)
                {
                    calculatorControl.ControlToResizeForExpanding = ControlToResizeForExpanding;
                }
            }
        }

        public override void SetUnits(Dictionary<fsCharacteristic, fsUnit> dictionary)
        {
            foreach (fsCakeFormationBaseControl calculatorControl in m_moduleNameToControl.Values)
            {
                calculatorControl.SetUnits(dictionary);
            }
        }

        public override Dictionary<fsParameterIdentifier, bool> GetInvolvedParametersWithVisibleStatus()
        {
            return m_currentCalculatorControl.GetInvolvedParametersWithVisibleStatus();
        }

        public override void ShowAndHideParameters(Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide)
        {
            m_currentCalculatorControl.ShowAndHideParameters(parametersToShowAndHide);
        }

        #region Internal Routines

        private void AddCalculatorControl(
            fsCakeFormationBaseControl calculatorControl,
            params string [] moduleNames)
        {
            foreach (string moduleName in moduleNames)
            {
                comboBox1.Items.Add(moduleName);
                m_moduleNameToControl.Add(moduleName, calculatorControl);
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeAndShowCurrentCalculatorControl();
        }

        private void ChangeAndShowCurrentCalculatorControl()
        {
            fsCakeFormationBaseControl lastCalculatorControl = m_currentCalculatorControl;

            foreach (var keyValue in m_moduleNameToControl)
            {
                if (keyValue.Key == comboBox1.Text)
                {
                    m_currentCalculatorControl = keyValue.Value;
                    break;
                }
            }

            if (m_currentCalculatorControl != null)
            {
                m_currentCalculatorControl.Parent = panel1;
                m_currentCalculatorControl.Dock = DockStyle.Fill;

                if (lastCalculatorControl != null)
                {
                    m_currentCalculatorControl.SetCalculationOption(lastCalculatorControl.GetCalculationOption());
                    m_currentCalculatorControl.SetMaterialParametersTableVisible(lastCalculatorControl.GetMaterialParametersTableVisible());
                    m_currentCalculatorControl.SetValues(lastCalculatorControl.GetValues());

                    Control owningControl = m_currentCalculatorControl.ControlToResizeForExpanding;
                    m_currentCalculatorControl.ControlToResizeForExpanding = null;
                    m_currentCalculatorControl.SetDiagramVisible(lastCalculatorControl.GetDiagramVisible());
                    m_currentCalculatorControl.ControlToResizeForExpanding = owningControl;
                }
            }

            foreach (fsCakeFormationBaseControl calculatorControl in m_moduleNameToControl.Values)
            {
                if (calculatorControl != m_currentCalculatorControl)
                {
                    calculatorControl.Parent = null;
                }
            }
        }

        #endregion
    }
}
