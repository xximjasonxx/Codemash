using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Core
{
    public static class ParsingExtensionMethods
    {
        public static int AsInt(this string str)
        {
            int intVal;
            return int.TryParse(str, out intVal) ? intVal : int.MinValue;
        }
    }
}
