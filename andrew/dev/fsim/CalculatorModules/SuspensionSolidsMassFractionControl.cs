using System;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;
using Units;
using Value;

namespace CalculatorModules
{
    public partial class SuspensionSolidsMassFractionControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        private readonly fsSuspensionSolidsMassFractionCalculator m_calculator =
            new fsSuspensionSolidsMassFractionCalculator();

        protected override void InitializeCalculators()
        {
            Calculators.Add(m_calculator);
        }

        protected override void InitializeGroups()
        {
            fsParametersGroup wetMassGroup = AddGroup(fsParameterIdentifier.SuspensionMass);
            fsParametersGroup dryMassGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            fsParametersGroup cfmGroup = AddGroup(fsParameterIdentifier.SolutesMassFractionInMotherLiquid);
            fsParametersGroup cfGroup = AddGroup(fsParameterIdentifier.SolutesConcentrationInMotherLiquid);
            fsParametersGroup rhofGroup = AddGroup(fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup cmGroup = AddGroup(fsParameterIdentifier.SuspensionSolidsMassFraction);

            var groups = new[]
                             {
                                 wetMassGroup,
                                 dryMassGroup,
                                 cfmGroup,
                                 cfGroup,
                                 rhofGroup,
                                 cmGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
                SetGroupInput(groups[i], true);
            }
            SetGroupInput(cmGroup, false);
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(saltContentComboBox.Items, typeof(fsCalculationOptions.fsSaltContentOption));
            EstablishCalculationOption(fsCalculationOptions.fsSaltContentOption.Neglected);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsSaltContentOption), saltContentComboBox);

            fsMisc.FillList(concentrationComboBox.Items, typeof(fsCalculationOptions.fsConcentrationOption));
            EstablishCalculationOption(fsCalculationOptions.fsConcentrationOption.SolidsMassFraction);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsConcentrationOption), concentrationComboBox);
        }

        protected override void InitializeDefaultDiagrams()
        {
            var neglectedDiagram = new DiagramConfiguration(
                fsParameterIdentifier.DryCakeMass,
                new DiagramConfiguration.DiagramRange(0.001, 0.020),
                new[] {fsParameterIdentifier.SuspensionSolidsMassFraction},
                new fsParameterIdentifier[] {});

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCalculationOptions.fsSaltContentOption.Neglected,
                        fsCalculationOptions.fsConcentrationOption.SolidsMassFraction
                    },
                neglectedDiagram);

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCalculationOptions.fsSaltContentOption.Neglected,
                        fsCalculationOptions.fsConcentrationOption.Concentration
                    },
                neglectedDiagram);

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCalculationOptions.fsSaltContentOption.Considered,
                        fsCalculationOptions.fsConcentrationOption.SolidsMassFraction
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SolutesMassFractionInMotherLiquid,
                    new DiagramConfiguration.DiagramRange(0, 0.2),
                    new[] {fsParameterIdentifier.SuspensionSolidsMassFraction},
                    new fsParameterIdentifier[] {}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCalculationOptions.fsSaltContentOption.Considered,
                        fsCalculationOptions.fsConcentrationOption.Concentration
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SolutesConcentrationInMotherLiquid,
                    new DiagramConfiguration.DiagramRange(0, 100),
                    new[] {fsParameterIdentifier.SuspensionSolidsMassFraction},
                    new fsParameterIdentifier[] { }));
        }

        protected override void InitializeUnits()
        {
            SetUnits(fsCharacteristicScheme.LaboratoryScale.CharacteristicToUnit);
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.SuspensionMass, new fsValue(0.050));
            SetDefaultValue(fsParameterIdentifier.DryCakeMass, new fsValue(0.012));
            SetDefaultValue(fsParameterIdentifier.SolutesMassFractionInMotherLiquid, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.SolutesConcentrationInMotherLiquid, new fsValue(49.5));
            SetDefaultValue(fsParameterIdentifier.MotherLiquidDensity, new fsValue(1000));
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid,
                                      saltContentComboBox,
                                      concentrationComboBox };
        }

        public SuspensionSolidsMassFractionControl()
        {
            InitializeComponent();
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control has only one calculation group -- RF
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            m_calculator.SaltContentOption =
                (fsCalculationOptions.fsSaltContentOption)
                CalculationOptions[typeof (fsCalculationOptions.fsSaltContentOption)];
            m_calculator.ConcentrationOption =
                (fsCalculationOptions.fsConcentrationOption)
                CalculationOptions[typeof (fsCalculationOptions.fsConcentrationOption)];
            m_calculator.RebuildEquationsList();
        }

        protected override void UpdateUIFromData()
        {
            var saltContentOption =
                (fsCalculationOptions.fsSaltContentOption)
                CalculationOptions[typeof (fsCalculationOptions.fsSaltContentOption)];
            bool isSaltContConsidered = saltContentOption == fsCalculationOptions.fsSaltContentOption.Considered;

            concentrationLabel.Visible = isSaltContConsidered;
            concentrationComboBox.Visible = isSaltContConsidered;

            var concentrationOption =
                (fsCalculationOptions.fsConcentrationOption)
                CalculationOptions[typeof (fsCalculationOptions.fsConcentrationOption)];
            bool isCmInput = concentrationOption ==
                             fsCalculationOptions.fsConcentrationOption.SolidsMassFraction;

            ParameterToCell[fsParameterIdentifier.SolutesMassFractionInMotherLiquid].OwningRow.Visible =
                isSaltContConsidered && isCmInput;
            ParameterToCell[fsParameterIdentifier.SolutesConcentrationInMotherLiquid].OwningRow.Visible =
                isSaltContConsidered && !isCmInput;
            ParameterToCell[fsParameterIdentifier.MotherLiquidDensity].OwningRow.Visible = isSaltContConsidered &&
                                                                                           !isCmInput;

            base.UpdateUIFromData();
        }

        #endregion
    }
}