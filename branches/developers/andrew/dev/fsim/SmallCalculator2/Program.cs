using System;
using System.IO;
using System.Windows.Forms;

namespace SmallCalculator2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 1)
            {
                Application.Run(new fsSmallCalculatorMainWindow(args[1]));
            }
            else
            {
                Application.Run(new fsSmallCalculatorMainWindow());
            }

        }
    }
}
