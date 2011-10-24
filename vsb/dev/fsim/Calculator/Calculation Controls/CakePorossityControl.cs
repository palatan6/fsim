﻿using System.Drawing;
using Parameters;
using StepCalculators;

namespace Calculator.Calculation_Controls
{
    public sealed partial class fsCakePorossityControl : fsCalculatorControl
    {
        private readonly fsCakePorosityCalculator m_calculator = new fsCakePorosityCalculator();

        public fsCakePorossityControl()
        {
            InitializeComponent();

            Calculators.Add(m_calculator);

            var machineDiameterGroup = AddGroup(fsParameterIdentifier.MachineDiameter);
            var areaBGroup = AddGroup(fsParameterIdentifier.FilterB,
                fsParameterIdentifier.FilterBOverDiameter,
                fsParameterIdentifier.FilterArea);
            var filterElementDiameterGroup = AddGroup(fsParameterIdentifier.FilterElementDiameter);
            var cakeHeightGroup = AddGroup(fsParameterIdentifier.CakeHeight);
            var wetGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
            var dryGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            var concentrationGroup = AddGroup(fsParameterIdentifier.SaltConcentrationInTheCakeLiquid);
            var liquidGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
            var solidsGroup = AddGroup(fsParameterIdentifier.SolidsDensity);
            var porosityGroup = AddGroup(fsParameterIdentifier.CakePorosity);

            var groups = new[] {
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

            var colors = new[] {
                Color.FromArgb(255, 255, 230),
                Color.FromArgb(255, 230, 255)
            };

            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i].IsInput = true;
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            porosityGroup.IsInput = false;
            ParameterToCell[fsParameterIdentifier.CakePorosity].ReadOnly = true;

            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterArea].RowIndex, Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterB].RowIndex, Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].RowIndex, Color.FromArgb(255, 230, 230));

            fsMisc.FillList(saturationComboBox.Items, typeof(fsCakePorosityCalculator.fsSaturationOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaturationOption), saturationComboBox); 
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaturationOption.NotSaturatedCake);

            fsMisc.FillList(saltContentComboBox.Items, typeof(fsCakePorosityCalculator.fsSaltContentOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsSaltContentOption), saltContentComboBox); 
            EstablishCalculationOption(fsCakePorosityCalculator.fsSaltContentOption.Neglected);

            fsMisc.FillList(machineTypeComboBox.Items, typeof(fsCakePorosityCalculator.fsMachineTypeOption));
            AssignCalculationOptionAndControl(typeof(fsCakePorosityCalculator.fsMachineTypeOption), machineTypeComboBox);
            EstablishCalculationOption(fsCakePorosityCalculator.fsMachineTypeOption.PlainArea);

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid,
                saturationComboBox,
                saltContentComboBox,
                machineTypeComboBox);
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control has only one calculation group -- porosity
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            m_calculator.SaturationOption = (fsCakePorosityCalculator.fsSaturationOption)
                CalculationOptions[typeof(fsCakePorosityCalculator.fsSaturationOption)];
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
                CalculationOptions[typeof(fsCakePorosityCalculator.fsSaltContentOption)];

            var machineTypeOption =
                (fsCakePorosityCalculator.fsMachineTypeOption)
                CalculationOptions[typeof(fsCakePorosityCalculator.fsMachineTypeOption)];

            bool isSaltContentNeglected = saltContentOption == fsCakePorosityCalculator.fsSaltContentOption.Neglected;
            bool isSaturated = saturationOption == fsCakePorosityCalculator.fsSaturationOption.SaturatedCake;

            bool geometryVisible = !isSaturated;
            bool filterElementDiameterVisible = machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConvexCylindric;
            bool machineDiameterVisible = machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            bool bAndBOverDVisible = machineTypeOption == fsCakePorosityCalculator.fsMachineTypeOption.ConcaveCylindric;
            ParameterToCell[fsParameterIdentifier.FilterArea].OwningRow.Visible = geometryVisible;
            ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = geometryVisible && machineDiameterVisible;
            ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = geometryVisible && filterElementDiameterVisible;
            ParameterToCell[fsParameterIdentifier.FilterB].OwningRow.Visible = geometryVisible && bAndBOverDVisible;
            ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].OwningRow.Visible = geometryVisible && bAndBOverDVisible;

            ParameterToCell[fsParameterIdentifier.SaltConcentrationInTheCakeLiquid].OwningRow.Visible = !isSaltContentNeglected;
            ParameterToCell[fsParameterIdentifier.WetCakeMass].OwningRow.Visible = !isSaltContentNeglected || isSaturated;
            ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = !isSaltContentNeglected || isSaturated;
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
