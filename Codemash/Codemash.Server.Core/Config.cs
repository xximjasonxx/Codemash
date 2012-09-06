using System.Configuration;
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
    }
}
