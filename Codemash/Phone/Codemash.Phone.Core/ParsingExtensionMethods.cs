
using System;

namespace Codemash.Phone.Core
{
    public static class ParsingExtensionMethods
    {
        /// <summary>
        /// Takes a string and parses it a unique key value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsKey(this string str)
        {
            return str.GetHashCode();
        }

        /// <summary>
        /// Convert a string to its int representation, return MinValue if the parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int AsInt(this string str)
        {
            int intVal;
            return int.TryParse(str, out intVal) ? intVal : int.MinValue;
        }

        /// <summary>
        /// Convert a string to its boolean representation, return the default is the parse fails
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool AsBoolean(this string str, bool defaultValue)
        {
            bool boolVal;
            return bool.TryParse(str, out boolVal) ? boolVal : defaultValue;
        }

        /// <summary>
        /// Convert a string to its DateTime representation or the Minimum Allowable Value
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime AsDateTime(this string str)
        {
            DateTime dtVal;
            return DateTime.TryParse(str, out dtVal) ? dtVal : DateTime.MinValue;
        }
    }
}
