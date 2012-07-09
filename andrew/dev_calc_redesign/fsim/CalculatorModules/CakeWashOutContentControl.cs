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
//     public sealed partial class fsCakeWashOutContentControl : fsOptionsSingleTableAndCommentsCalculatorControl
//     {
//         private readonly fsCakeWashOutContentCalculator m_calculator = new fsCakeWashOutContentCalculator();
// 
//         protected override void InitializeCalculators()
//         {
//             Calculator.AddEquations(m_calculator);
//         }
// 
//         protected override void InitializeGroups()
//         {
//             fsParametersGroup wetMassGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
//             fsParametersGroup liquidGroup = AddGroup(fsParameterIdentifier.LiquidMassForResuspension);
//             fsParametersGroup cwmGroup = AddGroup(fsParameterIdentifier.LiquidWashOutMassFraction);
//             fsParametersGroup rholGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
//             fsParametersGroup cwGroup = AddGroup(fsParameterIdentifier.LiquidWashOutConcentration);
//             fsParametersGroup phGroup = AddGroup(fsParameterIdentifier.Ph);
//             fsParametersGroup dryMassGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
//             fsParametersGroup outGroup = AddGroup(
//                 fsParameterIdentifier.PHcake,
//                 fsParameterIdentifier.CakeMoistureContentRf,
//                 fsParameterIdentifier.CakeWashOutContent);
// 
//             var groups = new[]
//                              {
//                                  wetMassGroup,
//                                  liquidGroup,
//                                  rholGroup,
//                                  cwGroup,
//                                  cwmGroup,
//                                  dryMassGroup,
//                                  phGroup,
//                                  outGroup
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
//             SetGroupInput(outGroup, false);
//         }
// 
//         protected override void InitializeCalculationOptionsUIControls()
//         {
//             fsMisc.FillList(fromComboBox.Items, typeof(fsCalculationOptions.fsFromCalculationOption));
//             EstablishCalculationOption(fsCalculationOptions.fsFromCalculationOption.WashOutContent);
//             AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsFromCalculationOption), fromComboBox);
// 
//             fsMisc.FillList(washOutContentComboBox.Items, typeof(fsCalculationOptions.fsWashOutContentOption));
//             EstablishCalculationOption(fsCalculationOptions.fsWashOutContentOption.AsConcentration);
//             AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsWashOutContentOption),
//                                               washOutContentComboBox);
//         }
// 
//         protected override Control[] GetUIControlsToConnectWithDataUpdating()
//         {
//             return new Control[] { dataGrid,
//                                       fromComboBox,
//                                       washOutContentComboBox };
//         }
// 
//         protected override void InitializeUnits()
//         {
//             SetUnits(fsCharacteristicScheme.LaboratoryScale.CharacteristicToUnit);
//         }
// 
//         protected override void InitializeParametersValues()
//         {
//             SetDefaultValue(fsParameterIdentifier.WetCakeMass, new fsValue(0.056));
//             SetDefaultValue(fsParameterIdentifier.LiquidMassForResuspension, new fsValue(0.100));
//             SetDefaultValue(fsParameterIdentifier.LiquidDensity, new fsValue(1000));
//             SetDefaultValue(fsParameterIdentifier.LiquidWashOutConcentration, new fsValue(30));
//             SetDefaultValue(fsParameterIdentifier.DryCakeMass, new fsValue(0.043));
//             SetDefaultValue(fsParameterIdentifier.LiquidWashOutMassFraction, new fsValue(0.03));
//             SetDefaultValue(fsParameterIdentifier.Ph, new fsValue(5.2));
//         }
// 
//         protected override void InitializeDefaultDiagrams()
//         {
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsFromCalculationOption.WashOutContent,
//                         fsCalculationOptions.fsWashOutContentOption.AsConcentration
//                     },
//                 new DiagramConfiguration(
//                     fsParameterIdentifier.LiquidWashOutConcentration,
//                     new DiagramConfiguration.DiagramRange(0, 100),
//                     new[] {fsParameterIdentifier.CakeWashOutContent}));
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsFromCalculationOption.WashOutContent,
//                         fsCalculationOptions.fsWashOutContentOption.AsMassFraction
//                     },
//                 new DiagramConfiguration(
//                     fsParameterIdentifier.LiquidWashOutMassFraction,
//                     new DiagramConfiguration.DiagramRange(0, 0.05),
//                     new[] {fsParameterIdentifier.CakeWashOutContent}));
// 
//             var pHDiagram = new DiagramConfiguration(
//                 fsParameterIdentifier.Ph,
//                 new DiagramConfiguration.DiagramRange(2, 14),
//                 new[] {fsParameterIdentifier.PHcake}); 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsFromCalculationOption.Ph,
//                         fsCalculationOptions.fsWashOutContentOption.AsMassFraction
//                     },
//                 pHDiagram);
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsFromCalculationOption.Ph,
//                         fsCalculationOptions.fsWashOutContentOption.AsConcentration
//                     },
//                 pHDiagram);
//         }
// 
//         public fsCakeWashOutContentControl()
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
//             m_calculator.FromCalculationOption =
//                 (fsCalculationOptions.fsFromCalculationOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsFromCalculationOption)];
//             m_calculator.WashOutContentOption =
//                 (fsCalculationOptions.fsWashOutContentOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsWashOutContentOption)];
//             m_calculator.RebuildEquationsList();
//         }
// 
//         protected override void UpdateUIFromData()
//         {
//             var fromContentOption =
//                 (fsCalculationOptions.fsFromCalculationOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsFromCalculationOption)];
//             bool isFromWashOutConcentration = fromContentOption ==
//                                               fsCalculationOptions.fsFromCalculationOption.WashOutContent;
// 
//             washOutContentLabel.Visible = isFromWashOutConcentration;
//             washOutContentComboBox.Visible = isFromWashOutConcentration;
// 
//             var washOutContentOption =
//                 (fsCalculationOptions.fsWashOutContentOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsWashOutContentOption)];
//             bool isCmInput = washOutContentOption ==
//                              fsCalculationOptions.fsWashOutContentOption.AsMassFraction;
// 
//             ParameterToCell[fsParameterIdentifier.LiquidWashOutMassFraction].OwningRow.Visible =
//                 isFromWashOutConcentration &&
//                 isCmInput;
//             ParameterToCell[fsParameterIdentifier.LiquidWashOutConcentration].OwningRow.Visible =
//                 isFromWashOutConcentration && !isCmInput;
//             ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = !isFromWashOutConcentration ||
//                                                                                      !isCmInput;
//             ParameterToCell[fsParameterIdentifier.Ph].OwningRow.Visible = !isFromWashOutConcentration;
//             ParameterToCell[fsParameterIdentifier.PHcake].OwningRow.Visible = !isFromWashOutConcentration;
// 
//             base.UpdateUIFromData();
//         }
// 
//         #endregion
//     }
}