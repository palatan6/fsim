using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Units
{
    public struct fsUnit
    {
        #region fsUnit

        public string Name { get; private set; }

        public double Coefficient { get; private set; }

        public fsUnit(string name, double coefficient)
            : this()
        {
            Name = name;
            Coefficient = coefficient;
        }

        #endregion

        #region Units Listing

        public static fsUnit NoUnit = new fsUnit("-", 1);
        public static fsUnit MilliMeter = new fsUnit("mm", 1e-3);
        public static fsUnit SantiMeter = new fsUnit("sm", 1e-2);
        public static fsUnit DeciMeter = new fsUnit("dm", 1e-1);
        public static fsUnit Meter = new fsUnit("m", 1);
        public static fsUnit SquareMeter = new fsUnit("m2", 1);
        public static fsUnit SquareDeciMeter = new fsUnit("dm2", 1e-2);
        public static fsUnit SquareSantiMeter = new fsUnit("cm2", 1e-4);
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
        public static fsUnit KiloGrammePerSquaredMeter = new fsUnit("kg/m2", 1);
        public static fsUnit CubicMeterPerSquaredMeter = new fsUnit("m3/m2", 1);
        public static fsUnit LiterPerSquaredMeter = new fsUnit("l/m2", 1e-3);
        public static fsUnit Micrometer = new fsUnit("um", 1e-6);
        
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
