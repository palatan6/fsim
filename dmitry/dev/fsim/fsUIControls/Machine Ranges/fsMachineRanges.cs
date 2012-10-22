using System.Collections.Generic;
using System.Linq;
using Parameters;
using ParametersIdentifiers.Ranges;
using Units;

namespace CalculatorModules.Machine_Ranges
{
    public class fsParameterRange
    {
        public fsParameterRange(
            fsParameterIdentifier parameterIdentifier,
            fsUnit units,
            double rangeStart,
            double rangeEnd)
        {
            Identifier = parameterIdentifier;
            Units = units;
            Range = new fsRange(rangeStart * units.Coefficient,
                                rangeEnd * units.Coefficient);
        }

        public fsParameterIdentifier Identifier { get; private set; }
        public fsUnit Units { get; private set; }
        public fsRange Range { get; private set; }
    }

    public class fsMachineRanges
    {
        #region Constructor

        public fsMachineRanges(string name, IEnumerable<fsParameterRange> ranges)
        {
            Name = name;
            Ranges = CreateDefaultRanges();

            if (ranges == null)
                return;

            SetRanges(ranges);
        }

        private void SetRanges(IEnumerable<fsParameterRange> ranges)
        {
            foreach (fsParameterRange parameterRange in ranges)
            {
                if (Ranges.ContainsKey(parameterRange.Identifier))
                {
                    Ranges[parameterRange.Identifier] = parameterRange;
                }
                else
                {
                    Ranges.Add(parameterRange.Identifier, parameterRange);
                }
            }
        }

        private static Dictionary<fsParameterIdentifier, fsParameterRange> CreateDefaultRanges()
        {
            var defaultRanges = new[]
                                    {
                                        new fsParameterRange(fsParameterIdentifier.MotherLiquidViscosity,
                                                             fsUnit.MilliPascalSecond, 0.2, 100.0),
                                        new fsParameterRange(fsParameterIdentifier.MotherLiquidDensity,
                                                             fsUnit.KiloGrammePerCubicMeter, 500, 2000),
                                        new fsParameterRange(fsParameterIdentifier.SolidsDensity,
                                                             fsUnit.KiloGrammePerCubicMeter, 1000, 8000),
                                        new fsParameterRange(fsParameterIdentifier.SuspensionDensity,
                                                             fsUnit.KiloGrammePerCubicMeter, 500, 4000),
                                        new fsParameterRange(fsParameterIdentifier.SuspensionSolidsMassFraction,
                                                             fsUnit.Percent, 1, 80),
                                        new fsParameterRange(fsParameterIdentifier.SuspensionSolidsVolumeFraction,
                                                             fsUnit.Percent, 0.1, 40),
                                        new fsParameterRange(fsParameterIdentifier.SuspensionSolidsConcentration,
                                                             fsUnit.KiloGrammePerCubicMeter, 5, 2000),
                                        new fsParameterRange(fsParameterIdentifier.CakePorosity,
                                                             fsUnit.Percent, 30, 90),
                                        new fsParameterRange(fsParameterIdentifier.Kappa,
                                                             fsUnit.NoUnit, 0.01, 1.2),
                                        new fsParameterRange(fsParameterIdentifier.DryCakeDensity,
                                                             fsUnit.KiloGrammePerCubicMeter, 500, 5000),
                                        new fsParameterRange(fsParameterIdentifier.CakePermeability0,
                                                             fsUnit.PerTen13SquareMeter, 0.01, 20),
                                        new fsParameterRange(fsParameterIdentifier.CakeResistance0,
                                                             fsUnit.Ten13PerSquareMeter, 0.01, 100),
                                        new fsParameterRange(fsParameterIdentifier.CakeResistanceAlpha0,
                                                             fsUnit.Ten10MeterPerKiloGramme, 0.01, 100),
                                        new fsParameterRange(fsParameterIdentifier.CakeCompressibility,
                                                             fsUnit.NoUnit, 0, 1),
                                        new fsParameterRange(fsParameterIdentifier.FilterMediumResistanceRm0,
                                                             fsUnit.Ten10PerMeter, 0, 100),
                                        new fsParameterRange(fsParameterIdentifier.FilterMediumResistanceHce0,
                                                             fsUnit.MilliMeter, 0, 50),
                                        new fsParameterRange(fsParameterIdentifier.FilterArea,
                                                             fsUnit.SquareMeter, 0.001, 200),
                                        new fsParameterRange(fsParameterIdentifier.PressureDifference,
                                                             fsUnit.Bar, 0.1, 10),
                                        new fsParameterRange(fsParameterIdentifier.CakeHeight,
                                                             fsUnit.MilliMeter, 1, 1000),
                                        new fsParameterRange(fsParameterIdentifier.RotationalSpeed,
                                                             fsUnit.PerMinute, 0.1, 5),
                                        new fsParameterRange(fsParameterIdentifier.CycleTime,
                                                             fsUnit.Minute, 1, 600),
                                        new fsParameterRange(fsParameterIdentifier.SpecificFiltrationTime,
                                                             fsUnit.Percent, 0.05, 100),
                                        new fsParameterRange(fsParameterIdentifier.FiltrationTime,
                                                             fsUnit.Minute, 0.01, 300),
                                        new fsParameterRange(fsParameterIdentifier.SpecificResidualTime,
                                                             fsUnit.Percent, 0.01, 90),
                                        new fsParameterRange(fsParameterIdentifier.ResidualTime,
                                                             fsUnit.Minute, 0.1, 600),
                                        new fsParameterRange(fsParameterIdentifier.ReducedCutSize,
                                                             fsUnit.Meter, 1.0e-7, 1.0e-3)
                                    };

            return defaultRanges.ToDictionary(parameterRange => parameterRange.Identifier);
        }

