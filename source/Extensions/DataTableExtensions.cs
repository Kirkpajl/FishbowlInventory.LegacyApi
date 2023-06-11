using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FishbowlInventory.Serialization;

namespace FishbowlInventory.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Parse the CSV <paramref name="dataTable"/> into a collection of new instances of the <typeparamref name="T"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static T[] ToObjects<T>(this DataTable dataTable) where T : new()
        {
            // Ensure that a valid DataTable was provided
            if (dataTable == null) return Array.Empty<T>();

            // define return list
            List<T> items = new List<T>();

            // go through each row
            foreach (DataRow r in dataTable.Rows)
            {
                // add to the list
                items.Add(ToObject<T>(r));
            }

            // return the list
            return items.ToArray();
        }

        /// <summary>
        /// Parse the CSV <paramref name="dataRow"/> into a new instance of the <typeparamref name="T"/> class.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public static T ToObject<T>(this DataRow dataRow)where T : new()
        {
            T item = new T();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                // Check if the class has a property matching the column name (or a compatible CsvProperty attribute)
                PropertyInfo property = GetProperty<T>(column.ColumnName);
                if (property == null) continue;

                // Ensure that the class property doesn't have the CsvIgnore attribute
                if (Attribute.IsDefined(property, typeof(CsvIgnoreAttribute))) continue;

                // Parse the cell value and insert it in the class object
                if (dataRow[column] != DBNull.Value && dataRow[column].ToString() != "NULL")
                {
                    property.SetValue(item, ChangeType(dataRow[column], property.PropertyType), null);
                }
            }

            return item;
        }

        /// <summary>
        /// Retrieve the Property corresponding to the CSV Column Name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private static PropertyInfo GetProperty<T>(string columnName)
        {
            Type type = typeof(T);

            // Search for a property matching the Column Name
            PropertyInfo property = type.GetProperty(columnName);
            if (property != null) return property;

            // If no property name was matched, search for any property with a corresponding CsvPropertyAttribute
            return type.GetProperties()
                 .Where(p => p.IsDefined(typeof(CsvPropertyNameAttribute), false) && p.GetCustomAttributes(typeof(CsvPropertyNameAttribute), false).Cast<CsvPropertyNameAttribute>().Single().Name == columnName)
                 //.Where(p => p.IsDefined(typeof(DisplayAttribute), false) && p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().Single().Name == columnName)
                 .FirstOrDefault();
        }

        /// <summary>
        /// Case the object to the specified type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            // Handle the nullable types
            if (type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                type = Nullable.GetUnderlyingType(type);

            // Parse the enum as a string
            if (type.IsEnum)
            {
                if (value == null) return default(Enum);

                return Enum.TryParse(type, value.ToString(), out object en) ? en : default(Enum);  //return Enum.Parse(type, value.ToString());
            }

            // Date/Time
            if (type == typeof(DateTime))
            {
                if (value == null) return DateTime.MinValue;

                return DateTime.TryParse(value.ToString(), out DateTime dt) ? dt : default;
            }

            // 32-bit Integer
            if (type == typeof(int))
            {
                if (value == null) return null;

                return int.TryParse(value.ToString(), out int i) ? i : default;
            }

            // Double
            if (type == typeof(double))
            {
                if (value == null) return null;

                return double.TryParse(value.ToString(), out double d) ? d : default;
            }

            // Decimal
            if (type == typeof(decimal))
            {
                if (value == null) return null;

                return decimal.TryParse(value.ToString(), out decimal d) ? d : default;
            }

            // TimeSpan
            if (type == typeof(TimeSpan))
            {
                if (value == null) return null;

                return TimeSpan.TryParse(value.ToString(), out TimeSpan t) ? t : default;
            }

            // Boolean
            if (type == typeof(bool))
            {
                if (value == null) return null;

                return bool.TryParse(value.ToString(), out bool b) && b;
            }

            // Attempt to parse any other type
            try
            {
                return Convert.ChangeType(value, type);
            }
            catch
            {
                return default;
            }
        }
    }
}
