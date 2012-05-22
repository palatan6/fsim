using System;
using System.Collections.Generic;
using fsNumericalMethods;
using Value;

namespace fmCalculationLibrary.Equations
{
    public class fsMathEquations
    {
        private class fsFunctionPowerSum : fsFunction
        {
            private readonly fsValue m_freeCoeff;
            private readonly fsValue[,] m_coeffsAndPowers;

            public fsFunctionPowerSum(fsValue freeCoeff, fsValue[,] coeffsAndPowers)
            {
                m_freeCoeff = freeCoeff;
                m_coeffsAndPowers = coeffsAndPowers;
            }

            public override fsValue Eval(fsValue x)
            {
                fsValue result = m_freeCoeff;
                for (int i = 0; i < m_coeffsAndPowers.GetLength(0); ++i)
                {
                    result += m_coeffsAndPowers[i, 0]*fsValue.Pow(x, m_coeffsAndPowers[i, 1]);
                }
                return result;
            }
        }

        private class fsFunctionC1Xp1C2Xp2C3 : fsFunction
        {
            private readonly fsValue m_c1;
            private readonly fsValue m_p1;
            private readonly fsValue m_c2;
            private readonly fsValue m_p2;
            private readonly fsValue m_c3;

            public fsFunctionC1Xp1C2Xp2C3(fsValue c1, fsValue p1, fsValue c2, fsValue p2, fsValue c3)
            {
                m_c1 = c1;
                m_p1 = p1;
                m_c2 = c2;
                m_p2 = p2;
                m_c3 = c3;
            }

            public override fsValue Eval(fsValue x)
            {
                return m_c1 * fsValue.Pow(x, m_p1) + m_c2 * fsValue.Pow(x, m_p2) + m_c3;
            }
        }

        private class fsFunctionC1Xp1C2Xp2C3P3C4 : fsFunction
        {
            private readonly fsValue m_c1;
            private readonly fsValue m_p1;
            private readonly fsValue m_c2;
            private readonly fsValue m_p2;
            private readonly fsValue m_c3;
            private readonly fsValue m_p3;
            private readonly fsValue m_c4;

            public fsFunctionC1Xp1C2Xp2C3P3C4(fsValue c1, fsValue p1, fsValue c2, fsValue p2, fsValue c3, fsValue p3, fsValue c4)
            {
                m_c1 = c1;
                m_p1 = p1;
                m_c2 = c2;
                m_p2 = p2;
                m_c3 = c3;
                m_p3 = p3;
                m_c4 = c4;
            }

            public override fsValue Eval(fsValue x)
            {
                return m_c1 * fsValue.Pow(x, m_p1) + m_c2 * fsValue.Pow(x, m_p2) + m_c3 * fsValue.Pow(x, m_p3) + m_c4;
            }
        }

        private static readonly fsValue Infinity = new fsValue(1e20);
        private static readonly fsValue Zero = new fsValue(0);

