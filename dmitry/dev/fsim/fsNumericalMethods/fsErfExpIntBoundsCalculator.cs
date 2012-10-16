using System;
using Value;
using AGLibrary;
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
         * Determines in a fast way (for Bisection Method) a shorter interval inside [left, right]  
         * in which the solution of the equation
         *                ErfcExpInt(a, b, x) = f(x)
         * lies
         */
        public static double[] getInterv(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double b,
          fsFunction f)
        {
            if (0.0e0 < a)
                return getIntervPos(n, eps, left, right, a, b, f);
            else
                return getIntervNeg(n, eps, left, right, a, b, f);
        }

        /*
         * Determines in a fast way a point near the solution of the equation
         *                ErfcExpInt(a, b, x) = f(b)
         */
        public static double getRootNeighborSecondArg(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double x,
          fsFunction f)
        {
            if (0.0e0 < a)
                return getRootNeighborPos(n, eps, left, right, a, x, f);
            else
                return getRootNeighborNeg(n, eps, left, right, a, x, f);
        }

        /*
         * Determines in a fast way a point near the solution of the equation
         *                ErfcExpInt(a, b, x) = f(x)
         */
        public static double getRootNeighborThirdArg(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double b,
          fsFunction f)
        {
            double[] bounds = getInterv(n, eps, left, right, a, b, f);
            if (bounds[0] == 0.0)
                return 0.5 * (left + right);
            if (bounds[0] == 2.0)
                return bounds[1];
            if (bounds[0] == 3.0)
                return bounds[2];
            return 0.5 * (bounds[1] + bounds[2]);
        }

        private static double intExpATSqrB(
          double alpha,
          double beta,
          double y1,
          double y2)
        {
            double bOver2a;
            double ay1bOver2a;
            double ay2bOver2a;
            double erfAB1;
            double erfAB2;
            bOver2a = 0.5e0 * beta / alpha;
            ay1bOver2a = alpha * y1 + bOver2a;
            ay2bOver2a = alpha * y2 + bOver2a;
            erfAB1 = normaldistr.erf(ay1bOver2a);
            erfAB2 = normaldistr.erf(ay2bOver2a);
            return Math.Exp(bOver2a * bOver2a) / alpha * (erfAB2 - erfAB1);
        }

        private static double intExpATSqrBInf(double alpha, double beta, double y)
        {
            double bOver2a;
            double aybOver2a;
            bOver2a = 0.5e0 * beta / alpha;
            aybOver2a = alpha * y + bOver2a;
            return Math.Exp(bOver2a * bOver2a) / alpha * (double)((alpha < 0 ? -1 : 1) - normaldistr.erf(aybOver2a));
        }

        private static double int2Low_x(
          double a,
          double b,
          double x,
          double b2,
          double erfB2,
          double erfX)
        {
            if (a * x + b <= -2)
                return 0.1995322265018952734162069256367e1 * (0.1e1 + erfX);
            return 0.497661132509476367081034628184e0 * (0.564189583547756286948079451561e0 * a * (Math.Exp(-x * x) - Math.Exp(-b2 * b2)) - b * (erfX + erfB2)) + 0.1e1 + erfX + 0.995322265018952734162e0 * (0.1e1 - erfB2);
        }

        private static double int2Up_x(
          double a,
          double b,
          double x,
          double b2,
          double erfB2,
          double erfX)
        {
            if (a * x + b <= -2)
                return 0.2e1 + 0.2e1 * erfX;
            return 0.995322265018952734162069256367e0 * (erfX + erfB2) + 0.2e1 + erfX - erfB2;
        }

        private static double int2Low_bOverA(
          double a,
          double b,
          double b2,
          double bOverA,
          double erfB2,
          double erfBOverA)
        {
            return 0.497661132509476367081034628184e0 * (0.564189583547756286948079451561e0 * a * (Math.Exp(-bOverA * bOverA) - Math.Exp(-b2 * b2)) - b * (erfB2 - erfBOverA)) + 0.1e1 - erfBOverA + 0.995322265018952734162e0 * (0.1e1 - erfB2);
        }

        private static double int2Up_bOverA(
          double a,
          double b,
          double b2,
          double bOverA,
          double erfB2,
          double erfBOverA)
        {
            return 0.995322265018952734162069256367e0 * (erfB2 - erfBOverA) + 0.2e1 - erfBOverA - erfB2;
        }

        private static double ErfExpIntLowPos(
          double a,
          double b,
          double x,
          double b2,
          double bOverA,
          double halfBOverA,
          double oneBOverA,
          double erfB2,
          double erfX,
          double erfBOverA,
          double erfHalfBOverA)
        {
            if (x < -bOverA)
                return int2Low_x(a, b, x, b2, erfB2, erfX);
            else if (x < halfBOverA)
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       normaldistr.erfc(a * x + b) * (erfX + erfBOverA);
            else if (x < oneBOverA)
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       0.479500122186953462317253346108e0 * (erfHalfBOverA + erfBOverA) +
                       normaldistr.erfc(a * x + b) * (erfX - erfHalfBOverA);
            else
                return int2Low_bOverA(a, b, b2, bOverA, erfB2, erfBOverA) +
                       0.479500122186953462317253346108e0 * (erfHalfBOverA + erfBOverA) +
                       0.157299207050285130658779364917e0 * (normaldistr.erf(oneBOverA) - erfHalfBOverA) +
                       0.206507720129041778112344897057e0 * Math.Exp(-b * (0.186602540378443864676372317077e1 * b + 0.1e1) +
                       0.1e1) * intExpATSqrB(Math.Sqrt(0.186602540378443864676372317077e1 * a * a + 0.1e1),
                       a * (0.373205080756887729352744634154e1 * b + 0.1e1), oneBOverA, x);
        }

        private static double ErfExpIntUpPos(
          double a,
          double b,
          double x,
          double b2,
          double bOverA,
          double halfBOverA,
          double oneBOverA,
          double erfB2,
          double erfX,
          double erfBOverA,
          double erfHalfBOverA)
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
                       0.479500122186953462317253346108e0 * (normaldistr.erf(oneBOverA) - erfHalfBOverA) +
                       0.564189583547756286948079451561e0 * Math.Exp(-b * b) *
                       intExpATSqrB(Math.Sqrt(0.1e1 + a * a), 0.2e1 * a * b, oneBOverA, x);
        }

        private static double ErfExpIntLowNeg(
          double a,
          double b,
          double x,
          double y,
          double aInv,
          double bOverA,
          double halfBOverA,
          double oneBOverA,
          double multLow,
          double a1Low,
          double b1Low,
          double expBOverAsqr,
          double expXsqr,
          double erfX,
          double erfcY,
          double erfBOverA,
          double erfHalfBOverA,
          double erfOneBOverA)
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

        private static double ErfExpIntUpNeg(
          double a,
          double b,
          double x,
          double y,
          double aInv,
          double bOverA,
          double halfBOverA,
          double oneBOverA,
          double multUp,
          double a1Up,
          double b1Up,
          double erfX,
          double erfcY,
          double erfBOverA,
          double erfHalfBOverA,
          double erfOneBOverA)
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

        private static double getRootNeighborPos(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double x,
          fsFunction f)
        {
            int i;
            bool lowStop;
            bool upStop;
            double lowLeft;
            double lowRight;
            double upLeft;
            double upRight;
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
            double erfX;
            double aInv;
            double b2;
            double bOverA;
            double halfBOverA;
            double oneBOverA;
            double erfB2;
            double erfBOverA;
            double erfHalfBOverA;
            double sd;
            erfX = normaldistr.erf(x);
            aInv = 0.1e1 / a;
            b2 = (0.2e1 + left) * aInv;
            bOverA = left * aInv;
            halfBOverA = (0.5e0 - left) * aInv;
            oneBOverA = (0.1e1 - left) * aInv;
            erfB2 = normaldistr.erf(b2);
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            sd = f.Eval(new fsValue(left)).Value;
            lowLeft = ErfExpIntLowPos(a, left, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            upLeft = ErfExpIntUpPos(a, left, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            b2 = (0.2e1 + right) * aInv;
            bOverA = right * aInv;
            halfBOverA = (0.5e0 - right) * aInv;
            oneBOverA = (0.1e1 - right) * aInv;
            erfB2 = normaldistr.erf(b2);
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            sd = f.Eval(new fsValue(right)).Value;
            lowRight = ErfExpIntLowPos(a, right, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            upRight = ErfExpIntUpPos(a, right, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
            lowStop = false;
            upStop = false;
            if (0 < lowLeft && 0 < lowRight || lowLeft < 0 && lowRight < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft && 0 < upRight || upLeft - eps < 0 && upRight < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
                return 0.1e7;
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
                    erfB2 = normaldistr.erf(b2);
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    sd = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfExpIntLowPos(a, lbLow1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    b2 = (0.2e1 + ubLow1) * aInv;
                    bOverA = ubLow1 * aInv;
                    halfBOverA = (0.5e0 - ubLow1) * aInv;
                    oneBOverA = (0.1e1 - ubLow1) * aInv;
                    erfB2 = normaldistr.erf(b2);
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    sd = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfExpIntLowPos(a, ubLow1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
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
                    erfB2 = normaldistr.erf(b2);
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    sd = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfExpIntUpPos(a, lbUp1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
                    b2 = (0.2e1 + ubUp1) * aInv;
                    bOverA = ubUp1 * aInv;
                    halfBOverA = (0.5e0 - ubUp1) * aInv;
                    oneBOverA = (0.1e1 - ubUp1) * aInv;
                    erfB2 = normaldistr.erf(b2);
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    sd = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfExpIntUpPos(a, ubUp1, x, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - sd;
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
            if (!(lowStop || upStop))
                return 0.25e0 * (leftLow + rightLow + leftUp + rightUp);
            else if (!lowStop)
                return 0.5e0 * (leftLow + rightLow);
            else
                return 0.5e0 * (leftUp + rightUp);
        }

        private static double getRootNeighborNeg(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double x,
          fsFunction f)
        {
            int i;
            bool lowStop;
            bool upStop;
            double lowLeft;
            double lowRight;
            double upLeft;
            double upRight;
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
            double erfX;
            double aInv;
            double bOverA;
            double halfBOverA;
            double oneBOverA;
            double erfBOverA;
            double erfHalfBOverA;
            double sd;
            double expXsqr;
            double a1Up;
            double a1Low;
            double y;
            double expBOverAsqr;
            double mult;
            double multUp;
            double b1Up;
            double multLow;
            double b1Low;
            double erfcY;
            double erfOneBOverA;
            erfX = normaldistr.erf(x);
            aInv = 0.1e1 / a;
            expXsqr = Math.Exp(-x * x);
            a1Up = Math.Sqrt(0.1e1 + aInv * aInv);
            a1Low = Math.Sqrt(0.186602540378443864676372317077e1 + aInv * aInv);
            bOverA = left * aInv;
            halfBOverA = (0.5e0 - left) * aInv;
            oneBOverA = (0.1e1 - left) * aInv;
            y = a * x + left;
            sd = f.Eval(new fsValue(left)).Value;
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            expBOverAsqr = Math.Exp(-bOverA * bOverA);
            mult = -aInv * expBOverAsqr;
            multUp = 0.564189583547756286948079451561e0 * mult;
            b1Up = -0.2e1 * bOverA * aInv;
            multLow = 0.561346183063280465485947633796e0 * mult;
            b1Low = 0.1e1 + b1Up;
            erfcY = normaldistr.erfc(y);
            erfOneBOverA = normaldistr.erf(oneBOverA);
            lowLeft = ErfExpIntLowNeg(a, left, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            upLeft = ErfExpIntUpNeg(a, left, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            bOverA = right * aInv;
            halfBOverA = (0.5e0 - right) * aInv;
            oneBOverA = (0.1e1 - right) * aInv;
            y = a * x + right;
            sd = f.Eval(new fsValue(right)).Value;
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            expBOverAsqr = Math.Exp(-bOverA * bOverA);
            mult = -aInv * expBOverAsqr;
            multUp = 0.564189583547756286948079451561e0 * mult;
            b1Up = -0.2e1 * bOverA * aInv;
            multLow = 0.561346183063280465485947633796e0 * mult;
            b1Low = 0.1e1 + b1Up;
            erfcY = normaldistr.erfc(y);
            erfOneBOverA = normaldistr.erf(oneBOverA);
            lowRight = ErfExpIntLowNeg(a, right, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            upRight = ErfExpIntUpNeg(a, right, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
            lowStop = false;
            upStop = false;
            if (0 < lowLeft && 0 < lowRight || lowLeft < 0 && lowRight < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft && 0 < upRight || upLeft < 0 && upRight < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            if (lowStop && upStop)
                return 0.1e7;
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
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = normaldistr.erfc(y);
                    erfOneBOverA = normaldistr.erf(oneBOverA);
                    lowLeft = ErfExpIntLowNeg(a, lbLow1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    bOverA = ubLow1 * aInv;
                    halfBOverA = (0.5e0 - ubLow1) * aInv;
                    oneBOverA = (0.1e1 - ubLow1) * aInv;
                    sd = f.Eval(new fsValue(ubLow1)).Value;
                    y = a * x + ubLow1;
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = normaldistr.erfc(y);
                    erfOneBOverA = normaldistr.erf(oneBOverA);
                    lowRight = ErfExpIntLowNeg(a, ubLow1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
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
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = normaldistr.erfc(y);
                    erfOneBOverA = normaldistr.erf(oneBOverA);
                    upLeft = ErfExpIntUpNeg(a, lbUp1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
                    bOverA = ubUp1 * aInv;
                    halfBOverA = (0.5e0 - ubUp1) * aInv;
                    oneBOverA = (0.1e1 - ubUp1) * aInv;
                    sd = f.Eval(new fsValue(ubUp1)).Value;
                    y = a * x + ubUp1;
                    erfBOverA = normaldistr.erf(bOverA);
                    erfHalfBOverA = normaldistr.erf(halfBOverA);
                    expBOverAsqr = Math.Exp(-bOverA * bOverA);
                    mult = -aInv * expBOverAsqr;
                    multUp = 0.564189583547756286948079451561e0 * mult;
                    b1Up = -0.2e1 * bOverA * aInv;
                    multLow = 0.561346183063280465485947633796e0 * mult;
                    b1Low = 0.1e1 + b1Up;
                    erfcY = normaldistr.erfc(y);
                    erfOneBOverA = normaldistr.erf(oneBOverA);
                    upRight = ErfExpIntUpNeg(a, ubUp1, x, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - sd;
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
            if (!(lowStop || upStop))
                return 0.25e0 * (leftLow + rightLow + leftUp + rightUp);
            else if (!lowStop)
                return 0.5e0 * (leftLow + rightLow);
            else
                return 0.5e0 * (leftUp + rightUp);
        }

        private static double[] getIntervPos(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double b,
          fsFunction f)
        {
            int i;
            bool lowStop;
            bool upStop;
            double lowLeft;
            double lowRight;
            double upLeft;
            double upRight;
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
            double erfX;
            double aInv;
            double b2;
            double bOverA;
            double halfBOverA;
            double oneBOverA;
            double erfB2;
            double erfBOverA;
            double erfHalfBOverA;
            double h;
            aInv = 0.1e1 / a;
            b2 = (0.2e1 + b) * aInv;
            bOverA = b * aInv;
            halfBOverA = (0.5e0 - b) * aInv;
            oneBOverA = (0.1e1 - b) * aInv;
            erfB2 = normaldistr.erf(b2);
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            erfX = normaldistr.erf(left);
            h = f.Eval(new fsValue(left)).Value;
            lowLeft = ErfExpIntLowPos(a, b, left, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            upLeft = ErfExpIntUpPos(a, b, left, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            erfX = normaldistr.erf(right);
            h = f.Eval(new fsValue(right)).Value;
            lowRight = ErfExpIntLowPos(a, b, right, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            upRight = ErfExpIntUpPos(a, b, right, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
            lowStop = false;
            upStop = false;
            if (0 < lowLeft && 0 < lowRight || lowLeft < 0 && lowRight < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft && 0 < upRight || upLeft < 0 && upRight < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            double[] cgret;
            if (lowStop && upStop)
            {
                cgret = new[] { 0e0, left, right };
                return cgret;
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
                    erfX = normaldistr.erf(lbLow1);
                    h = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfExpIntLowPos(a, b, lbLow1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    erfX = normaldistr.erf(ubLow1);
                    h = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfExpIntLowPos(a, b, ubLow1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
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
                    erfX = normaldistr.erf(lbUp1);
                    h = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfExpIntUpPos(a, b, lbUp1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
                    erfX = normaldistr.erf(ubUp1);
                    h = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfExpIntUpPos(a, b, ubUp1, b2, bOverA, halfBOverA, oneBOverA, erfB2, erfX, erfBOverA, erfHalfBOverA) - h;
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
            if (!(lowStop || upStop))
                cgret = new[] { 1.0, 0.5e0 * (leftUp + rightUp), 0.5e0 * (leftLow + rightLow) };
            else if (!upStop)
                if (upLeft < 0)
                    cgret = new[] { 2.0, 0.5e0 * (leftUp + rightUp), right };
                else
                    cgret = new[] { 3.0, left, 0.5e0 * (leftUp + rightUp) };
            else
                if (lowLeft > 0)
                    cgret = new[] { 2.0, 0.5e0 * (leftLow + rightLow), right };
                else
                    cgret = new[] { 3.0, left, 0.5e0 * (leftLow + rightLow) };
            return cgret;
        }

        private static double[] getIntervNeg(
          int n,
          double eps,
          double left,
          double right,
          double a,
          double b,
          fsFunction f)
        {
            int i;
            bool lowStop;
            bool upStop;
            double lowLeft;
            double lowRight;
            double upLeft;
            double upRight;
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
            double erfX;
            double aInv;
            double bOverA;
            double halfBOverA;
            double oneBOverA;
            double erfBOverA;
            double erfHalfBOverA;
            double expXsqr;
            double a1Up;
            double a1Low;
            double y;
            double expBOverAsqr;
            double mult;
            double multUp;
            double b1Up;
            double multLow;
            double b1Low;
            double erfcY;
            double erfOneBOverA;
            double h;
            aInv = 0.1e1 / a;
            a1Up = Math.Sqrt(0.1e1 + aInv * aInv);
            a1Low = Math.Sqrt(0.186602540378443864676372317077e1 + aInv * aInv);
            bOverA = b * aInv;
            halfBOverA = (0.5e0 - b) * aInv;
            oneBOverA = (0.1e1 - b) * aInv;
            erfBOverA = normaldistr.erf(bOverA);
            erfHalfBOverA = normaldistr.erf(halfBOverA);
            expBOverAsqr = Math.Exp(-bOverA * bOverA);
            mult = -aInv * expBOverAsqr;
            multUp = 0.564189583547756286948079451561e0 * mult;
            b1Up = -0.2e1 * bOverA * aInv;
            multLow = 0.561346183063280465485947633796e0 * mult;
            b1Low = 0.1e1 + b1Up;
            erfOneBOverA = normaldistr.erf(oneBOverA);
            erfX = normaldistr.erf(left);
            expXsqr = Math.Exp(-left * left);
            y = a * left + b;
            erfcY = normaldistr.erfc(y);
            h = f.Eval(new fsValue(left)).Value;
            lowLeft = ErfExpIntLowNeg(a, b, left, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            upLeft = ErfExpIntUpNeg(a, b, left, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            erfX = normaldistr.erf(right);
            expXsqr = Math.Exp(-right * right);
            y = a * right + b;
            erfcY = normaldistr.erfc(y);
            h = f.Eval(new fsValue(right)).Value;
            lowRight = ErfExpIntLowNeg(a, b, right, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            upRight = ErfExpIntUpNeg(a, b, right, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
            lowStop = false;
            upStop = false;
            if (0 < lowLeft && 0 < lowRight || lowLeft < 0 && lowRight < 0)
                lowStop = true;
            else
            {
                lbLow = left;
                ubLow = right;
            }
            if (0 < upLeft && 0 < upRight || upLeft < 0 && upRight < 0)
                upStop = true;
            else
            {
                lbUp = left;
                ubUp = right;
            }
            double[] cgret;
            if (lowStop && upStop)
            {
                cgret = new[] { 0.0, left, right };
                return cgret;
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
                    erfX = normaldistr.erf(lbLow1);
                    expXsqr = Math.Exp(-lbLow1 * lbLow1);
                    y = a * lbLow1 + b;
                    erfcY = normaldistr.erfc(y);
                    h = f.Eval(new fsValue(lbLow1)).Value;
                    lowLeft = ErfExpIntLowNeg(a, b, lbLow1, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    erfX = normaldistr.erf(ubLow1);
                    expXsqr = Math.Exp(-ubLow1 * ubLow1);
                    y = a * ubLow1 + b;
                    erfcY = normaldistr.erfc(y);
                    h = f.Eval(new fsValue(ubLow1)).Value;
                    lowRight = ErfExpIntLowNeg(a, b, ubLow1, y, aInv, bOverA, halfBOverA, oneBOverA, multLow, a1Low, b1Low, expBOverAsqr, expXsqr, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
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
                    erfX = normaldistr.erf(lbUp1);
                    expXsqr = Math.Exp(-lbUp1 * lbUp1);
                    y = a * lbUp1 + b;
                    erfcY = normaldistr.erfc(y);
                    h = f.Eval(new fsValue(lbUp1)).Value;
                    upLeft = ErfExpIntUpNeg(a, b, lbUp1, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
                    erfX = normaldistr.erf(ubUp1);
                    expXsqr = Math.Exp(-ubUp1 * ubUp1);
                    y = a * ubUp1 + b;
                    erfcY = normaldistr.erfc(y);
                    h = f.Eval(new fsValue(ubUp1)).Value;
                    upRight = ErfExpIntUpNeg(a, b, ubUp1, y, aInv, bOverA, halfBOverA, oneBOverA, multUp, a1Up, b1Up, erfX, erfcY, erfBOverA, erfHalfBOverA, erfOneBOverA) - h;
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
            if (!(lowStop || upStop))
                cgret = new[] { 1.0, 0.5e0 * (leftUp + rightUp), 0.5e0 * (leftLow + rightLow) };
            else if (!upStop)
                if (upLeft < 0)
                    cgret = new[] { 2.0, 0.5e0 * (leftUp + rightUp), right };
                else
                    cgret = new[] { 3.0, left, 0.5e0 * (leftUp + rightUp) };
            else
                if (lowLeft > 0)
                    cgret = new[] { 2.0, 0.5e0 * (leftLow + rightLow), right };
                else
                    cgret = new[] { 3.0, left, 0.5e0 * (leftLow + rightLow) };
            return cgret;
        }
    }
}
