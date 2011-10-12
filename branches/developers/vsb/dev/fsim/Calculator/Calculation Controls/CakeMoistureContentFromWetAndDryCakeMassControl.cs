﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Parameters;
using StepCalculators;

namespace Calculator.Calculation_Controls
{
    public sealed partial class fsCakeMoistureContentFromWetAndDryCakeMassControl : fsCalculatorControl
    {
        #region Calculation Data

        private fsRfFromWetDryCakeCalculator.fsSaltContentOption m_saltContentOption;
        private fsRfFromWetDryCakeCalculator.fsConcentrationOption m_concentrationOption;

        #endregion

        private readonly fsRfFromWetDryCakeCalculator m_calculator = new fsRfFromWetDryCakeCalculator();

        public fsCakeMoistureContentFromWetAndDryCakeMassControl()
        {
            InitializeComponent();

            Calculators.Add(m_calculator);

            var wetMassGroup = AddGroup(fsParameterIdentifier.WetCakeMass);
            var dryMassGroup = AddGroup(fsParameterIdentifier.DryCakeMass);
            var cmGroup = AddGroup(fsParameterIdentifier.SolidsMassFraction);
            var cGroup = AddGroup(fsParameterIdentifier.SolidsConcentration);
            var rholGroup = AddGroup(fsParameterIdentifier.LiquidDensity);
            var rfGroup = AddGroup(fsParameterIdentifier.CakeMoistureContent);
            
            var groups = new[] {
                wetMassGroup,
                dryMassGroup,
                cmGroup,
                cGroup,
                rholGroup,
                rfGroup
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
            rfGroup.IsInput = false;
            ParameterToCell[fsParameterIdentifier.CakeMoistureContent].ReadOnly = true;

            fsMisc.FillComboBox(saltContentComboBox.Items, typeof(fsRfFromWetDryCakeCalculator.fsSaltContentOption));
            fsMisc.FillComboBox(concentrationComboBox.Items, typeof(fsRfFromWetDryCakeCalculator.fsConcentrationOption));
            m_saltContentOption = fsRfFromWetDryCakeCalculator.fsSaltContentOption.Neglected;
            m_concentrationOption = fsRfFromWetDryCakeCalculator.fsConcentrationOption.SolidsMassFraction;
            UpdateUIFromData();
            UpdateCalculationOptionAndInputGroupsFromUI();

            ConnectUIWithDataUpdating(dataGrid);
            UpdateUIFromData();
        }

        #region Routine Methods

        override protected void UpdateCalculationOptionAndInputGroupsFromUI()
        {
            m_saltContentOption =
                (fsRfFromWetDryCakeCalculator.fsSaltContentOption)
                fsMisc.GetEnum(typeof (fsRfFromWetDryCakeCalculator.fsSaltContentOption),
                               saltContentComboBox.Text);
            m_concentrationOption =
                (fsRfFromWetDryCakeCalculator.fsConcentrationOption)
                fsMisc.GetEnum(typeof (fsRfFromWetDryCakeCalculator.fsConcentrationOption),
                               concentrationComboBox.Text);

            m_calculator.SaltContentOption = m_saltContentOption;
            m_calculator.ConcentrationOption = m_concentrationOption;
            m_calculator.RebuildEquationsList();

            base.UpdateCalculationOptionAndInputGroupsFromUI();
        }

        protected override void UpdateUIFromData()
        {
            saltContentComboBox.Text = fsMisc.GetEnumDescription(m_saltContentOption);

            bool isSaltContConsidered = m_saltContentOption == fsRfFromWetDryCakeCalculator.fsSaltContentOption.Considered;

            concentrationLabel.Visible = isSaltContConsidered;
            concentrationComboBox.Visible = isSaltContConsidered;
            concentrationComboBox.Text = fsMisc.GetEnumDescription(m_concentrationOption);

            bool isCmInput = m_concentrationOption ==
                             fsRfFromWetDryCakeCalculator.fsConcentrationOption.SolidsMassFraction;
            ParameterToCell[fsParameterIdentifier.SolidsMassFraction].OwningRow.Visible = isSaltContConsidered && isCmInput;
            ParameterToCell[fsParameterIdentifier.SolidsConcentration].OwningRow.Visible = isSaltContConsidered && !isCmInput;
            ParameterToCell[fsParameterIdentifier.LiquidDensity].OwningRow.Visible = isSaltContConsidered && !isCmInput;

            base.UpdateUIFromData();
        }

        protected override void ConnectUIWithDataUpdating(fmDataGrid.fmDataGrid grid)
        {
            base.ConnectUIWithDataUpdating(grid);
            saltContentComboBox.TextChanged += RadioButtonCheckedChanged;
            concentrationComboBox.TextChanged += RadioButtonCheckedChanged;
        }

        #endregion
    }
}
