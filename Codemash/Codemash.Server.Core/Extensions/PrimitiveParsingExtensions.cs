using System;

namespace Codemash.Server.Core.Extensions
{
    public static class PrimitiveParsingExtensions
    {
        /// <summary>
        /// Return a string parsed to an integer - return MinValue if the parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsInt(this string str)
        {
            int intVal;
            return int.TryParse(str, out intVal) ? intVal : int.MinValue;
        }

        /// <summary>
        /// Return a string parsed to a DateTime struct. Return MinValue for DateTime if the parse fails
        /// </summary>
        /// <param name="str">The string to be parsed to a DateTime instance</param>
        /// <returns>A parsed DateTime instance</returns>
        public static DateTime AsDateTime(this string str)
        {
            DateTime dtVal;
            if (!DateTime.TryParse(str, out dtVal))
                return DateTime.MinValue;
            
            return DateTime.SpecifyKind(DateTime.Parse(str), DateTimeKind.Utc);
        }

        /// <summary>
        /// Return a string representation of a datetime object with both the date and time portions included in the string
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string AsDateTimeDisplay(this DateTime dt)
        {
            return dt.ToString("MM/dd/yyyy hh:mmtt");
        }
    }
}
