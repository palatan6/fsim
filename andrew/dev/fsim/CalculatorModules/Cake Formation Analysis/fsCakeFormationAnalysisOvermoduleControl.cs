using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules.Cake_Formation_Analysis;
using CalculatorModules.Cake_Fromation;
using Parameters;
using StepCalculators;
using Units;

namespace CalculatorModules
{
    public partial class fsCakeFormationAnalysisOvermoduleControl : fsCalculatorControl
    {
        private readonly Dictionary<string, CakeFormationAnalysisBaseControl> m_moduleNameToControl = new Dictionary<string, CakeFormationAnalysisBaseControl>();
        private CakeFormationAnalysisBaseControl m_currentCalculatorControl;

        protected override void InitializeCalculators()
        {
            AddCalculatorControl(new CakeFormationBatchFiltersAnalysisControl(), "Batch Filters");
            AddCalculatorControl(new CakeFormationContinuousFiltersAnalysisControl(), "Continuous Filters");
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            ChangeAndShowCurrentCalculatorControl();
            EstablishCalculationOption(fsCalculationOptions.fsFiltersKindOption.BatchFilterCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsFiltersKindOption), filtrationOptionBox);
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // no groups in this module
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // no equations in this module
        }

        public fsCakeFormationAnalysisOvermoduleControl()
        {
            InitializeComponent();
        }

        public override Control ControlToResizeForExpanding
        {
            get { return base.ControlToResizeForExpanding; }
            set
            {
                base.ControlToResizeForExpanding = value;
                foreach (CakeFormationAnalysisBaseControl calculatorControl in m_moduleNameToControl.Values)
                {
                    calculatorControl.ControlToResizeForExpanding = ControlToResizeForExpanding;
                }
            }
        }

        public override void SetUnits(Dictionary<fsCharacteristic, fsUnit> dictionary)
        {
            CharacteristicWithCurrentUnits = dictionary;
            m_currentCalculatorControl.SetUnits(dictionary);
        }

        public override void AplySelectedCalculatorSettings()
        {
            m_currentCalculatorControl.AplySelectedCalculatorSettings();
        }

        public override Dictionary<fsParameterIdentifier, bool> GetInvolvedParametersWithVisibleStatus()
        {
            return m_currentCalculatorControl.GetInvolvedParametersWithVisibleStatus();
        }

        public override void ShowAndHideParameters(Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide)
        {
            m_currentCalculatorControl.ShowAndHideParameters(parametersToShowAndHide);
        }

        private void filtrationOptionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeAndShowCurrentCalculatorControl();
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] {filtrationOptionBox};
        }

        #region Internal Routines

        private void AddCalculatorControl(
            CakeFormationAnalysisBaseControl calculatorControl,
            params string [] moduleNames)
        {
            foreach (string moduleName in moduleNames)
            {
                filtrationOptionBox.Items.Add(moduleName);
                m_moduleNameToControl.Add(moduleName, calculatorControl);
                SubCalculatorControls.Add(calculatorControl);
            }
            filtrationOptionBox.SelectedItem = filtrationOptionBox.Items[0];
            calculatorControl.AllowDiagramView = false;
        }

        private void ChangeAndShowCurrentCalculatorControl()
        {
            CakeFormationAnalysisBaseControl lastCalculatorControl = m_currentCalculatorControl;

            foreach (var keyValue in m_moduleNameToControl)
            {
                if (keyValue.Key == filtrationOptionBox.Text)
                {
                    m_currentCalculatorControl = keyValue.Value;
                    CharacteristicWithCurrentUnits = m_currentCalculatorControl.CharacteristicWithCurrentUnits;
                    m_currentCalculatorControl.AplySelectedCalculatorSettings();
                    break;
                }
            }

            if (m_currentCalculatorControl != null)
            {
                
                if (lastCalculatorControl != null)
                {
                    Control owningControl = m_currentCalculatorControl.ControlToResizeForExpanding;
                    m_currentCalculatorControl.ControlToResizeForExpanding = null;
                    m_currentCalculatorControl.SetDiagramVisible(lastCalculatorControl.GetDiagramVisible());
                    m_currentCalculatorControl.ControlToResizeForExpanding = owningControl;
                }

                m_currentCalculatorControl.Parent = tablesPanel;
                m_currentCalculatorControl.Dock = DockStyle.Fill;
                filtrationOptionBox.Parent = m_currentCalculatorControl.calculationOptionsPanel;
                filtrationOptionBox.Location = m_currentCalculatorControl.GetFilterTypesComboBoxPosition();
             }

            foreach (CakeFormationAnalysisBaseControl calculatorControl in m_moduleNameToControl.Values)
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
