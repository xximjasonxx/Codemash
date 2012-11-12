
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
        public string Name { get; set; }

        public Speaker()
        {
            Biography = string.Empty;
            Twitter = string.Empty;
            EmailAddress = string.Empty;
            BlogUrl = string.Empty;
            Name = string.Empty;
        }

        #region Implementation of IHasIdentifier

        public int ID { get { return SpeakerId; } }

        #endregion
    }
}
