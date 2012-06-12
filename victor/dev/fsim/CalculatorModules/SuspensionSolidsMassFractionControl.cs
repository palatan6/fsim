using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;

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