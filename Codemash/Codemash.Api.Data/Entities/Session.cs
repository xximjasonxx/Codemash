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
        public int SessionId { get; set; }

        [Comparable]
        [Required]
        public int SpeakerId { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }

        [Comparable, Required, StringLength(100)]
        public string Title { get; set; }

        [Comparable, Required]
        public string Abstract { get; set; }

        [Comparable, Required]
        public DateTime Start { get; set; }

        [Comparable]
        public DateTime End { get; set; }

        [Comparable("Level")]
        [Required]
        public Level LevelType
        {
            get { return (Level) this.Level; }
            set { this.Level = (int) value; }
        }

        [Comparable("Track")]
        [Required]
        public Track TrackType
        {
            get { return (Track) this.Track; }
            set { this.Track = (int) value; }
        }

        [Comparable("Roonm")]
        [Required]
        public Room RoomType
        {
            get { return (Room) this.Room; }
            set { this.Room = (int) value; }
        }

        // constructor
        public Session()
        {
            Title = string.Empty;
            Abstract = string.Empty;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            LevelType = Data.Level.Unknown;
            RoomType = Data.Room.Unknown;
            TrackType = Data.Track.Unknown;
        }

        public int Level { get; private set; }
        public int Room { get; private set; }
        public int Track { get; private set; }

        #region Implementation of IHasIdentifier

        public int ID { get { return SessionId; } }

        #endregion
    }
}
