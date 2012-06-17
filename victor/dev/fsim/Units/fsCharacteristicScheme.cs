using System.Collections.Generic;

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

        #region Schemas

        public static readonly fsCharacteristicScheme InternationalSystemOfUnits =
            new fsCharacteristicScheme("International System of Units",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerSecond),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.CubicMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerSec),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.CubicMeterPerSecond)
                });

        public static readonly fsCharacteristicScheme LaboratoryScale =
            new fsCharacteristicScheme("Laboratory Scale",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareSantiMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.Gramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.MilliLiter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerHour),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.CubicMeterPerSecond)
                });

        public static fsCharacteristicScheme PilotIndustrialScale =
            new fsCharacteristicScheme("Pilot/Industrial Scale",
                new[]
                {
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Frequency, fsUnit.PerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Area, fsUnit.SquareMeter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Mass, fsUnit.KiloGramme),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.Volume, fsUnit.Liter),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.MassFlowrate, fsUnit.KiloGrammePerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.VolumeFlowrate, fsUnit.LiterPerMinute),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificMassFlowrate, fsUnit.KiloGrammePerSquaredMeterPerMin),
                    new KeyValuePair<fsCharacteristic, fsUnit>(fsCharacteristic.SpecificVolumeFlowrate, fsUnit.LiterPerSquaredMeterPerMin)
                });

        #endregion
    }
}
