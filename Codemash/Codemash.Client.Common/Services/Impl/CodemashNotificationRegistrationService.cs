using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codemash.Client.Common.Result;
using Codemash.Client.Core;

namespace Codemash.Client.Common.Services.Impl
{
    public class CodemashNotificationRegistrationService : INotificationRegistrationService
    {
        public async Task<RegistrationResult> Register(string channelUri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var postUrl = string.Format("{0}/Notification", Config.DeltaApiRoot);
                    var content = new StringContent(@"{""ChannelUri"": """ + channelUri + @""", ""ClientType"": ""Win8""}");
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.PostAsync(postUrl, content);
                    return response.StatusCode == HttpStatusCode.OK
                               ? new RegistrationResult(true)
                               : new RegistrationResult(false);
                }
            }
            catch (Exception)
            {
                return new RegistrationResult(false);
            }
        }
    }
}
