using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data.Entities
{
    public class Session : EntityBase
    {
        public int SessionId { get; internal set; }
        public string Title { get; internal set; }
        public string Abstract { get; internal set; }
        public DateTime Starts { get; internal set; }
        public DateTime Ends { get; internal set; }
        public string Level { get; internal set; }
        public string Room { get; internal set; }
        public int SpeakerId { get; internal set; }
        public string Track { get; internal set; }

        public string SpeakerName { get; set; }
    }
}
