
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Codemash.Api.Data.Entities.Interfaces;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Speaker : EntityBase, IHasIdentifier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SpeakerId { get; set; }

        [Comparable, Required]
        public string Biography { get; set; }

        [Comparable, StringLength(100)]
        public string Twitter { get; set; }

        [Comparable, Required, StringLength(100)]
        public string EmailAddress { get; set; }

        [Comparable, StringLength(100)]
        public string BlogUrl { get; set; }

        [Comparable, Required, StringLength(100)]
        public string FirstName { get; set; }

        [Comparable, Required, StringLength(100)]
        public string LastName { get; set; }

        [Comparable, StringLength(100)]
        public string Company { get; set; }

        public ICollection<Session> Sessions { get; set; }

        public Speaker()
        {
            Biography = string.Empty;
            Twitter = string.Empty;
            EmailAddress = string.Empty;
            BlogUrl = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Company = string.Empty;
        }

        #region Implementation of IHasIdentifier

        public int ID { get { return SpeakerId; } }

        #endregion
    }
}
