using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;
using StepCalculators;

namespace CalculatorModules.Filter_Cake_And_Suspension_Relations
{
    public partial class MsusAndHcConcaveControl : MsusAndHcBaseControl
    {
        public MsusAndHcConcaveControl()
        {
            InitializeComponent();
        }

        protected override void InitializeGroups()
        {
            fsParametersGroup filtrateGroup = AddGroup(
               fsParameterIdentifier.MotherLiquidDensity);
            fsParametersGroup densitiesGroup = AddGroup(
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.SuspensionDensity);
            fsParametersGroup concentrationGroup = AddGroup(
                fsParameterIdentifier.SuspensionSolidsMassFraction,
                fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                fsParameterIdentifier.SuspensionSolidsConcentration);
            fsParametersGroup epsKappaGroup = AddGroup(
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.Kappa,
                fsParameterIdentifier.Kappam,
                fsParameterIdentifier.DryCakeDensity,
                fsParameterIdentifier.CakeWetDensity,
                fsParameterIdentifier.CakeMoistureContent,
                fsParameterIdentifier.CakeSolidsContentCmc);
            fsParametersGroup machineDiameterGroup = AddGroup(
                fsParameterIdentifier.MachineDiameter);
            m_areaBGroup = AddGroup(
                fsParameterIdentifier.MachineWidth,
                fsParameterIdentifier.WidthOverDiameterRatio,
                fsParameterIdentifier.FilterArea);
            fsParametersGroup cakeHeightGroup = AddGroup(
                fsParameterIdentifier.CakeHeight);
            fsParametersGroup massVolumeGroup = AddGroup(
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.FiltrateMass,
                fsParameterIdentifier.FiltrateVolume,
                fsParameterIdentifier.CakeMass,
                fsParameterIdentifier.CakeVolume,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.LiquidMassInCake,
                fsParameterIdentifier.LiquidVolumeInCake,
                fsParameterIdentifier.SpecificSuspensionMass,
                fsParameterIdentifier.SpecificSuspensionVolume,
                fsParameterIdentifier.SpecificFiltrateMass,
                fsParameterIdentifier.SpecificFiltrateVolume,
                fsParameterIdentifier.SpecificCakeMass,
                fsParameterIdentifier.SpecificCakeVolume,
                fsParameterIdentifier.SpecificSolidsMass,
                fsParameterIdentifier.SpecificSolidsVolume,
                fsParameterIdentifier.SpecificLiquidMassInCake,
                fsParameterIdentifier.SpecificLiquidVolumeInCake
                );
            var groups = new[]
                             {
                                 filtrateGroup,
                                 densitiesGroup,
                                 concentrationGroup,
                                 epsKappaGroup,
                                 machineDiameterGroup,
                                 m_areaBGroup,
                                 cakeHeightGroup,
                                 massVolumeGroup
                             };

            var colors = new[]
                             {
                                 Color.FromArgb(255, 255, 230),
                                 Color.FromArgb(255, 230, 255)
                             };

            for (int i = 0; i < groups.Length; ++i)
            {
                AddGroupToUI(dataGrid, groups[i], colors[i % colors.Length]);
            }
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.FilterArea].RowIndex,
                        Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.MachineWidth].RowIndex,
                        Color.FromArgb(255, 230, 230));
            SetRowColor(dataGrid, ParameterToCell[fsParameterIdentifier.WidthOverDiameterRatio].RowIndex,
                        Color.FromArgb(255, 230, 230));
        }

        protected override void FillCalculationComboBox()
        {
            var restrictedOptions = new List<fsCalculationOption>();
            restrictedOptions.Add(fsCalculationOption.FilterElementDiameterCalculated);
            

            var calculationOption = (fsCalculationOption)CalculationOptions[typeof(fsCalculationOption)];
            if (restrictedOptions.Contains(calculationOption))
            {
                EstablishCalculationOption(fsCalculationOption.MassVolumeCalculated);
            }

            fsMisc.FillList(calculationOptionComboBox.Items, typeof(fsCalculationOption));
            foreach (fsCalculationOption restrictedOption in restrictedOptions)
            {
                calculationOptionComboBox.Items.Remove(fsMisc.GetEnumDescription(restrictedOption));
            }
            calculationOptionComboBox.Text =
                fsMisc.GetEnumDescription((fsCalculationOption)CalculationOptions[typeof(fsCalculationOption)]);
        }

        protected override void UpdateEquationsFromCalculationOptions()
        {
            Calculators = m_concaveCylindricAreaCalculators;
            m_areaBGroup.Representator = fsParameterIdentifier.FilterArea;
        }
    }
}
