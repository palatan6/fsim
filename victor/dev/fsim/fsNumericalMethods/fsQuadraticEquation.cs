using System;
using Value;

namespace fsNumericalMethods
{
    public class fsQuadraticEquation
    {
        public static bool Solve(fsValue a, fsValue b, fsValue c, out fsValue x1, out fsValue x2)
        {
            x1 = new fsValue();
            x2 = new fsValue();
            if (!a.Defined || !b.Defined || !c.Defined)
            {
                return false;
            }

            if (a == fsValue.Zero)
            {
                if (b == fsValue.Zero)
                {
                    if (c == fsValue.Zero)
                    {
                        x1 = x2 = fsValue.Zero;
                        return true;
                    }
                    return false;
                }
                x1 = x2 = -c / b;
                return true;
            }
            fsValue b2 = b * b;
            fsValue ac4 = 4 * a * c;
            fsValue d2 = b2 - ac4;
            var eps = fsValue.Max(b2, fsValue.Abs(ac4)) * 1e-9;
            if (d2 < -eps)
            {
                return false;
            }
            fsValue d = d2 < fsValue.Zero ? fsValue.Zero : fsValue.Sqrt(d2);
            x1 = (-b - d) / (2 * a);
            x2 = (-b + d) / (2 * a);
            return true;
        }
    }
}
