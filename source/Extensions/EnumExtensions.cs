using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace FishbowlInventory.Extensions
{
    internal static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            // Retrieve the display name property
            string displayName = enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();

            // If the attribute is missing, cast the value to a string
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = enumValue.ToString();
            }

            // Return the result
            return displayName;
        }

        /*public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }*/
    }
}
