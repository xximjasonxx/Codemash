using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Provider
{
    public class ProviderLogEntry
    {
        public long EntityId { get; set; }
        public ActionType ActionType { get; set; }
        public string EntityDisplay { get; set; }
    }
}
