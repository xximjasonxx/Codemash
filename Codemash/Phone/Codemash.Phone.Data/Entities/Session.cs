using System;
using System.Data.Linq.Mapping;
using Codemash.Phone.Core;

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
        public string Room { get; set; }

        [Column(CanBeNull = false)]
        public string Starts { get; set; }

        [Column(CanBeNull = false)]
        public string Ends { get; set; }

        [Column(IsPrimaryKey = true, CanBeNull = false, DbType = "int")]
        public int SessionId { get; set; }

        [Column(CanBeNull = false, DbType = "int")]
        public int SpeakerId { get; set; }

        public TimeSpan Duration
        {
            get { return Ends.AsDateTime() - Starts.AsDateTime(); }
        }

        #region Overrides of EntityBase

        internal override int PrimaryKey
        {
            get { return SessionId; }
        }

        #endregion
    }
}
