using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Data
{
    public class Session
    {
        public string Title { get; set; }
        public string SpeakerName { get; set; }
        public string Room { get; set; }
        public DateTime StartsAt { get; set; }
        public string Track { get; set; }
        public string Level { get; set; }

        public string StartsAtDisplay
        {
            get { return StartsAt.ToString("d"); }
        }

        public string RoomTimeDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(Room) && StartsAt == DateTime.MinValue)
                    return string.Empty;
                
                return string.Format("{0} - {1}", Room, StartsAtDisplay);
            }
        }

        public string TrackLevelDisplay
        {
            get
            {
                if (string.IsNullOrEmpty(Track) && string.IsNullOrEmpty(Level))
                    return string.Empty;

                return string.Format("{0} - {1}", Track, Level);
            }
        }

        public string Abstract { get; set; }
    }
}
