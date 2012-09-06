using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Server.Core.Extensions
{
    public static class ParsingExtensions
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
    }
}
