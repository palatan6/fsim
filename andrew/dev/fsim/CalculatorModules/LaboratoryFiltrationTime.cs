using System.Drawing;
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
            Calculators.Add(new fsPorosityCalculator());
            Calculators.Add(new fsPermeabilityCalculator());
            Calculators.Add(new fsLaboratoryFiltrationCalculator());
            
            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);
            fsParametersGroup filtrateGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup solidsGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);
            fsParametersGroup neGroup = AddGroup(
                fsParameterIdentifier.Ne);
            fsParametersGroup epsKappaGroup = AddGroup(
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.DryCakeDensity0,
                fsParameterIdentifier.Kappa0,
                fsParameterIdentifier.CakeMoistureContentRf0,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.Kappa,
                fsParameterIdentifier.CakeMoistureContentRf);
            fsParametersGroup viscosityGroup = AddGroup(
                fsParameterIdentifier.MotherLiquidViscosity);
            fsParametersGroup pc0Rc0Alpha0Group = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha);
            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce0,
                fsParameterIdentifier.FilterMediumResistanceRm0);
            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);
            fsParametersGroup areaGroup = AddGroup(
                fsParameterIdentifier.FilterArea);
            fsParametersGroup cakeFormationGroup = AddGroup(
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.FiltrateMass,
                fsParameterIdentifier.CakeMass,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.qmft,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.FiltrateVolume,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.qft);
            fsParametersGroup resultsGroup = AddGroup(
                fsParameterIdentifier.SolidsMassInSuspension,
                fsParameterIdentifier.LiquidMassInSuspension);


            var groups = new[]
                             {
                                 neGroup,
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
                groups[i].SetIsInputFlag(true);
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            resultsGroup.SetIsInputFlag(false);
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

        protected override void UpdateUIFromData()
        {
            ParameterToCell[fsParameterIdentifier.Ne].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.CakePorosity0].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.DryCakeDensity0].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.Kappa0].OwningRow.Visible = false;
        }
    }
}