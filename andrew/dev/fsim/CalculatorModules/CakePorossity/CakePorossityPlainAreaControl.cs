using System;
using System.Drawing;
using CalculatorModules.Cake_Formation_Analysis;
using Parameters;
using StepCalculators;
using System.Windows.Forms;
using Units;
using Value;

namespace CalculatorModules
{
    public sealed partial class CakePorossityPlainAreaControl : CakePorossityBaseControl
    {
        protected override void InitializeGroups()
        {
            //fsParametersGroup machineDiameterGroup = AddGroup(fsParameterIdentifier.MachineDiameter);
            fsParametersGroup areaBGroup = AddGroup(/*fsParameterIdentifier.MachineWidth,
                                                    fsParameterIdentifier.WidthOverDiameterRatio,*/
                                                    fsParameterIdentifier.FilterArea
                                                                                      );
            //fsParametersGroup filterElementDiameterGroup = AddGroup(fsParameterIdentifier.FilterElementDiameter);
            fsParametersGroup cakeHeightGroup = AddGroup(fsParameterIdentifier.CakeHeight);
            fsParametersGroup wetGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
            fsParametersGroup dryGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            fsParametersGroup concentrationGroup = AddGroup(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
            fsParametersGroup liquidGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(fsParameterIdentifier.SolidsDensity);
            fsParametersGroup porosityGroup = AddGroup(fsParameterIdentifier.CakePorosity);

            var groups = new[]
                             {
                                 //machineDiameterGroup,
                                 areaBGroup,
                                 //filterElementDiameterGroup,
                                 cakeHeightGroup,
                                 wetGroup,
                                 dryGroup,
                                 concentrationGroup,
                                 liquidGroup,
                                 solidsGroup,
                                 porosityGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i].SetIsInputFlag(true);
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            porosityGroup.SetIsInputFlag(false);
            ParameterToCell[fsParameterIdentifier.CakePorosity].ReadOnly = true;

            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterArea].RowIndex,
                        Color.FromArgb(255, 230, 230));
            //SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.MachineWidth].RowIndex,
            //            Color.FromArgb(255, 230, 230));
            //SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].RowIndex,
            //            Color.FromArgb(255, 230, 230));
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(saturationComboBox.Items, typeof(fsCakePorosityCalculator.fsSaturationOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaturationOption), saturationComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake);

            fsMisc.FillList(saltContentComboBox.Items, typeof(fsCakePorosityCalculator.fsSaltContentOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaltContentOption), saltContentComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaltContentOption.Neglected);