        #endregion

        #region Machine Ranges Set

        public static fsMachineRanges DefaultMachineRanges =
            new fsMachineRanges("Default Machine Ranges", null);

        public static fsMachineRanges LaboratoryPressureNutscheFilter =
            new fsMachineRanges("Laboratory Pressure Nutsche Filter",
                                new[]
                                    {
                                        new fsParameterRange(fsParameterIdentifier.FilterArea,
                                                             fsUnit.SquareSantiMeter, 0.001, 200),
                                        new fsParameterRange(fsParameterIdentifier.CakeHeight,
                                                             fsUnit.MilliMeter, 1, 70),
                                        new fsParameterRange(fsParameterIdentifier.CycleTime,
                                                             fsUnit.Second, 5, 6000),
                                        new fsParameterRange(fsParameterIdentifier.FiltrationTime,
                                                             fsUnit.Second, 1, 1000),
                                        new fsParameterRange(fsParameterIdentifier.ResidualTime,
                                                             fsUnit.Second, 10, 1000)
                                    });

        public static fsMachineRanges LaboratoryVacuumFilter =
            new fsMachineRanges("Laboratory Vacuum Filter",
                                new[]
                                    {
                                        new fsParameterRange(fsParameterIdentifier.FilterArea,
                                                             fsUnit.SquareSantiMeter, 0.001, 200),
                                        new fsParameterRange(fsParameterIdentifier.PressureDifference,
                                                             fsUnit.Bar, 0.1, 0.9),
                                        new fsParameterRange(fsParameterIdentifier.CycleTime,
                                                             fsUnit.Second, 5, 6000),
                                        new fsParameterRange(fsParameterIdentifier.CakeHeight,
                                                             fsUnit.MilliMeter, 1, 70),
                                        new fsParameterRange(fsParameterIdentifier.FiltrationTime,
                                                             fsUnit.Second, 1, 1000),
                                        new fsParameterRange(fsParameterIdentifier.ResidualTime,
                                                             fsUnit.Second, 10, 1000)
                                    });

        #endregion

        public string Name { get; private set; }
        public Dictionary<fsParameterIdentifier, fsParameterRange> Ranges { get; private set; }
    }
}