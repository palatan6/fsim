using System;
using System.ComponentModel;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using Value;

namespace CalculatorModules.Cake_Formation
{
    public partial class fsCakeFormationBaseControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        private enum fsCalculationOption
        {
            [Description("Standard Calculation")]
            StandardCalculation,
            [Description("Filter Design")]
            FilterDesign
        }

        #endregion

        public fsCakeFormationBaseControl()
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

        protected override sealed void Recalculate()
        {
            base.Recalculate();
        }

        protected virtual void AddCakeFormationCalculator()
        {
            throw new Exception("Implement AddCakeFormationCalculator with specific cake formation calculator in deriviative class.");
        }

        private void MaterialParametersDisplayCheckBoxCheckedChanged(object sender, EventArgs e)
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
            Values[fsParameterIdentifier.PressureDifference].Value = new fsValue(0.7e5);
            
            // u
            if (Values.ContainsKey(fsParameterIdentifier.u))
            {
                Values[fsParameterIdentifier.u].Value = new fsValue(2.0 / 60);
            }
            
            // ttech0 and lambda
            if (Values.ContainsKey(fsParameterIdentifier.StandardTechnicalTime))
            {
                Values[fsParameterIdentifier.StandardTechnicalTime].Value = new fsValue(2);
                Values[fsParameterIdentifier.lambda].Value = new fsValue(0.1);
            }

            // ns, ls, nsf
            if (Values.ContainsKey(fsParameterIdentifier.ns))
            {
                Values[fsParameterIdentifier.ns].Value = new fsValue(12);
                Values[fsParameterIdentifier.ls].Value = new fsValue(0.2);
                Values[fsParameterIdentifier.nsf].Value = new fsValue(3);
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

        protected virtual fsParametersGroup[] MakeMachiningStandardGroups()
        {
            throw new Exception("MakeMachiningStandardGroups should be implemented in deriviative class.");
        }

        protected virtual fsParametersGroup[] MakeMachiningDesignGroups()
        {
            throw new Exception("MakeMachiningDesignGroups should be implemented in deriviative class.");
        }

        private fsParametersGroup[] MakeMaterialGroups()
        {
            fsParametersGroup etafGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);

            fsParametersGroup rhofGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);

            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);

            fsParametersGroup cGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);

            fsParametersGroup neGroup = AddGroup(
                fsParameterIdentifier.Ne);

            fsParametersGroup epsGroup = AddGroup(
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.Kappa0,
                fsParameterIdentifier.DryCakeDensity0,
                fsParameterIdentifier.CakeWetDensity0,
                fsParameterIdentifier.CakeWetMassSolidsFractionRs0,
                fsParameterIdentifier.CakeMoistureContentRf0,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.Kappa,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.CakeWetDensity,
                fsParameterIdentifier.CakeWetMassSolidsFractionRs,
                fsParameterIdentifier.CakeMoistureContentRf);

            fsParametersGroup pcrcGroup = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha);

            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);

            fsParametersGroup hce0Group = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce0,
                fsParameterIdentifier.FilterMediumResistanceRm0);

            return new[]
                       {
                           etafGroup,
                           rhofGroup,
                           densitiesGroup,
                           cGroup,
                           neGroup,
                           epsGroup,
                           pcrcGroup,
                           ncGroup,
                           hce0Group,
                       };
        }
        
        protected override sealed void UpdateGroupsInputInfoFromCalculationOptions()
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

        protected override sealed void UpdateEquationsFromCalculationOptions()
        {
        }

        #endregion
    }
}
