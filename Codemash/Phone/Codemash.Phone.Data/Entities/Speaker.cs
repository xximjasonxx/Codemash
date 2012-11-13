using System.Data.Linq.Mapping;

namespace Codemash.Phone.Data.Entities
{
    [Table]
    public class Speaker : EntityBase
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false, DbType = "int")]
        public int SpeakerId { get; set; }

        [Column(CanBeNull = false)]
        public string Name { get; set; }

        [Column(CanBeNull = false, DbType = "ntext")]
        public string Biography { get; set; }

        [Column(CanBeNull = false)]
        public string Twitter { get; set; }

        [Column(CanBeNull = false)]
        public string BlogUrl { get; set; }

        #region Overrides of EntityBase

        internal override int PrimaryKey
        {
            get { return SpeakerId; }
        }

        #endregion
    }
}
