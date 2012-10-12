using System;
using Value;

namespace fsNumericalMethods
{
    public class fsTernaryMethod
    {
        public static bool FindMinimum(fsFunction function, fsValue beginArg, fsValue endArg, int iterationsCount, out fsValue beginRes, out fsValue endRes)
        {
            beginRes = new fsValue();
            endRes = new fsValue();

            if (beginArg.Defined == false || endArg.Defined == false)
            {
                return false;
            }

            fsValue len = endArg - beginArg;
            if (len <= new fsValue(0))
                throw new Exception("Wrong interval for ternary search");

            var eps = new fsValue(1e-8);
            
            fsValue left = beginArg;
            fsValue right = endArg;
            for (int i = 0; i < iterationsCount; ++i)
            {
                fsValue middle = 0.5 * (left + right);
                fsValue mid1 = middle - eps * (right - left);
                fsValue mid2 = middle + eps * (right - left);
                fsValue val1 = function.Eval(mid1);
                fsValue val2 = function.Eval(mid2);

                if (!val1.Defined)
                    throw new Exception("Function given to TernaryMethod is not defined at the point " + mid1.Value);
                if (!val2.Defined)
                    throw new Exception("Function given to TernaryMethod is not defined at the point " + mid2.Value);

                if (val1 > val2)
                    left = mid1;
                else
                    right = mid2;
            }

            beginRes = left;
            endRes = right;
            return true;
        }

        public static fsValue FindMinimum(fsFunction function, fsValue beginArg, fsValue endArg, int iterationsCount)
        {
            fsValue beginRes;
            fsValue endRes;
            if (FindMinimum(function, beginArg, endArg, iterationsCount, out beginRes, out endRes))
            {
                return 0.5 * (beginRes + endRes);
            }
            return new fsValue();
        }
    }
}
