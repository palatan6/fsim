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

        public fsMainWindow()
        {
            InitializeComponent();
        }

        private void RebuildWindowsList()
        {
            DataGridViewCell cell = windowsDataGrid.CurrentCell;
            string currentWindowName = cell == null || cell.Value == null
                                           ? ""
                                           : cell.Value.ToString();
            windowsDataGrid.Rows.Clear();
            foreach (fsModule module in m_modules)
            {
                int ind = windowsDataGrid.Rows.Add(module.Name);
                if (module.Name == currentWindowName)
                {
                    windowsDataGrid.CurrentCell = windowsDataGrid[0, ind];
                }
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

        private void WindowsDataGridCurrentCellChanged(object sender, EventArgs e)
        {
            var dataGrid = (DataGridView) sender;
            DataGridViewCell cell = dataGrid.CurrentCell;
            if (cell != null && cell.Value != null)
            {
                string text = cell.Value.ToString();
                foreach (fsModule module in m_modules)
                {
                    if (module.Name == text)
                    {
                        module.Form.Activate();
                        dataGrid.Focus();
                    }
                }
            }
        }

        private void AddModuleButtonClick(object sender, EventArgs e)
        {
            var modulesForm = new fsModulesForm();
            modulesForm.ShowDialog();
            if (modulesForm.DialogResult == DialogResult.OK)
            {
                if (modulesForm.SelectedCalculatorControl is fsOptionsSingleTableAndCommentsCalculatorControl)
                {
                    (modulesForm.SelectedCalculatorControl as fsOptionsSingleTableAndCommentsCalculatorControl).
                        AllowCommentsView = true;
                }
                ++m_counter;
                var module = new fsModule("#" + m_counter + " - " + modulesForm.SelectedCalculatorControlName,
                                          modulesForm.SelectedCalculatorControl);
                m_modules.Add(module);
                module.Form.MdiParent = this;
                module.Form.Closed += FormClose;
                module.Form.Activated += FormActivated;
                RebuildWindowsList();
                windowsDataGrid.CurrentCell = windowsDataGrid[0, windowsDataGrid.RowCount - 1];
            }
        }

        private void FormActivated(object sender, EventArgs e)
        {
            var form = (Form) sender;
            foreach (DataGridViewRow row in windowsDataGrid.Rows)
            {
                DataGridViewCell currentCell = row.Cells[0];
                if (currentCell.Value.ToString() == form.Text)
                {
                    windowsDataGrid.CurrentCell = currentCell;
                }
            }
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
            addModuleButton.PerformClick();
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
            DataGridViewCell cell = windowsDataGrid.CurrentCell;
            string currentWindowName = cell == null || cell.Value == null
                                           ? ""
                                           : cell.Value.ToString();
            return m_modules.FirstOrDefault(module => module.Name == currentWindowName);
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
//             var precisionDialog = new PrecisionDialog();
//             precisionDialog.ShowDialog();
//             GetCurrentActiveModule().RecalculateAndRedraw();
        }
    }
}