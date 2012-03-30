﻿using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;

namespace CalculatorModules
{
    public sealed partial class fsLaboratoryFiltrationTime : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        public fsLaboratoryFiltrationTime()
        {
            InitializeComponent();

            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsEps0Kappa0Calculator());
            Calculators.Add(new fsPc0Rc0Alpha0Calculator());
            Calculators.Add(new fsLaboratoryFiltrationCalculator());

            fsParametersGroup filtrateGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);
            fsParametersGroup epsKappaGroup = AddGroup(
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.Kappa0);
            fsParametersGroup viscosityGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);
            fsParametersGroup pc0Rc0Alpha0Group = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0);
            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);
            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce0);
            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);
            fsParametersGroup areaGroup = AddGroup(
                fsParameterIdentifier.FilterArea);
            fsParametersGroup cakeFormationGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.SuspensionMass);
            fsParametersGroup resultsGroup = AddGroup(
                fsParameterIdentifier.SolidsMassInSuspension,
                fsParameterIdentifier.LiquidMassInSuspension);


            var groups = new[]
                             {
                                 filtrateGroup,
                                 solidsGroup,
                                 concentrationGroup,
                                 epsKappaGroup,
                                 viscosityGroup,
                                 pc0Rc0Alpha0Group,
                                 ncGroup,
                                 hceGroup,
                                 pressureGroup,
                                 areaGroup,
                                 cakeFormationGroup,
                                 resultsGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                groups[i].IsInput = true;
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            resultsGroup.IsInput = false;
            ParameterToCell[fsParameterIdentifier.SolidsMassInSuspension].ReadOnly = true;
            ParameterToCell[fsParameterIdentifier.LiquidMassInSuspension].ReadOnly = true;

            UpdateGroupsInputInfoFromCalculationOptions();
            UpdateEquationsFromCalculationOptions();
            Recalculate();
            UpdateUIFromData();
            ConnectUIWithDataUpdating(dataGrid);
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