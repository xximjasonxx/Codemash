using System;
using SQLite;

namespace Codemash.Client.Data.Entities
{
    public class Session : EntityBase
    {
        private string _title;
        private string _abstract;
        private DateTime _starts;
        private DateTime _ends;
        private string _difficulty;
        private string _room;
        private long _speakerId;
        private string _technology;

        [PrimaryKey]
        public long SessionId { get; internal set; }

        public string Title
        {
            get { return _title; }
            internal set
            {
                _title = value;
                MarkDirty();
            }
        }

        public string Abstract
        {
            get { return _abstract; }
            internal set
            {
                _abstract = value;
                MarkDirty();
            }
        }

        public DateTime Starts
        {
            get { return _starts; }
            internal set
            {
                MarkDirty();
                _starts = value;
            }
        }

        public DateTime Ends
        {
            get { return _ends; }
            internal set
            {
                MarkDirty();
                _ends = value;
            }
        }

        public string Difficulty
        {
            get { return _difficulty; }
            internal set
            {
                MarkDirty();
                _difficulty = value;
            }
        }

        public string Room
        {
            get { return _room; }
            internal set
            {
                MarkDirty();
                _room = value;
            }
        }

        public long SpeakerId
        {
            get { return _speakerId; }
            internal set
            {
                MarkDirty();
                _speakerId = value;
            }
        }

        public string Technology
        {
            get { return _technology; }
            internal set
            {
                MarkDirty();
                _technology = value;
            }
        }

        [Ignore]
        public TimeSpan Duration
        {
            get { return Ends - Starts; }
        }
    }
}
