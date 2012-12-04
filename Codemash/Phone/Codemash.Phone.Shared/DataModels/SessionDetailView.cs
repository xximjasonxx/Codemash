
namespace Codemash.Phone.Shared.DataModels
{
    public class SessionDetailView
    {
        public string Title { get; set; }
        public string Technology { get; set; }
        public string Starts { get; set; }
        public string Difficulty { get; set; }
        public string Room { get; set; }
        public string Abstract { get; set; }
        public string Duration { get; set; }

        public SpeakerDetailView Speaker { get; set; }

        public bool IsFavorite { get; set; }
    }
}
