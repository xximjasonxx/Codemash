using System.ComponentModel.DataAnnotations;

namespace Codemash.Api.Data.Entities
{
    public class Client : EntityBase
    {
        [Key]
        public string ChannelUri { get; set; }

        public string ClientType { get; set; }

        public int CurrentChangeSet { get; set; }
    }
}
