using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace CalculatorModules
{
    public static class fsMisc
    {
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);

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

        public static void FillList(IList collection, Type enumType)
        {
            collection.Clear();
            foreach (Enum element in Enum.GetValues(enumType))
            {
                collection.Add(GetEnumDescription(element));
            }
        }
    }
}