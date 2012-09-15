using System;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Session : EntityBase, IHasIdentifier
    {
        // constructor
        public Session()
        {
            _title = string.Empty;
            _abstract = string.Empty;
            _start = DateTime.MinValue;
            _end = DateTime.MinValue;
            _level = Level.Unknown;
            _room = Room.Unknown;
            _track = Track.Unknown;
        }

        // member fields
        private string _title;
        private string _abstract;
        private DateTime _start;
        private DateTime _end;
        private Level _level;
        private Track _track;
        private Room _room;

        public int SessionId { get; set; }

        [Comparable]
        public int SpeakerId { get; set; }

        [Comparable]
        public string Title
        {
            get { return _title; }
            set
            {
                ValueChanged();
                _title = value;
            }
        }

        [Comparable]
        public string Abstract
        {
            get { return _abstract; }
            set
            {
                ValueChanged();
                _abstract = value;
            }
        }

        [Comparable]
        public DateTime Start
        {
            get { return _start; }
            set
            {
                ValueChanged();
                _start = value;
            }
        }

        [Comparable]
        public DateTime End
        {
            get { return _end; }
            set
            {
                ValueChanged();
                _end = value;
            }
        }

        [Comparable]
        public Level Level
        {
            get { return _level; }
            set
            {
                ValueChanged();
                _level = value;
            }
        }

        [Comparable]
        public Track Track
        {
            get { return _track; }
            set
            {
                ValueChanged();
                _track = value;
            }
        }

        [Comparable]
        public Room Room
        {
            get { return _room; }
            set
            {
                ValueChanged();
                _room = value;
            }
        }

        #region Implementation of IHasIdentifier

        public int ID { get { return SessionId; } }

        #endregion
    }
}
