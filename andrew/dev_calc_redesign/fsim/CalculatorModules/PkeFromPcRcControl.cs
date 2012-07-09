using System;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;
using Value;

namespace CalculatorModules
{
//     public sealed partial class fsPkeFromPcRcControl : fsOptionsSingleTableAndCommentsCalculatorControl
//     {
//         private readonly fsPkeFromPcRcCalculator m_calculator = new fsPkeFromPcRcCalculator();
// 
//         protected override void InitializeCalculators()
//         {
//             Calculator.AddEquations(m_calculator);
//         }
// 
//         protected override void InitializeGroups()
//         {
//             fsParametersGroup permeabilityGroup = AddGroup(fsParameterIdentifier.CakePermeability);
//             fsParametersGroup resistanceGroup = AddGroup(fsParameterIdentifier.CakeResistance);
//             fsParametersGroup alphaGroup = AddGroup(fsParameterIdentifier.CakeResistanceAlpha);
//             fsParametersGroup rhosGroup = AddGroup(fsParameterIdentifier.SolidsDensity);
//             fsParametersGroup epsGroup = AddGroup(fsParameterIdentifier.CakePorosity);
//             fsParametersGroup rhosBulkGroup = AddGroup(fsParameterIdentifier.DryCakeDensity);
//             fsParametersGroup sigmaGroup = AddGroup(fsParameterIdentifier.SurfaceTensionLiquidInCake);
//             fsParametersGroup pkestGroup = AddGroup(fsParameterIdentifier.StandardCapillaryPressure);
//             fsParametersGroup pkeGroup = AddGroup(fsParameterIdentifier.CapillaryPressure);
// 
//             var groups = new[]
//                              {
//                                  permeabilityGroup,
//                                  resistanceGroup,
//                                  alphaGroup,
//                                  rhosGroup,
//                                  epsGroup,
//                                  rhosBulkGroup,
//                                  sigmaGroup,
//                                  pkestGroup,
//                                  pkeGroup
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
//             SetGroupInput(pkeGroup, false);
//         }
// 
//         protected override void InitializeCalculationOptionsUIControls()
//         {
//             fsMisc.FillList(inputCakeComboBox.Items, typeof(fsCalculationOptions.fsCakeInputOption));
//             AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsCakeInputOption), inputCakeComboBox);
//             EstablishCalculationOption(fsCalculationOptions.fsCakeInputOption.PermeabilityPc);
// 
//             fsMisc.FillList(enterSolidsDensityComboBox.Items, typeof(fsCalculationOptions.fsEnterSolidsDensity));
//             AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsEnterSolidsDensity), enterSolidsDensityComboBox);
//             EstablishCalculationOption(fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids);
//         }
// 
//         protected override Control[] GetUIControlsToConnectWithDataUpdating()
//         {
//             return new Control[] { dataGrid,
//                                       inputCakeComboBox,
//                                       enterSolidsDensityComboBox };
//         }
// 
//         protected override void InitializeParametersValues()
//         {
//             SetDefaultValue(fsParameterIdentifier.CakePermeability, new fsValue(0.3e-13));
//             SetDefaultValue(fsParameterIdentifier.SurfaceTensionLiquidInCake, new fsValue(60e-3));
//             SetDefaultValue(fsParameterIdentifier.DryCakeDensity, new fsValue(800));
//             SetDefaultValue(fsParameterIdentifier.CakeResistanceAlpha, new fsValue(2e10));
//             SetDefaultValue(fsParameterIdentifier.StandardCapillaryPressure, new fsValue(0.15e5));
//             SetDefaultValue(fsParameterIdentifier.CakePorosity, new fsValue(0.55));
//             SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(2300));
//         }
// 
//         protected override void InitializeDefaultDiagrams()
//         {
//             var permeabilityDiagram = new DiagramConfiguration(
//                 fsParameterIdentifier.CakePermeability,
//                 new DiagramConfiguration.DiagramRange(0.1e-13, 1e-13),
//                 new[] {fsParameterIdentifier.CapillaryPressure});
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.PermeabilityPc,
//                         fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids
//                     },
//                 permeabilityDiagram);
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.PermeabilityPc,
//                         fsCalculationOptions.fsEnterSolidsDensity.SolidsDensityAndCakePorosity
//                     },
//                 permeabilityDiagram);
// 
//             var resistanceDiagram = new DiagramConfiguration(
//                 fsParameterIdentifier.CakeResistance,
//                 new DiagramConfiguration.DiagramRange(0.1e+13, 10e+13),
//                 new[] { fsParameterIdentifier.CapillaryPressure });
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.ResistanceRc,
//                         fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids
//                     },
//                 resistanceDiagram);
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.ResistanceRc,
//                         fsCalculationOptions.fsEnterSolidsDensity.SolidsDensityAndCakePorosity
//                     },
//                 resistanceDiagram);
// 
//             var resistanceAlphaDiagram = new DiagramConfiguration(
//                 fsParameterIdentifier.CakeResistanceAlpha,
//                 new DiagramConfiguration.DiagramRange(0.1e+10, 20e+10),
//                 new[] {fsParameterIdentifier.CapillaryPressure});
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.ResistanceAlpha,
//                         fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids
//                     },
//                 resistanceAlphaDiagram);
// 
//             m_defaultDiagrams.Add(
//                 new Enum[]
//                     {
//                         fsCalculationOptions.fsCakeInputOption.ResistanceAlpha,
//                         fsCalculationOptions.fsEnterSolidsDensity.SolidsDensityAndCakePorosity
//                     },
//                 resistanceAlphaDiagram);
//         }
// 
//         public fsPkeFromPcRcControl()
//         {
//             InitializeComponent();
//         }
// 
//         #region Routine Methods
// 
//         protected override void UpdateGroupsInputInfoFromCalculationOptions()
//         {
//             m_calculator.CakeInputOption =
//                 (fsCalculationOptions.fsCakeInputOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsCakeInputOption)];
//             m_calculator.EnterSolidsOption =
//                 (fsCalculationOptions.fsEnterSolidsDensity)
//                 CalculationOptions[typeof (fsCalculationOptions.fsEnterSolidsDensity)];
//             m_calculator.RebuildEquationsList();
//         }
// 
//         protected override void UpdateEquationsFromCalculationOptions()
//         {
//             // this control uses only one calculator
//         }
// 
//         protected override void UpdateUIFromData()
//         {
//             var cakeInputOption =
//                 (fsCalculationOptions.fsCakeInputOption)
//                 CalculationOptions[typeof (fsCalculationOptions.fsCakeInputOption)];
//             ParameterToCell[fsParameterIdentifier.CakePermeability].OwningRow.Visible =
//                 cakeInputOption == fsCalculationOptions.fsCakeInputOption.PermeabilityPc;
//             ParameterToCell[fsParameterIdentifier.CakeResistance].OwningRow.Visible =
//                 cakeInputOption == fsCalculationOptions.fsCakeInputOption.ResistanceRc;
//             ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha].OwningRow.Visible =
//                 cakeInputOption == fsCalculationOptions.fsCakeInputOption.ResistanceAlpha;
// 
//             bool isAlpha = cakeInputOption == fsCalculationOptions.fsCakeInputOption.ResistanceAlpha;
//             enterSolidsDensityLabel.Visible = isAlpha;
//             enterSolidsDensityComboBox.Visible = isAlpha;
// 
//             var enterSolidsDensityOption =
//                 (fsCalculationOptions.fsEnterSolidsDensity)
//                 CalculationOptions[typeof (fsCalculationOptions.fsEnterSolidsDensity)];
//             bool isBulk = enterSolidsDensityOption == fsCalculationOptions.fsEnterSolidsDensity.BulkDensityDrySolids;
//             ParameterToCell[fsParameterIdentifier.DryCakeDensity].OwningRow.Visible = isAlpha && isBulk;
//             ParameterToCell[fsParameterIdentifier.SolidsDensity].OwningRow.Visible = isAlpha && !isBulk;
//             ParameterToCell[fsParameterIdentifier.CakePorosity].OwningRow.Visible = isAlpha && !isBulk;
// 
//             base.UpdateUIFromData();
//         }
// 
//         #endregion
//     }
}