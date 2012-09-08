using System;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        
        [ComparableProperty]
        public string Title { get; set; }

        [ComparableProperty]
        public string Abstract { get; set; }

        [ComparableProperty]
        public DateTime Start { get; set; }

        [ComparableProperty]
        public DateTime End { get; set; }

        [ComparableProperty]
        public Level Level { get; set; }

        [ComparableProperty]
        public Track Track { get; set; }

        [ComparableProperty]
        public Room Room { get; set; }

        public int SpeakerId { get; set; }
    }
}
