using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data.Extensions
{
    public static class EnumParsingExtensionMethods
    {
        public static ActionType AsActionTypeEnum(this string str)
        {
            return (ActionType)Enum.Parse(typeof(ActionType), str, true);
        }
    }
}
