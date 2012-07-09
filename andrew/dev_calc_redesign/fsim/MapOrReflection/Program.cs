using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parameters;
using Value;

namespace MapOrReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryTest();
            ReflectionTest();
        }

        const int ITERATIONS = 40000;

        private static void DictionaryTest()
        {
            Dictionary<fsParameterIdentifier, fsCalculatorParameter> m_variables = new Dictionary<fsParameterIdentifier, fsCalculatorParameter>();

            var list = new[] {
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistanceAlpha0,
                fsParameterIdentifier.CakeCompressibility,
                fsParameterIdentifier.PressureDifference,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha
            };

            foreach (var x in list)
            {
                m_variables[x] = new fsCalculatorVariable(x);
            }

            double start = DateTime.Now.TimeOfDay.TotalMilliseconds;

            var c = new MapClass();
            for (int it = 0; it < ITERATIONS; ++it)
            {
                c.CopyValuesToStorage(m_variables);
                m_variables[list[it % list.Length]].Value = new fsValue((it + 1.0) / 3);
                c.ReadDataFromStorage(m_variables);
            }

            double end = DateTime.Now.TimeOfDay.TotalMilliseconds;

            System.Console.WriteLine("DictionaryTest running time: " + (end - start));
            System.Console.WriteLine();
            foreach (var p in m_variables)
            {
                System.Console.WriteLine(p.Key.Name + ": " + p.Value);
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
        }

        private static void ReflectionTest()
        {
            Dictionary<fsParameterIdentifier, fsCalculatorParameter> m_variables = new Dictionary<fsParameterIdentifier, fsCalculatorParameter>();

            var list = new[] {
                fsParameterIdentifier.SolidsDensity,
                fsParameterIdentifier.CakePorosity,
                fsParameterIdentifier.CakePermeability0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeResistance0,
                fsParameterIdentifier.CakeCompressibility,
                fsParameterIdentifier.PressureDifference,
                fsParameterIdentifier.CakePermeability,
                fsParameterIdentifier.CakeResistance,
                fsParameterIdentifier.CakeResistanceAlpha
            };

            foreach (var x in list)
            {
                m_variables[x] = new fsCalculatorVariable(x);
            }

            double start = DateTime.Now.TimeOfDay.TotalMilliseconds;

            var c = new ReflectionClass();
            for (int it = 0; it < ITERATIONS; ++it)
            {
                c.CopyValuesToStorage(m_variables);
                m_variables[list[it % list.Length]].Value = new fsValue((it + 1.0) / 3);
                c.ReadDataFromStorage(m_variables);
            }

            double end = DateTime.Now.TimeOfDay.TotalMilliseconds;

            System.Console.WriteLine("ReflectionTest running time: " + (end - start));
            System.Console.WriteLine();
            foreach (var p in m_variables)
            {
                System.Console.WriteLine(p.Key.Name + ": " + p.Value);
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
        }
    }
}
