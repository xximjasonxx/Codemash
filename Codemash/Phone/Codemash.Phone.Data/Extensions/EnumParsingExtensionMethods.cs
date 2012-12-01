using System;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Extensions
{
    public static class EnumParsingExtensionMethods
    {
        public static ActionType AsActionTypeEnum(this string str)
        {
            return (ActionType) Enum.Parse(typeof (ActionType), str, true);
        }
    }
}
