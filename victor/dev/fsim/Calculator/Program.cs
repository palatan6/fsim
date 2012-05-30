using System;
using System.Windows.Forms;
using Units;

namespace Calculator
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            fsCharacteristic.Time.CurrentUnit = fsUnit.Second;
            fsCharacteristic.CakeHeight.CurrentUnit = fsUnit.MilliMeter;
            fsCharacteristic.MachineGeometryLength.CurrentUnit = fsUnit.Meter;
            fsCharacteristic.Area.CurrentUnit = fsUnit.SquareMeter;
            fsCharacteristic.Mass.CurrentUnit = fsUnit.KiloGramme;
            fsCharacteristic.Volume.CurrentUnit = fsUnit.Liter;
            fsCharacteristic.MassFlowrate.CurrentUnit = fsUnit.KiloGrammePerMin;
            fsCharacteristic.VolumeFlowrate.CurrentUnit = fsUnit.LiterPerMinute;
            fsCharacteristic.Frequency.CurrentUnit = fsUnit.PerMinute;
            fsCharacteristic.SpecificMassFlowrate.CurrentUnit = fsUnit.KiloGrammePerSquaredMeterPerMin;
            fsCharacteristic.SpecificVolumeFlowrate.CurrentUnit = fsUnit.LiterPerSquaredMeterPerMin;

            Application.Run(new fsMainWindow());
        }
    }
}