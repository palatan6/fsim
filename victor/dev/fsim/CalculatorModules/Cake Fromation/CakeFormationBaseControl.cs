using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using Value;

namespace CalculatorModules.Cake_Fromation
{
    public partial class CakeFormationBaseControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        protected enum fsCalculationOption
        {
            [Description("Standard Calculation")]
            StandardCalculation,
            [Description("Filter Design")]
            FilterDesign
        }

        #endregion

        public CakeFormationBaseControl()
        {
            InitializeComponent();

            #region Calculators

            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsPorosityCalculator());
            Calculators.Add(new fsPermeabilityCalculator());
            Calculators.Add(new fsRm0Hce0Calculator());
            AddCakeFormationCalculator();

            #endregion

            fsMisc.FillList(calculationComboBox.Items, typeof(fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.StandardCalculation);
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), calculationComboBox);

            UpdateGroupsInputInfoFromCalculationOptions();

            AssignDefaultValues();

            UpdateEquationsFromCalculationOptions();
            SetDefaultDiagram(fsParameterIdentifier.u, fsParameterIdentifier.FilterArea, fsParameterIdentifier.SpecificFiltrationTime);
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(materialParametersDataGrid, dataGrid, calculationComboBox);
        }

        protected virtual void AddCakeFormationCalculator()
        {
            throw new Exception("Implement AddCakeFormationCalculator with specific cake formation calculator in deriviative class.");
        }

        private void materialParametersDisplayCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            tablesSplitContainer.Panel1Collapsed = !materialParametersDisplayCheckBox.Checked;
        }

        private void AssignDefaultValues()
        {
            Values[fsParameterIdentifier.MotherLiquidViscosity].Value = new fsValue(1e-3);
            Values[fsParameterIdentifier.MotherLiquidDensity].Value = new fsValue(1000);
            Values[fsParameterIdentifier.SolidsDensity].Value = new fsValue(1500);
            Values[fsParameterIdentifier.SuspensionSolidsMassFraction].Value = new fsValue(15e-2);
            Values[fsParameterIdentifier.Ne].Value = new fsValue(0.05);
            Values[fsParameterIdentifier.CakePorosity0].Value = new fsValue(55e-2);
            Values[fsParameterIdentifier.CakeCompressibility].Value = new fsValue(0.3);
            Values[fsParameterIdentifier.CakePermeability0].Value = new fsValue(1.5e-13);
            Values[fsParameterIdentifier.FilterMediumResistanceHce0].Value = new fsValue(3e-3);

            Values[fsParameterIdentifier.FilterArea].Value = new fsValue(1);
            Values[fsParameterIdentifier.ns].Value = new fsValue(12);
            Values[fsParameterIdentifier.ls].Value = new fsValue(0.2);
            Values[fsParameterIdentifier.nsf].Value = new fsValue(3);
            Values[fsParameterIdentifier.PressureDifference].Value = new fsValue(0.7e5);
            Values[fsParameterIdentifier.u].Value = new fsValue(2.0 / 60);
            
            // ttech0 and lambda
            if (Values.ContainsKey(fsParameterIdentifier.StandardTechnicalTime))
            {
                Values[fsParameterIdentifier.StandardTechnicalTime].Value = new fsValue(2);
            }
            if (Values.ContainsKey(fsParameterIdentifier.lambda))
            {
                Values[fsParameterIdentifier.lambda].Value = new fsValue(0.1);
            }
        }

        #region Routine Methods

        private void CreateStandardGourps()
        {
            Groups.Clear();
            AddGroupsToUI(materialParametersDataGrid, MakeMaterialGroups());
            AddGroupsToUI(dataGrid, MakeMachiningStandardGroups());
        }

        private void CreateDesignGroups()
        {
            Groups.Clear();
            AddGroupsToUI(materialParametersDataGrid, MakeMaterialGroups());
            AddGroupsToUI(dataGrid, MakeMachiningDesignGroups());
        }

        virtual protected fsParametersGroup[] MakeMaterialGroups()
        {
            throw new Exception("MakeMaterialGroups should be implemented in deriviative class.");
        }

        virtual protected fsParametersGroup[] MakeMachiningStandardGroups()
        {
            throw new Exception("MakeMachiningStandardGroups should be implemented in deriviative class.");
        }


        virtual protected fsParametersGroup[] MakeMachiningDesignGroups()
        {
            throw new Exception("MakeMachiningDesignGroups should be implemented in deriviative class.");
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption)CalculationOptions[typeof(fsCalculationOption)];
            if (calculationOption == fsCalculationOption.FilterDesign)
            {
                CreateDesignGroups();
            }
            else
            {
                CreateStandardGourps();
            }
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
        }

        #endregion
    }
}
