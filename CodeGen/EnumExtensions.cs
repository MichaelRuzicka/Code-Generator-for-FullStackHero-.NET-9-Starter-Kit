using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    internal class EnumExtensions
    {
        public static string GetEnumValue<T>(T enumValue) where T : Enum
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);
            return ((EnumMemberAttribute)attributes[0]).Value;
        }

        public static T GetEnumFromString<T>(string value) where T : Enum
        {
            var type = typeof(T);
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) as EnumMemberAttribute;
                if (attribute != null && attribute.Value == value)
                {
                    return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException($"Not a enum value: {value}");
        }

    }
}
