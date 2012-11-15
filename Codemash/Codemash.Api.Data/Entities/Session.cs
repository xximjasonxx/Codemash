using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Codemash.Api.Data.Entities.Interfaces;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Session : EntityBase, IHasIdentifier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SessionId { get; set; }

        [Comparable]
        [Required]
        public long SpeakerId { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }

        [Comparable, Required, StringLength(200)]
        public string Title { get; set; }

        [Comparable, Required]
        public string Abstract { get; set; }

        [Comparable, Required]
        public DateTime Start { get; set; }

        [Comparable, Required]
        public DateTime End { get; set; }

        [Comparable]
        public string Level { get; set; }

        [Comparable]
        public string Track { get; set; }

        [Comparable]
        public string Room { get; set; }

        // constructor
        public Session()
        {
            Title = string.Empty;
            Abstract = string.Empty;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            Level = string.Empty;
            Track = string.Empty;
            Room = string.Empty;
        }

        #region Implementation of IHasIdentifier

        public long ID { get { return SessionId; } }

        #endregion
    }
}
