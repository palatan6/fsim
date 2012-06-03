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
                           fsCharacteristic.Frequency
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

            foreach (FieldInfo field in typeof (fsScheme).GetFields())
            {
                var scheme = ((fsScheme) field.GetValue(null));
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
            Type type = typeof (fsScheme);
            foreach (FieldInfo field in type.GetFields())
            {
                var scheme = ((fsScheme) field.GetValue(null));
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

        #region Nested type: fsScheme

        private class fsScheme
        {
            public static readonly fsScheme InternationalSystemOfUnits =
                new fsScheme("International System of Units",
                    new[]
                    {
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerSecond),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.CubicMeter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerSec),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.CubicMeterPerSecond)
                    });

            public static readonly fsScheme LaboratoryScale =
                new fsScheme("Laboratory Scale",
                    new[]
                    {
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareCentiMeter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.Gramme),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.MilliLiter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerHour),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.CubicMeterPerSecond)
                    });

            public static fsScheme PilotIndustrialScale =
                new fsScheme("Pilot/Industrial Scale",
                    new[]
                    {
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.Liter),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerMin),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.LiterPerMinute),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificMassFlowrate, fsUnit.KiloGrammePerSquaredMeterPerMin),
                        new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificVolumeFlowrate, fsUnit.LiterPerSquaredMeterPerMin)
                    });

            private fsScheme(string name, params KeyValuePair<fsCharacteristic, fsUnit>[] characteristicToUnit)
            {
                CharacteristicToUnit = new Dictionary<fsCharacteristic, fsUnit>();
                Name = name;
                foreach (var pair in characteristicToUnit)
                {
                    CharacteristicToUnit.Add(pair.Key, pair.Value);
                }
            }

            public Dictionary<fsCharacteristic, fsUnit> CharacteristicToUnit { get; private set; }

            public string Name { get; private set; }
        }

        #endregion
    }
}