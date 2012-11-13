using System;
using SQLite;

namespace Codemash.Client.Data.Entities
{
    public class Session : EntityBase
    {
        [PrimaryKey]
        public int SessionId { get; internal set; }

        public string Title { get; internal set; }
        public string Abstract { get; internal set; }
        public DateTime Starts { get; internal set; }
        public DateTime Ends { get; internal set; }

        public string Difficulty { get; internal set; }
        public string Room { get; internal set; }
        public int SpeakerId { get; internal set; }
        public string Technology { get; internal set; }

        [Ignore]
        public TimeSpan Duration
        {
            get { return Ends - Starts; }
        }
    }
}