            //fsMisc.FillList(machineTypeComboBox.Items, typeof(fsCakePorosityCalculator.fsMachineTypeOption));
            //AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsMachineTypeOption), machineTypeComboBox);
            //EstablishCalculationOption(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid,
                                      saturationComboBox,
                                      saltContentComboBox};
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(20e-4));
            SetDefaultValue(fsParameterIdentifier.CakeHeight, new fsValue(25e-3));
            SetDefaultValue(fsParameterIdentifier.WetCakeMass, new fsValue(55e-3));
            SetDefaultValue(fsParameterIdentifier.DryCakeMass, new fsValue(41e-3));
            SetDefaultValue(fsParameterIdentifier.SolutesConcentrationInCakeLiquid, new fsValue(12));
            SetDefaultValue(fsParameterIdentifier.LiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(2300));
            SetDefaultValue(fsParameterIdentifier.FilterElementDiameter, new fsValue(25e-3));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(22e-3, 27e-3),
                    new[] {fsParameterIdentifier.CakePorosity}));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCakePorosityCalculator.fsSaltContentOption.Considered
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.SolutesConcentrationInCakeLiquid,
                    new DiagramConfiguration.DiagramRange(0, 100),
                    new[] { fsParameterIdentifier.CakePorosity }));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(8e-3, 12e-3),
                    new[] { fsParameterIdentifier.CakePorosity }));

            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                new DiagramConfiguration(
                    fsParameterIdentifier.CakeHeight,
                    new DiagramConfiguration.DiagramRange(38e-3, 45e-3),
                    new[] { fsParameterIdentifier.CakePorosity }));


            #region Saturated Cake And Neglected Salt Content

            var diagramForSaturatedCakeAndNeglectedSaltContent = new DiagramConfiguration(
                fsParameterIdentifier.SolidsDensity,
                new DiagramConfiguration.DiagramRange(2200, 2500),
                new[] {fsParameterIdentifier.CakePorosity});
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                diagramForSaturatedCakeAndNeglectedSaltContent);
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                diagramForSaturatedCakeAndNeglectedSaltContent);
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Neglected
                    },
                diagramForSaturatedCakeAndNeglectedSaltContent);

            #endregion

            #region Saturated Cake And Considered Salt Content

            var diagramForSaturatedCakeAndConsideredSaltContent = new DiagramConfiguration(
                fsParameterIdentifier.SolutesConcentrationInCakeLiquid,
                new DiagramConfiguration.DiagramRange(0, 200),
                new[] {fsParameterIdentifier.CakePorosity});
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.PlainArea,
                        fsCakePorosityCalculator.fsSaltContentOption.Considered
                    },
                diagramForSaturatedCakeAndConsideredSaltContent);
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Considered
                    },
                diagramForSaturatedCakeAndConsideredSaltContent);
            m_defaultDiagrams.Add(
                new Enum[]
                    {
                        fsCakePorosityCalculator.fsSaturationOption.SaturatedCake,
                        fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric,
                        fsCakePorosityCalculator.fsSaltContentOption.Considered
                    },
                diagramForSaturatedCakeAndConsideredSaltContent);

            #endregion
        }

        public CakePorossityPlainAreaControl()
        {
            InitializeComponent();
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control has only one calculation group -- porosity
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            m_calculator.SaturationOption = (fsCakePorosityCalculator.fsSaturationOption)
                                            CalculationOptions[typeof (fsCakePorosityCalculator.fsSaturationOption)];
            m_calculator.SaltContentOption =
                (fsCakePorosityCalculator.fsSaltContentOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsSaltContentOption)];
            //m_calculator.MachineTypeOption =
            //    (fsCakePorosityCalculator.fsMachineTypeOption)
            //    CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];
            m_calculator.RebuildEquationsList();
        }

        protected override void UpdateUIFromData()
        {
            var saturationOption =
                (fsCakePorosityCalculator.fsSaturationOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsSaturationOption)];
            if (saturationOption == fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake)
            {
                //machineTypeComboBox.Enabled = true;
            }
            if (saturationOption == fsCakePorosityCalculator.fsSaturationOption.SaturatedCake)
            {
                //machineTypeComboBox.Enabled = false;
            }

            var saltContentOption =
                (fsCakePorosityCalculator.fsSaltContentOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsSaltContentOption)];

            //var machineTypeOption =
            //    (fsCakePorosityCalculator.fsMachineTypeOption)
            //    CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];

            bool isSaltContentNeglected = saltContentOption == fsCakePorosityCalculator.fsSaltContentOption.Neglected;
            bool isSaturated = saturationOption == fsCakePorosityCalculator.fsSaturationOption.SaturatedCake;

            bool geometryVisible = !isSaturated;
            //bool filterElementDiameterVisible = machineTypeOption ==
            //                                    fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric;
            //bool machineDiameterVisible = machineTypeOption ==
            //                              fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            //bool bAndBOverDVisible = machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            ParameterToCell[fsParameterIdentifier.FilterArea].OwningRow.Visible = geometryVisible;
            //ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = geometryVisible /*&&
            //                                                                           machineDiameterVisible*/;
            //ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = geometryVisible /*&&
            //                                                                                 filterElementDiameterVisible*/;
            //ParameterToCell[fsParameterIdentifier.MachineWidth].OwningRow.Visible = geometryVisible /*&& bAndBOverDVisible*/;
            //ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].OwningRow.Visible = geometryVisible /*&&
            //                                                                                  bAndBOverDVisible*/;

            ParameterToCell[fsParameterIdentifier.SolutesConcentrationInCakeLiquid].OwningRow.Visible =
                !isSaltContentNeglected;
            ParameterToCell[fsParameterIdentifier.WetCakeMass].OwningRow.Visible = !isSaltContentNeglected ||
                                                                                   isSaturated;
            ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = !isSaltContentNeglected ||
                                                                                     isSaturated;
            ParameterToCell[fsParameterIdentifier.CakeHeight].OwningRow.Visible = !isSaturated;


            base.UpdateUIFromData();
        }
        #endregion
    }
}