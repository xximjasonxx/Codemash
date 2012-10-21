using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Codemash.Client.Core
{
    public static class FormattingExtensionMethods
    {
        public static string AsBlockDisplay(this DateTime dtVal)
        {
            return dtVal.ToString("M/d hh:mm");
        }

        public static string AsDurationString(this TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();
            if (ts.Days > 0)
                sb.AppendFormat("{0}d ", ts.Days);

            if (ts.Hours > 0)
                sb.AppendFormat("{0}h ", ts.Hours);

            if (ts.Minutes > 0)
                sb.AppendFormat("{0}m ", ts.Minutes);

            Regex regex = new Regex(@" $");
            return "Duration: " + regex.Replace(sb.ToString(), string.Empty);
        }

        public static string AsTimeDisplay(this DateTime dt)
        {
            return dt.ToString("hh:mmt");
        }
    }
}
