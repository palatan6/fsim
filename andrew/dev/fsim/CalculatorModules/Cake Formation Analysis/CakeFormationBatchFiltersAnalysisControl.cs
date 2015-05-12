using System;
using System.Drawing;
using CalculatorModules.Cake_Formation_Analysis;
using Parameters;
using StepCalculators;
using Value;
using System.Windows.Forms;

namespace CalculatorModules.Cake_Fromation
{
    public sealed partial class CakeFormationBatchFiltersAnalysisControl : CakeFormationAnalysisBaseControl
    {
        public CakeFormationBatchFiltersAnalysisControl()
        {
            InitializeComponent();
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
            fsParametersGroup sfGroup = AddGroup(
                fsParameterIdentifier.FiltrationTime);
            fsParametersGroup porosityGroup = AddGroup(
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SpecificSolidsMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.CakeMass,
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
                                 sfGroup,
                                 porosityGroup,                                 
                                 cakeFormationAndCharacterGroup,
                                 hceGroup,                                 
                                 ncGroup,
                                 neGroup
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
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid, simulationBox };
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

        protected override void UpdateUIFromData()
        {
            HideUnusedParametersDependingOnCalculationOption();

            base.UpdateUIFromData();
        }

        void HideUnusedParametersDependingOnCalculationOption()
        {
            var defaultSimuationOption =
                (fsCalculationOptions.fsSimulationsOption)
                CalculationOptions[typeof(fsCalculationOptions.fsSimulationsOption)];
            bool isDefaultSimulationCalculation = defaultSimuationOption == fsCalculationOptions.fsSimulationsOption.DefaultSimulationsCalculations;

            //ParameterToCell[fsParameterIdentifier.CakePermeability].OwningRow.Visible = !isDefaultSimulationCalculation;
            //ParameterToCell[fsParameterIdentifier.CakeResistance].OwningRow.Visible = !isDefaultSimulationCalculation;
            //ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha].OwningRow.Visible = !isDefaultSimulationCalculation;
            //ParameterToCell[fsParameterIdentifier.FilterMediumResistanceHce].OwningRow.Visible = !isDefaultSimulationCalculation;
            //ParameterToCell[fsParameterIdentifier.FilterMediumResistanceRm].OwningRow.Visible = !isDefaultSimulationCalculation;
            
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakePermeability].OwningRow, !isDefaultSimulationCalculation);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeResistance].OwningRow, !isDefaultSimulationCalculation);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha].OwningRow, !isDefaultSimulationCalculation);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.FilterMediumResistanceHce].OwningRow, !isDefaultSimulationCalculation);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.FilterMediumResistanceRm].OwningRow, !isDefaultSimulationCalculation);

            int isDefaultSimulationCalculationCoef = Convert.ToInt32(!isDefaultSimulationCalculation);
            int rowHeight = ParameterToCell[fsParameterIdentifier.MotherLiquidViscosity].DataGridView.RowTemplate.Height;

            ParameterToCell[fsParameterIdentifier.CakePermeability].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef;
            ParameterToCell[fsParameterIdentifier.CakeResistance].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef;
            ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef;
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceHce].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef;
            ParameterToCell[fsParameterIdentifier.FilterMediumResistanceRm].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef;

            bool isMrSimulationOption = defaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MediumResistanceSimulationsCalculations;

            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakePermeability0].OwningRow, !isDefaultSimulationCalculation &&
                                                                                        !isMrSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeResistance0].OwningRow, !isDefaultSimulationCalculation &&
                                                                                        !isMrSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha0].OwningRow, !isDefaultSimulationCalculation &&
                                                                                        !isMrSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeCompressibility].OwningRow, !isDefaultSimulationCalculation &&
                                                                                        !isMrSimulationOption);

            int isMrSimulationOptionCoef = Convert.ToInt32(!isMrSimulationOption);

            ParameterToCell[fsParameterIdentifier.CakePermeability0].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef *
                                                                                        isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.CakeResistance0].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef *
                                                                                        isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.CakeResistanceAlpha0].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef *
                                                                                        isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.CakeCompressibility].OwningRow.Height = rowHeight *
                                                                                        isDefaultSimulationCalculationCoef *
                                                                                        isMrSimulationOptionCoef;

            bool isMrAndCcSimulationOption = defaultSimuationOption == fsCalculationOptions.fsSimulationsOption.MediumResistanceAndCakeCompressibilitySimulationsCalculations;

            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakePorosity0].OwningRow,
                !isDefaultSimulationCalculation &&
                !isMrSimulationOption && !isMrAndCcSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.DryCakeDensity0].OwningRow,
                !isDefaultSimulationCalculation &&
                !isMrSimulationOption && !isMrAndCcSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.Kappa0].OwningRow,
                !isDefaultSimulationCalculation &&
                !isMrSimulationOption && !isMrAndCcSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.CakeMoistureContentRf0].OwningRow,
                !isDefaultSimulationCalculation &&
                !isMrSimulationOption && !isMrAndCcSimulationOption);
            HideParameterRow(ParameterToCell[fsParameterIdentifier.Ne].OwningRow,
                !isDefaultSimulationCalculation &&
                !isMrSimulationOption && !isMrAndCcSimulationOption);

            int isMrAndCcSimulationOptionCoef = Convert.ToInt32(!isMrAndCcSimulationOption);

            ParameterToCell[fsParameterIdentifier.CakePorosity0].OwningRow.Height = rowHeight *
                                                                                    isDefaultSimulationCalculationCoef *
                                                                                    isMrAndCcSimulationOptionCoef *
                                                                                    isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.DryCakeDensity0].OwningRow.Height = rowHeight *
                                                                                    isDefaultSimulationCalculationCoef *
                                                                                    isMrAndCcSimulationOptionCoef *
                                                                                    isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.Kappa0].OwningRow.Height = rowHeight *
                                                                                    isDefaultSimulationCalculationCoef *
                                                                                    isMrAndCcSimulationOptionCoef *
                                                                                    isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.CakeMoistureContentRf0].OwningRow.Height = rowHeight *
                                                                                    isDefaultSimulationCalculationCoef *
                                                                                    isMrAndCcSimulationOptionCoef *
                                                                                    isMrSimulationOptionCoef;
            ParameterToCell[fsParameterIdentifier.Ne].OwningRow.Height = rowHeight *
                                                                                    isDefaultSimulationCalculationCoef *
                                                                                    isMrAndCcSimulationOptionCoef *
                                                                                    isMrSimulationOptionCoef;
            
        }
    }
}
