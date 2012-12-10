
using Codemash.Notification.Result;
using Codemash.Server.Core;
using Newtonsoft.Json.Linq;

namespace Codemash.Notification
{
    internal static class ExtensionMethods
    {
        internal static AuthorizationResult ParseAuthorizationToken(this string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);
            return new AuthorizationResult
                       {
                           TokenType = new StringWrapper(jsonObject["token_type"]).ToString(),
                           AccessToken = new StringWrapper(jsonObject["access_token"]).ToString()
                       };
        }
    }
}
