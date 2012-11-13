using System;
using System.Data.Linq.Mapping;

namespace Codemash.Phone.Data.Entities
{
    [Table]
    public class Session : EntityBase
    {
        [Column(CanBeNull = false)]
        public string Title { get; set; }

        [Column(CanBeNull = false, DbType = "ntext")]
        public string Abstract { get; set; }

        [Column(CanBeNull = false)]
        public string Difficulty { get; set; }

        [Column(CanBeNull = false)]
        public string Technology { get; set; }

        [Column(CanBeNull = false)]
        public string EventType { get; set; }

        [Column(CanBeNull = false)]
        public string Room { get { return "Indigo"; } set { } }

        [Column(CanBeNull = false, DbType = "datetime")]
        public DateTime Starts { get { return DateTime.Now; } set { } }

        [Column(CanBeNull = false, DbType = "datetime")]
        public DateTime Ends { get { return DateTime.Now.AddHours(1); } set { } }

        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int")]
        public int SessionId { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int SpeakerId { get; set; }

        public TimeSpan Duration
        {
            get { return Ends - Starts; }
        }

        #region Overrides of EntityBase

        internal override int PrimaryKey
        {
            get { return SessionId; }
        }

        #endregion
    }
}
