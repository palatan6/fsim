using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Units
{
    public class fsCharacteristic
    {
        #region fsCharacteristic

        private string m_name;
        public string Name
        {
            get { return m_name; }
        }

        private readonly fsUnit[] m_units;
        public fsUnit[] Units
        {
            get { return m_units; }
        }

        private fsUnit m_currentUnit;
        public fsUnit CurrentUnit
        {
            get { return m_currentUnit; }
            set { m_currentUnit = value; }
        }
        private fsCharacteristic(string name, fsUnit[] units)
        {
            m_name = name;
            m_units = new fsUnit[units.Length];
            for (int i = 0; i < units.Length; ++i)
            {
                m_units[i] = units[i];
            }
            CurrentUnit = m_units[0];
        }


        #endregion

        #region Characteristic Listing

        public static fsCharacteristic NoUnits = new fsCharacteristic(
            "No units",
            new[] {
                fsUnit.NoUnit
            }
        );

        public static fsCharacteristic Length = new fsCharacteristic(
            "Length",
            new[] {
                fsUnit.MilliMeter,
                fsUnit.SantiMeter,
                fsUnit.DeciMeter,
                fsUnit.Meter
            }
        );

        public static fsCharacteristic CakeHeight = new fsCharacteristic(
            "Cake Height",
            new[] {
                fsUnit.MilliMeter,
                fsUnit.SantiMeter,
                fsUnit.DeciMeter,
                fsUnit.Meter
            }
        );

        public static fsCharacteristic MachineGeometryLength = new fsCharacteristic(
            "Machine Geometry Length",
            new[] {
                fsUnit.Meter,
                fsUnit.DeciMeter,
                fsUnit.SantiMeter,
                fsUnit.MilliMeter
            }
        );

        public static fsCharacteristic Area = new fsCharacteristic(
            "Area",
            new[] {
                fsUnit.SquareSantiMeter,
                fsUnit.SquareDeciMeter,
                fsUnit.SquareMeter,
            }
        );

        public static fsCharacteristic Pressure = new fsCharacteristic(
            "Pressure",
            new[] {
                fsUnit.Bar,
                fsUnit.Pascal
            }
        );

        public static fsCharacteristic Time = new fsCharacteristic(
            "Time",
            new[] {
                fsUnit.Second,
                fsUnit.Minute,
                fsUnit.Hour
            }
        );

        public static fsCharacteristic Speed = new fsCharacteristic(
            "Speed",
            new[] {
                fsUnit.MeterPerMinute,
                fsUnit.MeterPerSecond
            }
        );

        public static fsCharacteristic Power = new fsCharacteristic(
            "Power",
            new[] {
                fsUnit.KiloWatt,
                fsUnit.Watt
            }
        );

        public static fsCharacteristic Frequency = new fsCharacteristic(
            "Frequency",
            new[] {
                fsUnit.PerMinute,
                fsUnit.PerSecond
            }
        );

        public static fsCharacteristic Concentration = new fsCharacteristic(
            "Concentration",
            new[] {
                fsUnit.Percent,
                fsUnit.NoUnit
            }
        );


        public static fsCharacteristic CakeWashOutContent = new fsCharacteristic(
            "Cake with wash out content",
            new[] {
                fsUnit.GrammeOverKilogram,
                fsUnit.Percent,
                fsUnit.MilliGrammOverKiloGramme
            }
        );

        public static fsCharacteristic SolidsConcentration = new fsCharacteristic(
            "Solids concentration",
            new[] {
                fsUnit.GrammePerLiter
            }
        );

        public static fsCharacteristic CakeWashOutConcentration = new fsCharacteristic(
            "Cake wash out concentration",
            new[]{
                fsUnit.GrammePerLiter,
                fsUnit.KiloGrammePerCubicMeter
            }
        );

        public static fsCharacteristic Density = new fsCharacteristic(
            "Density",
            new[] {
                fsUnit.KiloGrammePerCubicMeter,
                fsUnit.GrammePerCubicSantiMeter
            }
        );

        public static fsCharacteristic Viscosity = new fsCharacteristic(
            "Viscosity",
            new[] {
                fsUnit.MilliPascalSecond,
                fsUnit.PascalSecond
            }
        );

        public static fsCharacteristic SurfaceTension = new fsCharacteristic(
            "Surface tension",
            new[] {
                fsUnit.MilliNewtonPerMeter,
                fsUnit.NewtonPerMeter
            }
        );

        public static fsCharacteristic Mass = new fsCharacteristic(
            "Mass",
            new[] {
                fsUnit.Gramme,
                fsUnit.KiloGramme,
                fsUnit.Ton
            }
        );

        public static fsCharacteristic SpecificMass = new fsCharacteristic(
            "Specific Mass",
            new[] {
                fsUnit.KiloGrammePerSquaredMeter
            }
        );

        public static fsCharacteristic Volume = new fsCharacteristic(
            "Volume",
            new[] {
                fsUnit.MilliLiter,
                fsUnit.Liter,
                fsUnit.CubicMeter
            }
        );

        public static fsCharacteristic SpecificVolume = new fsCharacteristic(
            "Specific Volume",
            new[] {
                fsUnit.CubicMeterPerSquaredMeter,
                fsUnit.LiterPerSquaredMeter
            }
        );

        public static fsCharacteristic MassFlowrate  = new fsCharacteristic(
            "Mass flowrate",
            new[] {
                fsUnit.KiloGrammePerSec,
                fsUnit.KiloGrammePerMin,
                fsUnit.KiloGrammePerHour,
                fsUnit.TonPerHour
            }
        );

        public static fsCharacteristic VolumeFlowrate  = new fsCharacteristic(
            "Volume flowrate",
            new[] {
                fsUnit.CubicMeterPerSecond,
                fsUnit.CubicMeterPerMinute,
                fsUnit.CubicMeterPerHour,
                fsUnit.LiterPerSecond,
                fsUnit.LiterPerMinute,
                fsUnit.LiterPerHour
            }
        );

        public static fsCharacteristic SpecificMassFlowrate = new fsCharacteristic(
            "Specific Mass flowrate",
            new[] {
                fsUnit.KiloGrammePerSquaredMeterPerSec,
                fsUnit.KiloGrammePerSquaredMeterPerMin,
                fsUnit.KiloGrammePerSquaredMeterPerHour,
            }
        );

        public static fsCharacteristic SpecificVolumeFlowrate = new fsCharacteristic(
            "Specific Volume flowrate",
            new[] {
                fsUnit.LiterPerSquaredMeterPerSec,
                fsUnit.LiterPerSquaredMeterPerMin,
                fsUnit.LiterPerSquaredMeterPerHour,
            }
        );

        public static fsCharacteristic CakePermeability = new fsCharacteristic(
            "Cake permeability",
            new[] {
                fsUnit.PerTen13SquareMeter
            }
        );

        public static fsCharacteristic PracticalCakePermeability = new fsCharacteristic(
            "Practical Cake Permeability",
            new[] {
                fsUnit.SquaredSantiMeterPerBarPerMinute,
                fsUnit.SquaredMeterPerPascalPerSecond
            }
        );

        public static fsCharacteristic CakeResistance = new fsCharacteristic(
            "Cake resistance",
            new[] {
                fsUnit.Ten13PerSquareMeter
            }
        );

        public static fsCharacteristic CakeResistanceAlpha = new fsCharacteristic(
            "Cake resistance alpha",
            new[] {
                fsUnit.Ten10MeterPerKiloGramme
            }
        );

        public static fsCharacteristic FilterMediumResistance = new fsCharacteristic(
            "Filter medium resistance",
            new[] {
                fsUnit.Ten10PerMeter
            }
        );

        public static fsCharacteristic FeedDerivative = new fsCharacteristic(
            "Feed Derivative",
            new[] {
                fsUnit.PerMeter
            }
        );

        public static fsCharacteristic ParticleSize = new fsCharacteristic(
            "Particle size",
            new[] {
                fsUnit.Micrometer,
                fsUnit.MilliMeter,
                fsUnit.Meter
            }
        );

        public static fsCharacteristic WashRatioWv = new fsCharacteristic(
            "Wash ratio",
            new[] {
                fsUnit.LiterPerKiloGramme,
                fsUnit.CubicMeterPerKiloGramme
            }
        );

        #endregion
    }
}
