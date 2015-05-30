using System;
using System.Reflection;

namespace Units
{
    public struct fsUnit
    {
        #region fsUnit

        public string Name { get; private set; }

        public double Coefficient { get; private set; }

        public bool IsUsUnit { get; private set; }

        public fsUnit(string name, double coefficient)
            : this()
        {
            Name = name;
            Coefficient = coefficient;
            IsUsUnit = false;
        }

        public fsUnit(string name, double coefficient, bool isUsUnit)
            : this()
        {
            Name = name;
            Coefficient = coefficient;
            IsUsUnit = isUsUnit;
        }

        #endregion

        #region Units Listing

        public static fsUnit NoUnit = new fsUnit("-", 1);
        public static fsUnit MilliMeter = new fsUnit("mm", 1e-3);
        public static fsUnit SantiMeter = new fsUnit("cm", 1e-2);
        public static fsUnit DeciMeter = new fsUnit("dm", 1e-1);
        public static fsUnit Meter = new fsUnit("m", 1);
        public static fsUnit SquareMeter = new fsUnit("m2", 1);
        public static fsUnit SquareDeciMeter = new fsUnit("dm2", 1e-2);
        public static fsUnit SquareSantiMeter = new fsUnit("cm2", 1e-4);
        public static fsUnit SquareCentimeterPerBarMin = new fsUnit("cm2/(bar min)", 1e-2 / (1e5 * 60));
        public static fsUnit SquareMeterPerPaSec = new fsUnit("m2/(Pa s)", 1);
        public static fsUnit SquareMilliMeter = new fsUnit("mm2", 1e-6);
        public static fsUnit Bar = new fsUnit("bar", 1e5);
        public static fsUnit Pascal = new fsUnit("Pa", 1);
        public static fsUnit Second = new fsUnit("s", 1);
        public static fsUnit Minute = new fsUnit("min", 60);
        public static fsUnit Hour = new fsUnit("h", 3600);
        public static fsUnit MeterPerSecond = new fsUnit("m/s", 1.0);
        public static fsUnit MeterPerMinute = new fsUnit("m/min", 1.0 / 60);
        public static fsUnit PerSecond = new fsUnit("1/s", 1);
        public static fsUnit PerMinute = new fsUnit("1/min", 1.0 / 60);
        public static fsUnit Percent = new fsUnit("%", 1e-2);
        public static fsUnit GrammeOverKilogram = new fsUnit("g/kg", 1e-3);
        public static fsUnit MilliGrammOverKiloGramme = new fsUnit("mg/kg", 1e-6);
        public static fsUnit GrammePerLiter = new fsUnit("g/l", 1);
        public static fsUnit GrammePerCubicSantiMeter = new fsUnit("g/cm3", 1e-3 / 1e-6);
        public static fsUnit KiloGrammePerCubicMeter = new fsUnit("kg/m3", 1);
        public static fsUnit MilliPascalSecond = new fsUnit("mPa s", 1e-3);
        public static fsUnit PascalSecond = new fsUnit("Pa s", 1);
        public static fsUnit MilliNewtonPerMeter = new fsUnit("mili N/m", 1e-3);
        public static fsUnit NewtonPerMeter = new fsUnit("N/m", 1);
        public static fsUnit Ton = new fsUnit("t", 1e3);
        public static fsUnit KiloGramme = new fsUnit("kg", 1);
        public static fsUnit Gramme = new fsUnit("g", 1e-3);
        public static fsUnit Liter = new fsUnit("l", 1e-3);
        public static fsUnit MilliLiter = new fsUnit("ml", 1e-6);
        public static fsUnit CubicMeter = new fsUnit("m3", 1);
        public static fsUnit TonPerHour = new fsUnit("t/h", 1000 / 3600.0);
        public static fsUnit KiloGrammePerHour = new fsUnit("kg/h", 1 / 3600.0);
        public static fsUnit KiloGrammePerMin = new fsUnit("kg/min", 1 / 60.0);
        public static fsUnit KiloGrammePerSec = new fsUnit("kg/s", 1.0);
        public static fsUnit KiloGrammePerSquaredMeterPerSec = new fsUnit("kg/(m2 s)", 1.0);
        public static fsUnit KiloGrammePerSquaredMeterPerMin = new fsUnit("kg/(m2 min)", 1.0 / 60);
        public static fsUnit KiloGrammePerSquaredMeterPerHour = new fsUnit("kg/(m2 h)", 1.0 / 3600);
        public static fsUnit CubicMeterPerSecond = new fsUnit("m3/s", 1.0);
        public static fsUnit CubicMeterPerMinute = new fsUnit("m3/min", 1.0 / 60);
        public static fsUnit CubicMeterPerHour = new fsUnit("m3/h", 1.0 / 3600);
        public static fsUnit CubicMeterPerKiloGramme = new fsUnit("m3/kg", 1.0);
        public static fsUnit LiterPerSecond = new fsUnit("l/s", 1.0e-3);
        public static fsUnit LiterPerMinute = new fsUnit("l/min", 1.0e-3 / 60);
        public static fsUnit LiterPerHour = new fsUnit("l/h", 1.0e-3 / 3600);
        public static fsUnit LiterPerSquaredMeterPerSec = new fsUnit("l/(m2 s)", 1e-3);
        public static fsUnit LiterPerSquaredMeterPerMin = new fsUnit("l/(m2 min)", 1e-3 / 60);
        public static fsUnit LiterPerSquaredMeterPerHour = new fsUnit("l/(m2 h)", 1e-3 / 3600);
        public static fsUnit PerTen13SquareMeter = new fsUnit("10-13m2", 1e-13);
        public static fsUnit Ten13PerSquareMeter = new fsUnit("10+13m-2", 1e13);
        public static fsUnit Ten10MeterPerKiloGramme = new fsUnit("10+10m/kg", 1e10);
        public static fsUnit Ten10PerMeter = new fsUnit("10^10/m", 1e10);
        public static fsUnit GrammePerSquaredCentimeter = new fsUnit("g/cm2", 1e-3 / 1e-4);
        public static fsUnit KiloGrammePerSquaredDeciMeter = new fsUnit("kg/dm2", 1 / 1e-2);
        public static fsUnit KiloGrammePerSquaredMeter = new fsUnit("kg/m2", 1);
        public static fsUnit TonsPerSquaredMeter = new fsUnit("t/m2", 1e3);
        public static fsUnit MilliLiterPerSquaredCentimeter = new fsUnit("ml/cm2", 1e-6 / 1e-4);
        public static fsUnit LiterPerSquaredCentimeter = new fsUnit("l/cm2", 1e-3 / 1e-4);
        public static fsUnit CubicMeterPerSquaredCentimeter = new fsUnit("m3/cm2", 1 / 1e-4);
        public static fsUnit LiterPerSquaredDecimeter = new fsUnit("l/dm2", 1e-3 / 1e-2);
        public static fsUnit CubicMeterPerSquaredDecimeter = new fsUnit("m3/dm2", 1 / 1e-2);
        public static fsUnit CubicMeterPerSquaredMeter = new fsUnit("m3/m2", 1);
        public static fsUnit LiterPerSquaredMeter = new fsUnit("l/m2", 1e-3);
        public static fsUnit LiterPerKiloGramme = new fsUnit("l/kg", 1.0e-3);
        public static fsUnit Micrometer = new fsUnit("um", 1e-6);
        public static fsUnit SquaredMeterPerPascalPerSecond = new fsUnit("m2/(Pa s)", 1);
        public static fsUnit SquaredSantiMeterPerBarPerMinute = new fsUnit("cm2/(bar min)", 1e-10 / 6);

