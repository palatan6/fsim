using System.ComponentModel;
using System.Drawing;
using Parameters;
using StepCalculators;
using StepCalculators.Simulation_Calculators;
using Value;


namespace CalculatorModules.Hydrocyclone
{
    public partial class HydrocycloneControl : OptionsSingleTableWithPanelAndCommentsCalculatorControl
    {
        #region Calculation Option

        public enum fsCalculationOption
        {
            [Description("Dp")]
            Dp,
            [Description("Q")]
            Q,
            [Description("n")]
            n
        }

        #endregion

        public HydrocycloneControl()
        {
            InitializeComponent();

            #region Calculators

            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsHydrocycloneCalculator());

            #endregion

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            #region Groups

            fsParametersGroup etaGroup = AddGroup(
               fsParameterIdentifier.MotherLiquidViscosity);    //eta

            fsParametersGroup rhoGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);       //rho

            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,        //rho_s
                fsParameterIdentifier.SuspensionDensity);   //rho_sus

            fsParametersGroup cGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,     //cm
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,   //cv
                fsParameterIdentifier.SuspensionSolidsConcentration);   //c

            fsParametersGroup xgGroup = AddGroup(
                fsParameterIdentifier.xg);   //xg

            fsParametersGroup sigma_gGroup = AddGroup(
                fsParameterIdentifier.sigma_g);   //sigma_g

            fsParametersGroup sigma_sGroup = AddGroup(
                fsParameterIdentifier.sigma_s);   //sigma_s

            fsParametersGroup underFlowGroup = AddGroup(
                fsParameterIdentifier.rf,                              //rf
                fsParameterIdentifier.DuOverD,                         //Du/D
                fsParameterIdentifier.UnderflowSolidsMassFraction);    //cm_u

            fsParametersGroup cxdGroup = AddGroup(
                fsParameterIdentifier.OverflowSolidsMassFraction,    //cmo
                fsParameterIdentifier.OverflowParticleSize,          //x0_i
                fsParameterIdentifier.ReducedCutSize,                //x’50
                fsParameterIdentifier.MachineDiameter);              //D
            
            fsParametersGroup numCyclonesGroup = AddGroup(
                fsParameterIdentifier.NumberOfCyclones);     //n

            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);   //Dp

            fsParametersGroup qGroup = AddGroup(
                fsParameterIdentifier.VolumeFlowRate,       //Q
                fsParameterIdentifier.MassFlowRate,   //Qm
                fsParameterIdentifier.Qms);                     //Qms

            fsParametersGroup alpha1Group = AddGroup(
                fsParameterIdentifier.Alpha1);

            fsParametersGroup alpha2Group = AddGroup(
                fsParameterIdentifier.Alpha2);

            fsParametersGroup alpha3Group = AddGroup(
                fsParameterIdentifier.Alpha3);

            fsParametersGroup beta1Group = AddGroup(
                fsParameterIdentifier.Beta1);

            fsParametersGroup beta2Group = AddGroup(
                fsParameterIdentifier.Beta2);

            fsParametersGroup beta3Group = AddGroup(
                fsParameterIdentifier.Beta3);

            fsParametersGroup gamma1Group = AddGroup(
                fsParameterIdentifier.Gamma1);

            fsParametersGroup gamma2Group = AddGroup(
                fsParameterIdentifier.Gamma2);

            fsParametersGroup gamma3Group = AddGroup(
                fsParameterIdentifier.Gamma3);


            fsParametersGroup bigLOverDGroup = AddGroup(
                fsParameterIdentifier.bigLOverD);

            fsParametersGroup smallLOverDGroup = AddGroup(
                fsParameterIdentifier.smallLOverD);

            fsParametersGroup diOverDGroup = AddGroup(
                fsParameterIdentifier.DiOverD);

            fsParametersGroup doOverDGroup = AddGroup(
                fsParameterIdentifier.DoOverD);

            fsParametersGroup onlyCalculatedParametersGroup = AddGroup(
                 fsParameterIdentifier.StokesNumber,
                 fsParameterIdentifier.EulerNumber,
                 fsParameterIdentifier.ReynoldsNumber,
                 fsParameterIdentifier.AverageVelocity,
                 fsParameterIdentifier.TotalEfficiency,
                 fsParameterIdentifier.ReducedTotalEfficiency,
                 fsParameterIdentifier.CycloneLength,
                 fsParameterIdentifier.LengthOfCylindricalPart,
                 fsParameterIdentifier.InletDiameter,
                 fsParameterIdentifier.OutletDiameter,
                 fsParameterIdentifier.UnderflowDiameter,
                 fsParameterIdentifier.OverflowVolumeFlowRate,
                 fsParameterIdentifier.OverflowMassFlowRate,
                 fsParameterIdentifier.OverflowSolidsMassFlowRate,
                 fsParameterIdentifier.UnderflowVolumeFlowRate,
                 fsParameterIdentifier.UnderflowMassFlowRate,
                 fsParameterIdentifier.UnderflowSolidsMassFlowRate,
                 fsParameterIdentifier.OverflowMeanParticleSize,
                 fsParameterIdentifier.UnderflowMeanParticleSize,
                 fsParameterIdentifier.OverflowSolidsVolumeFraction,
                 fsParameterIdentifier.OverflowSolidsConcentration,
                 fsParameterIdentifier.UnderflowSolidsVolumeFraction,
                 fsParameterIdentifier.UnderflowSolidsConcentration);
            var groups = new[]
                             {
                                etaGroup,
                                rhoGroup,
                                densitiesGroup,
                                cGroup,
                                xgGroup,
                                sigma_gGroup,
                                sigma_sGroup,
                                underFlowGroup,
                                cxdGroup,
                                numCyclonesGroup,
                                pressureGroup,
                                qGroup,
                                onlyCalculatedParametersGroup,
                                alpha1Group,
                                alpha2Group,
                                alpha3Group,
                                beta1Group,
                                beta2Group,
                                beta3Group,
                                gamma1Group,
                                gamma2Group,
                                gamma3Group,
                                bigLOverDGroup,
                                smallLOverDGroup,
                                diOverDGroup,
                                doOverDGroup
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
                SetGroupInput(groups[i], true);
            }
            SetGroupInput(onlyCalculatedParametersGroup, false);
            

            #endregion

            AssignDefaultValues();

            fsMisc.FillList(comboBoxCalculationOption.Items, typeof(fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.Dp);
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), comboBoxCalculationOption);

            UpdateGroupsInputInfoFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid, comboBoxCalculationOption);

        }

        private void AssignDefaultValues()
        {
            Values[fsParameterIdentifier.MotherLiquidViscosity].Value = new fsValue(1e-3);
            Values[fsParameterIdentifier.MotherLiquidDensity].Value = new fsValue(1000);
            Values[fsParameterIdentifier.SolidsDensity].Value = new fsValue(2000);
            Values[fsParameterIdentifier.SuspensionSolidsMassFraction].Value = new fsValue(5e-2);

            Values[fsParameterIdentifier.xg].Value = new fsValue(100e-6);
            Values[fsParameterIdentifier.sigma_g].Value = new fsValue(3);
            Values[fsParameterIdentifier.sigma_s].Value = new fsValue(2);
            ParameterToGroup[fsParameterIdentifier.ReducedCutSize].Representator = fsParameterIdentifier.ReducedCutSize;
            Values[fsParameterIdentifier.rf].Value = new fsValue(5e-2);
            Values[fsParameterIdentifier.ReducedCutSize].Value = new fsValue(100e-6);
            Values[fsParameterIdentifier.NumberOfCyclones].Value = new fsValue(3);
            Values[fsParameterIdentifier.VolumeFlowRate].Value = new fsValue(16900/3600.0);

            Values[fsParameterIdentifier.Alpha1].Value = new fsValue(0.0474);
            Values[fsParameterIdentifier.Alpha2].Value = new fsValue(0.742);
            Values[fsParameterIdentifier.Alpha3].Value = new fsValue(8.96);
            Values[fsParameterIdentifier.Beta1].Value = new fsValue(371.5);
            Values[fsParameterIdentifier.Beta2].Value = new fsValue(0.116);
            Values[fsParameterIdentifier.Beta3].Value = new fsValue(2.12);
            Values[fsParameterIdentifier.Gamma1].Value = new fsValue(1218);
            Values[fsParameterIdentifier.Gamma2].Value = new fsValue(4.75);
            Values[fsParameterIdentifier.Gamma3].Value = new fsValue(0.3);

            Values[fsParameterIdentifier.bigLOverD].Value = new fsValue(5);
            Values[fsParameterIdentifier.smallLOverD].Value = new fsValue(3);
            Values[fsParameterIdentifier.DiOverD].Value = new fsValue(0.2);
            Values[fsParameterIdentifier.DoOverD].Value = new fsValue(0.3);
        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            var calculationOption = (fsCalculationOption)CalculationOptions[typeof(fsCalculationOption)];
            fsParametersGroup calculateGroup = null;
            switch (calculationOption)
            {
                case fsCalculationOption.Dp:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.PressureDifference];
                    break;
                case fsCalculationOption.n:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.NumberOfCyclones];
                    break;
                case fsCalculationOption.Q:
                    calculateGroup = ParameterToGroup[fsParameterIdentifier.VolumeFlowRate];
                    break;
            }
            var groups = new[]
                {
                    ParameterToGroup[fsParameterIdentifier.PressureDifference],
                    ParameterToGroup[fsParameterIdentifier.NumberOfCyclones],
                    ParameterToGroup[fsParameterIdentifier.VolumeFlowRate]
                };
            foreach (fsParametersGroup group in groups)
            {
                SetGroupInput(group, group != calculateGroup);
            }
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            //override
        }

        protected override void UpdateUIFromData()
        {
            //override

            base.UpdateUIFromData();
        }

        #endregion

        
   
    }
}

