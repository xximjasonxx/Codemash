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

        public static DateTime AsDateTime(this string str, bool isZulu = false)
        {
            DateTime dtVal;
            if (!DateTime.TryParse(str, out dtVal))
                return DateTime.MinValue;

            if (isZulu)
            {
                dtVal = DateTime.SpecifyKind(DateTime.Parse(str), DateTimeKind.Utc);
            }

            return dtVal.ToLocalTime();
        }

        /// <summary>
        /// Takes a string and parses it a unique key value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsKey(this string str)
        {
            return str.GetHashCode();
        }
    }
}
