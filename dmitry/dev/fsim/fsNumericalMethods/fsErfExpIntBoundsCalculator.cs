using System;
using Value;
using fsNumericalMethods;

namespace ErfExpIntBoundsCalculator
{
    /*
     * This class was automatically generated from the Maple module using CodeGeneration[CSharp] procedure
     * and then slightly tuned.
     * What's happening here in details is a terrible mystery! :-)
     * See the attached Maple doc (if I'll have a time and a possibility to attach it!). 
     * ---Tyshkevich Dmitry---.
     */
    public class fsErfExpIntBoundsCalculator
    {
        /*
         * Determines in a fast way (for the Bisection Method) a shorter interval inside [left, right]  
         * in which the solution of the equation
         *                ErfcExpInt(a, b, x) = f(x)
         * lies.
         */
        public static double[] getIntervThirdArg(int n, double eps, double left, double right, double a, double b, fsFunction f)
        {
            if (0.0e0 < a)
                return getIntervPosThirdArg(n, eps, left, right, a, b, f);
            else
                return getIntervNegThirdArg(n, eps, left, right, a, b, f);
        }

        /*
         * Determines in a fast way (for the Bisection Method) a shorter interval inside [left, right]  
         * in which the solution of the equation
         *                ErfcExpInt(a, b, x) = f(b)
         * lies.
         */
        public static double[] getIntervSecondArg(int n, double eps, double left, double right, double a, double x, fsFunction f)
        {
            if (0.0e0 < a)
                return getIntervPosSecondArg(n, eps, left, right, a, x, f);
            else
                return getIntervNegSecondArg(n, eps, left, right, a, x, f);
        }

        /*
         * Abramowitz and Stegun's  approximation.
         * Abramowitz, Milton; Stegun, Irene A., eds. (1965), "Chapter 7", 
         * Handbook of Mathematical Functions with Formulas, Graphs, and Mathematical Tables, 
         * New York: Dover, pp. 297, ISBN 978-0486612720, MR 0167642
         * (Taken from Wikipedia)
         * Maximum error 2.5e-5 
         */
        public static double erf(double x)
        {
            double t;
            double res;
            double xSqr = x * x;
            if (0.0e0 < x)
                t = 0.1e1 / (0.1e1 + 0.47047e0 * x);
            else if (x < 0.0e0)
                t = 0.1e1 / (0.1e1 - 0.47047e0 * x);
            else
                return 0.0e0;
            res = 0.1e1 - t * Math.Exp(-xSqr) * (0.3480242e0 + t * (0.7478556e0 * t - 0.958798e-1));
            if (0.0e0 <= x)
                return res;
            else
                return -res;
        }

        public static double erfc(double x)
        {
            return 1.0 - erf(x);
        }

        private static double intExpATSqrB(double alpha, double beta, double y1, double y2)
        {
            double bOver2a = 0.5e0 * beta / alpha;
            double ay1bOver2a = alpha * y1 + bOver2a;
            double ay2bOver2a = alpha * y2 + bOver2a;
            double erfAB1 = erf(ay1bOver2a);
            double erfAB2 = erf(ay2bOver2a);
            return Math.Exp(bOver2a * bOver2a) / alpha * (erfAB2 - erfAB1);
        }

        private static double intExpATSqrBInf(double alpha, double beta, double y)
        {
            double bOver2a = 0.5e0 * beta / alpha;
            double aybOver2a = alpha * y + bOver2a;
            return Math.Exp(bOver2a * bOver2a) / alpha * (double)((alpha < 0 ? -1 : 1) - erf(aybOver2a));
        }

        private static double int2Low_x(double a, double b, double x, double b2, double erfB2, double erfX)
        {
            if (a * x + b <= -2)
                return 0.1995322265018952734162069256367e1 * (0.1e1 + erfX);
            return 0.497661132509476367081034628184e0 * (0.564189583547756286948079451561e0 * a * (Math.Exp(-x * x) - Math.Exp(-b2 * b2)) - b * (erfX + erfB2)) + 0.1e1 + erfX + 0.995322265018952734162e0 * (0.1e1 - erfB2);
        }

