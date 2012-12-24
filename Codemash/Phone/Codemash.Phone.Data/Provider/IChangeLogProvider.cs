using System.Collections.Generic;

namespace Codemash.Phone.Data.Provider
{
    public interface IChangeLogProvider
    {
        /// <summary>
        /// Get a log of the Session Changes which occurd
        /// </summary>
        IList<ProviderLogEntry> SessionChangeLog { get; }

        /// <summary>
        /// Get a log of the Speaker Changes which occured
        /// </summary>
        IList<ProviderLogEntry> SpeakerChangeLog { get; }

        /// <summary>
        /// Clear provider logs
        /// </summary>
        void Clear();
    }
}