        static public List<fsValue> SolveC1Xp1C2Xp2C3(fsValue c1, fsValue p1, fsValue c2, fsValue p2, fsValue c3, fsValue upperBoundForBisection)
        {
            //return SolvePowerSumEquation(c3, new fsValue[,] {{c1, p1}, {c2, p2}});
            var result = new List<fsValue>();
            if (!c1.Defined || !p1.Defined || !c2.Defined || !p2.Defined || !c3.Defined)
            {
                result.Add(new fsValue());
                return result;
            }

            if (c1 == Zero)
            {
                result.Add(SolveC1Xp1C2(c2, p2, c3));
                return result;
            }
            if (c2 == Zero)
            {
                result.Add(SolveC1Xp1C2(c1, p1, c3));
                return result;
            }
            if (c3 == Zero)
            {
                result.Add(SolveC1Xp1C2Xp2(c1, p1, c2, p2));
                return result;
            }
            if (p1 == Zero)
            {
                result.Add(SolveC1Xp1C2(c2, p2, c1 + c3));
                return result;
            }
            if (p2 == Zero)
            {
                result.Add(SolveC1Xp1C2(c1, p1, c2 + c3));
                return result;
            }
            if (p1 == p2)
            {
                result.Add(SolveC1Xp1C2(c1 + c2, p1, c3));
                return result;
            }

            fsValue x0 = SolveC1Xp1C2Xp2(c1 * p1, p1 - 1, c2 * p2, p2 - 1);

            const int iterations = 100;  // precision is 1e20 / 2^100 ~~ 1e-11

            if (!x0.Defined)
            {
                result.Add(fsBisectionMethod.FindRoot(new fsFunctionC1Xp1C2Xp2C3(c1, p1, c2, p2, c3), Zero, upperBoundForBisection, iterations));
                return result;
            }

            result.Add(fsBisectionMethod.FindRoot(new fsFunctionC1Xp1C2Xp2C3(c1, p1, c2, p2, c3), Zero, x0, iterations));
            result.Add(fsBisectionMethod.FindRoot(new fsFunctionC1Xp1C2Xp2C3(c1, p1, c2, p2, c3), x0, upperBoundForBisection, iterations));

            if (!result[1].Defined)
            {
                result.RemoveAt(1);
            }
            else if (!result[0].Defined)
            {
                result.RemoveAt(0);
            }

            result.Sort();
            return result;
        }

        static public fsValue SolveC1Xp1C2(fsValue c1, fsValue p1, fsValue c2)
        {
            if (!c1.Defined || !p1.Defined || !c2.Defined)
                return new fsValue();

            if (c2 == Zero)
                return SolveC1Xp1(c1, p1);
            if (c1 == Zero)
                return new fsValue();
            return fsValue.Pow(-c2 / c1, 1 / p1);
        }

        static public fsValue SolveC1Xp1C2Xp2(fsValue c1, fsValue p1, fsValue c2, fsValue p2)
        {
            if (!c1.Defined || !p1.Defined || !c2.Defined || !p2.Defined)
                return new fsValue();

            if (c1 == Zero)
                return SolveC1Xp1(c2, p2);
            if (c2 == Zero)
                return SolveC1Xp1(c1, p1);
            if (p1 == Zero)
                return SolveC1Xp1C2(c2, p2, c1);
            if (p2 == Zero)
                return SolveC1Xp1C2(c1, p1, c2);
            if (p1 == p2)
                return SolveC1Xp1(c1 + c2, p1);

            return fsValue.Pow(-c1 / c2, 1 / (p2 - p1));
        }

        static public fsValue SolveC1Xp1(fsValue c1, fsValue p1)
        {
            if (!c1.Defined || !p1.Defined)
                return new fsValue();

            if (c1 == Zero)
                return Zero;

            if (p1 < Zero)
                return new fsValue();

            return Zero;
        }

        internal static List<fsValue> SolveC1Xp1C2Xp2C3Xp3C4(fsValue c1, fsValue p1, fsValue c2, fsValue p2, fsValue c3, fsValue p3, fsValue c4)
        {
            List<fsValue> breakPoints = SolveC1Xp1C2Xp2C3(c1*p1, p1 - p3, c2*p2, p2 - p3, c3*p3, Infinity);
            while (breakPoints.Count > 0 && breakPoints[0] == Zero)
                breakPoints.RemoveAt(0);
            breakPoints.Insert(0, Zero);
            breakPoints.Add(Infinity);

            const int interations = 100;  // precision is 1e20 / 2^100 ~~ 1e-11

            var result = new List<fsValue>();

            for (int i = 1; i < breakPoints.Count; ++i)
            {
                fsValue localRoot = fsBisectionMethod.FindRoot(
                    new fsFunctionC1Xp1C2Xp2C3P3C4(c1, p1, c2, p2, c3, p3, c4),
                    breakPoints[i - 1], breakPoints[i], interations);
                if (localRoot.Defined)
                    result.Add(localRoot);
            }
                
            result.Sort();
            return result;
        }

