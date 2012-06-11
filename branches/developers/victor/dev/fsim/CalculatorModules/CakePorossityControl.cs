﻿using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using System.Windows.Forms;

namespace CalculatorModules
{
    public sealed partial class fsCakePorossityControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        private readonly fsCakePorosityCalculator m_calculator = new fsCakePorosityCalculator();

        protected override void InitializeCalculatorControl()
        {
            Calculators.Add(m_calculator);

            fsParametersGroup machineDiameterGroup = AddGroup(fsParameterIdentifier.MachineDiameter);
            fsParametersGroup areaBGroup = AddGroup(fsParameterIdentifier.MachineWidth,
                                                    fsParameterIdentifier.WidthOverDiameterRatio,
                                                    fsParameterIdentifier.FilterArea);
            fsParametersGroup filterElementDiameterGroup = AddGroup(fsParameterIdentifier.FilterElementDiameter);
            fsParametersGroup cakeHeightGroup = AddGroup(fsParameterIdentifier.CakeHeight);
            fsParametersGroup wetGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
            fsParametersGroup dryGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            fsParametersGroup concentrationGroup = AddGroup(fsParameterIdentifier.SolutesConcentrationInCakeLiquid);
            fsParametersGroup liquidGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(fsParameterIdentifier.SolidsDensity);
            fsParametersGroup porosityGroup = AddGroup(fsParameterIdentifier.CakePorosity);

            var groups = new[]
                             {
                                 machineDiameterGroup,
                                 areaBGroup,
                                 filterElementDiameterGroup,
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
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.MachineWidth].RowIndex,
                        Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].RowIndex,
                        Color.FromArgb(255, 230, 230));

            fsMisc.FillList(saturationComboBox.Items, typeof(fsCakePorosityCalculator.fsSaturationOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaturationOption), saturationComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake);

            fsMisc.FillList(saltContentComboBox.Items, typeof(fsCakePorosityCalculator.fsSaltContentOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaltContentOption), saltContentComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaltContentOption.Neglected);

            fsMisc.FillList(machineTypeComboBox.Items, typeof(fsCakePorosityCalculator.fsMachineTypeOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsMachineTypeOption), machineTypeComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid,
                                      saturationComboBox,
                                      saltContentComboBox,
                                      machineTypeComboBox };
        }

        public fsCakePorossityControl()
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
            m_calculator.MachineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];
            m_calculator.RebuildEquationsList();
        }

        protected override void UpdateUIFromData()
        {
            var saturationOption =
                (fsCakePorosityCalculator.fsSaturationOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsSaturationOption)];
            if (saturationOption == fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake)
            {
                machineTypeComboBox.Enabled = true;
            }
            if (saturationOption == fsCakePorosityCalculator.fsSaturationOption.SaturatedCake)
            {
                machineTypeComboBox.Enabled = false;
            }

            var saltContentOption =
                (fsCakePorosityCalculator.fsSaltContentOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsSaltContentOption)];

            var machineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof (fsCakePorosityCalculator.fsMachineTypeOption)];

            bool isSaltContentNeglected = saltContentOption == fsCakePorosityCalculator.fsSaltContentOption.Neglected;
            bool isSaturated = saturationOption == fsCakePorosityCalculator.fsSaturationOption.SaturatedCake;

            bool geometryVisible = !isSaturated;
            bool filterElementDiameterVisible = machineTypeOption ==
                                                fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric;
            bool machineDiameterVisible = machineTypeOption ==
                                          fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            bool bAndBOverDVisible = machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            ParameterToCell[fsParameterIdentifier.FilterArea].OwningRow.Visible = geometryVisible;
            ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = geometryVisible &&
                                                                                       machineDiameterVisible;
            ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = geometryVisible &&
                                                                                             filterElementDiameterVisible;
            ParameterToCell[fsParameterIdentifier.MachineWidth].OwningRow.Visible = geometryVisible && bAndBOverDVisible;
            ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].OwningRow.Visible = geometryVisible &&
                                                                                              bAndBOverDVisible;

            ParameterToCell[fsParameterIdentifier.SolutesConcentrationInCakeLiquid].OwningRow.Visible =
                !isSaltContentNeglected;
            ParameterToCell[fsParameterIdentifier.WetCakeMass].OwningRow.Visible = !isSaltContentNeglected ||
                                                                                   isSaturated;
            ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = !isSaltContentNeglected ||
                                                                                     isSaturated;
            ParameterToCell[fsParameterIdentifier.CakeHeight].OwningRow.Visible = !isSaturated;


            base.UpdateUIFromData();
        }

        //protected override void UpdateUIFromData()
        //{

        //    base.UpdateUIFromData();
        //}

        #endregion
    }
}