using System;

namespace Codemash.DeltaApi.Models
{
    public class SessionViewModel
    {
        public string Abstract { get; set; }

        public string End { get; set; }

        public string Level { get; set; }

        public string Room { get; set; }

        public int SessionId { get; set; }

        public int SpeakerId { get; set; }

        public string Start { get; set; }

        public string Title { get; set; }

        public string Track { get; set; }
    }
}