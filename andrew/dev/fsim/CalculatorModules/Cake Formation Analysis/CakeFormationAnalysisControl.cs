using System;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using Value;
using System.Windows.Forms;

namespace CalculatorModules.Cake_Fromation
{
    public sealed partial class CakeFormationAnalysisControl : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        public CakeFormationAnalysisControl()
        {
            InitializeComponent();
        }

        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsAnalysisFiltrationCalculator());
        }
        protected override void InitializeGroups()
        {
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
                fsParameterIdentifier.RotationalSpeed,                
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.ResidualTime);
            fsParametersGroup sfGroup = AddGroup(
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.SpecificFiltrationTime);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SpecificSolidsMass,
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
                fsParameterIdentifier.CakeSolidsContentCmc,
                fsParameterIdentifier.CakeMoistureContentRf,
                fsParameterIdentifier.CakeMoistureContentRf0);
            fsParametersGroup cakeFormationAndCharacterGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrateMass,
                fsParameterIdentifier.FiltrateVolume,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.qf,
                fsParameterIdentifier.qmf,
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
                fsParameterIdentifier.FilterMediumResistanceHce,
                fsParameterIdentifier.FilterMediumResistanceRm);         
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
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid, filtrationOptionBox, simulationBox };
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.MotherLiquidViscosity, new fsValue(1e-3));
            SetDefaultValue(fsParameterIdentifier.MotherLiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(1500));
            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(0.15));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(20e-4));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.FiltrationTime, new fsValue(95));
            SetDefaultValue(fsParameterIdentifier.SuspensionMass, new fsValue(0.250));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.FiltrateMass, new fsValue(0.187));
        }

        protected override void InitializeDefaultDiagrams()
        {
            var batchFilterDiagram = new DiagramConfiguration(
                fsParameterIdentifier.FiltrateMass,
                new DiagramConfiguration.DiagramRange(0.100, 0.200),
                new[] {fsParameterIdentifier.CakePlusMediumPermeability},
                new[] {fsParameterIdentifier.CakePorosity, fsParameterIdentifier.CakeHeight}); 

            var continuousFilterDiagram = new DiagramConfiguration(
                fsParameterIdentifier.CakeHeight,
                new DiagramConfiguration.DiagramRange(0.009, 0.015),
                new[] {fsParameterIdentifier.CakePlusMediumPermeability},
                new[] {fsParameterIdentifier.CakePorosity});

            var secondOptions = new[]
                                    {
                                        fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations,
                                        fsCalculationOptions.fsSimulationsOption.
                                            MediumResistanceAndCakeCompressibilitySimulationsCalculations,
                                        fsCalculationOptions.fsSimulationsOption.MediumResistanceSimulationsCalculations
                                        ,
                                        fsCalculationOptions.fsSimulationsOption.ShowAlsoNeSimulationsCalculations
                                    };

            foreach (fsCalculationOptions.fsSimulationsOption e2 in secondOptions)
            {
                m_defaultDiagrams.Add(new Enum[] {fsCalculationOptions.fsFiltersKindOption.BatchFilterCalculations, e2}, batchFilterDiagram);
                m_defaultDiagrams.Add(new Enum[] {fsCalculationOptions.fsFiltersKindOption.ContinuousFilterCalculations, e2}, continuousFilterDiagram);
            }
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(filtrationOptionBox.Items, typeof(fsCalculationOptions.fsFiltersKindOption));
            EstablishCalculationOption(fsCalculationOptions.fsFiltersKindOption.BatchFilterCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsFiltersKindOption), filtrationOptionBox);

            fsMisc.FillList(simulationBox.Items, typeof(fsCalculationOptions.fsSimulationsOption));
            EstablishCalculationOption(fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsSimulationsOption), simulationBox);
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
            ParameterToCell[fsParameterIdentifier.SpecificSolidsMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.SolidsVolume].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.CakeMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.FiltrateMass].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.FiltrateVolume].OwningRow.Visible = isBatchFilterOption;
            ParameterToCell[fsParameterIdentifier.CakeVolume].OwningRow.Visible = isBatchFilterOption;

            ParameterToCell[fsParameterIdentifier.RotationalSpeed].OwningRow.Visible = ! isBatchFilterOption;
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
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceHce].OwningRow.Visible = !isDefaultSimulationCalculation;
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceRm].OwningRow.Visible = !isDefaultSimulationCalculation;

            bool isMrSimulationOption = DefaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MediumResistanceSimulationsCalculations;

            ParameterToCell[fsParameterIdentifier.CakePermeability0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeResistance0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha0].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;
            ParameterToCell[fsParameterIdentifier.CakeCompressibility].OwningRow.Visible = !isDefaultSimulationCalculation && !isMrSimulationOption;

            bool isMrAndCcSimulationOption = DefaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MediumResistanceAndCakeCompressibilitySimulationsCalculations;

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

        private void simulationBox_DropDown(object sender, EventArgs e)
        {
            //ComboBox's DropDown menu fitting
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }
    }
}