        private static double int2Up_x(double a, double b, double x, double b2, double erfB2, double erfX)
        {
            if (a * x + b <= -2)
                return 0.2e1 + 0.2e1 * erfX;
            return 0.995322265018952734162069256367e0 * (erfX + erfB2) + 0.2e1 + erfX - erfB2;
        }

        private static double int2Low_bOverA(double a, double b, double b2, double bOverA, double erfB2, double erfBOverA)
        {
            return 0.497661132509476367081034628184e0 * (0.564189583547756286948079451561e0 * a * (Math.Exp(-bOverA * bOverA) - Math.Exp(-b2 * b2)) - b * (erfB2 - erfBOverA)) + 0.1e1 - erfBOverA + 0.995322265018952734162e0 * (0.1e1 - erfB2);
        }

        private static double int2Up_bOverA(double a, double b, double b2, double bOverA, double erfB2, double erfBOverA)
        {
            return 0.995322265018952734162069256367e0 * (erfB2 - erfBOverA) + 0.2e1 - erfBOverA - erfB2;
        }

        private static double ErfcExpIntLowPos(double a, double b, double x, double b2, double bOverA, double halfBOverA, double oneBOverA, double erfB2, double erfX, double erfBOverA, double erfHalfBOverA)
        {
            if (x < -bOverA)
                return int2Low_x(a, b, x, b2, erfB2, erfX);
            else if (x < halfBOverA)
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       erfc(a * x + b) * (erfX + erfBOverA);
            else if (x < oneBOverA)
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       0.479500122186953462317253346108e0 * (erfHalfBOverA + erfBOverA) +
                       erfc(a * x + b) * (erfX - erfHalfBOverA);
            else
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       0.479500122186953462317253346108e0 * (erfHalfBOverA + erfBOverA) +
                       0.157299207050285130658779364917e0 * (erf(oneBOverA) - erfHalfBOverA) +
                       0.206507720129041778112344897057e0 * Math.Exp(-b * (0.186602540378443864676372317077e1 * b + 0.1e1) +
                       0.1e1) * intExpATSqrB(Math.Sqrt(0.186602540378443864676372317077e1 * a * a + 0.1e1),
                       a * (0.373205080756887729352744634154e1 * b + 0.1e1), oneBOverA, x);
        }

