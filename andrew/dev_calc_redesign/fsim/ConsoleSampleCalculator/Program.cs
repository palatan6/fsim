using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Value;
using Parameters;
using System.Threading;

namespace ConsoleSampleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //CubeSimulationSample();
            GeomericalProgressionSimulationSample();
        }

        class fsSimTracker
        {
            private fsSimulation m_simulation;
            public fsSimTracker (fsSimulation simulation)
            {
                m_simulation = simulation;
            }
            public void Out()
            {
                while (true)
                {
                    Console.WriteLine(m_simulation.ToString());
                    Thread.Sleep(50);
                }
            }
        }

        private static void GeomericalProgressionSimulationSample()
        {
            fsSimulation gpSim = new fsSimulation(
                fsParameterIdentifier.a1,
                fsParameterIdentifier.a2,
                fsParameterIdentifier.a3,
                fsParameterIdentifier.a4,
                fsParameterIdentifier.a5);
            fsGProgressionInitStep initStep = new fsGProgressionInitStep();
            gpSim.Steps.Add(initStep);

            fsSimTracker simTracker = new fsSimTracker(gpSim);
            Thread trackThread = new Thread(simTracker.Out);
            trackThread.Start();

            DateTime startTime = DateTime.Now;
            for (int it = 0; it < 2; ++it)
            {
                // first call
                {
                    gpSim.StopCalculations();
                    initStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.q, new fsValue(2));
                    initStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.a1, new fsValue(2));
                    gpSim.RunCalculations();
                }
                int j = 0;
                for (int i = 0; i < 10000000; ++i)
                {
                    j += i * i;
                }
                //while (gpSim.IsCalculating()) ;
                //simTracker.Out();

                // second call
                {
                    gpSim.StopCalculations();
                    initStep.SetParameterInputedFlag(fsParameterIdentifier.q, false);
                    initStep.SetParameterInputedFlag(fsParameterIdentifier.a1, false);
                    initStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.a3, new fsValue(10));
                    initStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.a2, new fsValue(20));
                    gpSim.RunCalculations();
                }
                //while (gpSim.IsCalculating()) ;
                //simTracker.Out();
            }
            while (gpSim.IsCalculating()) ;
            {
                int j = 0;
                for (int i = 0; i < 100000000; ++i)
                {
                    j += i * i;
                }
            }
            trackThread.Abort();
            DateTime endTime = DateTime.Now;
            Console.WriteLine((endTime - startTime).TotalSeconds);
        }

        private static void CubeSimulationSample()
        {
            fsSimulation cube = new fsSimulation(
                fsParameterIdentifier.volume,
                fsParameterIdentifier.height,
                fsParameterIdentifier.length,
                fsParameterIdentifier.width);
            fsCubeFormationStep formationStep = new fsCubeFormationStep();
            cube.Steps.Add(formationStep);
            fsCubeHeightFromWidthScalingStep heightDefStep = new fsCubeHeightFromWidthScalingStep();
            cube.Steps.Add(heightDefStep);

            DateTime startTime = DateTime.Now;
            for (int it = 0; it < 1; ++it)
            {
                formationStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.volume, new fsValue(8));
                heightDefStep.SetParameterInputedFlag(fsParameterIdentifier.width, true);
                heightDefStep.SetParameterInputedFlag(fsParameterIdentifier.length, true);
                heightDefStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.coefficient, new fsValue(0.5));

                cube.RunCalculations();
                Console.WriteLine(cube.ToString());

                heightDefStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.coefficient, new fsValue(2));

                cube.RunCalculations();
                Console.WriteLine(cube.ToString());

                formationStep.SetParameterInputedAndAssignValue(fsParameterIdentifier.volume, new fsValue(27));
                cube.RunCalculations();
                Console.WriteLine(cube.ToString());
            }
            DateTime endTime = DateTime.Now;
            Console.WriteLine((endTime - startTime).TotalSeconds);
        }
    }
}
