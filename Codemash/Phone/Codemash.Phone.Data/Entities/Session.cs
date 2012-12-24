using System;
using System.Data.Linq.Mapping;
using System.Text;
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
        private bool _isFavorite;
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

        [Column(CanBeNull = true, DbType = "ntext", UpdateCheck = UpdateCheck.Never)]
        public string Abstract
        {
            get { return _abstract; }
            set
            {
                MarkAsDirty();
                _abstract = new StringWrapper(value).ToString();
            }
        }

        [Column(CanBeNull = true)]
        public string Difficulty
        {
            get { return _difficulty; }
            set
            {
                MarkAsDirty();
                _difficulty = new StringWrapper(value).ToString();
            }
        }

        [Column(CanBeNull = true)]
        public string Technology
        {
            get { return _technology; }
            set
            {
                MarkAsDirty();
                _technology = new StringWrapper(value).ToString();
            }
        }

        [Column(CanBeNull = true)]
        public string Room
        {
            get { return _room; }
            set
            {
                MarkAsDirty();
                _room = new StringWrapper(value).ToString();
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
        public bool IsFavorite
        {
            get { return _isFavorite; }
            set
            {
                MarkAsDirty();
                _isFavorite = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Ends
        {
            get { return new StringWrapper(_ends).ToString(); }
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
