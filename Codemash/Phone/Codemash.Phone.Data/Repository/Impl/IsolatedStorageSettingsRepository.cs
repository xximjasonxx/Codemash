using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Codemash.Phone.Core;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class IsolatedStorageSettingsRepository : ISettingsRepository
    {
        private const string IsolatedStorageFilename = "Codemash.data";
        private const string HasSeenListPageKeyName = "HasSeenList";

        public IsolatedStorageSettingsRepository()
        {
            var config = GetValues();

            // set values
            HasSeenListPage = config.ContainsKey(HasSeenListPageKeyName) && config[HasSeenListPageKeyName].AsBoolean(false);
        }

        #region Implementation of ISettingsRepository

        /// <summary>
        /// Indicates whether the user has seen the Session List page
        /// </summary>
        public bool HasSeenListPage { get; set; }

        #endregion

        private IDictionary<string, string> GetValues()
        {
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var stream = new StreamReader(storage.OpenFile(IsolatedStorageFilename, FileMode.OpenOrCreate, FileAccess.Read)))
                {
                    var fileContents = stream.ReadToEnd();
                    if (fileContents.Length == 0)
                        return new Dictionary<string, string>();

                    return fileContents.Split(new[] {'&'})
                        .ToDictionary(s => s.Split(new[] {'='})[0], s => s.Split(new[] {'='})[1]);
                }
            }
        }

        private void SaveValues(IDictionary<string, string> configValues)
        {
            var parameterString = configValues.AsParameterString();
            using (var storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var file = new StreamWriter(storage.OpenFile(IsolatedStorageFilename, FileMode.Create, FileAccess.Write)))
                {
                    file.Write(parameterString);
                }
            }
        }

        public void Save()
        {
            var config = GetValues();
            config[HasSeenListPageKeyName] = HasSeenListPage.ToString();

            SaveValues(config);
        }
    }
}
