using System;
using Value;

namespace fsNumericalMethods
{
    public class fsBisectionMethod
    {
        public static bool FindRootRange(fsFunction function, fsValue beginArg, fsValue endArg, int iterationsCount, out fsValue beginRes, out fsValue endRes, fsValue eps)
        {
            beginRes = new fsValue();
            endRes = new fsValue();

            if (beginArg.Defined == false || endArg.Defined == false)
            {
                return false;
            }

            fsValue len = endArg - beginArg;
            if (len.Value < 0)
            {
                eps = -eps;
            }
        
            fsValue beginValue = function.Eval(beginArg + eps);
            fsValue endValue = function.Eval(endArg - eps);
            if (beginValue.Defined == false || endValue.Defined == false)
            {
                return false;
            }
            if (beginValue == endValue)
            {
                if (beginValue.Value == 0)
                {
                    if (function.Eval(beginArg).Value == 0)
                    {
                        beginRes = endRes = beginArg;
                        return true;
                    }
                    beginRes = beginArg + eps;
                    endRes = beginArg + eps;
                    return true;
                }
                if (function.Eval(endArg).Value == 0)
                {
                    beginRes = endRes = endArg;
                    return true;
                }

                return false;
            }

            fsValue beginSign = fsValue.Sign(beginValue, eps);
            fsValue endSign = fsValue.Sign(endValue, eps);
            if ((beginSign * endSign).Value > 0)
                return false;

            fsValue left = beginArg;
            fsValue right = endArg;
            for (int i = 0; i < iterationsCount; ++i)
            {
                fsValue middle = 0.5 * (left + right);
                if (fsValue.Abs(middle - left) < eps || fsValue.Abs(middle - right) < eps) break;
                fsValue value = function.Eval(middle);

                if (!value.Defined)
                    throw new Exception("The function given to BisectionMethod is not defined at the point " 
                                        + middle.Value + " inside the given interval <" + beginArg.Value + ", " + endArg.Value + ">"); 
                
                fsValue midSign = fsValue.Sign(value, eps);
                if (midSign.Value == 0 || midSign == endSign)
                    right = middle;
                else
                    left = middle;
            }

            beginRes = left;
            endRes = right;
            return true;
        }

        public static fsValue FindRoot(fsFunction function, fsValue beginArg, fsValue endArg, int iterationsCount, fsValue eps)
        {
            fsValue beginRes;
            fsValue endRes;
            if (FindRootRange(function, beginArg, endArg, iterationsCount, out beginRes, out endRes, eps))
            {
                return 0.5 * (beginRes + endRes);
            }
            return new fsValue();
        }
        
        public static fsValue FindBreakInUnimodalFunction(fsFunction function, fsValue beginArg, fsValue endArg, int iterationsCount)
        {
            if (beginArg.Defined == false || endArg.Defined == false)
                return new fsValue();
            
            fsValue len = endArg - beginArg;
            var eps = new fsValue(1e-8);
            if (len.Value < 0)
            {
                eps = -eps;
            }

            fsValue beginValue = function.Eval(beginArg);
            fsValue endValue = function.Eval(endArg);
            if (fsValue.Sign(beginValue, eps) * fsValue.Sign(endValue, eps) == new fsValue(-1))
                throw new Exception("Search limits represent different signs of values already");            

            beginValue = function.Eval(beginArg + eps);
            endValue = function.Eval(endArg - eps);
            if (fsValue.Sign(beginValue, eps) * fsValue.Sign(endValue, eps) == new fsValue(-1))
                throw new Exception("Search limits represent different signs of values already");            

            if (beginValue.Defined == false || endValue.Defined == false)
                return new fsValue();

            fsValue initialSign = fsValue.Sign(beginValue, eps);
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
                    throw new Exception("Function given to FindBreakInUnimodalFunction not defind in point " + mid1.Value);
                if (!val2.Defined)
                    throw new Exception("Function given to FindBreakInUnimodalFunction not defind in point " + mid2.Value);

                if (fsValue.Sign(val1, eps) != initialSign)
                    return mid1;

                if (fsValue.Sign(val2, eps) != initialSign)
                    return mid2;


                if (fsValue.Sign(val1 - val2, eps) == initialSign)
                    left = middle;
                else
                    right = middle;
            }

            return new fsValue();
        }
    }
}