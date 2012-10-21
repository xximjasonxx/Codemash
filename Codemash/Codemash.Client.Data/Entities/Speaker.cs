using SQLite;

namespace Codemash.Client.Data.Entities
{
    public class Speaker : EntityBase
    {
        public string Name { get; internal set; }
        public string Twitter { get; internal set; }
        public string Biography { get; internal set; }
        public string BlogUrl { get; internal set; }

        [PrimaryKey]
        public int SpeakerId { get; internal set; }
    }
}
