using System;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Extensions
{
    public static class EnumParsingExtensionMethods
    {
        public static ActionType AsActionTypeEnum(this string str)
        {
            if (!Enum.IsDefined(typeof(ActionType), str))
                throw new InvalidOperationException("Cannot parse the action type to an available ActionType enum value");

            return (ActionType) Enum.Parse(typeof (ActionType), str, true);
        }
    }
}
