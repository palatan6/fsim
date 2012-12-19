using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Units;

namespace fsUIControls
{
    public partial class fsUnitsControl : UserControl
    {
        private const string CustomSchemeTitle = "Custom";

        private readonly Dictionary<fsCharacteristic, ComboBox> m_characteristicToComboBox =
            new Dictionary<fsCharacteristic, ComboBox>();

        private readonly Dictionary<fsCharacteristic, Label> m_characteristicToLabel =
            new Dictionary<fsCharacteristic, Label>();

        public Dictionary<fsCharacteristic, fsUnit> Characteristics = new Dictionary<fsCharacteristic, fsUnit>();
        private bool m_schemeApplyingInProcess;

        public fsUnitsControl()
        {
            InitializeComponent();
            InitializeShemeBox();
        }

        private static IEnumerable<fsCharacteristic> GetSecondaryCharacteristics()
        {
            return new[]
                       {
                           fsCharacteristic.Pressure,
                           fsCharacteristic.Viscosity,
                           fsCharacteristic.Density,
                           fsCharacteristic.SurfaceTension,
                           fsCharacteristic.CakeWashOutContent,
                           fsCharacteristic.Frequency,
                           fsCharacteristic.Power
                       };
        }

        private static IEnumerable<fsCharacteristic> GetPrimaryCharacteristics()
        {
            return new[]
                       {
                           fsCharacteristic.Time,
                           fsCharacteristic.MachineGeometryLength,
                           fsCharacteristic.Area,
                           fsCharacteristic.Mass,
                           fsCharacteristic.Volume,
                           fsCharacteristic.MassFlowrate,
                           fsCharacteristic.VolumeFlowrate,
                           fsCharacteristic.SpecificMassFlowrate,
                           fsCharacteristic.SpecificVolumeFlowrate
                       };
        }

        private void InitializeShemeBox()
        {
            schemeBox.Items.Add(CustomSchemeTitle);
            schemeBox.SelectedItem = schemeBox.Items[0];

            foreach (FieldInfo field in typeof (fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));
                schemeBox.Items.Add(scheme.Name);
            }
        }

        private void UnitsDialogLoad(object sender, EventArgs e)
        {
            InitializeUnitsPanel();
            ShowHideSecondaryCharacteristics(false);
        }

        public void ShowHideSecondaryCharacteristics(bool isVisible)
        {
            foreach (fsCharacteristic characteristic in GetSecondaryCharacteristics())
            {
                m_characteristicToComboBox[characteristic].Visible = isVisible;
                m_characteristicToLabel[characteristic].Visible = isVisible;
            }
        }

        private void InitializeUnitsPanel()
        {
            IEnumerable<fsCharacteristic> primaryCharacteristics = GetPrimaryCharacteristics();
            IEnumerable<fsCharacteristic> secondaryCharacteristics = GetSecondaryCharacteristics();

            var allCharacteristics = new List<fsCharacteristic>();
            allCharacteristics.AddRange(primaryCharacteristics);
            allCharacteristics.AddRange(secondaryCharacteristics);

            var characteristicControls = new List<KeyValuePair<Label, ComboBox>>();

            const int sizeBeforeLabel = 8;
            const int sizeFromLabelToCombobox = 16;
            const int sizeAfterCombobox = 24;
            rightPanel.Width = 0;

            foreach (fsCharacteristic characteristic in allCharacteristics)
            {
                var characteristicLabel = new Label {Text = characteristic.Name, AutoSize = true, Parent = unitsPanel};

                var unitsComboBox = new ComboBox();

                foreach (fsUnit unit in characteristic.Units)
                {
                    unitsComboBox.Items.Add(unit.Name);
                }
                unitsComboBox.Text = characteristic.CurrentUnit.Name;

                int width = sizeBeforeLabel
                            + characteristicLabel.Width
                            + sizeFromLabelToCombobox
                            + unitsComboBox.Width
                            + sizeAfterCombobox;
                if (rightPanel.Width < width)
                {
                    rightPanel.Width = width;
                }

                characteristicControls.Add(new KeyValuePair<Label, ComboBox>(characteristicLabel, unitsComboBox));
                m_characteristicToComboBox[characteristic] = unitsComboBox;
                m_characteristicToLabel[characteristic] = characteristicLabel;
            }

            int currentHeight = 8;
            foreach (var pair in characteristicControls)
            {
                Label characteristicLabel = pair.Key;
                ComboBox unitsComboBox = pair.Value;

                unitsComboBox.Parent = unitsPanel;
                unitsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                unitsComboBox.Location = new Point(unitsPanel.Width - sizeAfterCombobox - unitsComboBox.Width,
                                                   currentHeight);
                unitsComboBox.SelectedValueChanged += UnitChanged;

                characteristicLabel.Parent = unitsPanel;
                characteristicLabel.Location =
                    new Point(unitsComboBox.Location.X - sizeFromLabelToCombobox - characteristicLabel.Width,
                              currentHeight + 4);

                currentHeight += 32;
            }
        }

        private void UnitChanged(object sender, EventArgs e)
        {
            if (!m_schemeApplyingInProcess)
            {
                schemeBox.Text = CustomSchemeTitle;
            }
        }

        private void SchemeBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedScheme = schemeBox.SelectedItem.ToString();
            if (selectedScheme == CustomSchemeTitle)
                return;
            Type type = typeof(fsCharacteristicScheme);
            foreach (FieldInfo field in type.GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));
                if (selectedScheme == scheme.Name)
                {
                    UpdateSchemeBox(scheme.CharacteristicToUnit);
                    return;
                }
            }
        }

        public void Save()
        {
            foreach (var pair in m_characteristicToComboBox)
            {
                Characteristics[pair.Key] = fsUnit.UnitFromText(pair.Value.Text);
            }
        }

        private void UpdateSchemeBox(Dictionary<fsCharacteristic, fsUnit> dictionary)
        {
            m_schemeApplyingInProcess = true;
            foreach (var pair in dictionary)
            {
                m_characteristicToComboBox[pair.Key].Text = pair.Value.Name;
            }
            m_schemeApplyingInProcess = false;
        }
    }
}