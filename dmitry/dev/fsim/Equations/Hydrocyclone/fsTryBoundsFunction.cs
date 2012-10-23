using System;
using fsNumericalMethods;
using ErfExpIntBoundsCalculator;

namespace Equations.Hydrocyclone
{
    public class fsTryBoundsFunction
    {
        private static readonly int nCycles = 8;

        public static double[] Second(int n, double eps, double left, double right, double a, double x, fsFunction f)
        {
            double[] bounds = fsErfExpIntBoundsCalculator.getIntervSecondArg(n, eps, left, right, a, x, f);
            if (bounds[0] > 0.0)
                return bounds;
            double step = (right - left) / (double)(2 * nCycles);
            for (int i = 0; i < nCycles; i++)
			{
                left += step;
                right -= step;
                bounds = fsErfExpIntBoundsCalculator.getIntervSecondArg(n, eps, left, right, a, x, f);
                if (bounds[0] > 0.0)
                    return bounds;
			}
            return bounds;
        }

        public static double[] Third(int n, double eps, double left, double right, double a, double b, fsFunction f)
        {
            double[] bounds = fsErfExpIntBoundsCalculator.getIntervThirdArg(n, eps, left, right, a, b, f);
            if (bounds[0] > 0.0)
                return bounds;
            double step = (right - left) / (double)(2 * nCycles);
            for (int i = 0; i < nCycles; i++)
            {
                left += step;
                right -= step;
                bounds = fsErfExpIntBoundsCalculator.getIntervSecondArg(n, eps, left, right, a, b, f);
                if (bounds[0] > 0.0)
                    return bounds;
            }
            return bounds;
        }
    }
}