using System.IO.IsolatedStorage;
using Codemash.Phone.Core;
using Codemash.Phone.Core.Ev;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class IsolatedStorageSettingsRepository : ISettingsRepository
    {
        private const string SeenListPageKey = "SeenListPage";
        private const string ClientUriKey = "ClientUri";

        #region Implementation of ISettingsRepository

        /// <summary>
        /// Indicates whether the user has seen the Session List page
        /// </summary>
        public bool HasSeenListPage
        {
            get { return IsolatedStorageSettings.ApplicationSettings[SeenListPageKey].ToString().AsBoolean(false); }
            set { IsolatedStorageSettings.ApplicationSettings[SeenListPageKey] = value.ToString(); }
        }

        /// <summary>
        /// Stores the Push Channel URI for the application which Push will use for communication
        /// </summary>
        public string PushChannelUri
        {
            get
            {
                if (!IsolatedStorageSettings.ApplicationSettings.Contains(ClientUriKey) ||
                    string.IsNullOrEmpty(IsolatedStorageSettings.ApplicationSettings[ClientUriKey].ToString()))
                    throw new ChannelNotInitializedException();

                return IsolatedStorageSettings.ApplicationSettings[ClientUriKey].ToString();
            }
            set { IsolatedStorageSettings.ApplicationSettings[ClientUriKey] = value; }
        }

        #endregion
    }
}
