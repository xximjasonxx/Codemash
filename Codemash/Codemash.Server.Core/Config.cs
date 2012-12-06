using System.Configuration;
using System.IO;
using Codemash.Server.Core.Extensions;

namespace Codemash.Server.Core
{
    public static class Config
    {
        /// <summary>
        /// Number of minutes to wait between checks against the Master sources
        /// </summary>
        public static int MinutesWaitTime
        {
            get
            {
                int waitTime = ConfigurationManager.AppSettings["MinutesWaitTime"].AsInt();
                if (waitTime == int.MinValue)
                    throw new ConfigurationErrorsException("MinutesWaitTime is not defined in the configuration context");

                return waitTime;
            }
        }

        public static string DriveRoot
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DriveRoot"];
                if (string.IsNullOrEmpty(value))
                    throw new ConfigurationErrorsException("No DriveRoot specified");

                return value;
            }
        }

        public static string DeltaApiDomain
        {
            get
            {
                string value = ConfigurationManager.AppSettings["DeltaApiDomain"];
                if (string.IsNullOrEmpty(value))
                    throw new ConfigurationErrorsException("No Domain for the DeltaApi Provided");

                return value;
            }
        }

        public static string PackageSecurityToken
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PackageSecurityToken"];
                if (string.IsNullOrEmpty(value))
                    throw new ConfigurationErrorsException("No Package Security Token found");

                return value;
            }
        }

        public static string PackageSecret
        {
            get
            {
                string value = ConfigurationManager.AppSettings["PackageSecret"];
                if (string.IsNullOrEmpty(value))
                    throw new ConfigurationErrorsException("No Package Secret Found");

                return value;
            }
        }
    }
}
