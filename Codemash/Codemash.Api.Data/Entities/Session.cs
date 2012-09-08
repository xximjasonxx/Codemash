using System;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Session : EntityBase
    {
        private string _title;
        private string _abstract;
        private DateTime _start;
        private DateTime _end;
        private Level _level;
        private Track _track;
        private Room _room;

        public int SessionId { get; set; }
        public int SpeakerId { get; set; }

        public string Title
        {
            get { return _title; }
            set
            {
                ValueChanged();
                _title = value;
            }
        }

        public string Abstract
        {
            get { return _abstract; }
            set
            {
                ValueChanged();
                _abstract = value;
            }
        }

        public DateTime Start
        {
            get { return _start; }
            set
            {
                ValueChanged();
                _start = value;
            }
        }

        public DateTime End
        {
            get { return _end; }
            set
            {
                ValueChanged();
                _end = value;
            }
        }

        public Level Level
        {
            get { return _level; }
            set
            {
                ValueChanged();
                _level = value;
            }
        }

        public Track Track
        {
            get { return _track; }
            set
            {
                ValueChanged();
                _track = value;
            }
        }

        public Room Room
        {
            get { return _room; }
            set
            {
                ValueChanged();
                _room = value;
            }
        }
    }
}
