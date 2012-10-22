using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.DataModels
{
    public class SessionDetailModel
    {
        public string Title { get; set; }
        public string Technology { get; set; }
        public string Room { get; set; }
        public string StartsAt { get; set; }
        public string Duration { get; set; }
        public string Difficulty { get; set; }
        public string Abstract { get; set; }

        public SpeakerDetailModel Speaker { get; set; }
    }
}
