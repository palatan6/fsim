using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules.BeltFiltersWithReversibleTrays;
using CalculatorModules.Cake_Fromation.Other_Filters_Controls;
using Parameters;
using Units;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCakeFormationOvermoduleControl : fsCalculatorControl
    {
        private readonly Dictionary<string, fsCakeFormationBaseControl> m_moduleNameToControl = new Dictionary<string, fsCakeFormationBaseControl>();
        private fsCakeFormationBaseControl m_currentCalculatorControl;

        protected override void InitializeCalculators()
        {
            AddCalculatorControl(new fsContinuousNonModularBeltFilterControl(), "Continuous Belt Filters (Non Modular)");
            AddCalculatorControl(new fsContinuousModularBeltFilterControl(), "Continuous Belt Filters (Modular)");
            AddCalculatorControl(new fsBeltFilterWithReversibleTrayControl(), "Belt Filter With Reversible Tray");
            AddCalculatorControl(new fsDrumFilterControl(), "Drum Filters");
            AddCalculatorControl(new fsDiscFilterControl(), "Disc Filters");
            AddCalculatorControl(new fsPanFilterControl(), "Pan Filters");
            AddCalculatorControl(new fsRotaryPressureFilters(), "Rotary Pressure Filters");
            AddCalculatorControl(new fsNutcheFilters(), "Nutsche Filters");
            AddCalculatorControl(new fsPressureLeafFilters(), "Pressure Leaf Filters");
            AddCalculatorControl(new fsFilterPressesControl(), "Filter Presses");
            AddCalculatorControl(new fsFilterPressAutomatControl(), "Filter Press Automats");
            AddCalculatorControl(new fsPneumaPressControl(), "Pneuma Presses");
            AddCalculatorControl(new fsLaboratoryPressureNutscheFilterControl(), "Laboratory Pressure Nutsche Filters");
            AddCalculatorControl(new fsLaboratoryVacuumFilterControl(), "Laboratory Vacuum Filters");
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            ChangeAndShowCurrentCalculatorControl();
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // no groups in this module
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // no equations in this module
        }

        public fsCakeFormationOvermoduleControl()
        {
            InitializeComponent();
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
                    m_currentCalculatorControl.SetCalculationOptionAndRefreshCalculatorControl(lastCalculatorControl.GetCalculationOption());
                    m_currentCalculatorControl.SetMaterialParametersTableVisible(lastCalculatorControl.GetMaterialParametersTableVisible());
                    m_currentCalculatorControl.SetValuesAndRefreshCalculatorControl(lastCalculatorControl.GetValues());

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
