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

            AddModule(new fsContinuousNonModularBeltFilterControl(), "Continuous Belt Filters (Non Modular)");
            AddModule(new fsContinuousModularBeltFilterControl(), "Continuous Belt Filters (Modular)");
            AddModule(new fsBeltFilterWithReversibleTrayControl(), "Belt Filter With Reversible Tray");
            AddModule(new fsCommonCakeFormationControl(),
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
            ChangeAndShowCurrentModule();
        }

        public override Control ControlToResizeForExpanding
        {
            get { return base.ControlToResizeForExpanding; }
            set
            {
                base.ControlToResizeForExpanding = value;
                foreach (fsCalculatorControl module in m_moduleNameToControl.Values)
                {
                    module.ControlToResizeForExpanding = ControlToResizeForExpanding;
                }
            }
        }

        #region Internal Routines

        private void AddModule(
            fsCakeFormationBaseControl moduleControl,
            params string [] moduleNames)
        {
            foreach (string moduleName in moduleNames)
            {
                comboBox1.Items.Add(moduleName);
                m_moduleNameToControl.Add(moduleName, moduleControl);
            }
            comboBox1.SelectedItem = comboBox1.Items[0];
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
                if (keyValue.Key == comboBox1.Text)
                {
                    m_currentModule = keyValue.Value;
                    break;
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

            foreach (fsCakeFormationBaseControl module in m_moduleNameToControl.Values)
            {
                if (module != m_currentModule)
                {
                    module.Parent = null;
                }
            }
        }

        #endregion
    }
}
