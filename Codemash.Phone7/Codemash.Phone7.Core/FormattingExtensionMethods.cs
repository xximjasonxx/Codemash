using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Codemash.Phone7.Core
{
    public static class FormattingExtensionMethods
    {
        public static string AsBlockDisplay(this DateTime dtVal)
        {
            return dtVal.ToString("M/d hh:mm");
        }

        public static string AsParameterString(this IDictionary<string, string> configValues)
        {
            StringBuilder sb = new StringBuilder();
            var isFirst = true;

            foreach (var kv in configValues)
            {
                if (!isFirst)
                    sb.Append("&");
                sb.AppendFormat("{0}={1}", kv.Key, kv.Value);
                isFirst = false;
            }

            return sb.ToString();
        }

        public static string AsTimeDisplay(this DateTime dt)
        {
            return dt.ToString("h:mmt").ToLower();
        }

        public static string AsFullDurationString(this TimeSpan ts)
        {
            StringBuilder sb = new StringBuilder();
            if (ts.Days > 0)
                sb.AppendFormat("{0} day{1} ", ts.Days, ts.Days == 1 ? string.Empty : "s");

            if (ts.Hours > 0)
                sb.AppendFormat("{0} hour{1} ", ts.Hours, ts.Hours == 1 ? string.Empty : "s");

            if (ts.Minutes > 0)
                sb.AppendFormat("{0} minute{1} ", ts.Minutes, ts.Minutes == 1 ? string.Empty : "s");

            Regex regex = new Regex(@" $");
            return regex.Replace(sb.ToString(), string.Empty);
        }
    }
}
