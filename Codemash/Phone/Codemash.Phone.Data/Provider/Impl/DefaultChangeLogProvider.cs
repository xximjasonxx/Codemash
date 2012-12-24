using System.Collections.Generic;

namespace Codemash.Phone.Data.Provider.Impl
{
    public class DefaultChangeLogProvider : IChangeLogProvider
    {
        private IList<ProviderLogEntry> _sessionChangeLog;
        public IList<ProviderLogEntry> SessionChangeLog
        {
            get { return _sessionChangeLog ?? (_sessionChangeLog = new List<ProviderLogEntry>()); }
        }

        private IList<ProviderLogEntry> _speakerChangeLog;
        public IList<ProviderLogEntry> SpeakerChangeLog
        {
            get { return _speakerChangeLog ?? (_speakerChangeLog = new List<ProviderLogEntry>()); }
        }

        public void Clear()
        {
            _sessionChangeLog = null;
            _speakerChangeLog = null;
        }
    }
}
