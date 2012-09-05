using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public class MasterSpeaker
    {
        public int SpeakerId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Twitter { get; set; }
        public string EmailAddress { get; set; }
        public string BlogUrl { get; set; }
    }
}
