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
    }
}
