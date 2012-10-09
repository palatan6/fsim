using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;
using Value;

namespace CalculatorModules
{
    public sealed partial class fsMsusAndHcControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        #region Calculation Data

        private enum fsCalculationOption
        {
            [Description("Densities")] DensitiesCalculated,
            [Description("Concentrations")] ConcentreationsCalculated,
            [Description("Porosity / Kappa")] PorosityKappaCalculated,
            [Description("Machine Diameter")] MachineDiameterCalculated,
            [Description("Filter Element Diameter")] FilterElementDiameterCalculated,
            [Description("Machine Geometry")] MachineAreaBCalculated,
            [Description("Cake Height")] CakeHeightCalculated,
            [Description("Mass / Volume")] MassVolumeCalculated
        }

        #endregion

        #region Routine Data

        private fsParametersGroup m_areaBGroup;

        private readonly List<fsCalculator> m_concaveCylindricAreaCalculators = new List<fsCalculator>();
        private readonly List<fsCalculator> m_convexCylindricAreaCalculators = new List<fsCalculator>();
        private readonly List<fsCalculator> m_plainAreaCalculators = new List<fsCalculator>();

        #endregion

        protected override void InitializeCalculators()
        {
            m_plainAreaCalculators.Add(new fsMsusHcPlainAreaCalculator());
            m_convexCylindricAreaCalculators.Add(new fsMsusHcConvexCylindricAreaCalculator());
            m_concaveCylindricAreaCalculators.Add(new fsMsusHcConcaveCylindricAreaCalculator());
            Calculators = m_plainAreaCalculators;
        }

