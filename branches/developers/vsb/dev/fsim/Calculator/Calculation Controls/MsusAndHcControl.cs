﻿using System.Collections.Generic;
using System.Drawing;
using Parameters;
using StepCalculators;

namespace Calculator.Calculation_Controls
{
    public sealed partial class fsMsusAndHcControl : fsCalculatorControl
    {
        #region Calculation Data

        enum fsMachineTypeOption
        {
            PlainArea,
            ConvexCylindric,
            ConcaveCylindric
        }
        private fsMachineTypeOption m_machineTypeOption;

        enum fsCalculationOption
        {
            DenisitiesCalculated,
            ConcentreationsCalculated,
            PorosityKappaCalculated,
            MachineDiameterCalculated,
            FilterElementDiameterCalculated,
            MachineAreaBCalculated,
            CakeHeightCalculated,
            MassVolumeCalculated
        }

        #endregion

        #region Routine Data

        private readonly fsParametersGroup m_areaBGroup;

        private readonly List<fsCalculator> m_plainAreaCalculators = new List<fsCalculator>();
        private readonly List<fsCalculator> m_convexCylindricAreaCalculators = new List<fsCalculator>();
        private readonly List<fsCalculator> m_concaveCylindricAreaCalculators = new List<fsCalculator>();

        #endregion

        public fsMsusAndHcControl()
        {
            InitializeComponent();

            m_plainAreaCalculators.Add(new fsMsusHcPlainAreaCalculator());
            m_convexCylindricAreaCalculators.Add(new fsMsusHcConvexCylindricAreaCalculator());
            m_concaveCylindricAreaCalculators.Add(new fsMsusHcConcaveCylindricAreaCalculator());
            Calculators = m_plainAreaCalculators;

            var filtrateGroup = AddGroup(
                fsParameterIdentifier.FiltrateDensity);
            var densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            var concentrationGroup = AddGroup(
                fsParameterIdentifier.SolidsMassFraction,
                fsParameterIdentifier.SolidsVolumeFraction,
                fsParameterIdentifier.SolidsConcentration);
            var epsKappaGroup = AddGroup(
                fsParameterIdentifier.Porosity,
                fsParameterIdentifier.Kappa);
            var machineDiameterGroup = AddGroup(
                fsParameterIdentifier.MachineDiameter);
            m_areaBGroup = AddGroup(
                fsParameterIdentifier.FilterB,
                fsParameterIdentifier.FilterBOverDiameter,
                fsParameterIdentifier.FilterArea);
            var diameterFilterElementGroup = AddGroup(
                fsParameterIdentifier.FilterElementDiameter);
            var cakeHeightGroup = AddGroup(
                fsParameterIdentifier.CakeHeight);
            var massVolumeGroup = AddGroup(
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SuspensionMass);

            var groups = new[] {
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

            var colors = new[] {
                Color.FromArgb(255, 255, 230),
                Color.FromArgb(255, 230, 255)
            };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);    
            }
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterArea].RowIndex, Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterB].RowIndex, Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].RowIndex, Color.FromArgb(255, 230, 230));

            AssignCalculationOption(fsCalculationOption.DenisitiesCalculated, denisitiesRadioButton, densitiesGroup);
            AssignCalculationOption(fsCalculationOption.ConcentreationsCalculated, concentrationRadioButton, concentrationGroup);
            AssignCalculationOption(fsCalculationOption.PorosityKappaCalculated, epsKappaRadioButton, epsKappaGroup);
            AssignCalculationOption(fsCalculationOption.MachineDiameterCalculated, machineDiameterRadioButton, machineDiameterGroup);
            AssignCalculationOption(fsCalculationOption.FilterElementDiameterCalculated, filterElementDiameterRadioButton, diameterFilterElementGroup);
            AssignCalculationOption(fsCalculationOption.MachineAreaBCalculated, areaRadioButton, m_areaBGroup);
            AssignCalculationOption(fsCalculationOption.CakeHeightCalculated, cakeHeightRadioButton, cakeHeightGroup);
            AssignCalculationOption(fsCalculationOption.MassVolumeCalculated, massVolumeRadioButton, massVolumeGroup);
            
            CalculationOption = fsCalculationOption.MassVolumeCalculated;
            m_machineTypeOption = fsMachineTypeOption.PlainArea;
            UpdateCalculationOptionAndInputGroupsFromUI();

            ConnectUIWithDataUpdating(dataGrid);
            UpdateUIFromData();
        }
        
        #region Routine Methods

        override protected void UpdateCalculationOptionAndInputGroupsFromUI()
        {
            if (planeAreaRadioButton.Checked)
            {
                m_machineTypeOption = fsMachineTypeOption.PlainArea;
            }
            if (convexAreaRadioButton.Checked)
            {
                m_machineTypeOption = fsMachineTypeOption.ConvexCylindric;
            }
            if (concaveAreaRadioButton.Checked)
            {
                m_machineTypeOption = fsMachineTypeOption.ConcaveCylindric;
            }

            base.UpdateCalculationOptionAndInputGroupsFromUI();
        }

        protected override void UpdateUIFromData()
        {
            if (m_machineTypeOption == fsMachineTypeOption.PlainArea)
            {
                planeAreaRadioButton.Checked = true;
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterB].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].OwningRow.Visible = false;
                machineDiameterRadioButton.Visible = false;
                filterElementDiameterRadioButton.Visible = false;
                if (machineDiameterRadioButton.Checked)
                {
                    areaRadioButton.Checked = true;
                }
                Calculators = m_plainAreaCalculators;
                m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
            }
            if (m_machineTypeOption == fsMachineTypeOption.ConvexCylindric)
            {
                convexAreaRadioButton.Checked = true;
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.FilterB].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].OwningRow.Visible = false;
                machineDiameterRadioButton.Visible = false;
                filterElementDiameterRadioButton.Visible = true;
                Calculators = m_convexCylindricAreaCalculators;
                m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
            }
            if (m_machineTypeOption == fsMachineTypeOption.ConcaveCylindric)
            {
                concaveAreaRadioButton.Checked = true;
                ParameterToCell[fsParameterIdentifier.MachineDiameter].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.FilterElementDiameter].OwningRow.Visible = false;
                ParameterToCell[fsParameterIdentifier.FilterB].OwningRow.Visible = true;
                ParameterToCell[fsParameterIdentifier.FilterBOverDiameter].OwningRow.Visible = true;
                machineDiameterRadioButton.Visible = true;
                filterElementDiameterRadioButton.Visible = false;
                Calculators = m_concaveCylindricAreaCalculators;
            }

            base.UpdateUIFromData();

            textBox1.Lines = Calculators[0].GetStatusMessage().Split('\n');
        }

        protected override void ConnectUIWithDataUpdating(fmDataGrid.fmDataGrid grid)
        {
            base.ConnectUIWithDataUpdating(grid);
            planeAreaRadioButton.CheckedChanged += RadioButtonCheckedChanged;
            convexAreaRadioButton.CheckedChanged += RadioButtonCheckedChanged;
            concaveAreaRadioButton.CheckedChanged += RadioButtonCheckedChanged;
        }

        #endregion
    }
}
