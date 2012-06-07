using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Calculator.Dialogs;
using CalculatorModules;
using CalculatorModules.Base_Controls;
using Parameters;

namespace Calculator
{
    public partial class fsMainWindow : Form
    {
        private readonly List<fsModule> m_modules = new List<fsModule>();
        private int m_counter;
        private fsModule m_currentModule;

        public fsMainWindow()
        {
            InitializeComponent();
        }

        private void RebuildWindowsList()
        {
            string currentModuleName = m_currentModule == null
                                           ? ""
                                           : m_currentModule.Name;

            // Last to elements of Modules menu are splitter ("-") and "Add Module..."
            // so we while there are more than two elements
            while (modulesToolStripMenuItem.DropDownItems.Count > 2)
            {
                modulesToolStripMenuItem.DropDownItems.RemoveAt(0);
            }

            foreach (fsModule module in m_modules)
            {
                int lastIndex = modulesToolStripMenuItem.DropDownItems.Count - 1 - 2;
                var menuItem = new ToolStripMenuItem(module.Name, null, ModuleMenuItemClick);
                if (menuItem.Text == currentModuleName)
                {
                    menuItem.Checked = true;
                }
                modulesToolStripMenuItem.DropDownItems.Insert(lastIndex + 1, menuItem);
            }
        }

        private void FormClose(object sender, EventArgs e)
        {
            string text = ((Form) sender).Text;
            foreach (fsModule f in m_modules)
            {
                if (f.Name == text)
                {
                    m_modules.Remove(f);
                    RebuildWindowsList();
                    return;
                }
            }
        }

        private void ModuleMenuItemClick(object sender, EventArgs e)
        {
            SetCurrentModule((sender as ToolStripMenuItem).Text);
        }

        private void SetCurrentModule(string checkedModuleName)
        {
            if (m_currentModule != null && m_currentModule.Name == checkedModuleName)
                return;

            foreach (ToolStripItem toolStripItem in modulesToolStripMenuItem.DropDownItems)
            {
                if (toolStripItem is ToolStripMenuItem)
                {
                    var menuItem = (ToolStripMenuItem) toolStripItem;
                    menuItem.Checked = menuItem.Text == checkedModuleName;
                }
            }

            foreach (fsModule module in m_modules)
            {
                if (module.Name == checkedModuleName)
                {
                    m_currentModule = module;
                    m_currentModule.Form.Activate();
                    break;
                }
            }
        }

        private void FormActivated(object sender, EventArgs e)
        {
            SetCurrentModule((sender as Form).Text);
        }

        private void WindowTilesToolStripMenuItemClick(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            int maxHeight = 0;
            foreach (fsModule module in m_modules)
            {
                if (x > 0 && x + module.Form.Width > ClientSize.Width)
                {
                    x = 0;
                    y += maxHeight;
                    maxHeight = 0;
                }
                module.Form.Left = x;
                module.Form.Top = y;
                x += module.Form.Width;
                maxHeight = Math.Max(maxHeight, module.Form.Height);
            }
        }

        private void CloseToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void MainWindowLoad(object sender, EventArgs e)
        {
            addModuleToolStripMenuItem.PerformClick();
        }

        private void UnitsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var unitsDialog = new fsUnitsDialog();
            unitsDialog.AssignModulesList(m_modules);
            unitsDialog.SetInitiallyCheckedModule(GetCurrentActiveModule());
            unitsDialog.ShowDialog();
            if (unitsDialog.DialogResult == DialogResult.OK)
            {
                foreach (fsModule module in unitsDialog.GetModifiedModules())
                {
                    module.SetUnits(unitsDialog.Characteristics);
                }
                if (unitsDialog.GetFutureModulesModified())
                {
                    foreach (var unit in unitsDialog.Characteristics)
                    {
                        unit.Key.CurrentUnit = unit.Value;
                    }
                }
            }
        }

        private fsModule GetCurrentActiveModule()
        {
            return m_currentModule;
        }

        private void MachineTypeToolStripMenuItemClick(object sender, EventArgs e)
        {
            var machineDialog = new fsMachineDialog();
            machineDialog.AssignModulesList(m_modules);
            machineDialog.SetInitiallyCheckedModule(GetCurrentActiveModule());
            DialogResult dialogResult = machineDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                foreach (fsModule module in machineDialog.GetModifiedModules())
                {
                    module.SetRanges(machineDialog.Ranges);
                }
            }
        }

        private void showHideParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new fsShowHideParametersDialog();
            form.InvolveParameters(GetCurrentActiveModule().GetInvolvedParametersWithVisibleStatus());
            form.ShowDialog();
            Dictionary<fsParameterIdentifier, bool> parametersToShowAndHide = form.GetParametersToShowAndHide();
            GetCurrentActiveModule().ShowAndHideParameters(parametersToShowAndHide);
        }

        private void precisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var precisionDialog = new PrecisionDialog();
            precisionDialog.ShowDialog();
            GetCurrentActiveModule().RecalculateAndRedraw();
        }

        private void addModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var modulesForm = new fsModulesForm();
            modulesForm.ShowDialog();
            if (modulesForm.DialogResult == DialogResult.OK)
            {
                if (modulesForm.SelectedCalculatorControl is fsOptionsSingleTableAndCommentsCalculatorControl)
                {
                    (modulesForm.SelectedCalculatorControl as fsOptionsSingleTableAndCommentsCalculatorControl).
                        AllowDiagramView = true;
                }
                ++m_counter;
                var module = new fsModule("#" + m_counter + " - " + modulesForm.SelectedCalculatorControlName,
                                          modulesForm.SelectedCalculatorControl);
                m_modules.Add(module);
                module.Form.MdiParent = this;
                module.Form.Closed += FormClose;
                module.Form.Activated += FormActivated;
                m_currentModule = module;
                RebuildWindowsList();
            }
        }
    }
}