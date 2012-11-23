using System.IO;
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
            get
            {
                using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var stream = new StreamReader(storage.OpenFile(SeenListPageKey, FileMode.Open, FileAccess.Read)))
                    {
                        return stream.ReadLine().Trim().AsBoolean(false);
                    }
                }
            }
            set
            {
                using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var stream = new StreamWriter(storage.OpenFile(SeenListPageKey, FileMode.Create, FileAccess.Write)))
                    {
                        stream.WriteLine(value.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Stores the Push Channel URI for the application which Push will use for communication
        /// </summary>
        public string PushChannelUri
        {
            get
            {
                using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (var stream = new StreamReader(storage.OpenFile(ClientUriKey, FileMode.Open, FileAccess.Read)))
                        {
                            string channelUri = stream.ReadLine().Trim();
                            if (string.IsNullOrEmpty(channelUri))
                                throw new ChannelNotInitializedException();

                            return channelUri;
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        throw new ChannelNotInitializedException();
                    }
                }
            }
            set
            {
                using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (var stream = new StreamWriter(storage.OpenFile(ClientUriKey, FileMode.Create, FileAccess.Write)))
                    {
                        stream.WriteLine(value);
                    }
                }
            }
        }

        #endregion
    }
}
