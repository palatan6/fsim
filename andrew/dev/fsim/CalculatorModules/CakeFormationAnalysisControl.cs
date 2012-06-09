using System;
using System.ComponentModel;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using Value;
using System.Drawing;

namespace CalculatorModules.Cake_Fromation
{
    public partial class CakeFormationAnalysisControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {

        public CakeFormationAnalysisControl()
        {
            InitializeComponent();

            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsTimeCalculator());
            Calculators.Add(new fsPorosityCalculator());
            Calculators.Add(new fsAnalysisFiltrationCalculator());

            #region Groups

            fsParametersGroup viscosityGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);
            fsParametersGroup filtrateGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);
            fsParametersGroup areaGroup = AddGroup(
                fsParameterIdentifier.FilterArea);
            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);            
            fsParametersGroup timeGroup = AddGroup(
                fsParameterIdentifier.NumberOfCyclones,                
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.ResidualTime);
            fsParametersGroup sfGroup = AddGroup(
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.SpecificFiltrationTime);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.CakeMass,
                fsParameterIdentifier.Qms,
                fsParameterIdentifier.Qsus,
                fsParameterIdentifier.SuspensionMassFlowrate,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.DryCakeDensity0,
                fsParameterIdentifier.Kappa,
                fsParameterIdentifier.Kappa0,
                fsParameterIdentifier.CakeMoistureContentRf,
                fsParameterIdentifier.CakeMoistureContentRf0);
            fsParametersGroup cakeFormationAndCharacterGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrateMass,
                fsParameterIdentifier.FiltrateVolume,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.qft,
                fsParameterIdentifier.qmft,
                fsParameterIdentifier.CakePlusMediumPermeability,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakePlusMediumResistance,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakePlusMediumResistanceAlpha,
                fsParameterIdentifier.CakeResistanceAlpha,
                fsParameterIdentifier.CakeResistanceAlpha0,
                fsParameterIdentifier.PracticalCakePermeability);
            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce0,
                fsParameterIdentifier.FilterMediumResistanceRm0);         
            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);
            fsParametersGroup neGroup = AddGroup(
                fsParameterIdentifier.Ne);

            var groups = new[]
                             {
                                 viscosityGroup,
                                 filtrateGroup,
                                 solidsGroup,
                                 concentrationGroup,
                                 areaGroup,
                                 pressureGroup,                                 
                                 timeGroup,
                                 sfGroup,
                                 porosityGroup,                                 
                                 cakeFormationAndCharacterGroup,
                                 hceGroup,                                 
                                 ncGroup,
                                 neGroup,
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255),
                                 Color.FromArgb(230, 255, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i].SetIsInputFlag(true);
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }

            ParameterToCell[fsParameterIdentifier.ResidualTime].OwningRow.Visible = false;

            #endregion

            fsMisc.FillList(filtrOptionBox.Items, typeof(fsCalculationOptions.fsFiltersKindOption));
            EstablishCalculationOption(fsCalculationOptions.fsFiltersKindOption.BatchFilterCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsFiltersKindOption), filtrOptionBox);

            fsMisc.FillList(simulationBox.Items, typeof(fsCalculationOptions.fsSimulationsOption));
            EstablishCalculationOption(fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsSimulationsOption), simulationBox);

            AssignDefaultValues();

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid, filtrOptionBox, simulationBox);
        }

        private void AssignDefaultValues()
        {
            SetDefaultValue(fsParameterIdentifier.SpecificFiltrationTime, new fsValue(1));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce0, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0));            
        }

        protected override void UpdateUIFromData()
        {
            var BatchFilterOption =
                (fsCalculationOptions.fsFiltersKindOption)
                CalculationOptions[typeof(fsCalculationOptions.fsFiltersKindOption)];
            bool isBatchFilterOption = BatchFilterOption ==
                                              fsCalculationOptions.fsFiltersKindOption.BatchFilterCalculations;

            ParameterToCell[fsParameterIdentifier.SuspensionMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SuspensionVolume].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SolidsMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SolidsVolume].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.CakeMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.FiltrateMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.FiltrateVolume].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.CakeVolume].OwningRow.Visible = isBatchFilterOption;

            ParameterToCell[fsParameterIdentifier.NumberOfCyclones].OwningRow.Visible = ! isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.CycleTime].OwningRow.Visible = !isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SpecificFiltrationTime].OwningRow.Visible = ! isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.Qms].OwningRow.Visible = !isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.Qsus].OwningRow.Visible = !isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SuspensionMassFlowrate].OwningRow.Visible = !isBatchFilterOption;
            
            var DefaultSimuationOption =
                (fsCalculationOptions.fsSimulationsOption)
                CalculationOptions[typeof(fsCalculationOptions.fsSimulationsOption)];
            bool isDefaultSimulationCalculation = DefaultSimuationOption == fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations;

            ParameterToCell[fsParameterIdentifier.CakePermeability].OwningRow.Visible = !isDefaultSimulationCalculation;
            ParameterToCell[fsParameterIdentifier.CakeResistance].OwningRow.Visible = !isDefaultSimulationCalculation;
            ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha].OwningRow.Visible = !isDefaultSimulationCalculation;
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceHce0].OwningRow.Visible = !isDefaultSimulationCalculation;
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceRm0].OwningRow.Visible = !isDefaultSimulationCalculation;

            bool isMrSimulationOption = DefaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MrSimulationsCalculations;

            ParameterToCell[fsParameterIdentifier.CakePermeability0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeResistance0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeCompressibility].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;

            bool isMrAndCcSimulationOption = DefaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MrAndCcSimulationsCalculations;

            ParameterToCell[fsParameterIdentifier.CakePorosity0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption && !isMrAndCcSimulationOption;
            ParameterToCell[fsParameterIdentifier.DryCakeDensity0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption && !isMrAndCcSimulationOption;
            ParameterToCell[fsParameterIdentifier.Kappa0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption && !isMrAndCcSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeMoistureContentRf0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption && !isMrAndCcSimulationOption;
            ParameterToCell[fsParameterIdentifier.Ne].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption && !isMrAndCcSimulationOption;

            base.UpdateUIFromData();
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            // this control has only one equation
        }

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            // this control hasn't calculation options
        }
    }
}
