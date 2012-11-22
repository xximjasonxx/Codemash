using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codemash.DeltaApi.Handlers
{
    /// <summary>
    /// Summary description for Phone7Notification
    /// </summary>
    public class Phone7Notification : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            // get the file name that we are seeking

        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}