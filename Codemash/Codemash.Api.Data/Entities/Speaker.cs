
namespace Codemash.Api.Data.Entities
{
    public class Speaker : EntityBase
    {
        public int SpeakerId { get; set; }
        public string Biography { get; set; }
        public string Twitter { get; set; }
        public string EmailAddress { get; set; }
        public string BlogUrl { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
    }
}
