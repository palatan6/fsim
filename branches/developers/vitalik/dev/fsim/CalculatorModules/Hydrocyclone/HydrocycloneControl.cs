﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;
using StepCalculators;
using StepCalculators.Material_Calculators;
using StepCalculators.Simulation_Calculators;


namespace CalculatorModules.Hydrocyclone
{
    public partial class HydrocycloneControl : OptionsSingleTableWithPanelAndCommentsCalculatorControl
    {
        #region Calculation Option

        private enum fsCalculationOption
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
                fsParameterIdentifier.rf,       //rf
                fsParameterIdentifier.DuOverD,  //Du/D
                fsParameterIdentifier.cm_u);    //cm_u

            fsParametersGroup cxdGroup = AddGroup(
                fsParameterIdentifier.OverflowSolidsMassFraction,    //cmo
                fsParameterIdentifier.OverflowParticleSize,          //x0_i
                fsParameterIdentifier.ReducedCutSize,                //x’50
                fsParameterIdentifier.CycloneDiameter);              //D
            
            fsParametersGroup numCyclonesGroup = AddGroup(
                fsParameterIdentifier.NumberOfCyclones);     //n

            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);   //Dp

            fsParametersGroup qGroup = AddGroup(
                fsParameterIdentifier.FeedVolumeFlowRate,       //Q
                fsParameterIdentifier.FeedSolidsMassFlowRate,   //Qm
                fsParameterIdentifier.Qms);                     //Qms

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
                                qGroup
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
                SetGroupInput(groups[i], true);
            }
            

            #endregion

            fsMisc.FillList(comboBoxCalculationOption.Items, typeof(fsCalculationOption));
            EstablishCalculationOption(fsCalculationOption.Dp);
            AssignCalculationOptionAndControl(typeof(fsCalculationOption), comboBoxCalculationOption);
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid, comboBoxCalculationOption);

        }

        #region Routine Methods

        protected override void UpdateGroupsInputInfoFromCalculationOptions()
        {
            //override
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

