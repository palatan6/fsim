using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using CalculatorModules.Base_Controls;
using Parameters;


namespace CalculatorModules.CakeWashing
{
    public partial class fsCakeWashingControl : fsOptionsDoubleTableAndCommentsCalculatorControl
    {
        public fsCakeWashingControl()
        {
            InitializeComponent();

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
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);

            fsParametersGroup cGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);

            fsParametersGroup neGroup = AddGroup(
                fsParameterIdentifier.Ne);

            fsParametersGroup epsGroup = AddGroup(
                fsParameterIdentifier.CakePorosity0,
                fsParameterIdentifier.Kappa0,
                fsParameterIdentifier.DryCakeDensity0,
                fsParameterIdentifier.CakeWetDensity0,
                fsParameterIdentifier.CakeWetMassSolidsFractionRs0,
                fsParameterIdentifier.CakeMoistureContentRf0,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.Kappa,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.CakeWetDensity,
                fsParameterIdentifier.CakeWetMassSolidsFractionRs,
                fsParameterIdentifier.CakeMoistureContentRf);

            fsParametersGroup pcrcGroup = AddGroup(
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0);

            fsParametersGroup ncGroup = AddGroup(
                fsParameterIdentifier.CakeCompressibility);

            fsParametersGroup hce0Group = AddGroup(
                fsParameterIdentifier.FilterMediumResistanceHce0,
                fsParameterIdentifier.FilterMediumResistanceRm0);

            var materialGroups = new[]
                             {
                                etafGroup,
                                rhofGroup,
                                densitiesGroup,
                                cGroup,
                                neGroup,
                                epsGroup,
                                pcrcGroup,
                                ncGroup,
                                hce0Group, 
                             };

            for (int i = 0; i < materialGroups.Length; ++i)
            {
                materialGroups[i].Kind = fsParametersGroup.ParametersGroupKind.MaterialParameters;
                AddGroupToUI(materialParametersDataGrid, materialGroups[i], colors[i % colors.Length]);
                SetGroupInput(materialGroups[i], true);
            }

            #endregion
        }
    }
}
