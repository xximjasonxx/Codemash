using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Code
{
    internal static class ParsingExtensionMethods
    {
        internal static DisplayModeType AsDisplayType(this string str)
        {
            DisplayModeType result;
            return Enum.TryParse(str, true, out result) ? result : DisplayModeType.Filled;
        }
    }
}
