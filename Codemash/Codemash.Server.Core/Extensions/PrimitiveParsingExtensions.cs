using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            DateTime dtValue;
            return DateTime.TryParse(str, out dtValue) ? dtValue : DateTime.MinValue;
        }
    }
}
