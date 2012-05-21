using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parameters;

namespace CalculatorModules.Cake_Fromation
{
    public partial class fsCommonCakeFormationControl : fsCakeFormationBaseControl
    {
        public fsCommonCakeFormationControl()
        {
            InitializeComponent();
        }

        protected override void AddCakeFormationCalculator()
        {
            // do nothing here while we don't have specific calculator
        }

        override protected fsParametersGroup[] MakeMachiningStandardGroups()
        {
            fsParametersGroup abGroup = AddGroup(
               fsParameterIdentifier.FilterArea);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup specificTimeGroup = AddGroup(
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.SpecificResidualTime,
                fsParameterIdentifier.ResidualTime);

            fsParametersGroup timeQGroup = AddGroup(
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.qft,
                fsParameterIdentifier.qmft,
                fsParameterIdentifier.Qms,
                fsParameterIdentifier.Qsus,
                fsParameterIdentifier.SuspensionMassFlowrate);

            fsParametersGroup resultsGroup = AddOnlyCalculatedGroup(
                fsParameterIdentifier.MeanHeightRate,
                fsParameterIdentifier.HcOverTc,
                fsParameterIdentifier.DiffHeightRate,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SpecificSuspensionMass,
                fsParameterIdentifier.SpecificSuspensionVolume,
                fsParameterIdentifier.Qmsust,
                fsParameterIdentifier.Qmsusd,
                fsParameterIdentifier.Qsust,
                fsParameterIdentifier.Qsusd,
                fsParameterIdentifier.qmsust,
                fsParameterIdentifier.qmsusd,
                fsParameterIdentifier.qsust,
                fsParameterIdentifier.qsusd);

            return new[]
                       {
                           abGroup,
                           dpGroup,
                           specificTimeGroup,
                           timeQGroup,
                           resultsGroup
                       };
        }

        override protected fsParametersGroup[] MakeMachiningDesignGroups()
        {
            fsParametersGroup qsusGroup = AddGroup(
                fsParameterIdentifier.Qms,
                fsParameterIdentifier.Qsus,
                fsParameterIdentifier.SuspensionMassFlowrate);

            fsParametersGroup dpGroup = AddGroup(
                fsParameterIdentifier.PressureDifference);

            fsParametersGroup cycleGroup = AddGroup(
                fsParameterIdentifier.RotationalSpeed,
                fsParameterIdentifier.CycleTime,
                fsParameterIdentifier.SpecificFiltrationTime,
                fsParameterIdentifier.SpecificResidualTime,
                fsParameterIdentifier.ResidualTime);

            fsParametersGroup filtrationGroup = AddGroup(
                fsParameterIdentifier.CakeHeight,
                fsParameterIdentifier.FiltrationTime,
                fsParameterIdentifier.qft,
                fsParameterIdentifier.qmft);

            fsParametersGroup resultsGroup = AddOnlyCalculatedGroup(
                fsParameterIdentifier.FilterArea,
                fsParameterIdentifier.MeanHeightRate,
                fsParameterIdentifier.HcOverTc,
                fsParameterIdentifier.DiffHeightRate,
                fsParameterIdentifier.SolidsMass,
                fsParameterIdentifier.SuspensionMass,
                fsParameterIdentifier.SolidsVolume,
                fsParameterIdentifier.SuspensionVolume,
                fsParameterIdentifier.SpecificSuspensionMass,
                fsParameterIdentifier.SpecificSuspensionVolume,
                fsParameterIdentifier.Qmsust,
                fsParameterIdentifier.Qmsusd,
                fsParameterIdentifier.Qsust,
                fsParameterIdentifier.Qsusd,
                fsParameterIdentifier.qmsust,
                fsParameterIdentifier.qmsusd,
                fsParameterIdentifier.qsust,
                fsParameterIdentifier.qsusd);

            return new[]
                       {
                           qsusGroup,
                           dpGroup,
                           cycleGroup,
                           filtrationGroup,
                           resultsGroup
                       };
        }
    }
}
