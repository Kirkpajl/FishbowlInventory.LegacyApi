using System;

namespace FishbowlInventory.Extensions
{
    internal static class DateTimeExtensions
    {
        public static string ToFishbowlDateString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd");

        public static DateTime DateTimeOrDefault(this object obj, DateTime defaultValue) => DateTime.TryParse((obj ?? "").ToString(), out DateTime result) ? result : defaultValue;
    }
}
