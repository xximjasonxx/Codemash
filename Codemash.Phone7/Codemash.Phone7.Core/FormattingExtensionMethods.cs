using System;

namespace Codemash.Phone7.Core
{
    public static class FormattingExtensionMethods
    {
        public static string AsBlockDisplay(this DateTime dtVal)
        {
            return dtVal.ToString("M/d hh:mm");
        }
    }
}
