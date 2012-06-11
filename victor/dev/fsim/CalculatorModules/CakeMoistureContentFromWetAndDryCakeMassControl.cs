using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;

namespace CalculatorModules
{
    public sealed partial class fsCakeMoistureContentFromWetAndDryCakeMassControl :
        fsOptionsSingleTableAndCommentsCalculatorControl
    {
        private readonly fsRfFromWetDryCakeCalculator m_calculator = new fsRfFromWetDryCakeCalculator();

        protected override void InitializeCalculatorControl()
        {
            Calculators.Add(m_calculator);

            fsParametersGroup wetMassGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
            fsParametersGroup dryMassGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            fsParametersGroup cmGroup = AddGroup(fsParameterIdentifier.SolutesMassFractionInLiquid);
            fsParametersGroup cGroup = AddGroup(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
            fsParametersGroup rholGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
            fsParametersGroup rfGroup = AddGroup(fsParameterIdentifier.CakeMoistureContent);

            var groups = new[]
                             {
                                 wetMassGroup,
                                 dryMassGroup,
                                 cmGroup,
                                 cGroup,
                                 rholGroup,
                                 rfGroup
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
            SetGroupInput(rfGroup, false);

            fsMisc.FillList(saltContentComboBox.Items, typeof (fsCalculationOptions.fsSaltContentOption));
            EstablishCalculationOption(fsCalculationOptions.fsSaltContentOption.Neglected);
            AssignCalculationOptionAndControl(typeof (fsCalculationOptions.fsSaltContentOption), saltContentComboBox);

            fsMisc.FillList(concentrationComboBox.Items, typeof (fsCalculationOptions.fsConcentrationOption));
            EstablishCalculationOption(fsCalculationOptions.fsConcentrationOption.SolidsMassFraction);
            AssignCalculationOptionAndControl(typeof (fsCalculationOptions.fsConcentrationOption), concentrationComboBox);

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            RecalculateAndRedraw();
            ConnectUIWithDataUpdating(dataGrid,
                                      saltContentComboBox,
                                      concentrationComboBox);
        }

        public fsCakeMoistureContentFromWetAndDryCakeMassControl()
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

            ParameterToCell[fsParameterIdentifier.SolutesMassFractionInLiquid].OwningRow.Visible =
                isSaltContConsidered && isCmInput;
            ParameterToCell[fsParameterIdentifier.SolutesConcentrationInCakeLiquid].OwningRow.Visible =
                isSaltContConsidered && !isCmInput;
            ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = isSaltContConsidered && !isCmInput;

            base.UpdateUIFromData();
        }

        #endregion
    }
}