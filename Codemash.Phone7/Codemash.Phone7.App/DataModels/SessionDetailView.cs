
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.App.DataModels
{
    public class SessionDetailView
    {
        public string Title { get; set; }
        public string Technology { get; set; }
        public string Starts { get; set; }
        public string Difficulty { get; set; }
        public string Room { get; set; }
        public string Abstract { get; set; }

        public Speaker Speaker { get; set; }
    }
}
