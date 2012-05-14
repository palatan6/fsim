using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using CalculatorModules.Machine_Ranges;
using Parameters;
using ParametersIdentifiers.Ranges;

namespace Calculator
{
    public partial class fsMachineDialog : Form
    {
        private Dictionary<string, fsModule> m_modules;

        #region Machine Ranges Backup Routine

        private Dictionary<string, fsMachineRanges> m_machineRangesBackup;

        private void FillMachineRangesBackup()
        {
            m_machineRangesBackup = new Dictionary<string, fsMachineRanges>();
            Type type = typeof (fsMachineRanges);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                var machineRanges = ((fsMachineRanges) field.GetValue(null));
                AddMachineRangeBackup(machineRanges);
            }
        }

        private void AddMachineRangeBackup(fsMachineRanges machineRanges)
        {
            Dictionary<fsParameterIdentifier, fsParameterRange>.ValueCollection ranges = machineRanges.Ranges.Values;
            List<fsParameterRange> rangesCopy =
                ranges.Select(
                    range =>
                    new fsParameterRange(range.Identifier, range.Units, range.Range.From.Value / range.Units.Coefficient,
                                         range.Range.To.Value / range.Units.Coefficient)).ToList();
            m_machineRangesBackup.Add(machineRanges.Name,
                                      new fsMachineRanges(machineRanges.Name, rangesCopy));
        }

        private void BackupMachineRanges()
        {
            Type type = typeof (fsMachineRanges);
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                var machineRanges = ((fsMachineRanges) field.GetValue(null));
                BackupMachineRange(machineRanges);
            }
        }

        private void BackupMachineRange(fsMachineRanges machineRange)
        {
            CopyRanges(machineRange.Ranges, m_machineRangesBackup[machineRange.Name].Ranges);
        }

        private static void CopyRanges(
            Dictionary<fsParameterIdentifier, fsParameterRange> target,
            Dictionary<fsParameterIdentifier, fsParameterRange> source)
        {
            foreach (var parameterRange in source)
            {
                target[parameterRange.Key].Range.From = parameterRange.Value.Range.From;
                target[parameterRange.Key].Range.To = parameterRange.Value.Range.To;
            }
        }

        #endregion

        public fsMachineDialog()
        {
            InitializeComponent();
            FillMachineRangesBackup();
        }

        public Dictionary<fsParameterIdentifier, fsRange> Ranges
        {
            get
            {
                return
                    fsMachineSettings1.ParameterRanges.Values.ToDictionary(parameterRange => parameterRange.Identifier,
                                                                           parameterRange => parameterRange.Range);
            }
        }

        internal void AssignModulesList(List<fsModule> modules)
        {
            var list = new List<string>();
            m_modules = new Dictionary<string, fsModule>();
            foreach (fsModule module in modules)
            {
                list.Add(module.Name);
                m_modules.Add(module.Name, module);
            }
            fsCheckedList1.AssignList(list);
        }

        internal IEnumerable<fsModule> GetModifiedModules()
        {
            return (from ListViewItem item in fsCheckedList1.GetCheckedItems() select m_modules[item.Text]).ToList();
        }

        #region Buttons Events

        private void CancelButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            BackupMachineRanges();
            Close();
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion

        internal void SetInitiallyCheckedModule(fsModule fsModule)
        {
            fsCheckedList1.CheckItem(fsModule.Name);
        }
    }
}