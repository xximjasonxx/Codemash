using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Common.Services
{
    public interface ISettingsService
    {
        /// <summary>
        /// Returns the registered Channel URI as stored by application. This can be updated if the URI changes
        /// </summary>
        string RegisteredChannelUri { get; set; }
    }
}
