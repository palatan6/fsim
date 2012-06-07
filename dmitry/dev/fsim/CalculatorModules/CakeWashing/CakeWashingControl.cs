using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using StepCalculators.Simulation_Calculators;


namespace CalculatorModules.CakeWashing
{
    public partial class fsCakeWashingControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        public fsCakeWashingControl()
        {
            InitializeComponent();

            #region Calculators

            Calculators.Add(new fsCakeWashingCalculator());

            //Calculators.Add(new fsDensityConcentrationCalculator());
            //Calculators.Add(new fsPorosityCalculator());
            //Calculators.Add(new fsPermeabilityCalculator());
            //Calculators.Add(new fsRm0Hce0Calculator());
            //Calculators.Add(new fsBeltFiltersWithReversibleTraysCalculator());

            #endregion

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            #region Material groups

            fsParametersGroup etafGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);

            fsParametersGroup rhofGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);

            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity);

            fsParametersGroup epsGroup = AddGroup(
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.DryCakeDensity0,
                fsParameterIdentifier.CakeMoistureContentRf0,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.CakeMoistureContentRf);

            fsParametersGroup neGroup = AddGroup(
                fsParameterIdentifier.Ne);       

            fsParametersGroup pcrcGroup = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha,
                fsParameterIdentifier.PracticalCakePermeability);

            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);

            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce,
                fsParameterIdentifier.FilterMediumResistanceRm);

            fsParametersGroup cstartGroup = AddGroup(
                fsParameterIdentifier.CakeSaturationSw0,
                fsParameterIdentifier.CakeMoistureContentRfw0);

            fsParametersGroup fqGroup = AddGroup(
                fsParameterIdentifier.PredeliquorFlowRate);

            fsParametersGroup washoutGroup = AddGroup(
                fsParameterIdentifier.CakeWashOutConcentration,
                fsParameterIdentifier.CakeWashOutContentX0p);

            fsParametersGroup xrGroup = AddGroup(
                fsParameterIdentifier.RemanentWashOutContent);

            fsParametersGroup rhowGroup = AddGroup(
                fsParameterIdentifier.WashLiquidDensity);

            fsParametersGroup etawGroup = AddGroup(
                fsParameterIdentifier.WashLiquidViscosity);

            fsParametersGroup cwGroup = AddGroup(
                fsParameterIdentifier.LiquidWashOutConcentration);

            fsParametersGroup dnGroup = AddGroup(
                fsParameterIdentifier.WashIndexFor0,
                fsParameterIdentifier.WashIndexFor);

            fsParametersGroup aw1Group = AddGroup(
                fsParameterIdentifier.AdaptationPar1);

            fsParametersGroup aw2Group = AddGroup(
                fsParameterIdentifier.AdaptationPar2);


            var materialGroups = new[]
                             {
                                etafGroup,
                                rhofGroup,
                                densitiesGroup,
                                epsGroup,
                                neGroup,
                                pcrcGroup,
                                ncGroup,
                                hceGroup,
                                cstartGroup,
                                fqGroup,
                                washoutGroup,
                                xrGroup,
                                rhowGroup,
                                etawGroup,
                                cwGroup,
                                dnGroup,
                                aw1Group,
                                aw2Group
                             };

            for (int i = 0; i < materialGroups.Length; ++i)
            {
                materialGroups[i].Kind = fsParametersGroup.fsParametersGroupKind.MaterialParameters;
                AddGroupToUI(materialParametersDataGrid, materialGroups[i], colors[i % colors.Length]);
                SetGroupInput(materialGroups[i], true);
            }

            #endregion

            #region Machine groups 
           
            fsParametersGroup abGroup = AddGroup(
              fsParameterIdentifier.FilterArea,
              fsParameterIdentifier.MachineWidth);

            fsParametersGroup nsGroup = AddGroup(
                fsParameterIdentifier.ns);

            fsParametersGroup geometryGroup = AddGroup(
                fsParameterIdentifier.ls,
                fsParameterIdentifier.ls_over_b,
                fsParameterIdentifier.FilterLength,
                fsParameterIdentifier.l_over_b,
                fsParameterIdentifier.As);

            fsParametersGroup timeGroup = AddGroup(
                fsParameterIdentifier.StandardTechnicalTime,
                fsParameterIdentifier.TechnicalTime);

            fsParametersGroup lambdaGroup = AddGroup(
                fsParameterIdentifier.lambda);

            fsParametersGroup uGroup = AddGroup(
                fsParameterIdentifier.u,
                fsParameterIdentifier.CycleFrequency,
                fsParameterIdentifier.CycleTime);

            fsParametersGroup hcGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.Qms);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup washGroup = AddGroup(
                fsParameterIdentifier.WashingRatioW,
                fsParameterIdentifier.WashingRatioWv,
                fsParameterIdentifier.WashingRatioWm,
                fsParameterIdentifier.WashLiquidVolume,
                fsParameterIdentifier.WashLiquidMass,
                fsParameterIdentifier.NumberOfWashingSegments,
                fsParameterIdentifier.SpecificWashArea,
                fsParameterIdentifier.WashTime,
                fsParameterIdentifier.WashLiquidVolFlowRate,
                fsParameterIdentifier.WashLiquidMassFlowRate,
                fsParameterIdentifier.SpecificWashOutConcentration,
                fsParameterIdentifier.SpecificAverageWashOut,
                fsParameterIdentifier.SpecificWashOutConcentrationInCake,
                fsParameterIdentifier.WashOutConcentrationInWashfiltrate,
                fsParameterIdentifier.AverageWashOutConcentration,
                fsParameterIdentifier.WashOutConcentrationInCake,
                fsParameterIdentifier.SpecificWashOutXStar,
                fsParameterIdentifier.SpecificWashOutX,
                fsParameterIdentifier.CakeWashOutContent);

            fsParametersGroup washDatagroup = AddOnlyCalculatedGroup(
                fsParameterIdentifier.SpecificWashfiltrate,
                fsParameterIdentifier.VolumeOfWashfiltrate,
                fsParameterIdentifier.MassOfWashfiltrate,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.CakeMass,
                fsParameterIdentifier.LiquidVolumeInCake,
                fsParameterIdentifier.LiquidMassInCake,
                fsParameterIdentifier.CakeMoistureContentRfw,
                fsParameterIdentifier.VolumeFlowRate,
                fsParameterIdentifier.MassFlowRate,
                fsParameterIdentifier.AverageVolumeFlowRate,
                fsParameterIdentifier.AverageMassFlowRate,
                fsParameterIdentifier.SpecificVolumeFlowRate,
                fsParameterIdentifier.SpecificMassFlowRate,
                fsParameterIdentifier.SpecificAverageVolumeFlowRate,
                fsParameterIdentifier.SpecificAverageMassFlowRate
                );

            var machineGroups = new[]
                            {
                               abGroup,
                               nsGroup,
                               geometryGroup,
                               timeGroup,
                               lambdaGroup,
                               uGroup,
                               hcGroup,
                               dpGroup,
                               washGroup,
                               washDatagroup
                            };

            for (int i = 0; i < machineGroups.Length; ++i)
            {
                machineGroups[i].Kind = fsParametersGroup.fsParametersGroupKind.MachiningSettingsParameters;
                AddGroupToUI(dataGrid, machineGroups[i], colors[i % colors.Length]);
                SetGroupInput(machineGroups[i], true);
            }

            #endregion

            //SetDefaultDiagram(fsParameterIdentifier.u, fsParameterIdentifier.FilterArea, fsParameterIdentifier.SpecificFiltrationTime);
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(materialParametersDataGrid, dataGrid);
        }

            #region Routine Methods

            protected override void UpdateGroupsInputInfoFromCalculationOptions()
            {
            }

            protected override void UpdateEquationsFromCalculationOptions()
            {
            }

            protected override void UpdateUIFromData()
            {
                base.UpdateUIFromData();
            }

            #endregion
    }
}
