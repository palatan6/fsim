using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using StepCalculators.Simulation_Calculators;
using Value;


namespace CalculatorModules.CakeWashing
{
    public partial class fsCakeWashingControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        private void AssignDefaultValues()
        {
            Values[fsParameterIdentifier.MotherLiquidViscosity].Value = new fsValue(1e-3);
            Values[fsParameterIdentifier.MotherLiquidDensity].Value = new fsValue(1000);
            Values[fsParameterIdentifier.SolidsDensity].Value = new fsValue(2500);
            Values[fsParameterIdentifier.CakePorosity0].Value = new fsValue(55e-2);
            Values[fsParameterIdentifier.Ne].Value = new fsValue(0.02);
            Values[fsParameterIdentifier.CakePermeability0].Value = new fsValue(1.5e-13);
            Values[fsParameterIdentifier.CakeCompressibility].Value = new fsValue(0.3);
            Values[fsParameterIdentifier.FilterMediumResistanceHce].Value = new fsValue(3e-3);
            Values[fsParameterIdentifier.CakeSaturationSw0].Value = new fsValue(70e-2);
            Values[fsParameterIdentifier.PredeliquorFlowRate].Value = new fsValue(0.5);
            Values[fsParameterIdentifier.CakeWashOutConcentration].Value = new fsValue(50);
            Values[fsParameterIdentifier.RemanentWashOutContent].Value = new fsValue(0.1e-2);
            Values[fsParameterIdentifier.WashLiquidDensity].Value = new fsValue(900);
            Values[fsParameterIdentifier.WashLiquidViscosity].Value = new fsValue(0.95e-3);
            Values[fsParameterIdentifier.LiquidWashOutConcentration].Value = new fsValue(0.5);
            Values[fsParameterIdentifier.WashIndexFor0].Value = new fsValue(2);
            Values[fsParameterIdentifier.AdaptationPar1].Value = new fsValue(0.11);
            Values[fsParameterIdentifier.AdaptationPar2].Value = new fsValue(0.09);

            Values[fsParameterIdentifier.FilterArea].Value = new fsValue(7);
            Values[fsParameterIdentifier.ns].Value = new fsValue(10);
            Values[fsParameterIdentifier.ls].Value = new fsValue(0.7);
            Values[fsParameterIdentifier.StandardTechnicalTime].Value = new fsValue(2);
            Values[fsParameterIdentifier.lambda].Value = new fsValue(0.2);
            Values[fsParameterIdentifier.u].Value = new fsValue(3.0 / 60);
            Values[fsParameterIdentifier.CakeHeight].Value = new fsValue(20e-3);
            Values[fsParameterIdentifier.PressureDifference].Value = new fsValue(0.6e5);          
        }

        public fsCakeWashingControl()
        {
            InitializeComponent();

            #region Calculators

            Calculators.Add(new fsCakeWashingCalculator());

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
                fsParameterIdentifier.RotationalSpeed ,
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
                fsParameterIdentifier.FeedVolumeFlowRate ,
                fsParameterIdentifier.FeedSolidsMassFlowRate ,
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

            AssignDefaultValues();
            
            //SetDefaultDiagram(fsParameterIdentifier.WashTime, fsParameterIdentifier.WetCakeMass, fsParameterIdentifier.WashLiquidMassFlowRate);
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
