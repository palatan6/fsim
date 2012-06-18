using System;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;
using StepCalculators;
using Value;
using System.Windows.Forms;

namespace CalculatorModules
{
    public sealed partial class fsLaboratoryFiltrationTime : fsOptionsSingleTableAndCommentsCalculatorControl
    {
        protected override void InitializeCalculators()
        {
            Calculators.Add(new fsDensityConcentrationCalculator());
            Calculators.Add(new fsPorosityCalculator());
            Calculators.Add(new fsLaboratoryFiltrationCalculator());
        }

        protected override void InitializeGroups()
        {
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
            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);
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
                fsParameterIdentifier.CakeResistanceAlpha,
                fsParameterIdentifier.PracticalCakePermeability);
            fsParametersGroup hceGroup = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce,
                fsParameterIdentifier.FilterMediumResistanceRm);
            fsParametersGroup areaGroup = AddGroup(
                fsParameterIdentifier.FilterArea);
            fsParametersGroup pressureGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);
            fsParametersGroup cakeFormationGroup = AddGroup(
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.FiltrateMass,
                fsParameterIdentifier.CakeMass,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.qmf,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.FiltrateVolume,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.qf);

            var groups = new[]
                             {
                                 viscosityGroup,
                                 filtrateGroup,
                                 solidsGroup,
                                 concentrationGroup,
                                 epsKappaGroup,
                                 neGroup,
                                 ncGroup,
                                 pc0Rc0Alpha0Group,
                                 hceGroup,
                                 areaGroup,
                                 pressureGroup,
                                 cakeFormationGroup,
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

            ParameterToCell[fsParameterIdentifier.Ne].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.CakePorosity0].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.DryCakeDensity0].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.Kappa0].OwningRow.Visible = false;
            ParameterToCell[fsParameterIdentifier.CakeMoistureContentRf0].OwningRow.Visible = false;
        }

        protected override Control[] GetUIControlsToConnectWithDataUpdating()
        {
            return new Control[] { dataGrid };
        }

        public fsLaboratoryFiltrationTime()
        {
            InitializeComponent();
        }

        protected override void InitializeParametersValues()
        {
            SetDefaultValue(fsParameterIdentifier.MotherLiquidViscosity, new fsValue(1e-3));
            SetDefaultValue(fsParameterIdentifier.MotherLiquidDensity, new fsValue(1000));
            SetDefaultValue(fsParameterIdentifier.SolidsDensity, new fsValue(1500));
            SetDefaultValue(fsParameterIdentifier.SuspensionSolidsMassFraction, new fsValue(0.15));
            SetDefaultValue(fsParameterIdentifier.CakePorosity, new fsValue(0.56));
            SetDefaultValue(fsParameterIdentifier.Ne, new fsValue(0));
            SetDefaultValue(fsParameterIdentifier.CakeCompressibility, new fsValue(0.4));
            SetDefaultValue(fsParameterIdentifier.CakePermeability0, new fsValue(3e-13));
            SetDefaultValue(fsParameterIdentifier.FilterMediumResistanceHce, new fsValue(0.002));
            SetDefaultValue(fsParameterIdentifier.FilterArea, new fsValue(1));
            SetDefaultValue(fsParameterIdentifier.PressureDifference, new fsValue(2e5));
            SetDefaultValue(fsParameterIdentifier.SuspensionVolume, new fsValue(1));
        }

        protected override void InitializeDefaultDiagrams()
        {
            m_defaultDiagrams.Add(
                new Enum[] { },
                new DiagramConfiguration(
                    fsParameterIdentifier.SuspensionVolume,
                    new DiagramConfiguration.DiagramRange(0.5, 2),
                    new[] { fsParameterIdentifier.FiltrationTime },
                    new[] { fsParameterIdentifier.CakeHeight, fsParameterIdentifier.qf }));
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