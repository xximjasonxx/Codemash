using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Server.Core;

namespace Codemash.Api.Data.Entities
{
    public class Session
    {
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Level Difficulty { get; set; }
        public Track Track { get; set; }
        public Room Room { get; set; }

        //public string SpeakerName { get; set; }
        public int SpeakerId { get; set; }
    }
}
