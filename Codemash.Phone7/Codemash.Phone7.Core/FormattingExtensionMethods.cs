using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
