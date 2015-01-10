using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using StepCalculators.Simulation_Calculators;
using Value;

namespace CalculatorModules
{
    public sealed partial class fsDeliquoringAnalysisAndSimulation : OptionsQuadraTableAndCommentsCalculatorControl
    {
        public fsDeliquoringAnalysisAndSimulation()
        {
            InitializeComponent();
        }
        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDeliqCalculator());
        }

        protected override void InitializeGroups()
        {
        }

        protected override void InitializeDefaultDiagrams()
        {
            //m_defaultDiagrams.Add(
            //    new Enum[] { },
            //    new DiagramConfiguration(
            //        fsParameterIdentifier.WashingRatioW,
            //        new DiagramConfiguration.DiagramRange(0, 3),
            //        new[] { fsParameterIdentifier.WashLiquidVolFlowRate, fsParameterIdentifier.WashOutConcentrationInWashfiltrate },
            //        new[] { fsParameterIdentifier.CakeWashOutContent }));
        }

        protected override void InitializeParametersValues()
        {
            Values[fsParameterIdentifier.MotherLiquidViscosity].Value = new fsValue(1e-3);
            Values[fsParameterIdentifier.MotherLiquidDensity].Value = new fsValue(1000);
            Values[fsParameterIdentifier.SolidsDensity].Value = new fsValue(2000);
            Values[fsParameterIdentifier.SuspensionSolidsMassFraction].Value = new fsValue(15e-2);
            Values[fsParameterIdentifier.CakeCompressibility].Value = new fsValue(0.3);
            Values[fsParameterIdentifier.FilterMediumResistanceHce].Value = new fsValue(2e-3);
            Values[fsParameterIdentifier.SurfaceTensionLiquidInCake].Value = new fsValue(70e-3);
            Values[fsParameterIdentifier.StandardCapillaryPressure].Value = new fsValue(0.25e5);
            Values[fsParameterIdentifier.SpecificResidualTime].Value = new fsValue(10e-2);
            Values[fsParameterIdentifier.Ad2].Value = new fsValue(5);
            Values[fsParameterIdentifier.Theta].Value = new fsValue(20);
            Values[fsParameterIdentifier.GasLiquidViscosity].Value = new fsValue(0.02e-3);
            Values[fsParameterIdentifier.Ag2].Value = new fsValue(0);
            Values[fsParameterIdentifier.Ag3].Value = new fsValue(0.3);

            Values[fsParameterIdentifier.FilterArea].Value = new fsValue(20e-4);
            Values[fsParameterIdentifier.Dp_f].Value = new fsValue(2e5);
            Values[fsParameterIdentifier.Dp_d].Value = new fsValue(2e5);
            Values[fsParameterIdentifier.CakeHeight].Value = new fsValue(30e-3);
            Values[fsParameterIdentifier.SolidsMass].Value = new fsValue(23e-3);
            Values[fsParameterIdentifier.FiltrationTime].Value = new fsValue(20);
            Values[fsParameterIdentifier.DeliquoringTime].Value = new fsValue(60);
            Values[fsParameterIdentifier.CakeMoistureContentRf].Value = new fsValue(43.6e-2);
            Values[fsParameterIdentifier.Qgi].Value = new fsValue(351.0e-3 / 3600);
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { materialParametersDataGrid, dataGrid, cbDeliqModeComboBox, cbFormationModeComboBox, cbGasModeComboBox };
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            CreateParametersGroupsForEachMode();
        }

        private void CreateParametersGroupsForEachMode()
        {
            Groups.Clear();
            AddGroupsToUI(materialParametersDataGrid, MakeFormationMaterialGroups(), MakeDeliqMaterialGroups());
            AddGroupsToUI(dataGrid, MakeFormationMachiningGroups(),MakeDeliqMachiningStandardGroups());
        }

        private fsParametersGroup[] MakeFormationMaterialGroups()
        {
            var calculationOption = (fsCalculationOptions.fsFormationModeOption)CalculationOptions[typeof(fsCalculationOptions.fsFormationModeOption)];

            fsParametersGroup etafGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);

            fsParametersGroup rhofGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);

            var materialGroups = new[]
            {
                etafGroup,
                rhofGroup
            };

            if (calculationOption == fsCalculationOptions.fsFormationModeOption.AnalysisModeCalculations)
            {

                fsParametersGroup densitiesGroup = AddGroup(
                    fsParameterIdentifier.SolidsDensity,
                    fsParameterIdentifier.SuspensionDensity);

                fsParametersGroup concentrationGroup = AddGroup(
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                    fsParameterIdentifier.SuspensionSolidsConcentration);

                materialGroups = new[]
                {
                    etafGroup,
                    rhofGroup,
                    densitiesGroup,
                    concentrationGroup
                };
            }
            if (calculationOption == fsCalculationOptions.fsFormationModeOption.SimulationModeCalculations)
            {
                fsParametersGroup densitiesGroup = AddGroup(
                    fsParameterIdentifier.SolidsDensity,
                    fsParameterIdentifier.SuspensionDensity);

                fsParametersGroup concentrationGroup = AddGroup(
                    fsParameterIdentifier.SuspensionSolidsMassFraction,
                    fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                    fsParameterIdentifier.SuspensionSolidsConcentration);

                fsParametersGroup epsGroup = AddGroup(
                    fsParameterIdentifier.Kappa,
                    fsParameterIdentifier.CakePorosity);

                fsParametersGroup pcGroup = AddGroup(
                    fsParameterIdentifier.CakePermeability,
                    fsParameterIdentifier.CakeResistance,
                    fsParameterIdentifier.CakeResistanceAlpha,
                    fsParameterIdentifier.CakePermeability0,
                    fsParameterIdentifier.CakeResistance0,
                    fsParameterIdentifier.CakeResistanceAlpha0);

                materialGroups = new[]
                             {  
                                etafGroup,
                                rhofGroup,
                                densitiesGroup,
                                concentrationGroup,
                                epsGroup,
                                pcGroup
                             };
            }

            if (calculationOption == fsCalculationOptions.fsFormationModeOption.HiddenModeCalculations)
            {

                fsParametersGroup densitiesGroup = AddGroup(
                    fsParameterIdentifier.SolidsDensity);

                fsParametersGroup epsGroup = AddGroup(
                    fsParameterIdentifier.CakePorosity);

                fsParametersGroup pcGroup = AddGroup(
                    fsParameterIdentifier.CakePermeability,
                    fsParameterIdentifier.CakeResistance,
                    fsParameterIdentifier.CakeResistanceAlpha,
                    fsParameterIdentifier.CakePermeability0,
                    fsParameterIdentifier.CakeResistance0,
                    fsParameterIdentifier.CakeResistanceAlpha0);

                materialGroups = new[]
                             {  
                                etafGroup,
                                rhofGroup,
                                densitiesGroup,
                                epsGroup,
                                pcGroup
                             };
            }

            for (int i = 0; i < materialGroups.Length; ++i)
            {
                materialGroups[i].Kind = fsParametersGroup.fsParametersGroupKind.MaterialParameters;
            }
            return materialGroups;
        }

        private fsParametersGroup[] MakeFormationMachiningGroups()
        {
            var calculationOption = (fsCalculationOptions.fsFormationModeOption)CalculationOptions[typeof(fsCalculationOptions.fsFormationModeOption)];

            fsParametersGroup aGroup = AddGroup(
              fsParameterIdentifier.FilterArea);

            var machineGroups = new[]
            {
                aGroup
            };

            if (calculationOption == fsCalculationOptions.fsFormationModeOption.AnalysisModeCalculations)
            {
                fsParametersGroup dpfGroup = AddGroup(
                    fsParameterIdentifier.Dp_f);

                fsParametersGroup tfGroup = AddGroup(
                    fsParameterIdentifier.FiltrationTime);

                fsParametersGroup msGroup = AddGroup(
                    fsParameterIdentifier.SolidsMass,
                    fsParameterIdentifier.SuspensionMass,
                    fsParameterIdentifier.SuspensionVolume,
                    fsParameterIdentifier.CakeMass,
                    fsParameterIdentifier.SpecificSolidsMass,
                    fsParameterIdentifier.SolidsVolume,
                    fsParameterIdentifier.Kappa,
                    fsParameterIdentifier.CakePorosity);

                fsParametersGroup hcGroup = AddGroup(
                    fsParameterIdentifier.CakeHeight,
                    fsParameterIdentifier.FiltrateMass,
                    fsParameterIdentifier.FiltrateVolume,
                    fsParameterIdentifier.qf,
                    fsParameterIdentifier.qmf,
                    fsParameterIdentifier.CakeVolume,
                    fsParameterIdentifier.CakePermeability,
                    fsParameterIdentifier.CakeResistance,
                    fsParameterIdentifier.CakeResistanceAlpha,
                    fsParameterIdentifier.CakePermeability0,
                    fsParameterIdentifier.CakeResistance0,
                    fsParameterIdentifier.CakeResistanceAlpha0);

                machineGroups = new[]
                {
                    aGroup,
                    dpfGroup,
                    tfGroup,
                    msGroup,
                    hcGroup
                };
            }
            if (calculationOption == fsCalculationOptions.fsFormationModeOption.SimulationModeCalculations)
            {
                fsParametersGroup dpfGroup = AddGroup(
                    fsParameterIdentifier.Dp_f);

                fsParametersGroup tfGroup = AddGroup(
                    fsParameterIdentifier.FiltrationTime,
                    fsParameterIdentifier.CakeHeight,
                    fsParameterIdentifier.SuspensionMass,
                    fsParameterIdentifier.FiltrateMass,
                    fsParameterIdentifier.CakeMass,
                    fsParameterIdentifier.SolidsMass,
                    fsParameterIdentifier.SpecificSolidsMass,
                    fsParameterIdentifier.SuspensionVolume,
                    fsParameterIdentifier.FiltrateVolume,
                    fsParameterIdentifier.CakeVolume,
                    fsParameterIdentifier.SolidsVolume,
                    fsParameterIdentifier.qmf,
                    fsParameterIdentifier.qf
                    );

                machineGroups = new[]
                {
                    aGroup,
                    dpfGroup,
                    tfGroup
                };
            }
            if (calculationOption == fsCalculationOptions.fsFormationModeOption.HiddenModeCalculations)
            {
                fsParametersGroup hcGroup = AddGroup(
                    fsParameterIdentifier.CakeHeight,
                    fsParameterIdentifier.SolidsMass,
                    fsParameterIdentifier.SpecificSolidsMass);

                machineGroups = new[]
                {
                    aGroup,
                    hcGroup
                };
            }

            for (int i = 0; i < machineGroups.Length; ++i)
            {
                machineGroups[i].Kind = fsParametersGroup.fsParametersGroupKind.MachiningSettingsParameters;
            }
            return machineGroups;
        }

        private fsParametersGroup[] MakeDeliqMaterialGroups()
        {
            var deliqCalculationOption =
                (fsCalculationOptions.fsDeliquoringModeOption)
                    CalculationOptions[typeof (fsCalculationOptions.fsDeliquoringModeOption)];
            var gasCalculationOption =
                (fsCalculationOptions.fsGasModeOption) CalculationOptions[typeof (fsCalculationOptions.fsGasModeOption)];

            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);

            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce,
                fsParameterIdentifier.FilterMediumResistanceRm);

            fsParametersGroup sigmaGroup = AddGroup(
                fsParameterIdentifier.SurfaceTensionLiquidInCake);

            fsParametersGroup pkeGroup = AddGroup(
                fsParameterIdentifier.StandardCapillaryPressure,
                fsParameterIdentifier.CapillaryPressure);

            fsParametersGroup srGroup = AddGroup(
                fsParameterIdentifier.SpecificResidualTime);

            fsParametersGroup ad2Group = AddGroup(
                fsParameterIdentifier.Ad2);



            var materialGroups = new List<fsParametersGroup>
            {
                ncGroup,
                hceGroup,
                sigmaGroup,
                pkeGroup,
                srGroup,
                ad2Group,
            };

            if (deliqCalculationOption == fsCalculationOptions.fsDeliquoringModeOption.SimulationModeCalculations)
            {

                fsParametersGroup ad1Group = AddGroup(
                    fsParameterIdentifier.Ad1);

                materialGroups = new List<fsParametersGroup>
                {
                    ncGroup,
                    hceGroup,
                    sigmaGroup,
                    pkeGroup,
                    srGroup,
                    ad1Group,
                    ad2Group
                };
            }

            fsParametersGroup tettaGroup = AddGroup(
                        fsParameterIdentifier.Theta);

            fsParametersGroup etagGroup = AddGroup(
                fsParameterIdentifier.GasLiquidViscosity); //name?

            fsParametersGroup ag2Group = AddGroup(
                fsParameterIdentifier.Ag2);

            fsParametersGroup ag3Group = AddGroup(
                fsParameterIdentifier.Ag3);

            if (gasCalculationOption == fsCalculationOptions.fsGasModeOption.AnalysisModeCalculations)
            {
                materialGroups.AddRange(new List<fsParametersGroup> { tettaGroup, etagGroup, ag2Group, ag3Group });
            }
            else if (gasCalculationOption == fsCalculationOptions.fsGasModeOption.SimulationModeCalculations)
            {
                fsParametersGroup ag1Group = AddGroup(
                   fsParameterIdentifier.Ag1);

                materialGroups.AddRange(new List<fsParametersGroup> { tettaGroup, etagGroup, ag1Group, ag2Group, ag3Group });
            }

            var groupsArray =  materialGroups.ToArray();

            for (int i = 0; i < groupsArray.Length; ++i)
            {
                groupsArray[i].Kind = fsParametersGroup.fsParametersGroupKind.MaterialParameters;
            }
            return groupsArray;
        }

        private fsParametersGroup[] MakeDeliqMachiningStandardGroups()
        {
            var deliqCalculationOption =
                (fsCalculationOptions.fsDeliquoringModeOption)
                    CalculationOptions[typeof (fsCalculationOptions.fsDeliquoringModeOption)];
            var gasCalculationOption =
                (fsCalculationOptions.fsGasModeOption)CalculationOptions[typeof(fsCalculationOptions.fsGasModeOption)];

            fsParametersGroup dpdGroup = AddGroup(
                fsParameterIdentifier.Dp_d);

            var groupsList = new List<fsParametersGroup>
                {
                    dpdGroup
                };

            if (deliqCalculationOption == fsCalculationOptions.fsDeliquoringModeOption.AnalysisModeCalculations)
            {

                fsParametersGroup tdGroup = AddGroup(
                    fsParameterIdentifier.DeliquoringTime,
                    fsParameterIdentifier.DeliquoringStepKParameter);

                groupsList.Add(tdGroup);

                switch (gasCalculationOption)
                {
                    case fsCalculationOptions.fsGasModeOption.AnalysisModeCalculations:

                        fsParametersGroup rfGroup = AddGroup(
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Rho_Bulk,
                            fsParameterIdentifier.Ad1);

                        fsParametersGroup qgiGroup = AddGroup(
                            fsParameterIdentifier.Qgi,
                            fsParameterIdentifier.Qgt,
                            fsParameterIdentifier.GasVolume,
                            fsParameterIdentifier.SpecificGasConsumption,
                            fsParameterIdentifier.qgi,
                            fsParameterIdentifier.qgt,
                            fsParameterIdentifier.Ag1);

                        groupsList.AddRange(new List<fsParametersGroup> {rfGroup, qgiGroup});
                        break;

                    case fsCalculationOptions.fsGasModeOption.SimulationModeCalculations:

                        rfGroup = AddGroup(
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Rho_Bulk,
                            fsParameterIdentifier.Ad1,
                            fsParameterIdentifier.Qgi,
                            fsParameterIdentifier.Qgt,
                            fsParameterIdentifier.GasVolume,
                            fsParameterIdentifier.SpecificGasConsumption,
                            fsParameterIdentifier.qgi,
                            fsParameterIdentifier.qgt);

                        groupsList.Add(rfGroup);
                        break;

                    case fsCalculationOptions.fsGasModeOption.HiddenModeCalculations:

                        rfGroup = AddGroup(
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Rho_Bulk,
                            fsParameterIdentifier.Ad1);

                        groupsList.Add(rfGroup);
                        break;
                }
            }

            if (deliqCalculationOption == fsCalculationOptions.fsDeliquoringModeOption.SimulationModeCalculations)
            {
                switch (gasCalculationOption)
                {
                    case fsCalculationOptions.fsGasModeOption.AnalysisModeCalculations:

                        fsParametersGroup tdGroup = AddGroup(
                            fsParameterIdentifier.DeliquoringTime,
                            fsParameterIdentifier.DeliquoringStepKParameter,
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Rho_Bulk);

                        fsParametersGroup qgiGroup = AddGroup(
                            fsParameterIdentifier.Qgi,
                            fsParameterIdentifier.Qgt,
                            fsParameterIdentifier.qgi,
                            fsParameterIdentifier.qgt,
                            fsParameterIdentifier.GasVolume,
                            fsParameterIdentifier.SpecificGasConsumption,
                            fsParameterIdentifier.Ag1);

                        groupsList.AddRange(new List<fsParametersGroup> { tdGroup, qgiGroup });
                        break;

                    case fsCalculationOptions.fsGasModeOption.SimulationModeCalculations:

                        tdGroup = AddGroup(
                            fsParameterIdentifier.DeliquoringTime,
                            fsParameterIdentifier.DeliquoringStepKParameter,
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Qgi,
                            fsParameterIdentifier.Qgt,
                            fsParameterIdentifier.qgi,
                            fsParameterIdentifier.qgt,
                            fsParameterIdentifier.GasVolume,
                            fsParameterIdentifier.SpecificGasConsumption);

                        groupsList.Add(tdGroup);
                        break;

                    case fsCalculationOptions.fsGasModeOption.HiddenModeCalculations:

                        tdGroup = AddGroup(
                            fsParameterIdentifier.DeliquoringTime,
                            fsParameterIdentifier.DeliquoringStepKParameter,
                            fsParameterIdentifier.CakeMoistureContentRf,
                            fsParameterIdentifier.CakeSaturation,
                            fsParameterIdentifier.FiltrateVolumeD,
                            fsParameterIdentifier.FiltrateMassD,
                            fsParameterIdentifier.DryCakeMass,
                            fsParameterIdentifier.LiquidMassInSuspension,
                            fsParameterIdentifier.Rho_Bulk);

                        groupsList.Add(tdGroup);
                        break;
                }
            }

            var machineGroups = groupsList.ToArray();

            for (int i = 0; i < machineGroups.Length; ++i)
            {
                machineGroups[i].Kind = fsParametersGroup.fsParametersGroupKind.MachiningSettingsParameters;
            }
            return machineGroups;
        }
        
        protected override void UpdateEquationsFromCalculationOptions()
        {
        }

        protected override void InitializeCalculationOptionsUIControls()
        {
            fsMisc.FillList(cbDeliqModeComboBox.Items, typeof(fsCalculationOptions.fsDeliquoringModeOption));
            EstablishCalculationOption(fsCalculationOptions.fsDeliquoringModeOption.AnalysisModeCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsDeliquoringModeOption), cbDeliqModeComboBox);

            fsMisc.FillList(cbFormationModeComboBox.Items, typeof(fsCalculationOptions.fsFormationModeOption));
            EstablishCalculationOption(fsCalculationOptions.fsFormationModeOption.AnalysisModeCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsFormationModeOption), cbFormationModeComboBox);

            fsMisc.FillList(cbGasModeComboBox.Items, typeof(fsCalculationOptions.fsGasModeOption));
            EstablishCalculationOption(fsCalculationOptions.fsGasModeOption.AnalysisModeCalculations);
            AssignCalculationOptionAndControl(typeof(fsCalculationOptions.fsGasModeOption), cbGasModeComboBox);
        }

        protected override void UpdateUIFromData()
        {
            base.UpdateUIFromData();
        }

        protected override void InitializeResultParamatersList()
        {
            AddResultParameter(fsParameterIdentifier.CakeHeight);
            AddResultParameter(fsParameterIdentifier.SolidsMass);
            AddResultParameter(fsParameterIdentifier.CakePorosity);
            AddResultParameter(fsParameterIdentifier.CakePermeability);
            AddResultParameter(fsParameterIdentifier.DeliquoringTime);
            AddResultParameter(fsParameterIdentifier.DeliquoringStepKParameter);
            AddResultParameter(fsParameterIdentifier.CakeMoistureContentRf);
            AddResultParameter(fsParameterIdentifier.Ad1);
            AddResultParameter(fsParameterIdentifier.Qgi);
            AddResultParameter(fsParameterIdentifier.Ag1);
        }

        #endregion
    }
}