        public static fsUnit CelsiusDegree = new fsUnit("°C", 1);

        const double PoundFactor = 0.45359237;
        const double InchFactor = 0.0254;
        const double FootFactor = InchFactor * 12;
        const double YardFactor = FootFactor * 3;
        const double mmHgFactor = 133.3224;
        const double PsiFactor = 6894.8;

        public static fsUnit Pound = new fsUnit("lb", PoundFactor, true);
        public static fsUnit CubicFeet = new fsUnit("ft3", Math.Pow(FootFactor, 3), true);
        public static fsUnit CubicYards = new fsUnit("yd3", Math.Pow(YardFactor, 3), true);
        public static fsUnit SquareFeet = new fsUnit("ft2", Math.Pow(FootFactor, 2), true);
        public static fsUnit Inch = new fsUnit("in", InchFactor, true);
        public static fsUnit Foot = new fsUnit("ft", FootFactor, true);
        public static fsUnit Yard = new fsUnit("yd", YardFactor, true);
        public static fsUnit mmHg = new fsUnit("mmHg", mmHgFactor, true);
        public static fsUnit Torr = new fsUnit("Torr", mmHgFactor, true);
        public static fsUnit inHg = new fsUnit("inHg", mmHgFactor*1000*InchFactor, true);
        public static fsUnit psi = new fsUnit("psi", PsiFactor, true);
        public static fsUnit CubicFeetPerSecond = new fsUnit("ft3/s", Math.Pow(FootFactor, 3), true);
        public static fsUnit CubicFeetPerMinute = new fsUnit("ft3/min", Math.Pow(FootFactor, 3)/60, true);
        public static fsUnit CubicFeetPerHour = new fsUnit("ft3/h", Math.Pow(FootFactor, 3)/3600, true);
        public static fsUnit CubicYardsPerSecond = new fsUnit("yd3/s", Math.Pow(YardFactor, 3), true);
        public static fsUnit CubicYardsPerMinutes = new fsUnit("yd3/min", Math.Pow(YardFactor, 3)/60, true);
        public static fsUnit CubicYardsPerHour = new fsUnit("yd3/h", Math.Pow(YardFactor, 3)/3600, true);
        public static fsUnit PoundPerSecond = new fsUnit("lb/s", PoundFactor, true);
        public static fsUnit PoundPerMinutes = new fsUnit("lb/min", PoundFactor/60, true);
        public static fsUnit PoundPerHour = new fsUnit("lb/h", PoundFactor/3600, true);
        public static fsUnit PoundPerSquaredMeter = new fsUnit("lb/m2", PoundFactor, true);
        public static fsUnit PoundPerSquaredFoot = new fsUnit("lb/ft2", PoundFactor/Math.Pow(FootFactor, 2), true);
        public static fsUnit TonsPerSquaredFoot = new fsUnit("t/ft2", 1e3 / Math.Pow(FootFactor, 2), true);
        public static fsUnit CubicFeetPerSquaredMeter = new fsUnit("ft3/m2", Math.Pow(FootFactor, 3), true);
        public static fsUnit CubicFeetPerSquaredFeet = new fsUnit("ft3/ft2", FootFactor, true);
        public static fsUnit CubicYardsPerSquaredFeet = new fsUnit("yd3/ft2", Math.Pow(FootFactor, 3) / Math.Pow(FootFactor, 2), true);
        public static fsUnit CubicYardsPerSquaredMeter = new fsUnit("yd3/m2", Math.Pow(YardFactor, 3), true);
        public static fsUnit CubicFeetPerSquaredMeterPerHour = new fsUnit("ft3/m2h", Math.Pow(FootFactor, 3)/3600, true);
        public static fsUnit CubicFeetPerSquaredMeterPerMinute = new fsUnit("ft3/m2min", Math.Pow(FootFactor, 3)/60,
            true);
        public static fsUnit CubicFeetPerSquaredMeterPerSecond = new fsUnit("ft3/m2s", Math.Pow(FootFactor, 3), true);
        public static fsUnit CubicFeetPerSquaredFeetPerHour = new fsUnit("ft3/ft2h", FootFactor/3600, true);
        public static fsUnit CubicFeetPerSquaredFeetPerMinute = new fsUnit("ft3/ft2min", FootFactor/60, true);
        public static fsUnit CubicFeetPerSquaredFeetPerSecond = new fsUnit("ft3/ft2s", FootFactor, true);
        public static fsUnit CubicYardsPerSquaredMeterPerHour = new fsUnit("yd3/m2h", Math.Pow(YardFactor, 3)/3600, true);
        public static fsUnit CubicYardsPerSquaredMeterPerMinute = new fsUnit("yd3/m2min", Math.Pow(YardFactor, 3)/60,
            true);
        public static fsUnit CubicYardsPerSquaredMeterPerSecond = new fsUnit("yd3/m2s", Math.Pow(YardFactor, 3), true);
        public static fsUnit CubicYardsPerSquaredFeetPerHour = new fsUnit("yd3/ft2h",
            Math.Pow(YardFactor, 3)/Math.Pow(FootFactor, 2)/3600, true);
        public static fsUnit CubicYardsPerSquaredFeetPerMinute = new fsUnit("yd3/ft2min",
            Math.Pow(YardFactor, 3)/Math.Pow(FootFactor, 2)/60, true);
        public static fsUnit CubicYardsPerSquaredFeetPerSecond = new fsUnit("yd3/ft2s",
            Math.Pow(YardFactor, 3)/Math.Pow(FootFactor, 2), true);
        public static fsUnit PoundPerSquaredFeetPerHour = new fsUnit("lb/ft2h", PoundFactor/Math.Pow(FootFactor, 2)/3600,
            true);
        public static fsUnit PoundPerSquaredFeetPerMinute = new fsUnit("lb/ft2min",
            PoundFactor/Math.Pow(FootFactor, 2)/60, true);
        public static fsUnit PoundPerSquaredFeetPerSecond = new fsUnit("lb/ft2s", PoundFactor/Math.Pow(FootFactor, 2),
            true);
        public static fsUnit PoundPerSquaredMeterPerHour = new fsUnit("lb/m2h", PoundFactor/3600, true);
        public static fsUnit PoundPerSquaredMeterPerMinute = new fsUnit("lb/m2min", PoundFactor/60, true);
        public static fsUnit PoundPerSquaredMeterPerSecond = new fsUnit("lb/m2s", PoundFactor, true);

        #endregion

        public static fsUnit UnitFromText(string unitName)
        {
            Type type = typeof(fsUnit);
            FieldInfo[] fields = type.GetFields();
            foreach (var field in fields)
            {
                var unit = ((fsUnit)field.GetValue(null));
                if (unit.Name == unitName)
                {
                    return unit;
                }
            }
            throw new Exception("Desired name doesn't correspond to any units.");
        }
    }

}
