using System;
using System.Collections.Generic;
using fsNumericalMethods;
using AGLibrary;
using ErfExpIntCalculator;
using ExpLinCalculator;
using Value;

namespace fsNumericalMethods
{ 
    public class fsSpecialFunctions
    {       
        #region ExpLin

        /*
         * The transcendental equation
         *                 z = x * exp(z)           (*ExpLin*)
         * with respect to z given x has a unique solution if 
         *                 x < 0
         * and exactly 2 solutions if 
         *                 0 < x < exp(-1) ~= 0.367879441171442321595524; 
         * if 
         *                 x = exp(-1) 
         * then (*ExpLin*)  has a unique solution z = 1; if 
         *                 x > exp(-1)
         * then (*ExpLin*)  has no solution. So for domain (0, exp(-1)] we have two branches of z(x):
         * the first for which 
         *                 if x -> 0  then z -> infinity  (see ExpLinPosInfinity below),
         * and the second for which
         *                 if x -> 0  then z -> 0          (see ExpLinPosZero below)
         * ---------------------------------------------------------------------------
         * The accuracy of all ExpLinPosInfinity, ExpLinPosZero and ExpLinNeg is 1.0e-16
         */

        public static fsValue ExpLinPosInfinity(fsValue x)
        {
            if (x.Value > 0.367879441171442321595524 || x.Defined == false)
              return new fsValue();
            else
              return new fsValue(fsExpLinCalculator.ExpLinPosInfinity(x.Value));
        }

        public static fsValue ExpLinPosZero(fsValue x)
        {
            if (x.Value > 0.367879441171442321595524 || x.Defined == false)
                return new fsValue();
            else
                return new fsValue(fsExpLinCalculator.ExpLinPosZero(x.Value));
        }

        public static fsValue ExpLinNeg(fsValue x)
        {
            if (x.Value > 0 || x.Defined == false)
                return new fsValue();
            else
                return new fsValue(fsExpLinCalculator.ExpLinNeg(x.Value));
        }

        #endregion

        #region Error functions

        public static fsValue Erf(fsValue op)
        {
            var res = new fsValue { Defined = op.Defined };
            res.Value = res.Defined ? normaldistr.erf(op.Value) : 1;
            return res;
        }

        public static fsValue Erfc(fsValue op)
        {
            var res = new fsValue { Defined = op.Defined };
            res.Value = res.Defined ? normaldistr.erfc(op.Value) : 1;
            return res;
        }

        public static fsValue InvErf(fsValue op)
        {
            var res = new fsValue { Defined = op.Defined };
            res.Value = res.Defined ? normaldistr.inverf(op.Value) : 1;
            return res;
        }

        public static fsValue InvErfc(fsValue op)
        {
            return InvErf(1 - op);
        }

        #endregion

        public static fsValue LambertW(fsValue x)
        {
            if (x.Value < -Math.Exp(-1.0) || x.Defined == false)
            {
                return new fsValue();
            }
            if (x.Value > 0)
            {
                double y = 0.5 * x.Value, dy = 0.25 * x.Value;
                while (dy > 1e-9)
                {
                    double f = y * Math.Exp(y);
                    if (f > x.Value) y -= dy;
                    else y += dy;
                    dy *= 0.5;
                }
                return new fsValue(y);
            }
            else
            {
                double y = 0.5, dy = 0.25;
                while (dy > 1e-9)
                {
                    double f = -y * Math.Exp(-y);
                    if (f < x.Value) y -= dy;
                    else y += dy;
                    dy *= 0.5;
                }
                return new fsValue(-y);
            }
        }

        #region ErfExpInt(a, b, x)

        /*                   
         *                                   x      
         *                                   /
         *                           2       |
         *  ErfExpInt(a, b, x) =  -------- * | erf(a * t + b) * exp(-t^2) dt
         *                        pi^(1/2)   |
         *                                   /
         *                               - infinity
         *  Accuracy 0.2e-8  (In most cases it is considerably less, ~1.0e-16)                           
         */



        public static fsValue ErfExpInt(fsValue a, fsValue b, fsValue x)
        {
            var res = new fsValue { Defined = a.Defined && b.Defined && x.Defined };
            res.Value = res.Defined ? fsErfExpIntCalculator.ErfExpInt(a.Value, b.Value, x.Value) : 1;
            return res;
        }

        #endregion 
        
        #region ErfcExpInt(a, b, x)

        /*                   
         *                                    x      
         *                                    /
         *                            2       |
         *  ErfcExpInt(a, b, x) =  -------- * | erfc(a * t + b) * exp(-t^2) dt
         *                         pi^(1/2)   |
         *                                    /
         *                                - infinity                           
         */



        public static fsValue ErfcExpInt(fsValue a, fsValue b, fsValue x)
        {
            return 1 + Erf(x) - ErfExpInt(a, b, x);
        }

        #endregion 
    }
}