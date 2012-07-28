using System;
using System.Collections.Generic;
using fsNumericalMethods;
using Value;

namespace fsNumericalMethods
{ 
    public class fsSpecialFunctions
    {
        #region LlnMinus(x,y)

        /*
         *  The transcendental equation
         *
         *        ln(z) = -y * z + x         (*lln-*)
         *        
         *  has a unique solution for every pair (x,y) where y > 0  and x is an arbitrary real number.
         *  
         *  We define LlnMinus(x,y) = z  where z is a solution of (*lln-*).
         */

        private class LlnMinusCalculationFunction : fsFunction
        {
            private readonly fsValue m_x;
            private readonly fsValue m_y;

            public LlnMinusCalculationFunction(fsValue x, fsValue y)
            {
                m_x = x;
                m_y = y;
            }

            public override fsValue Eval(fsValue z)
            {
                return fsValue.Log(z) + m_y * z - m_x;
            }
        }

        static public fsValue LlnMinus(fsValue x, fsValue y)
        {
            if (y < fsValue.Zero)
            {
                return new fsValue();
            }

            if (x == y)
            {
                return fsValue.One;
            }

            if (y == fsValue.Zero)
            {
                return fsValue.Exp(x);
            }

            fsValue lowerBound;
            fsValue upperBound;
            var f = new LlnMinusCalculationFunction(x, y);
            
            if (x <= fsValue.Zero)
            {
                lowerBound = fsValue.Exp(x - y);
                upperBound = fsValue.One;
                return fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 40);
            }

            fsValue frac = x / y;
            if (frac <= fsValue.One)
            {
                lowerBound = frac;
                upperBound = fsValue.One;
                return fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 40);
            }

            lowerBound = fsValue.One;
            upperBound = fsValue.Exp(x);
            if (upperBound <= frac)
            {
                return fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 60);
            }

            upperBound = frac;
            return fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 60);
        }

        #endregion
       
        #region LlnPlus(x,y)

        /*
         *  The transcendental equation
         *
         *        ln(z) = y * z - x         (*lln+*)
         *        
         *  has exactly 2 solution z1, z2 (z1 < z2) for every pair (x,y), x >= 0, y > 0,
         *  satisfying the following condition:
         *  
         *        1 - x + ln(y) <= 0        (*cond*) 
         *        
         *  if x <> y; and a unique solution z = 1 if x = y.
         *  
         *  If (*cond*) doesn't hold then there is no solution of (*lln+*).
         *  
         *  And at that the inequality holds:
         *  
         *       z1 < y^(-1) < z2           (*sols*)           
         *  
         *  We define LlnPlus(x,y) = z2  where z2 is a solution of (*lln+*) satisfying (*sols*) if x <> y 
         *  and       LlnPlus(x,x) = 1   otherwise.
         */

        private class LlnPlusCalculationFunction : fsFunction
        {
            private readonly fsValue m_x;
            private readonly fsValue m_y;

            public LlnPlusCalculationFunction(fsValue x, fsValue y)
            {
                m_x = x;
                m_y = y;
            }

            public override fsValue Eval(fsValue z)
            {
                return fsValue.Log(z) - m_y * z + m_x;
            }
        }

        static private fsValue LlnPlusIteration(fsValue x, fsValue y, fsValue frac, int m)
        { 
            int n = (int)Math.Ceiling( ( m * Math.Log(10) + 
                                         (fsValue.Log(frac * fsValue.Log(frac) / (x - 1))).Value
                                       ) / (fsValue.Log(x)).Value
                         );
            for (int i = 1; i <= n; i++)
            {
                result = frac + fsValue.Log(result) / y;
            }
            return result;
        }

        static public fsValue LlnPlus(fsValue x, fsValue y)
        {
            if (x < fsValue.Zero || y <= fsValue.Zero)
            {
                return new fsValue();
            }
            if (1 - x + fsValue.Log(y) > fsValue.Zero)
            {
                return new fsValue();
            }
            if (x == y)
            {
                return fsValue.One;
            }

            fsValue kappa = 1 / (fsValue.Exp(fsValue.One) - 1);
            fsValue frac;
            if (x >= new fsValue(5))
            {
                frac = x / y;
                if (frac > fsValue.One)
                {
                    return LlnPlusIteration(x, y, frac, 12);
                }
                else
                {
                    fsValue lowerBound = 1 / y;
                    fsValue upperBound = (1 + kappa) * (x - fsValue.Log(y)) / y;
                    var f = new LlnPlusCalculationFunction(x, y);
                    return fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 40);
                }
            }
            else
            {
                fsValue a = fsValue.Exp(x - 5);
                frac = 5 * a / y;
                if (frac > fsValue.One)
                {
                    return LlnPlusIteration(new fsValue(5), y / a, frac, 14) / a;
                }
                else
                {
                    fsValue lowerBound = a / y;
                    fsValue upperBound = a * (1 + kappa) * (5 - fsValue.Log(y / a)) / y;
                    var f = new LlnPlusCalculationFunction(new fsValue(5), y /a);
                    return (fsBisectionMethod.FindRoot(f, lowerBound, upperBound, 50) / a);
                }
            }
        }

        #endregion
    }
}