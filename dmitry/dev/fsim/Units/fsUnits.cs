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

        // unicode: \u207B(uppercase minus) \u2009(tiny space) 
        //          \u2070(uppercase 0) \u00B9(uppercase 1) \u00B2(uppercase 2) \u00B3(uppercase 3)
        public static fsUnit NoUnit = new fsUnit("-", 1);
        public static fsUnit MilliMeter = new fsUnit("mm", 1e-3);
        public static fsUnit SantiMeter = new fsUnit("cm", 1e-2);
        public static fsUnit DeciMeter = new fsUnit("dm", 1e-1);
        public static fsUnit Meter = new fsUnit("m", 1);
        public static fsUnit SquareMeter = new fsUnit("m" + "\u00B2", 1); // previously "m2"
        public static fsUnit SquareDeciMeter = new fsUnit("dm" + "\u00B2", 1e-2); // previously "dm2"
        public static fsUnit SquareSantiMeter = new fsUnit("cm" + "\u00B2", 1e-4); // previously "cm2"
        public static fsUnit SquareCentimeterPerBarMin = new fsUnit("cm" + "\u00B2" + "/(bar min)", 1e-2 / (1e5 * 60)); // previously "cm2/(bar min)"
        public static fsUnit SquareMeterPerPaSec = new fsUnit("m" + "\u00B2" + "/(Pa s)", 1); // previously "m2/(Pa s)"
        public static fsUnit SquareMilliMeter = new fsUnit("mm" + "\u00B2", 1e-6); // previously "mm2"
        public static fsUnit Bar = new fsUnit("bar", 1e5);
        public static fsUnit Pascal = new fsUnit("Pa", 1);
        public static fsUnit Second = new fsUnit("s", 1);
        public static fsUnit Minute = new fsUnit("min", 60);
        public static fsUnit Hour = new fsUnit("h", 3600);
        public static fsUnit MeterPerSecond = new fsUnit("m/s", 1.0);
        public static fsUnit MeterPerMinute = new fsUnit("m/min", 1.0 / 60);
        public static fsUnit PerSecond = new fsUnit("1/s", 1);
        public static fsUnit PerMeter = new fsUnit("1/m", 1);
        public static fsUnit PerMinute = new fsUnit("1/min", 1.0 / 60);
        public static fsUnit Percent = new fsUnit("%", 1e-2);
        public static fsUnit GrammeOverKilogram = new fsUnit("g/kg", 1e-3);
        public static fsUnit MilliGrammOverKiloGramme = new fsUnit("mg/kg", 1e-6);
        public static fsUnit GrammePerLiter = new fsUnit("g/l", 1);
        public static fsUnit GrammePerCubicSantiMeter = new fsUnit("g/cm" + "\u00B3", 1e-3 / 1e-6);  // previously "g/cm3"
        public static fsUnit KiloGrammePerCubicMeter = new fsUnit("kg/m" + "\u00B3", 1); // previously "kg/m3"
        public static fsUnit MilliPascalSecond = new fsUnit("mPa s", 1e-3);
        public static fsUnit PascalSecond = new fsUnit("Pa s", 1);
        public static fsUnit MilliNewtonPerMeter = new fsUnit("mili N/m", 1e-3);
        public static fsUnit NewtonPerMeter = new fsUnit("N/m", 1);
        public static fsUnit Ton = new fsUnit("t", 1e3);
        public static fsUnit KiloGramme = new fsUnit("kg", 1);
        public static fsUnit Gramme = new fsUnit("g", 1e-3);
        public static fsUnit Liter = new fsUnit("l", 1e-3);
        public static fsUnit MilliLiter = new fsUnit("ml", 1e-6);
        public static fsUnit CubicMeter = new fsUnit("m" + "\u00B3", 1); // previously "m3"
        public static fsUnit TonPerHour = new fsUnit("t/h", 1000 / 3600.0);
        public static fsUnit KiloGrammePerHour = new fsUnit("kg/h", 1 / 3600.0);
        public static fsUnit KiloGrammePerMin = new fsUnit("kg/min", 1 / 60.0);
        public static fsUnit KiloGrammePerSec = new fsUnit("kg/s", 1.0);
        public static fsUnit KiloGrammePerSquaredMeterPerSec = new fsUnit("kg/(m" + "\u00B2" + " s)", 1.0); // previously "kg/(m2 s)"
        public static fsUnit KiloGrammePerSquaredMeterPerMin = new fsUnit("kg/(m" + "\u00B2" + " min)", 1.0 / 60); // previously "kg/(m2 min)"
        public static fsUnit KiloGrammePerSquaredMeterPerHour = new fsUnit("kg/(m" + "\u00B2" + " h)", 1.0 / 3600); // previously "kg/(m2 h)"
        public static fsUnit CubicMeterPerSecond = new fsUnit("m" + "\u00B3" + "/s", 1.0); // previously "m3/s"
        public static fsUnit CubicMeterPerMinute = new fsUnit("m" + "\u00B3" + "/min", 1.0 / 60); // previously "m3/min"
        public static fsUnit CubicMeterPerHour = new fsUnit("m" + "\u00B3" + "/h", 1.0 / 3600); // previously "m3/h"
        public static fsUnit CubicMeterPerKiloGramme = new fsUnit("m" + "\u00B3" + "/kg", 1.0); // previously "m3/kg"
        public static fsUnit LiterPerSecond = new fsUnit("l/s", 1.0e-3);
        public static fsUnit LiterPerMinute = new fsUnit("l/min", 1.0e-3 / 60);
        public static fsUnit LiterPerHour = new fsUnit("l/h", 1.0e-3 / 3600);
        public static fsUnit LiterPerSquaredMeterPerSec = new fsUnit("l/(m" + "\u00B2" + " s)", 1e-3); // previously "l/(m2 s)"
        public static fsUnit LiterPerSquaredMeterPerMin = new fsUnit("l/(m" + "\u00B2" + " min)", 1e-3 / 60); // previously "l/(m2 min)"
        public static fsUnit LiterPerSquaredMeterPerHour = new fsUnit("l/(m" + "\u00B2" + " h)", 1e-3 / 3600);  // previously "l/(m2 h)"
        public static fsUnit PerTen13SquareMeter = new fsUnit("10" + "\u207B\u00B9\u00B3\u2009" + "m" + "\u00B2", 1e-13); // previously "10-13m2"
        public static fsUnit Ten13PerSquareMeter = new fsUnit("10" + "\u00B9\u00B3\u2009" + "m" + "\u207B\u00B2", 1e13); // previously "10+13m-2"
        public static fsUnit Ten10MeterPerKiloGramme = new fsUnit("10" + "\u00B9\u2070\u2009" + "m/kg", 1e10); // previously "10+10m/kg"
        public static fsUnit Ten10PerMeter = new fsUnit("10" + "\u00B9\u2070\u2009" + "m" + "\u207B\u00B9", 1e10); // previously "10^10/m"
        public static fsUnit KiloGrammePerSquaredMeter = new fsUnit("kg/m" + "\u00B2", 1); // previously "kg/m2"
        public static fsUnit CubicMeterPerSquaredMeter = new fsUnit("m" + "\u00B3" + "/m" + "\u00B2", 1); // previously "m3/m2"
        public static fsUnit LiterPerSquaredMeter = new fsUnit("l/m" + "\u00B2", 1e-3); // previously "l/m2"
        public static fsUnit LiterPerKiloGramme = new fsUnit("l/kg", 1.0e-3);
        public static fsUnit Micrometer = new fsUnit("um", 1e-6);
        public static fsUnit SquaredMeterPerPascalPerSecond = new fsUnit("m"+ "\u00B2" + "/(Pa s)", 1); // previously "m2/(Pa s)"
        public static fsUnit SquaredSantiMeterPerBarPerMinute = new fsUnit("cm"+ "\u00B2" + "/(bar min)", 1e-10 / 6); // previously "cm2/(bar min)"

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
