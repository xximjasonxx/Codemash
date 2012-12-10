using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Core;
using Windows.Storage;

namespace Codemash.Client.Common.Services.Impl
{
    public class ApplicationSettingsService : ISettingsService
    {
        private readonly ApplicationDataContainer _dataContainer;

        public ApplicationSettingsService()
        {
            var dataContainer = ApplicationData.Current.LocalSettings;
            if (!dataContainer.Containers.ContainsKey("AppSettings"))
            {
                // create the storage location
                _dataContainer = dataContainer.CreateContainer("AppSettings", ApplicationDataCreateDisposition.Always);
            }
            else
            {
                _dataContainer = dataContainer.Containers["AppSettings"];
            }
        }

        public string RegisteredChannelUri
        {
            get { return new StringWrapper(_dataContainer.Values["ChannelUri"]).ToString(); }
            set { _dataContainer.Values["ChannelUri"] = value; }
        }
    }
}
