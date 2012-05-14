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
            fsCharacteristic.Area.CurrentUnit = fsUnit.SquareSantiMeter;
            fsCharacteristic.Mass.CurrentUnit = fsUnit.Gramme;
            fsCharacteristic.Volume.CurrentUnit = fsUnit.MilliLiter;
            fsCharacteristic.MassFlowrate.CurrentUnit = fsUnit.TonPerHour;
            fsCharacteristic.VolumeFlowrate.CurrentUnit = fsUnit.CubicMeterPerHour;

            Application.Run(new fsMainWindow());
        }
    }
}