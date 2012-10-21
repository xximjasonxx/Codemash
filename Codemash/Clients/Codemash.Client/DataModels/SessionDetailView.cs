using Codemash.Client.Core;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.DataModels
{
    public class SessionDetailView
    {
        public SessionDetailView(Session session)
        {
            Title = session.Title;
            Abstract = session.Abstract;
            Room = session.Room;
            Technology = session.Technology;
            Difficulty = session.Difficulty;
            Starts = session.Starts.AsBlockDisplay();
            SessionId = session.SessionId;
            SpeakerId = session.SpeakerId;
        }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Room { get; set; }
        public string Technology { get; set; }
        public string Difficulty { get; set; }
        public string Starts { get; set; }
        public int SessionId { get; set; }
        public int SpeakerId { get; set; }
        public string SpeakerName { get; set; }
    }
}