        static public List<fsValue> SolvePowerSumEquation(fsValue freeCoeff, fsValue[,] coeffsAndPowers)
        {
            var compressing = new SortedList<fsValue, fsValue>();
            for (int i = 0; i < coeffsAndPowers.GetLength(0); ++i)
            {
                fsValue c = coeffsAndPowers[i, 0];
                fsValue p = coeffsAndPowers[i, 1];
                if (!compressing.ContainsKey(p))
                {
                    compressing.Add(p, Zero);
                }

                compressing[p] += c;
            }
            
            var compressing2 = new SortedList<fsValue, fsValue>();
            foreach (KeyValuePair<fsValue, fsValue> cp in compressing)
            {
                if (cp.Value != Zero)
                    compressing2.Add(cp.Key, cp.Value);
            }
            compressing = compressing2;

            {
                var compressedCoeffsAndPowers = new fsValue[compressing.Count - (compressing.ContainsKey(Zero) ? 1 : 0), 2];
                int i = 0;
                foreach (KeyValuePair<fsValue, fsValue> cp in compressing)
                {
                    if (cp.Key == Zero)
                    {
                        freeCoeff += cp.Value;
                    }
                    else
                    {
                        compressedCoeffsAndPowers[i, 0] = cp.Value;
                        compressedCoeffsAndPowers[i, 1] = cp.Key;
                        ++i;
                    }
                }

                coeffsAndPowers = compressedCoeffsAndPowers;
            }

            var result = new List<fsValue>();

            for (int i = 0; i < coeffsAndPowers.GetLength(0); ++i)
            {
                if (coeffsAndPowers[i, 1] == new fsValue(0))
                {
                    throw new Exception(
                        "SolvePowerSumEquation: Powers must be non-zero values. Move such elemets to freeCoeff");
                }
            }

            for (int i = 1; i < coeffsAndPowers.GetLength(0); ++i)
            {
                if (coeffsAndPowers[i - 1, 1] >= coeffsAndPowers[i, 1])
                {
                    throw new Exception("SolvePowerSumEquation: Powers must be sorted in increasing order");
                }
            }

            if (coeffsAndPowers.GetLength(0) == 0)
            {
                return result;
            }
            if (coeffsAndPowers.GetLength(0) == 1)
            {
                fsValue resVal = fsValue.Pow(-freeCoeff/coeffsAndPowers[0, 0], 1/coeffsAndPowers[0, 1]);
                if (resVal.Defined)
                {
                    result.Add(resVal);
                }
                return result;
            }
            if (freeCoeff == Zero)
                result.Add(Zero);

            var newFreeCoeff = new fsValue(coeffsAndPowers[0, 0] * coeffsAndPowers[0, 1]);
            var newCoeffsAndPowers = new fsValue[coeffsAndPowers.GetLength(0) - 1, 2];
            for (int i = 0; i < newCoeffsAndPowers.GetLength(0); ++i)
            {
                newCoeffsAndPowers[i, 0] = coeffsAndPowers[i + 1, 0] * coeffsAndPowers[i + 1, 1];
                newCoeffsAndPowers[i, 1] = coeffsAndPowers[i + 1, 1] - coeffsAndPowers[0, 1];
            }

            var changingPoints = new List<fsValue> {Zero};
            changingPoints.AddRange(SolvePowerSumEquation(newFreeCoeff, newCoeffsAndPowers));
            changingPoints.Add(Infinity);

            const int iterations = 100;  // precision is 1e20 / 2^100 ~~ 1e-11

            for (int i = 1; i < changingPoints.Count; ++i)
            {
                fsValue localSolution = fsBisectionMethod.FindRoot(new fsFunctionPowerSum(freeCoeff, coeffsAndPowers),
                                                                   changingPoints[i - 1],
                                                                   changingPoints[i], iterations);
                if (localSolution.Defined)
                {
                    result.Add(localSolution);
                }
            }

            var eps = new fsValue(1e-12);
            result.Sort();
            for (int i = result.Count - 1; i > 0; --i)
            {
                if (result[i] <= (1 + eps) * result[i - 1]
                    || result[i] - result[i - 1] <= eps)
                    result.RemoveAt(i);
            }

            return result;
        }
    }
}