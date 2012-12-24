using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Phone.Core
{
    public static class Config
    {
        public static string DeltaApiDomain
        {
            get { return "http://codemashdelta.azurewebsites.net"; }
        }

        public static string DeltaApiRoot
        {
            get { return DeltaApiDomain + "/api"; }
        }
    }
}
