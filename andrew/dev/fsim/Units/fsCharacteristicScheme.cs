﻿using System.Collections.Generic;

namespace Units
{
    public class fsCharacteristicScheme
    {
        private fsCharacteristicScheme(string name, params KeyValuePair<fsCharacteristic, fsUnit>[] characteristicToUnit)
        {
            CharacteristicToUnit = new Dictionary<fsCharacteristic, fsUnit>();
            Name = name;
            foreach (var pair in characteristicToUnit)
            {
                CharacteristicToUnit.Add(pair.Key, pair.Value);
            }
        }

        public Dictionary<fsCharacteristic, fsUnit> CharacteristicToUnit { get; private set; }

        public string Name { get; private set; }

        public void SetCharacteristics(Dictionary<fsCharacteristic, fsUnit> characteristics)
        {
            CharacteristicToUnit = characteristics;
        }

        #region Schemas

        public static readonly fsCharacteristicScheme LaboratoryScale =
            new fsCharacteristicScheme("Laboratory",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareSantiMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.Gramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.MilliLiter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerHour),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.LiterPerHour),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificMassFlowrate, fsUnit.KiloGrammePerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificVolumeFlowrate, fsUnit.LiterPerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Time, fsUnit.Second),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MachineGeometryLength, fsUnit.MilliMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Pressure,fsUnit.Bar), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Viscosity, fsUnit.MilliPascalSecond), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Density,fsUnit.KiloGrammePerCubicMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SurfaceTension,fsUnit.MilliNewtonPerMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.CakeWashOutContent, fsUnit.GrammeOverKilogram)
                });

        public static readonly fsCharacteristicScheme IndustrialScale =
            new fsCharacteristicScheme("Industrial",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.Liter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.LiterPerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificMassFlowrate, fsUnit.KiloGrammePerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificVolumeFlowrate, fsUnit.LiterPerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Time, fsUnit.Second),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MachineGeometryLength, fsUnit.MilliMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Pressure,fsUnit.Bar), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Viscosity, fsUnit.MilliPascalSecond), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Density,fsUnit.KiloGrammePerCubicMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SurfaceTension,fsUnit.MilliNewtonPerMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.CakeWashOutContent, fsUnit.GrammeOverKilogram)
                });

        public static fsCharacteristicScheme PilotScale =
            new fsCharacteristicScheme("Pilot",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.Liter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.LiterPerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificMassFlowrate, fsUnit.KiloGrammePerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificVolumeFlowrate, fsUnit.LiterPerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Time, fsUnit.Second),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MachineGeometryLength, fsUnit.MilliMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Pressure,fsUnit.Bar), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Viscosity, fsUnit.MilliPascalSecond), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Density,fsUnit.KiloGrammePerCubicMeter), 
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SurfaceTension,fsUnit.MilliNewtonPerMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.CakeWashOutContent, fsUnit.GrammeOverKilogram)
                });

        #endregion
    }
}