        private static double ErfcExpIntUpPos(double a, double b, double x, double b2, double bOverA, double halfBOverA, double oneBOverA, double erfB2, double erfX, double erfBOverA, double erfHalfBOverA)
        {
            if (x < -bOverA)
                return int2Up_x(a, b, x, b2, erfB2, erfX);
            else if (x < halfBOverA)
                return int2Up_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) + erfX + erfBOverA;
            else if (x < oneBOverA)
                return int2Up_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) + erfHalfBOverA + erfBOverA +
                       0.479500122186953462317253346108e0 * (erfX - erfHalfBOverA);
            else
                return int2Up_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) + erfHalfBOverA + erfBOverA +
                       0.479500122186953462317253346108e0 * (erf(oneBOverA) - erfHalfBOverA) +
                       0.564189583547756286948079451561e0 * Math.Exp(-b * b) *
                       intExpATSqrB(Math.Sqrt(0.1e1 + a * a), 0.2e1 * a * b, oneBOverA, x);
        }

        private static double ErfcExpIntLowNeg(double a, double b, double x, double y, double aInv, double bOverA, double halfBOverA, double oneBOverA, double multLow, double a1Low, double b1Low, double expBOverAsqr, double expXsqr, double erfX, double erfcY, double erfBOverA, double erfHalfBOverA, double erfOneBOverA)
        {
            if (0.1e1 < y)
                return multLow * intExpATSqrBInf(a1Low, b1Low, y);
            else if (0.5e0 < y)
                return multLow * intExpATSqrBInf(a1Low, b1Low, 1) + 0.157299207050285130658779364917e0 * (erfX - erfOneBOverA);
            else if (0.0e0 < y)
                return multLow * intExpATSqrBInf(a1Low, b1Low, 1) + 0.157299207050285130658779364917e0 * (erfHalfBOverA - erfOneBOverA) + 0.479500122186953462317253346108e0 * (erfX - erfHalfBOverA);
            else
                return multLow * intExpATSqrBInf(a1Low, b1Low, 1) + 0.157299207050285130658779364917e0 * (erfHalfBOverA - erfOneBOverA) - 0.479500122186953462317253346108e0 * (erfBOverA + erfHalfBOverA) + erfX + erfBOverA + (erfcY - 0.1e1) / y * (b * (erfX + erfBOverA) + 0.564189583547756286948079451561e0 * a * (expBOverAsqr - expXsqr));
        }

        private static double ErfcExpIntUpNeg(double a, double b, double x, double y, double aInv, double bOverA, double halfBOverA, double oneBOverA, double multUp, double a1Up, double b1Up, double erfX, double erfcY, double erfBOverA, double erfHalfBOverA, double erfOneBOverA)
        {
            if (0.1e1 < y)
                return multUp * intExpATSqrBInf(a1Up, b1Up, y);
            else if (0.5e0 < y)
                return multUp * intExpATSqrBInf(a1Up, b1Up, 1) + erfcY * (erfX - erfOneBOverA);
            else if (0.0e0 < y)
                return multUp * intExpATSqrBInf(a1Up, b1Up, 1) + 0.479500122186953462317253346108e0 * (erfHalfBOverA - erfOneBOverA) + erfcY * (erfX - erfHalfBOverA);
            else
                return multUp * intExpATSqrBInf(a1Up, b1Up, 1) + 0.479500122186953462317253346108e0 * (erfHalfBOverA - erfOneBOverA) - erfBOverA - erfHalfBOverA + erfcY * (erfX + erfBOverA);
        }

        private static double[] getIntervPosSecondArg(int n, double eps, double left, double right, double a, double x, fsFunction f)
        {
            int i;
            double lbLow = 0.0;
            double ubLow = 0.0;
            double lbUp = 0.0;
            double ubUp = 0.0;
            double lbLow1;
            double ubLow1;
            double lbLow2;
            double ubLow2;
            double leftLow = 0.0;
            double rightLow = 0.0;
            double lbUp1;
            double ubUp1;
            double lbUp2;
            double ubUp2;
            double leftUp = 0.0;
            double rightUp = 0.0;
            double erfX = erf(x);
            double aInv = 0.1e1 / a;
            double b2 = (0.2e1 + left) * aInv;
            double bOverA = left * aInv;
            double halfBOverA = (0.5e0 - left) * aInv;
            double oneBOverA = (0.1e1 - left) * aInv;
            double erfB2 = erf(b2);
            double erfBOverA = erf(bOverA);
            double erfHalfBOverA = erf(halfBOverA);
            double sd = f.Eval(new fsValue(left)).Value;
            double lowLeft = ErfcExpIntLowPos(a, left, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            double upLeft = ErfcExpIntUpPos(a, left, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            b2 = (0.2e1 + right) * aInv;
            bOverA = right * aInv;
            halfBOverA = (0.5e0 - right) * aInv;
            oneBOverA = (0.1e1 - right) * aInv;
            erfB2 = erf(b2);
            erfBOverA = erf(bOverA);
            erfHalfBOverA = erf(halfBOverA);
            sd = f.Eval(new fsValue(right)).Value;
            double lowRight = ErfcExpIntLowPos(a, right, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            double upRight = ErfcExpIntUpPos(a, right, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            bool lowStop = false;
            bool upStop = false;
            if (0 < lowLeft + eps && 0 < lowRight - eps || lowLeft + eps < 0 && lowRight - eps < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft + eps && 0 < upRight - eps || upLeft + eps < 0 && upRight - eps < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
                return new[] { 0e0, left, right };
            if (!lowStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbLow1 = lbLow;
                    ubLow1 = 0.5e0 * (ubLow + lbLow);
                    if (Math.Abs(ubLow1 - ubLow) < eps)
                        break;
                    lbLow2 = ubLow1;
                    ubLow2 = ubLow;
                    b2 = (0.2e1 + lbLow1) * aInv;
                    bOverA = lbLow1 * aInv;
                    halfBOverA = (0.5e0 - lbLow1) * aInv;
                    oneBOverA = (0.1e1 - lbLow1) * aInv;
                    erfB2 = erf(b2);
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    sd = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfcExpIntLowPos(a, lbLow1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    b2 = (0.2e1 + ubLow1) * aInv;
                    bOverA = ubLow1 * aInv;
                    halfBOverA = (0.5e0 - ubLow1) * aInv;
                    oneBOverA = (0.1e1 - ubLow1) * aInv;
                    erfB2 = erf(b2);
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    sd = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfcExpIntLowPos(a, ubLow1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    if (0 <= lowLeft + eps && lowRight - eps <= 0 || lowLeft - eps <= 0 && 0 <= lowRight + eps)
                    {
                        lbLow = lbLow1;
                        ubLow = ubLow1;
                    }
                    else
                    {
                        lbLow = lbLow2;
                        ubLow = ubLow2;
                    }
                }
                leftLow = lbLow;
                rightLow = ubLow;
            }
            if (!upStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbUp1 = lbUp;
                    ubUp1 = 0.5e0 * (ubUp + lbUp);
                    if (Math.Abs(ubUp1 - ubUp) < eps)
                        break;
                    lbUp2 = ubUp1;
                    ubUp2 = ubUp;
                    b2 = (0.2e1 + lbUp1) * aInv;
                    bOverA = lbUp1 * aInv;
                    halfBOverA = (0.5e0 - lbUp1) * aInv;
                    oneBOverA = (0.1e1 - lbUp1) * aInv;
                    erfB2 = erf(b2);
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    sd = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfcExpIntUpPos(a, lbUp1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    b2 = (0.2e1 + ubUp1) * aInv;
                    bOverA = ubUp1 * aInv;
                    halfBOverA = (0.5e0 - ubUp1) * aInv;
                    oneBOverA = (0.1e1 - ubUp1) * aInv;
                    erfB2 = erf(b2);
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    sd = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfcExpIntUpPos(a, ubUp1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    if (0 <= upLeft + eps && upRight - eps <= 0 || upLeft - eps <= 0 && 0 <= upRight + eps)
                    {
                        lbUp = lbUp1;
                        ubUp = ubUp1;
                    }
                    else
                    {
                        lbUp = lbUp2;
                        ubUp = ubUp2;
                    }
                }
                leftUp = lbUp;
                rightUp = ubUp;
            }
            return getBounds(lowStop, upStop, left, right, lowLeft, upLeft, leftLow, rightLow, leftUp, rightUp);
        }

        private static double[] getIntervNegSecondArg(int n, double eps, double left, double right, double a, double x, fsFunction f)
        {
            int i;
            double lbLow = 0.0;
            double ubLow = 0.0;
            double lbUp = 0.0;
            double ubUp = 0.0;
            double lbLow1;
            double ubLow1;
            double lbLow2;
            double ubLow2;
            double leftLow = 0.0;
            double rightLow = 0.0;
            double lbUp1;
            double ubUp1;
            double lbUp2;
            double ubUp2;
            double leftUp = 0.0;
            double rightUp = 0.0;
            double erfX = erf(x);
            double aInv = 0.1e1 / a;
            double expXsqr = Math.Exp(-x * x);
            double a1Up = Math.Sqrt(0.1e1 + aInv * aInv);
            double a1Low = Math.Sqrt(0.186602540378443864676372317077e1 + aInv * aInv);
            double bOverA = left * aInv;
            double halfBOverA = (0.5e0 - left) * aInv;
            double oneBOverA = (0.1e1 - left) * aInv;
            double y = a * x + left;
            double sd = f.Eval(new fsValue(left)).Value;
            double erfBOverA = erf(bOverA);
            double erfHalfBOverA = erf(halfBOverA);
            double expBOverAsqr = Math.Exp(-bOverA * bOverA);
            double mult = -aInv * expBOverAsqr;
            double multUp = 0.564189583547756286948079451561e0 * mult;
            double b1Up = -0.2e1 * bOverA * aInv;
            double multLow = 0.561346183063280465485947633796e0 * mult;
            double b1Low = 0.1e1 + b1Up;
            double erfcY = erfc(y);
            double erfOneBOverA = erf(oneBOverA);
            double lowLeft = ErfcExpIntLowNeg(a, left, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            double upLeft = ErfcExpIntUpNeg(a, left, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            bOverA = right * aInv;
            halfBOverA = (0.5e0 - right) * aInv;
            oneBOverA = (0.1e1 - right) * aInv;
            y = a * x + right;
            sd = f.Eval(new fsValue(right)).Value;
            erfBOverA = erf(bOverA);
            erfHalfBOverA = erf(halfBOverA);
            expBOverAsqr = Math.Exp(-bOverA * bOverA);
            mult = -aInv * expBOverAsqr;
            multUp = 0.564189583547756286948079451561e0 * mult;
            b1Up = -0.2e1 * bOverA * aInv;
            multLow = 0.561346183063280465485947633796e0 * mult;
            b1Low = 0.1e1 + b1Up;
            erfcY = erfc(y);
            erfOneBOverA = erf(oneBOverA);
            double lowRight = ErfcExpIntLowNeg(a, right, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            double upRight = ErfcExpIntUpNeg(a, right, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            bool lowStop = false;
            bool upStop = false;
            if (0 < lowLeft + eps && 0 < lowRight - eps || lowLeft + eps < 0 && lowRight - eps < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft + eps && 0 < upRight - eps || upLeft + eps < 0 && upRight - eps < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
                return new[] { 0e0, left, right };
            if (!lowStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbLow1 = lbLow;
                    ubLow1 = 0.5e0 * (ubLow + lbLow);
                    if (Math.Abs(ubLow - ubLow1) < eps)
                        break;
                    lbLow2 = ubLow1;
                    ubLow2 = ubLow;
                    bOverA = lbLow1 * aInv;
                    halfBOverA = (0.5e0 - lbLow1) * aInv;
                    oneBOverA = (0.1e1 - lbLow1) * aInv;
                    sd = f.Eval(new fsValue(lbLow1)).Value;
                    y = a * x + lbLow1;
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = erfc(y);
                    erfOneBOverA = erf(oneBOverA);
                    lowLeft = ErfcExpIntLowNeg(a, lbLow1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    bOverA = ubLow1 * aInv;
                    halfBOverA = (0.5e0 - ubLow1) * aInv;
                    oneBOverA = (0.1e1 - ubLow1) * aInv;
                    sd = f.Eval(new fsValue(ubLow1)).Value;
                    y = a * x + ubLow1;
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = erfc(y);
                    erfOneBOverA = erf(oneBOverA);
                    lowRight = ErfcExpIntLowNeg(a, ubLow1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    if (0 <= lowLeft + eps && lowRight - eps <= 0 || lowLeft - eps <= 0 && 0 <= lowRight + eps)
                    {
                        lbLow = lbLow1;
                        ubLow = ubLow1;
                    }
                    else
                    {
                        lbLow = lbLow2;
                        ubLow = ubLow2;
                    }
                }
                leftLow = lbLow;
                rightLow = ubLow;
            }
            if (!upStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbUp1 = lbUp;
                    ubUp1 = 0.5e0 * (ubUp + lbUp);
                    if (Math.Abs(ubUp1 - ubUp) < eps)
                        break;
                    lbUp2 = ubUp1;
                    ubUp2 = ubUp;
                    bOverA = lbUp1 * aInv;
                    halfBOverA = (0.5e0 - lbUp1) * aInv;
                    oneBOverA = (0.1e1 - lbUp1) * aInv;
                    sd = f.Eval(new fsValue(lbUp1)).Value;
                    y = a * x + lbUp1;
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = erfc(y);
                    erfOneBOverA = erf(oneBOverA);
                    upLeft = ErfcExpIntUpNeg(a, lbUp1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    bOverA = ubUp1 * aInv;
                    halfBOverA = (0.5e0 - ubUp1) * aInv;
                    oneBOverA = (0.1e1 - ubUp1) * aInv;
                    sd = f.Eval(new fsValue(ubUp1)).Value;
                    y = a * x + ubUp1;
                    erfBOverA = erf(bOverA);
                    erfHalfBOverA = erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = erfc(y);
                    erfOneBOverA = erf(oneBOverA);
                    upRight = ErfcExpIntUpNeg(a, ubUp1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    if (0 <= upLeft + eps && upRight - eps <= 0 || upLeft - eps <= 0 && 0 <= upRight + eps)
                    {
                        lbUp = lbUp1;
                        ubUp = ubUp1;
                    }
                    else
                    {
                        lbUp = lbUp2;
                        ubUp = ubUp2;
                    }
                }
                leftUp = lbUp;
                rightUp = ubUp;
            }
            return getBounds(lowStop, upStop, left, right, lowLeft, upLeft, leftLow, rightLow, leftUp, rightUp);
        }

        private static double[] getIntervPosThirdArg(int n, double eps, double left, double right, double a, double b, fsFunction f)
        {
            int i;
            double lbLow = 0.0;
            double ubLow = 0.0;
            double lbUp = 0.0;
            double ubUp = 0.0;
            double lbLow1;
            double ubLow1;
            double lbLow2;
            double ubLow2;
            double leftLow = 0.0;
            double rightLow = 0.0;
            double lbUp1;
            double ubUp1;
            double lbUp2;
            double ubUp2;
            double leftUp = 0.0;
            double rightUp = 0.0;
            double aInv = 0.1e1 / a;
            double b2 = (0.2e1 + b) * aInv;
            double bOverA = b * aInv;
            double halfBOverA = (0.5e0 - b) * aInv;
            double oneBOverA = (0.1e1 - b) * aInv;
            double erfB2 = erf(b2);
            double erfBOverA = erf(bOverA);
            double erfHalfBOverA = erf(halfBOverA);
            double erfX = erf(left);
            double h = f.Eval(new fsValue(left)).Value;
            double lowLeft = ErfcExpIntLowPos(a, b, left, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            double upLeft = ErfcExpIntUpPos(a, b, left, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            erfX = erf(right);
            h = f.Eval(new fsValue(right)).Value;
            double lowRight = ErfcExpIntLowPos(a, b, right, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            double upRight = ErfcExpIntUpPos(a, b, right, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            bool lowStop = false;
            bool upStop = false;
            if (0 < lowLeft + eps && 0 < lowRight - eps || lowLeft + eps < 0 && lowRight - eps < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft + eps && 0 < upRight - eps || upLeft + eps < 0 && upRight - eps < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
            {
                return new[] { 0e0, left, right };
            }
            if (!lowStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbLow1 = lbLow;
                    ubLow1 = 0.5e0 * (ubLow + lbLow);
                    if (Math.Abs(ubLow - ubLow1) < eps)
                        break;
                    lbLow2 = ubLow1;
                    ubLow2 = ubLow;
                    erfX = erf(lbLow1);
                    h = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfcExpIntLowPos(a, b, lbLow1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    erfX = erf(ubLow1);
                    h = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfcExpIntLowPos(a, b, ubLow1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    if (0 <= lowLeft + eps && lowRight - eps <= 0 || lowLeft - eps <= 0 && 0 <= lowRight + eps)
                    {
                        lbLow = lbLow1;
                        ubLow = ubLow1;
                    }
                    else
                    {
                        lbLow = lbLow2;
                        ubLow = ubLow2;
                    }
                }
                leftLow = lbLow;
                rightLow = ubLow;
            }
            if (!upStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbUp1 = lbUp;
                    ubUp1 = 0.5e0 * (ubUp + lbUp);
                    if (Math.Abs(ubUp - ubUp1) < eps)
                        break;
                    lbUp2 = ubUp1;
                    ubUp2 = ubUp;
                    erfX = erf(lbUp1);
                    h = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfcExpIntUpPos(a, b, lbUp1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    erfX = erf(ubUp1);
                    h = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfcExpIntUpPos(a, b, ubUp1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    if (0 <= upLeft + eps && upRight - eps <= 0 || upLeft - eps <= 0 && 0 <= upRight + eps)
                    {
                        lbUp = lbUp1;
                        ubUp = ubUp1;
                    }
                    else
                    {
                        lbUp = lbUp2;
                        ubUp = ubUp2;
                    }
                }
                leftUp = lbUp;
                rightUp = ubUp;
            }
            return getBounds(lowStop, upStop, left, right, lowLeft, upLeft, leftLow, rightLow, leftUp, rightUp);
        }

        private static double[] getIntervNegThirdArg(int n, double eps, double left, double right, double a, double b, fsFunction f)
        {
            int i;
            double lbLow = 0.0;
            double ubLow = 0.0;
            double lbUp = 0.0;
            double ubUp = 0.0;
            double lbLow1;
            double ubLow1;
            double lbLow2;
            double ubLow2;
            double leftLow = 0.0;
            double rightLow = 0.0;
            double lbUp1;
            double ubUp1;
            double lbUp2;
            double ubUp2;
            double leftUp = 0.0;
            double rightUp = 0.0;
            double aInv = 0.1e1 / a;
            double a1Up = Math.Sqrt(0.1e1 + aInv * aInv);
            double a1Low = Math.Sqrt(0.186602540378443864676372317077e1 + aInv * aInv);
            double bOverA = b * aInv;
            double halfBOverA = (0.5e0 - b) * aInv;
            double oneBOverA = (0.1e1 - b) * aInv;
            double erfBOverA = erf(bOverA);
            double erfHalfBOverA = erf(halfBOverA);
            double expBOverAsqr = Math.Exp(-bOverA * bOverA);
            double mult = -aInv * expBOverAsqr;
            double multUp = 0.564189583547756286948079451561e0 * mult;
            double b1Up = -0.2e1 * bOverA * aInv;
            double multLow = 0.561346183063280465485947633796e0 * mult;
            double b1Low = 0.1e1 + b1Up;
            double erfOneBOverA = erf(oneBOverA);
            double erfX = erf(left);
            double expXsqr = Math.Exp(-left * left);
            double y = a * left + b;
            double erfcY = erfc(y);
            double h = f.Eval(new fsValue(left)).Value;
            double lowLeft = ErfcExpIntLowNeg(a, b, left, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            double upLeft = ErfcExpIntUpNeg(a, b, left, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            erfX = erf(right);
            expXsqr = Math.Exp(-right * right);
            y = a * right + b;
            erfcY = erfc(y);
            h = f.Eval(new fsValue(right)).Value;
            double lowRight = ErfcExpIntLowNeg(a, b, right, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            double upRight = ErfcExpIntUpNeg(a, b, right, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            bool lowStop = false;
            bool upStop = false;
            if (0 < lowLeft + eps && 0 < lowRight - eps || lowLeft + eps < 0 && lowRight - eps < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft + eps && 0 < upRight - eps || upLeft + eps < 0 && upRight - eps < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
            {
                return new[] { 0.0, left, right };
            }
            if (!lowStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbLow1 = lbLow;
                    ubLow1 = 0.5e0 * (ubLow + lbLow);
                    if (Math.Abs(ubLow - ubLow1) < eps)
                        break;
                    lbLow2 = ubLow1;
                    ubLow2 = ubLow;
                    erfX = erf(lbLow1);
                    expXsqr = Math.Exp(-lbLow1 * lbLow1);
                    y = a * lbLow1 + b;
                    erfcY = erfc(y);
                    h = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfcExpIntLowNeg(a, b, lbLow1, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    erfX = erf(ubLow1);
                    expXsqr = Math.Exp(-ubLow1 * ubLow1);
                    y = a * ubLow1 + b;
                    erfcY = erfc(y);
                    h = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfcExpIntLowNeg(a, b, ubLow1, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    if (0 <= lowLeft + eps && lowRight - eps <= 0 || lowLeft - eps <= 0 && 0 <= lowRight + eps)
                    {
                        lbLow = lbLow1;
                        ubLow = ubLow1;
                    }
                    else
                    {
                        lbLow = lbLow2;
                        ubLow = ubLow2;
                    }
                }
                leftLow = lbLow;
                rightLow = ubLow;
            }
            if (!upStop)
            {
                for (i = 1; i <= n; i++)
                {
                    lbUp1 = lbUp;
                    ubUp1 = 0.5e0 * (ubUp + lbUp);
                    if (Math.Abs(ubUp - ubUp1) < eps)
                        break;
                    lbUp2 = ubUp1;
                    ubUp2 = ubUp;
                    erfX = erf(lbUp1);
                    expXsqr = Math.Exp(-lbUp1 * lbUp1);
                    y = a * lbUp1 + b;
                    erfcY = erfc(y);
                    h = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfcExpIntUpNeg(a, b, lbUp1, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    erfX = erf(ubUp1);
                    expXsqr = Math.Exp(-ubUp1 * ubUp1);
                    y = a * ubUp1 + b;
                    erfcY = erfc(y);
                    h = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfcExpIntUpNeg(a, b, ubUp1, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    if (0 <= upLeft + eps && upRight - eps <= 0 || upLeft - eps <= 0 && 0 <= upRight + eps)
                    {
                        lbUp = lbUp1;
                        ubUp = ubUp1;
                    }
                    else
                    {
                        lbUp = lbUp2;
                        ubUp = ubUp2;
                    }
                }
                leftUp = lbUp;
                rightUp = ubUp;
            }
            return getBounds(lowStop, upStop, left, right, lowLeft, upLeft, leftLow, rightLow, leftUp, rightUp);
        }

        private static double[] getBounds(bool lowStop, bool upStop, double left, double right, double lowLeft, double upLeft, double leftLow, double rightLow, double leftUp, double rightUp)
        {
            if (!(lowStop || upStop))
                return new[] { 1.0, 0.5e0 * (leftUp + rightUp), 0.5e0 * (leftLow + rightLow) };
            else if (!upStop)
                if (upLeft < 0)
                    return new[] { 2.0, 0.5e0 * (leftUp + rightUp), right };
                else
                    return new[] { 3.0, left, 0.5e0 * (leftUp + rightUp) };
            else
                if (lowLeft > 0)
                    return new[] { 2.0, 0.5e0 * (leftLow + rightLow), right };
                else
                    return new[] { 3.0, left, 0.5e0 * (leftLow + rightLow) };
        }
    }
}
