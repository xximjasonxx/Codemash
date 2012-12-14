using Codemash.Client.Core;
using Windows.Storage;

namespace Codemash.Client.Data.Repository.Impl
{
    public class ApplicationSettingsRepository : ISettingsRepository
    {
        private readonly ApplicationDataContainer _dataContainer;

        public ApplicationSettingsRepository()
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

        public int RegisteredClientId
        {
            get { return new StringWrapper(_dataContainer.Values["ClientId"]).ToString().AsInt(); }
            set { _dataContainer.Values["ClientId"] = value; }
        }
    }
}
