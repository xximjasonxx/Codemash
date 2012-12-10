using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Codemash.Notification.Ex;

namespace Codemash.Notification.Context.Impl
{
    public class Win8SecurityContext : ISecurityContext
    {
        private string _tokenType;
        private string _accessToken;

        public string TokenType
        {
            get
            {
                if (string.IsNullOrEmpty(_tokenType))
                    throw new SecurityContextException("Security Context has not provisioned Token Type");

                return _tokenType;
            }
            private set { _tokenType = value; }
        }

        public string AccessToken
        {
            get
            {
                if (string.IsNullOrEmpty(_accessToken))
                    throw new SecurityContextException("Security Context has not provisioned Access Token");

                return _accessToken;
            }
            private set { _accessToken = value; }
        }

        public void Provision(string clientId, string clientSecret)
        {
            var urlEncodedClientId = HttpUtility.UrlEncode(clientId);
            var urlEncodedSecret = HttpUtility.UrlEncode(clientSecret);

            var body = String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=notify.windows.com",
                urlEncodedClientId, urlEncodedSecret);

            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string response = client.UploadString("https://login.live.com/accesstoken.srf", body);
                var result = response.ParseAuthorizationToken();

                TokenType = result.TokenType;
                AccessToken = result.AccessToken;
            }
        }
    }
}
