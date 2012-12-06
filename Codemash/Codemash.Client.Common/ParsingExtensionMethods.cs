using System;

namespace Codemash.Client.Common
{
    public static class ParsingExtensionMethods
    {
        public static DisplayModeType AsDisplayType(this string str)
        {
            DisplayModeType result;
            return Enum.TryParse(str, true, out result) ? result : DisplayModeType.Filled;
        }
    }
}
