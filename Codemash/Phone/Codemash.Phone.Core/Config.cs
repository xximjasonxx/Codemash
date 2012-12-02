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
            get { return "http://192.168.1.20/"; }
        }

        public static string DeltaApiRoot
        {
            get { return DeltaApiDomain + "DeltaApi/api"; }
        }
    }
}
