using System;

namespace Codemash.Client.Core
{
    public static class ParsingExtensionMethods
    {
        public static int AsInt(this string str)
        {
            int intVal;
            return int.TryParse(str, out intVal) ? intVal : int.MinValue;
        }

        public static DateTime AsDateTime(this string str)
        {
            DateTime dtVal;
            if (!DateTime.TryParse(str, out dtVal) || dtVal == DateTime.MinValue)
                return DateTime.MinValue;

            dtVal = DateTime.SpecifyKind(DateTime.Parse(str), DateTimeKind.Utc);

            // account for GMT Offset (EST -5)
            return dtVal.ToLocalTime();
        }

        /// <summary>
        /// Convert a string to its long representation, return MinValue if the parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long AsLong(this string str)
        {
            long longVal;
            return long.TryParse(str, out longVal) ? longVal : long.MinValue;
        }

        public static long AsLong(this string str, long defaultValue)
        {
            long value = str.AsLong();
            return value == long.MinValue ? defaultValue : value;
        }
    }
}
