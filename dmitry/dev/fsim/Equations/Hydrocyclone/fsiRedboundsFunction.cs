using System;
using Parameters;
using Value;
using CalculatorModules.Machine_Ranges;
using ParametersIdentifiers.Ranges;

namespace Equations.Hydrocyclone
{
    /*
     * 0.707106781186547524400845 ~= 1 / 2^(1/2)
     */
    public class fsiRedboundsFunction
    {
       public static double[] Reduced(
            double leftMin,
            double rightMax,
            double xg,
            double sigmas)
        {            
            fsRange machr = fsMachineRanges.DefaultMachineRanges.Ranges[fsParameterIdentifier.ReducedCutSize].Range;
            return new double[]  { 
                                   Math.Max(leftMin, 
                                   0.707106781186547524400845 * Math.Log(xg / machr.To.Value) / Math.Log(sigmas)),
                                   Math.Min(rightMax,
                                   0.707106781186547524400845 * Math.Log(xg / machr.From.Value) / Math.Log(sigmas))
                                 };
        }

        public static double[] i(
            double leftMin,
            double rightMax,
            double xg,
            double sigmag
            )
        {
            fsRange machr = fsMachineRanges.DefaultMachineRanges.Ranges[fsParameterIdentifier.ReducedCutSize].Range;
            return new double[]  { 
                                   Math.Max(leftMin, 
                                   0.707106781186547524400845 * Math.Log(machr.From.Value / xg) / Math.Log(sigmag)),
                                   Math.Min(rightMax,
                                   0.707106781186547524400845 * Math.Log(machr.To.Value / xg) / Math.Log(sigmag))
                                 };
        }
    }
}