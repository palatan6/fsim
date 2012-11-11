using System;
using System.IO;

namespace Value
{
    public struct fsValue : IComparable
    {
        private const string UndefinedValue = "";
        public static int OutputPrecision = 3;
        public static fsValue One = new fsValue(1);
        public static fsValue Zero = new fsValue(0);
        public bool Defined;
        public double Value;

        public fsValue(double x)
        {
            Defined = true;
            Value = x;
        }

        public fsValue(double x, bool d)
        {
            Defined = d;
            Value = x;
        }

        public fsValue(fsValue val)
        {
            Defined = val.Defined;
            Value = val.Value;
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj is fsValue)
            {
                var temp = (fsValue) obj;

                if (this < temp) return -1;
                if (this > temp) return 1;
                return 0;
            }

            throw new ArgumentException("object is not a fmValue");
        }

        #endregion

        #region Service functions

        public override string ToString()
        {
            int precision = OutputPrecision;
            if (Defined)
            {
                double x = 1;
                int digitsBeforePoint = 0;
                while (x < Value)
                {
                    x *= 10;
                    ++digitsBeforePoint;
                }
                if (precision < digitsBeforePoint)
                {
                    precision = digitsBeforePoint;
                }
            }
            return ToString(precision);
        }

        public string ToString(int precision)
        {
            fsValue val = Round(precision);
            string res = Convert.ToString(val.Value);
            res = val.Defined ? res : UndefinedValue;
            return res;
        }

        public static bool IsValueString(String s)
        {
            try
            {
                Convert.ToDouble(s);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public void WriteToStream(StreamWriter sw)
        {
            sw.WriteLine(Defined);
            sw.WriteLine(Value);
        }

        public void ReadFromStream(StreamReader sr)
        {
            Defined = Convert.ToBoolean(sr.ReadLine());
            Value = Convert.ToDouble(sr.ReadLine());
        }

        public static fsValue StringToValue(String s)
        {
            var res = new fsValue();
            res.Defined = double.TryParse(s, out res.Value);
            return res;
        }

        public static fsValue ObjectToValue(object obj)
        {
            if (obj == null)
            {
                return new fsValue();
            }

            if (obj.GetType() == typeof (string))
            {
                return StringToValue(Convert.ToString(obj));
            }

            if (obj.GetType() == typeof (fsValue))
            {
                return (fsValue) obj;
            }

            if (obj.GetType() == typeof (double))
            {
                return new fsValue((double) obj);
            }

            throw new Exception("Can't convert object to fsValue");
        }

        public bool Equals(fsValue obj)
        {
            return (!Defined && !obj.Defined || Defined && obj.Defined && obj.Value == Value);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(fsValue)) return false;
            return Equals((fsValue)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value.GetHashCode() * 397) ^ Defined.GetHashCode();
            }
        }

        #endregion

        #region Overloaded operators

        public static bool operator <(fsValue op1, fsValue op2)
        {
            if (!op1.Defined && !op2.Defined)
                return false;
            if (!op1.Defined)
                return true;
            if (!op2.Defined)
                return false;
            return op1.Value < op2.Value;
        }

        public static bool operator <=(fsValue op1, fsValue op2)
        {
            if (!op1.Defined && !op2.Defined)
                return false;
            if (!op1.Defined)
                return true;
            if (!op2.Defined)
                return false;
            return op1.Value <= op2.Value;
        }

        public static bool operator >(fsValue op1, fsValue op2)
        {
            return op2 < op1;
        }

        public static bool operator >=(fsValue op1, fsValue op2)
        {
            return op2 <= op1;
        }

        public static bool operator ==(fsValue op1, fsValue op2)
        {
            return op1.Equals(op2);
        }

        public static bool operator !=(fsValue op1, fsValue op2)
        {
            return !op1.Equals(op2);
        }

        public static fsValue operator -(fsValue op)
        {
            var res = new fsValue(-op.Value, op.Defined);
            return res;
        }

        public static fsValue operator +(fsValue op1, fsValue op2)
        {
            var res = new fsValue(op1.Value + op2.Value, op1.Defined && op2.Defined);
            return res;
        }

        public static fsValue operator +(fsValue op1, double op2)
        {
            var res = new fsValue(op1.Value + op2, op1.Defined);
            return res;
        }

        public static fsValue operator +(double op1, fsValue op2)
        {
            var res = new fsValue(op1 + op2.Value, op2.Defined);
            return res;
        }

        public static fsValue operator -(fsValue op1, fsValue op2)
        {
            var res = new fsValue(op1.Value - op2.Value, op1.Defined && op2.Defined);
            return res;
        }

        public static fsValue operator -(fsValue op1, double op2)
        {
            var res = new fsValue(op1.Value - op2, op1.Defined);
            return res;
        }

        public static fsValue operator -(double op1, fsValue op2)
        {
            var res = new fsValue(op1 - op2.Value, op2.Defined);
            return res;
        }

        public static fsValue operator *(fsValue op1, fsValue op2)
        {
            var res = new fsValue(op1.Value * op2.Value, op1.Defined && op2.Defined);
            return res;
        }

        public static fsValue operator *(fsValue op1, double op2)
        {
            var res = new fsValue(op1.Value * op2, op1.Defined);
            return res;
        }

        public static fsValue operator *(double op1, fsValue op2)
        {
            var res = new fsValue(op1 * op2.Value, op2.Defined);
            return res;
        }

