using System;
using System.Data.Linq.Mapping;
using Codemash.Phone.Core;

namespace Codemash.Phone.Data.Entities
{
    [Table]
    public class Session : EntityBase
    {
        private string _title;
        private string _abstract;
        private string _difficulty;
        private string _technology;
        private string _room;
        private string _starts;
        private string _ends;
        private long _speakerId;

        [Column(CanBeNull = false)]
        public string Title
        {
            get { return _title; }
            set
            {
                MarkAsDirty();
                _title = value;
            }
        }

        [Column(CanBeNull = false, DbType = "ntext")]
        public string Abstract
        {
            get { return _abstract; }
            set
            {
                MarkAsDirty();
                _abstract = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Difficulty
        {
            get { return _difficulty; }
            set
            {
                MarkAsDirty();
                _difficulty = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Technology
        {
            get { return _technology; }
            set
            {
                MarkAsDirty();
                _technology = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Room
        {
            get { return _room; }
            set
            {
                MarkAsDirty();
                _room = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Starts
        {
            get { return _starts; }
            set
            {
                MarkAsDirty();
                _starts = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Ends
        {
            get { return _ends; }
            set
            {
                MarkAsDirty();
                _ends = value;
            }
        }

        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int")]
        public long SessionId { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public long SpeakerId
        {
            get { return _speakerId; }
            set
            {
                MarkAsDirty();
                _speakerId = value;
            }
        }

        public TimeSpan Duration
        {
            get { return Ends.AsDateTime() - Starts.AsDateTime(); }
        }
    }
}
