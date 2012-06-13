using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Units;

namespace Calculator
{
    public partial class fsUnitsDialog : Form
    {
        private readonly Dictionary<fsModule, CheckBox> m_moduleToCheckBox = new Dictionary<fsModule, CheckBox>();
        public Dictionary<fsCharacteristic, fsUnit> Characteristics
        {
            get { return fsUnitsControl1.Characteristics; }
        }
        private List<fsModule> m_modules;
        private fsModule m_initiallyCheckedModule;

        public fsUnitsDialog()
        {
            InitializeComponent();
        }

        private void UnitsDialogLoad(object sender, EventArgs e)
        {
            InitializeElementListing();
            fsUnitsControl1.ShowHideSecondaryCharacteristics(m_showSecondaryCheckbox.Checked);
        }

        public List<fsModule> GetModifiedModules()
        {
            return (from pair in m_moduleToCheckBox where pair.Value.Checked select pair.Key).ToList();
        }

        public bool GetFutureModulesModified()
        {
            return m_futureCheckBox.Checked;
        }

        private void InitializeElementListing()
        {
            var checkBox = new CheckBox {Parent = listingPanel, Location = new Point(8, 16), Text = @"All"};
            checkBox.CheckStateChanged += CheckBoxCheckStateChanged;

            if (m_modules != null)
            {
                foreach (fsModule module in m_modules)
                {
                    AddModuleToList(module.Name, false, module);
                 }
            }
        }

        public void AssignModulesList(List<fsModule> modules)
        {
            m_modules = modules;
        }

        private void CheckBoxCheckStateChanged(object sender, EventArgs e)
        {
            var allCheckBox = (CheckBox) sender;
            foreach (CheckBox checkBox in m_moduleToCheckBox.Values)
            {
                checkBox.Checked = allCheckBox.Checked;
            }
        }

        private void AddModuleToList(string text, bool isChecked, fsModule module)
        {
            var checkBox = new CheckBox
                               {
                                   Parent = listingPanel,
                                   Location = new Point(8, 48 + 8 + (m_moduleToCheckBox.Count) * 24),
                                   Text = text,
                                   Checked = isChecked,
                                   AutoSize = true
                               };
            if (module == m_initiallyCheckedModule)
            {
                checkBox.Checked = true;
            }
            m_moduleToCheckBox[module] = checkBox;
        }
        
        private void Button1Click(object sender, EventArgs e)
        {
            fsUnitsControl1.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Button2Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void ParametersDisplayCheckedChanged(object sender, EventArgs e)
        {
            fsUnitsControl1.ShowHideSecondaryCharacteristics(m_showSecondaryCheckbox.Checked);
        }

        internal void SetInitiallyCheckedModule(fsModule module)
        {
            m_initiallyCheckedModule = module;
        }
    }
}