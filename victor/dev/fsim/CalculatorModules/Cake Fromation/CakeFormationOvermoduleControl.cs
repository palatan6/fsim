using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CalculatorModules.BeltFiltersWithReversibleTrays;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCakeFormationOvermoduleControl : fsCalculatorControl
    {
        private readonly Dictionary<string, fsCakeFormationBaseControl> m_moduleNameToControl = new Dictionary<string, fsCakeFormationBaseControl>();
        private fsCakeFormationBaseControl m_currentModule;

        public fsCakeFormationOvermoduleControl()
        {
            InitializeComponent();

            AddModule("Belt Filter With Reversible Tray", new fsBeltFilterWithReversibleTrayControl());
            AddModule("Continuous Belt Filter (modular)", new fsContinuousModularBeltFilterControl());
            AddModule("Continuous Belt Filter (non-modular)", new fsContinuousNonModularBeltFilterControl());
            AddModule("Other", new fsCommonCakeFormationControl());
            ChangeAndShowCurrentModule();
        }

        public override Control ControlToResizeForExpanding
        {
            get { return base.ControlToResizeForExpanding; }
            set
            {
                base.ControlToResizeForExpanding = value;
                m_currentModule.ControlToResizeForExpanding = ControlToResizeForExpanding;
            }
        }

        #region Internal Routines

        private void AddModule(string moduleName, fsCakeFormationBaseControl moduleControl)
        {
            comboBox1.Items.Add(moduleName);
            comboBox1.SelectedItem = comboBox1.Items[0];
            m_moduleNameToControl.Add(moduleName, moduleControl);
        }

        private void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeAndShowCurrentModule();
        }

        private void ChangeAndShowCurrentModule()
        {
            fsCakeFormationBaseControl lastModule = m_currentModule;

            foreach (var keyValue in m_moduleNameToControl)
            {
                if (keyValue.Key != comboBox1.Text)
                {
                    keyValue.Value.Parent = null;
                }
                else
                {
                    m_currentModule = keyValue.Value;
                }
            }

            if (m_currentModule != null)
            {
                m_currentModule.Parent = panel1;
                m_currentModule.Dock = DockStyle.Fill;

                if (lastModule != null)
                {
                    m_currentModule.SetCalculationOption(lastModule.GetCalculationOption());
                    m_currentModule.SetMaterialParametersTableVisible(lastModule.GetMaterialParametersTableVisible());
                    m_currentModule.SetDiagramVisible(lastModule.GetDiagramVisible());
                    m_currentModule.SetValues(lastModule.GetValues());
                }
            }
        }

        #endregion
    }
}
