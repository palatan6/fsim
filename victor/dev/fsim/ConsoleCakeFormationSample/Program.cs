using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Parameters;
using Value;
using StepCalculators;

namespace ConsoleCakeFormationSample
{
    class Program
    {
        enum Enum1
        {
            [Description("e1-f1")]
            field1,
            [Description("e1-f2")]
            field2
        }

        enum Enum2
        {
            [Description("en2-f1")]
            field1,
            [Description("en2-f2")]
            field2
        }

        static void Main(string[] args)
        {
            Dictionary<string, object > d = new Dictionary<string, object>();
            d["str1"] = Enum1.field1;
            d["str2"] = Enum2.field2;
            System.Console.WriteLine(GetEnumDescription((Enum)d["str1"]));
            System.Console.WriteLine(GetEnumDescription((Enum)d["str2"]));
            d["str1"] = GetEnum(d["str1"].GetType(), "e1-f2");
            System.Console.WriteLine(GetEnumDescription((Enum)d["str1"]));
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0
                ? attributes[0].Description
                : value.ToString();
        }

        public static Enum GetEnum(Type enumType, string description)
        {
            foreach (Enum element in Enum.GetValues(enumType))
            {
                if (GetEnumDescription(element) == description)
                    return element;
            }
            throw new Exception("Desired description not found in given enum type.");
        }
    }
}
