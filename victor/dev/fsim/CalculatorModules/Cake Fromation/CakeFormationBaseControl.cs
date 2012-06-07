using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using CalculatorModules.Base_Controls;
using Parameters;
using ParametersIdentifiers;
using StepCalculators;
using Value;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCakeFormationBaseControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        #region Calculation Option

        public enum fsCakeFormationCalculationOption
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

            fsMisc.FillList(calculationComboBox.Items, typeof(fsCakeFormationCalculationOption));
            EstablishCalculationOption(fsCakeFormationCalculationOption.StandardCalculation);
            AssignCalculationOptionAndControl(typeof(fsCakeFormationCalculationOption), calculationComboBox);

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
            SetDefaultValue(fsParameterIdentifier.MotherLiquidViscosity, new fsValue(1e-3));
            SetDefaultValue(fsParameterIdentifier.MotherLiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(1500));
            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(15e-2));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0.05));
            SetDefaultValue(fsParameterIdentifier.CakePorosity0, new fsValue(55e-2));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.3));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(1.5e-13));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(3e-3));

            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(1));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(0.7e5));
            
            // tc (u)
            SetDefaultValue(fsParameterIdentifier.CycleTime, new fsValue(72));
            
            // ttech0 and lambda
            SetDefaultValue(fsParameterIdentifier.StandardTechnicalTime, new fsValue(2));
            SetDefaultValue(fsParameterIdentifier.lambda, new fsValue(0.1));

            // ns, ls, nsf
            SetDefaultValue(fsParameterIdentifier.ns, new fsValue(12));
            SetDefaultValue(fsParameterIdentifier.FilterLength, new fsValue(2.4));
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(0.25));
        }

        private void SetDefaultValue(fsParameterIdentifier identifier, fsValue value)
        {
            if (Values.ContainsKey(identifier))
            {
                ParameterToGroup[identifier].Representator = identifier;
                Values[identifier].Value = value;
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

            var materialGroups = new fsParametersGroup[]
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
            foreach (fsParametersGroup group in materialGroups)
            {
                group.Kind = fsParametersGroup.fsParametersGroupKind.MaterialParameters;
            }
            return materialGroups;
        }
        
        protected override sealed void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCakeFormationCalculationOption)CalculationOptions[typeof(fsCakeFormationCalculationOption)];
            if (calculationOption == fsCakeFormationCalculationOption.FilterDesign)
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

        public fsCakeFormationCalculationOption GetCalculationOption()
        {
            return (fsCakeFormationCalculationOption) CalculationOptions[typeof (fsCakeFormationCalculationOption)];
        }

        internal void SetCalculationOption(fsCakeFormationCalculationOption cakeFormationCalculationOption)
        {
            if (GetCalculationOption() != cakeFormationCalculationOption)
            {
                EstablishCalculationOption(cakeFormationCalculationOption);
                UpdateGroupsInputInfoFromCalculationOptions();
                UpdateEquationsFromCalculationOptions();
                Recalculate();
                UpdateUIFromData();
            }
        }

        internal bool GetMaterialParametersTableVisible()
        {
            return materialParametersDisplayCheckBox.Checked;
        }

        internal void SetMaterialParametersTableVisible(bool isVisible)
        {
            materialParametersDisplayCheckBox.Checked = isVisible;
        }

        internal ICollection<fsSimulationModuleParameter> GetValues()
        {
            return Values.Values;
        }

        internal void SetValues(ICollection<fsSimulationModuleParameter> collection)
        {
            foreach (fsSimulationModuleParameter parameter in collection)
            {
                if (Values.ContainsKey(parameter.Identifier))
                {
                    fsSimulationModuleParameter internalParameter = Values[parameter.Identifier];
                    internalParameter.Value = parameter.Value;
                }
            }

            Recalculate();
            UpdateUIFromData();
        }
    }
}
