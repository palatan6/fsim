using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private readonly Dictionary<ComboBox, fsCharacteristic> m_comboBoxTocharacteristic =
            new Dictionary<ComboBox, fsCharacteristic>();

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

        #region CharacteristicsGrouping

        static IEnumerable<fsCharacteristic> GetMassAndVolumeList()
        {
            return new[]
            {
                fsCharacteristic.Mass,
                fsCharacteristic.Volume
            };
        }

        static IEnumerable<fsCharacteristic> GetDensityViscosityPressureList()
        {
            return new[]
            {
                fsCharacteristic.Density,
                fsCharacteristic.Viscosity,
                fsCharacteristic.Pressure
            };
        }

        static IEnumerable<fsCharacteristic> GetUnsortedList()
        {
            return new[]
            {
                fsCharacteristic.SurfaceTension,
                fsCharacteristic.CakeWashOutContent
            };
        }

        static IEnumerable<fsCharacteristic> GetFlowrateList()
        {
            return new[]
            {
                fsCharacteristic.MassFlowrate,
                fsCharacteristic.VolumeFlowrate,
                fsCharacteristic.SpecificMassFlowrate,
                fsCharacteristic.SpecificVolumeFlowrate
            };
        }

        static IEnumerable<fsCharacteristic> GetLengthAreaTimeFrequencyList()
        {
            return new[]
            {
                fsCharacteristic.MachineGeometryLength,
                fsCharacteristic.Area,
                fsCharacteristic.Time,
                fsCharacteristic.Frequency
            };
        }

        private Dictionary<string, IEnumerable<fsCharacteristic>> _grousWithParametersLists = new Dictionary
            <string, IEnumerable<fsCharacteristic>>
        {
            {"Mass and Volume", GetMassAndVolumeList()},
            {"Density, Viscosity, Pressure", GetDensityViscosityPressureList()},
            {"Unsorted", GetUnsortedList()},
            {"Flow Rate", GetFlowrateList()},
            {"Length, Area, Time, Frequency", GetLengthAreaTimeFrequencyList()},
        };

        #endregion

        private void InitializeShemeBox()
        {
            schemeBox.Items.Add(CustomSchemeTitle);
            schemeBox.SelectedItem = schemeBox.Items[0];

            foreach (FieldInfo field in typeof (fsCharacteristicScheme).GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));
                schemeBox.Items.Add(scheme.Name);
                splitButtonMenu.Items.Add(scheme.Name);
            }
        }

        private void UnitsDialogLoad(object sender, EventArgs e)
        {
            InitializeUnitsPanel();
            TryToFindMatchedScheme();
        }

        public void ShowHideSecondaryCharacteristics(bool isVisible)
        {
            m_schemeApplyingInProcess = true;
            foreach (fsCharacteristic characteristic in allCharacteristics())
            {
                ComboBox combo = m_characteristicToComboBox[characteristic];
                string currentSelectedItem = combo.Text;
                combo.Items.Clear();
                foreach (fsUnit unit in characteristic.Units)
                {
                    if (!unit.IsUsUnit || isVisible)
                    {
                        combo.Items.Add(unit.Name);
                    }
                }
                
                combo.Text = currentSelectedItem;

                if (combo.Text.Length<=0)
                {
                    combo.Text = combo.Items[0].ToString();
                }
            }
            m_schemeApplyingInProcess = false;
        }

        bool isShowUsUnits()
        {
            bool showUsUnits = false;
            foreach (var characteristic in allCharacteristics())
            {
                if (characteristic.CurrentUnit.IsUsUnit)
                {
                    showUsUnits = true;
                    showUsUnitsCheckbox.CheckedChanged -= showUsUnitsCheckbox_CheckedChanged;
                    showUsUnitsCheckbox.Checked = true;
                    showUsUnitsCheckbox.CheckedChanged += showUsUnitsCheckbox_CheckedChanged;
                    break;
                }
            }
            return showUsUnits;
        }

        List<fsCharacteristic> allCharacteristics()
        {
            IEnumerable<fsCharacteristic> primaryCharacteristics = GetPrimaryCharacteristics();
            IEnumerable<fsCharacteristic> secondaryCharacteristics = GetSecondaryCharacteristics();

            var AllCharacteristics = new List<fsCharacteristic>();
            AllCharacteristics.AddRange(primaryCharacteristics);
            AllCharacteristics.AddRange(secondaryCharacteristics);

            return AllCharacteristics;
        }

        private void InitializeUnitsPanel()
        {
            

            var characteristicControls = new List<KeyValuePair<Label, ComboBox>>();

            const int sizeBeforeLabel = 8;
            const int sizeFromLabelToCombobox = 16;
            const int sizeAfterCombobox = 10;
            rightPanel.Width = 0;

            foreach (fsCharacteristic characteristic in allCharacteristics())
            {
                var characteristicLabel = new Label {Text = characteristic.Name, AutoSize = true, Parent = unitsPanel};

                var unitsComboBox = new ComboBox();

                foreach (fsUnit unit in characteristic.Units)
                {
                    if (!unit.IsUsUnit || isShowUsUnits())
                    {
                        unitsComboBox.Items.Add(unit.Name);
                    }
                    
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
                m_comboBoxTocharacteristic.Add(unitsComboBox, characteristic);
            }

            const int groupBoxWidth = 300;
            int groupBoxTopX = 8;
            int groupBoxTopY = 8;
            foreach (var groupName in _grousWithParametersLists.Keys)
            {   
                GroupBox gb = new GroupBox();
                gb.Text = groupName;
                gb.Parent = unitsPanel;
                gb.Top = groupBoxTopY;
                gb.Left = groupBoxTopX;
                gb.Width = groupBoxWidth;

                int currentHeight = 15;
                int lastComboboxBottom=0;
                foreach (var pair in characteristicControls)
                {
                    if (!_grousWithParametersLists[groupName].Contains(m_comboBoxTocharacteristic[pair.Value]))
                    {
                        continue;
                    }

                    Label characteristicLabel = pair.Key;
                    ComboBox unitsComboBox = pair.Value;

                    unitsComboBox.Parent = gb;
                    unitsComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
                    
                    unitsComboBox.Location = new Point(gb.Width - sizeAfterCombobox - unitsComboBox.Width,
                        currentHeight);

                    unitsComboBox.SelectedValueChanged += UnitChanged;

                    characteristicLabel.Parent = gb;
                    characteristicLabel.Location =
                        new Point(unitsComboBox.Location.X - sizeFromLabelToCombobox - characteristicLabel.Width,
                            currentHeight + 4);

                    currentHeight += 32;
                    lastComboboxBottom = unitsComboBox.Bottom;
                }
                gb.Height = lastComboboxBottom + 10;
                groupBoxTopY += gb.Height + 6;

                if (groupName == "Unsorted")
                {
                    groupBoxTopX += groupBoxWidth + 8;
                    groupBoxTopY = 8;
                }
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

        private void splitButtonMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string selectedScheme = e.ClickedItem.ToString();

            Type type = typeof(fsCharacteristicScheme);
            foreach (FieldInfo field in type.GetFields())
            {
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));
                if (selectedScheme == scheme.Name)
                {
                    Save();
                    scheme.SetCharacteristics( Characteristics);
                }
            }

            schemeBox.SelectedItem = selectedScheme;
        }

        private void TryToFindMatchedScheme()
        {
            Save();

            string schemeName="";

            foreach (FieldInfo field in typeof(fsCharacteristicScheme).GetFields())
            {                
                var scheme = ((fsCharacteristicScheme)field.GetValue(null));
                bool characteristicsMatched = true;

                foreach (var characteristic in Characteristics.Keys)
                {
                    if (scheme.CharacteristicToUnit[characteristic].Name != Characteristics[characteristic].Name)
                    {
                        characteristicsMatched = false;
                        break;
                    }
                }

                if (characteristicsMatched)
                {
                    schemeName = scheme.Name;
                    break;
                }
            }

            schemeBox.SelectedItem = schemeName;
        }

        private void showUsUnitsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ShowHideSecondaryCharacteristics(showUsUnitsCheckbox.Checked);
        }
    }
}