        public static fsValue operator /(fsValue op1, fsValue op2)
        {
            var res = new fsValue
                          {
                              Defined = op1.Defined && op2.Defined && (op2.Value != 0.0)
                          };
            res.Value = res.Defined ? op1.Value / op2.Value : 1;
            return res;
        }

        public static fsValue operator /(fsValue op1, double op2)
        {
            var res = new fsValue {Defined = op1.Defined && (op2 != 0.0)};
            res.Value = res.Defined ? op1.Value / op2 : 1;
            return res;
        }

        public static fsValue operator /(double op1, fsValue op2)
        {
            var res = new fsValue {Defined = op2.Defined && (op2.Value != 0.0)};
            res.Value = res.Defined ? op1 / op2.Value : 1;
            return res;
        }

        #endregion

        #region Math functions

        #region Elementary functions

        public static fsValue Min(fsValue a, fsValue b)
        {
            return a < b ? a : b;
        }
        
        public static fsValue Max(fsValue a, fsValue b)
        {
            return a > b ? a : b;
        }

        public static fsValue Abs(fsValue op)
        {
            op.Value = Math.Abs(op.Value);
            return op;
        }

        public static fsValue Exp(fsValue op)
        {
            var res = new fsValue { Defined = op.Defined };
            res.Value = res.Defined ? Math.Exp(op.Value) : 1;
            return res;
        }

        public static fsValue Log(fsValue op)
        {
            var res = new fsValue { Defined = op.Defined && op.Value > 0 };
            res.Value = res.Defined ? Math.Log(op.Value) : 1;
            return res;
        }

        public static fsValue Pow(fsValue op1, fsValue degree)
        {
            var res = new fsValue
            {
                Defined =
                    op1.Defined && degree.Defined && (op1.Value > 0 || op1.Value == 0 && degree.Value > 0)
            };
            res.Value = res.Defined ? Math.Pow(op1.Value, degree.Value) : 1;
            return res;
        }

        public static fsValue Pow(fsValue op1, double degree)
        {
            var res = new fsValue { Defined = op1.Defined && op1.Value > 0 };
            res.Value = res.Defined ? Math.Pow(op1.Value, degree) : 1;
            return res;
        }

        public static fsValue Sqrt(fsValue op1)
        {
            var res = new fsValue { Defined = op1.Defined && op1.Value > 0 };
            res.Value = res.Defined ? Math.Sqrt(op1.Value) : 1;
            return res;
        }

        public static fsValue Sqr(fsValue op)
        {
            var res = new fsValue(op);
            res = res * op;
            return res;
        }

        #endregion      

        #region Specific

        public static bool Less(fsValue a, fsValue b)
        {
            var eps = new fsValue(1e-9 * Math.Max(Math.Abs(a.Value), Math.Abs(b.Value)));
            return a < b - eps;
        }

        public static bool Greater(fsValue a, fsValue b)
        {
            return Less(b, a);
        }

        public static fsValue Sign(fsValue beginValue, fsValue eps)
        {
            return new fsValue(Math.Abs(beginValue.Value) <= eps.Value ? 0 : beginValue.Value > 0 ? 1 : -1,
                               beginValue.Defined && eps.Defined);
        }

        public static fsValue Infinity()
        {
            return new fsValue(1e100);
        }

        public fsValue Round(int precision)
        {
            var result = new fsValue(0, Defined);

            if (Value == 0 || double.IsInfinity(Value))
            {
                return result;
            }

            double pMin = Math.Pow(10, precision - 1);
            double pMax = Math.Pow(10, precision);
            double factor = 1;
            const double eps = 1e-8;

            result.Value = Value;

            while (Math.Abs(result.Value) < pMin - eps)
            {
                result.Value *= 10;
                factor *= 10;
            }

            while (Math.Abs(result.Value) >= pMax - eps)
            {
                result.Value /= 10;
                factor /= 10;
            }

            result.Value = Math.Floor(result.Value + 0.5 + eps);
            result.Value /= factor;
            return result;
        }

        public fsValue RoundUp(fsValue x, int precision)
        {
            if (x.Value == 0)
                return x;

            double factor = GetFactor(x, precision);
            x.Value *= factor;
            x.Value = Math.Ceiling(x.Value - 1e-12);
            x.Value /= factor;
            return x;
        }

        public fsValue RoundDown(fsValue x, int precision)
        {
            if (x.Value == 0)
                return x;

            double factor = GetFactor(x, precision);
            x.Value *= factor;
            x.Value = Math.Floor(x.Value + 1e-12);
            x.Value /= factor;
            return x;
        }

        private static double GetFactor(fsValue x, int precision)
        {
            double pMin = Math.Pow(10, precision - 1);
            double pMax = Math.Pow(10, precision);
            double factor = 1;
            fsValue result = x;

            while (Math.Abs(result.Value) < pMin)
            {
                result.Value *= 10;
                factor *= 10;
            }

            while (Math.Abs(result.Value) >= pMax)
            {
                result.Value /= 10;
                factor /= 10;
            }

            return factor;
        }

        public static int EpsCompare(double x, double y, double eps)
        {
            double a = Math.Max(Math.Abs(x), Math.Abs(y));
            eps = Math.Max(eps, eps * a);
            if (Math.Abs(x - y) <= eps)
            {
                return 0;
            }
            return x < y ? -1 : 1;
        }

        #endregion

        #endregion
    }
}