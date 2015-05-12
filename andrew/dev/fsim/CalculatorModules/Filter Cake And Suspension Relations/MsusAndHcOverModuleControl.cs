using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculatorModules.Filter_Cake_And_Suspension_Relations;
using Parameters;
using StepCalculators;
using Units;

namespace CalculatorModules
{
    public partial class MsusAndHcOverModuleControl : fsCalculatorControl
    {
        private readonly Dictionary<string, MsusAndHcBaseControl> m_moduleNameToControl = new Dictionary<string, MsusAndHcBaseControl>();
        private MsusAndHcBaseControl m_currentControl;
        private MsusAndHcBaseControl m_currentCalculatorControl
        {
            get { return m_currentControl; }
            set
            {
                m_currentControl = value;
                CurrentSubControl = m_currentControl;
            }
        }

        public MsusAndHcOverModuleControl()
        {
            InitializeComponent();
        }

        protected override void InitializeCalculators()
        {
            AddCalculatorControl(new MsusAndHcPlainAreaControl(), fsMisc.GetEnumDescription(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea));
            AddCalculatorControl(new MsusAndHcConvexControl(), fsMisc.GetEnumDescription(fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric));
            AddCalculatorControl(new MsusAndHcConcaveControl(), fsMisc.GetEnumDescription(fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric));
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            ChangeAndShowCurrentCalculatorControl();
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsMachineTypeOption), filtrationOptionBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea);
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // no groups in this module
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // no equations in this module
        }

        public override Control ControlToResizeForExpanding
        {
            get { return base.ControlToResizeForExpanding; }
            set
            {
                base.ControlToResizeForExpanding = value;
                foreach (MsusAndHcBaseControl calculatorControl in m_moduleNameToControl.Values)
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

        public override void SetCommentsText(string text)
        {
            m_currentCalculatorControl.SetCommentsText(text);
        }

        public override string GetCommentsText()
        {
            return m_currentCalculatorControl.GetCommentsText();
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
            MsusAndHcBaseControl calculatorControl,
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
            MsusAndHcBaseControl lastCalculatorControl = m_currentCalculatorControl;

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

            foreach (MsusAndHcBaseControl calculatorControl in m_moduleNameToControl.Values)
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