        protected override void InitializeGroups()
        {
            fsParametersGroup filtrateGroup = AddGroup(
               fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);
            fsParametersGroup epsKappaGroup = AddGroup(
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.Kappa);
            fsParametersGroup machineDiameterGroup = AddGroup(
                fsParameterIdentifier.MachineDiameter);
            m_areaBGroup = AddGroup(
                fsParameterIdentifier.MachineWidth,
                fsParameterIdentifier.WidthOverDiameterRatio,
                fsParameterIdentifier.FilterArea);
            fsParametersGroup diameterFilterElementGroup = AddGroup(
                fsParameterIdentifier.FilterElementDiameter);
            fsParametersGroup cakeHeightGroup = AddGroup(
                fsParameterIdentifier.CakeHeight);
            fsParametersGroup massVolumeGroup = AddGroup(
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SuspensionMass);

            var groups = new[]
                             {
                                 filtrateGroup,
                                 densitiesGroup,
                                 concentrationGroup,
                                 epsKappaGroup,
                                 machineDiameterGroup,
                                 m_areaBGroup,
                                 diameterFilterElementGroup,
                                 cakeHeightGroup,
                                 massVolumeGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterArea].RowIndex,
                        Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.MachineWidth].RowIndex,
                        Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].RowIndex,
                        Color.FromArgb(255, 230, 230));
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(machineTypeComboBox.Items, typeof(fsCakePorosityCalculator.fsMachineTypeOption));
            EstablishCalculationOption(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea);
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsMachineTypeOption), machineTypeComboBox);

            EstablishCalculationOption(fsCalculationOption.MassVolumeCalculated);
            FillCalculationComboBox();

            m_isBlockedCalculationOptionChanged = false;
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculationOptionComboBox);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid,
                                      machineTypeComboBox,
                                      calculationOptionComboBox };
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.MotherLiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(2000));
            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(0.15));
            SetDefaultValue(fsParameterIdentifier.CakePorosity, new fsValue(0.5));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(1));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(0.020));
            SetDefaultValue(fsParameterIdentifier.FilterElementDiameter, new fsValue(0.050));
            SetDefaultValue(fsParameterIdentifier.MachineDiameter, new fsValue(0.400));
        }

        protected override void InitializeDefaultDiagrams()
        {
            #region Plain Area

            m_defaultDiagrams.Add(
                new Enum[]
                    {fsCakePorosityCalculator.fsMachineTypeOption.PlainArea, fsCalculationOption.MassVolumeCalculated},
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.005, 0.100),
                    new[] {fsParameterIdentifier.SuspensionMass},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {fsCakePorosityCalculator.fsMachineTypeOption.PlainArea, fsCalculationOption.CakeHeightCalculated},
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionMass,
                    new DiagramConfiguration.DiagramRange(10, 500),
                    new[] {fsParameterIdentifier.CakeHeight},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {fsCakePorosityCalculator.fsMachineTypeOption.PlainArea, fsCalculationOption.MachineAreaBCalculated},
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    new DiagramConfiguration.DiagramRange(0.05, 0.30),
                    new[] {fsParameterIdentifier.FilterArea},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCalculationOption.PorosityKappaCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    new DiagramConfiguration.DiagramRange(0.05, 0.30),
                    new[] {fsParameterIdentifier.CakePorosity},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCalculationOption.ConcentreationsCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakePorosity,
                    new DiagramConfiguration.DiagramRange(0.30, 0.80),
                    new[] {fsParameterIdentifier.SuspensionSolidsMassFraction},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {fsCakePorosityCalculator.fsMachineTypeOption.PlainArea, fsCalculationOption.DensitiesCalculated},
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.100, 0.300),
                    new[] {fsParameterIdentifier.SolidsDensity},
                    new[] {fsParameterIdentifier.SuspensionDensity}));

            #endregion

            #region Convex Area

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.DensitiesCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.005, 0.015),
                    new[] {fsParameterIdentifier.SolidsDensity},
                    new[] {fsParameterIdentifier.SuspensionDensity}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.ConcentreationsCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakePorosity,
                    new DiagramConfiguration.DiagramRange(0.30, 0.80),
                    new[] {fsParameterIdentifier.SuspensionSolidsMassFraction},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.PorosityKappaCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    new DiagramConfiguration.DiagramRange(0.05, 0.30),
                    new[] {fsParameterIdentifier.CakePorosity},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.FilterElementDiameterCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionVolume,
                    new DiagramConfiguration.DiagramRange(0.600, 1.000),
                    new[] {fsParameterIdentifier.FilterElementDiameter},
                    new fsParameterIdentifier[] {}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.MachineAreaBCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionMass,
                    new DiagramConfiguration.DiagramRange(300, 1000),
                    new[] {fsParameterIdentifier.FilterArea},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.CakeHeightCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionMass,
                    new DiagramConfiguration.DiagramRange(300, 1000),
                    new[] {fsParameterIdentifier.CakeHeight},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCalculationOption.MassVolumeCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.003, 0.030),
                    new[] {fsParameterIdentifier.SuspensionVolume},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            #endregion

            #region Concave Area

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.MassVolumeCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.010, 0.050),
                    new[] {fsParameterIdentifier.SuspensionVolume},
                    new[] {fsParameterIdentifier.SuspensionMass}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.CakeHeightCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionMass,
                    new DiagramConfiguration.DiagramRange(300, 1000),
                    new[] {fsParameterIdentifier.CakeHeight},
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.MachineAreaBCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionMass,
                    new DiagramConfiguration.DiagramRange(50, 100),
                    new[] {fsParameterIdentifier.FilterArea},
                    new[] {fsParameterIdentifier.WidthOverDiameterRatio}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.PorosityKappaCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.050, 0.150),
                    new[] {fsParameterIdentifier.CakePorosity},
                    new[] {fsParameterIdentifier.Kappa}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.ConcentreationsCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.050, 0.150),
                    new[]
                        {
                            fsParameterIdentifier.SuspensionSolidsMassFraction,
                            fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                            fsParameterIdentifier.SuspensionSolidsConcentration
                        },
                    new[] {fsParameterIdentifier.SuspensionVolume}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCalculationOption.DensitiesCalculated
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(0.050, 0.150),
                    new[]
                        {
                            fsParameterIdentifier.SolidsDensity,
                            fsParameterIdentifier.SuspensionDensity
                        },
                    new[] { fsParameterIdentifier.SuspensionSolidsVolumeFraction }));

            #endregion
        }

        public fsMsusAndHcControl()
        {
            InitializeComponent();
        }

        #region Routine Methods

        private bool m_isBlockedCalculationOptionChanged;

        private void FillCalculationComboBox()
        {
            var machineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];
            var restrictedOptions = new List<fsCalculationOption>();
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.PlainArea)
            {
                restrictedOptions.Add(fsCalculationOption.MachineDiameterCalculated);
                restrictedOptions.Add(fsCalculationOption.FilterElementDiameterCalculated);
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric)
            {
                restrictedOptions.Add(fsCalculationOption.MachineDiameterCalculated);
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric)
            {
                restrictedOptions.Add(fsCalculationOption.FilterElementDiameterCalculated);
            }

            var calculationOption = (fsCalculationOption) CalculationOptions[typeof (fsCalculationOption)];
            if (restrictedOptions.Contains(calculationOption))
            {
                EstablishCalculationOption(fsCalculationOption.MassVolumeCalculated);
            }

            fsMisc.FillList(calculationOptionComboBox.Items, typeof (fsCalculationOption));
            foreach (fsCalculationOption restrictedOption in restrictedOptions)
            {
                calculationOptionComboBox.Items.Remove(fsMisc.GetEnumDescription(restrictedOption));
            }
            calculationOptionComboBox.Text =
                fsMisc.GetEnumDescription((fsCalculationOption) CalculationOptions[typeof (fsCalculationOption)]);
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption) CalculationOptions[typeof (fsCalculationOption)];
            fsParametersGroup calculateGroup = null;
            switch (calculationOption)
            {
                case fsCalculationOption.DensitiesCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SolidsDensity];
                    break;
                case fsCalculationOption.ConcentreationsCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SuspensionSolidsMassFraction];
                    break;
                case fsCalculationOption.PorosityKappaCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakePorosity];
                    break;
                case fsCalculationOption.MachineDiameterCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.MachineDiameter];
                    break;
                case fsCalculationOption.FilterElementDiameterCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.FilterElementDiameter];
                    break;
                case fsCalculationOption.MachineAreaBCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.FilterArea];
                    break;
                case fsCalculationOption.CakeHeightCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.CakeHeight];
                    break;
                case fsCalculationOption.MassVolumeCalculated:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.SuspensionMass];
                    break;
            }
            foreach (fsParametersGroup group in ParameterToGroup.Values)
            {
                SetGroupInput(group, group != calculateGroup);
            }
        }

        protected override void CalculationOptionChanged(object sender, EventArgs e)
        {
            if (m_isBlockedCalculationOptionChanged)
                return;

            m_isBlockedCalculationOptionChanged = true;

            UpdateCalculationOptionFromUI();
            FillCalculationComboBox();

            m_isBlockedCalculationOptionChanged = false;

            base.CalculationOptionChanged(sender, e);
        }

        protected override void UpdateUIFromData()
        {
            var machineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.PlainArea)
            {
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.MachineWidth].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].OwningRow.Visible = false;
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric)
            {
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.MachineWidth].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].OwningRow.Visible = false;
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric)
            {
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.MachineWidth].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].OwningRow.Visible = true;
            }

            base.UpdateUIFromData();
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            var machineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.PlainArea)
            {
                Calculators = m_plainAreaCalculators;
                m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric)
            {
                Calculators = m_convexCylindricAreaCalculators;
                m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
            }
            if (machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric)
            {
                Calculators = m_concaveCylindricAreaCalculators;
                m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
            }
        }

        #endregion
    }
}