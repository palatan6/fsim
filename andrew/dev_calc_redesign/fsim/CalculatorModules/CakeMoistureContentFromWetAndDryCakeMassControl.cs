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
//     public sealed partial class fsCakeMoistureContentFromWetAndDryCakeMassControl :
//         fsOptionsSingleTableAndCommentsCalculatorControl
//     {
//         private readonly fsRfFromWetDryCakeCalculator m_calculator = new fsRfFromWetDryCakeCalculator();
// 
//         protected override void InitializeCalculators()
//         {
//             Calculator.AddEquations(m_calculator);
//         }
// 
//         protected override void InitializeGroups()
//         {
//             fsParametersGroup wetMassGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
//             fsParametersGroup dryMassGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
//             fsParametersGroup cmGroup = AddGroup(fsParameterIdentifier.SolutesMassFractionInLiquid);
//             fsParametersGroup cGroup = AddGroup(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
//             fsParametersGroup rholGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
//             fsParametersGroup rfGroup = AddGroup(fsParameterIdentifier.CakeMoistureContentRf);
// 
//             var groups = new[]
//                              {
//                                  wetMassGroup,
//                                  dryMassGroup,
//                                  cmGroup,
//                                  cGroup,
//                                  rholGroup,
//                                  rfGroup
//                              };
// 
//             var colors = new[]
//                              {
//                                  Color.FromArgb(255, 255, 230),
//                                  Color.FromArgb(255, 230, 255)
//                              };
// 
//             for (int i = 0; i < groups.Length; ++i)
//             {
//                 AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
//                 SetGroupInput(groups[i], true);
//             }
//             SetGroupInput(rfGroup, false);
//         }
// 
//         protected override void InitializeCalculationOptionsUIControls()
//         {
//             fsMisc.FillList(saltContentComboBox.Items, typeof (fsCalculationOptions.fsSaltContentOption));
//             EstablishCalculationOption(fsCalculationOptions.fsSaltContentOption.Neglected);
//             AssignCalculationOptionAndControl(typeof (fsCalculationOptions.fsSaltContentOption), saltContentComboBox);
// 
//             fsMisc.FillList(concentrationComboBox.Items, typeof (fsCalculationOptions.fsConcentrationOption));
//             EstablishCalculationOption(fsCalculationOptions.fsConcentrationOption.SolidsMassFraction);
//             AssignCalculationOptionAndControl(typeof (fsCalculationOptions.fsConcentrationOption), concentrationComboBox);
//         }
// 
//         protected override Control[] GetUIControlsToConnectWithDataUpdating()
//         {
//             return new Control[] { dataGrid,
//                                       saltContentComboBox,
//                                       concentrationComboBox };
//         }
// 
//         protected override void InitializeUnits()
//         {
//             SetUnits(fsCharacteristicScheme.LaboratoryScale.CharacteristicToUnit);
//         }
// 
//         protected override void InitializeParametersValues()
//         {
//             SetDefaultValue(fsParameterIdentifier.WetCakeMass, new fsValue(35e-3));
//             SetDefaultValue(fsParameterIdentifier.DryCakeMass, new fsValue(27e-3));
//             SetDefaultValue(fsParameterIdentifier.SolutesMassFractionInLiquid, new fsValue(0.22));
//             SetDefaultValue(fsParameterIdentifier.SolutesConcentrationInCakeLiquid, new fsValue(45));
//             SetDefaultValue(fsParameterIdentifier.LiquidDensity, new fsValue(1000));
//         }
// 
//         protected override void InitializeDefaultDiagrams()
//         {
//             var saltContentNeglectedDiagram = new DiagramConfiguration(
//                 fsParameterIdentifier.DryCakeMass,
//                 new DiagramConfiguration.DiagramRange(10e-3, 30e-3),
//                 new[] {fsParameterIdentifier.CakeMoistureContentRf});
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsSaltContentOption.Neglected,
//                         fsCalculationOptions.fsConcentrationOption.SolidsMassFraction
//                     },
//                 saltContentNeglectedDiagram);
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsSaltContentOption.Neglected,
//                         fsCalculationOptions.fsConcentrationOption.Concentration
//                     },
//                 saltContentNeglectedDiagram);
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsSaltContentOption.Considered,
//                         fsCalculationOptions.fsConcentrationOption.SolidsMassFraction
//                     },
//                 new DiagramConfiguration(
//                     fsParameterIdentifier.SolutesMassFractionInLiquid,
//                     new DiagramConfiguration.DiagramRange(0, 0.1),
//                     new[] {fsParameterIdentifier.CakeMoistureContentRf}));
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsSaltContentOption.Considered,
//                         fsCalculationOptions.fsConcentrationOption.Concentration
//                     },
//                 new DiagramConfiguration(
//                     fsParameterIdentifier.SolutesConcentrationInCakeLiquid,
//                     new DiagramConfiguration.DiagramRange(0, 100),
//                     new[] { fsParameterIdentifier.CakeMoistureContentRf }));
//         }
// 
//         public fsCakeMoistureContentFromWetAndDryCakeMassControl()
//         {
//             InitializeComponent();
//         }
// 
//         #region Routine Methods
// 
//         protected override void UpdateGroupsInputInfoFromCalculationOptions()
//         {
//             // this control has only one calculation group -- RF
//         }
// 
//         protected override void UpdateEquationsFromCalculationOptions()
//         {
//             m_calculator.SaltContentOption =
//                 (fsCalculationOptions.fsSaltContentOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsSaltContentOption)];
//             m_calculator.ConcentrationOption =
//                 (fsCalculationOptions.fsConcentrationOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsConcentrationOption)];
//             m_calculator.RebuildEquationsList();
//         }
// 
//         protected override void UpdateUIFromData()
//         {
//             var saltContentOption =
//                 (fsCalculationOptions.fsSaltContentOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsSaltContentOption)];
//             bool isSaltContConsidered = saltContentOption == fsCalculationOptions.fsSaltContentOption.Considered;
// 
//             concentrationLabel.Visible = isSaltContConsidered;
//             concentrationComboBox.Visible = isSaltContConsidered;
// 
//             var concentrationOption =
//                 (fsCalculationOptions.fsConcentrationOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsConcentrationOption)];
//             bool isCmInput = concentrationOption ==
//                              fsCalculationOptions.fsConcentrationOption.SolidsMassFraction;
// 
//             ParameterToCell[fsParameterIdentifier.SolutesMassFractionInLiquid].OwningRow.Visible =
//                 isSaltContConsidered && isCmInput;
//             ParameterToCell[fsParameterIdentifier.SolutesConcentrationInCakeLiquid].OwningRow.Visible =
//                 isSaltContConsidered && !isCmInput;
//             ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = isSaltContConsidered && !isCmInput;
// 
//             base.UpdateUIFromData();
//         }
// 
//         #endregion
//     }
}