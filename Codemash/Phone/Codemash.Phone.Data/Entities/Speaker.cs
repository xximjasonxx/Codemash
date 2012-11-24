using System.Data.Linq.Mapping;

namespace Codemash.Phone.Data.Entities
{
    [Table]
    public class Speaker : EntityBase
    {
        private string _name;
        private string _biography;
        private string _twitter;
        private string _blogUrl;

        [Column(IsPrimaryKey = true, IsDbGenerated = false, CanBeNull = false, DbType = "int")]
        public long SpeakerId { get; set; }

        [Column(CanBeNull = false)]
        public string Name
        {
            get { return _name; }
            set
            {
                MarkAsDirty();
                _name = value;
            }
        }

        [Column(CanBeNull = false, DbType = "ntext")]
        public string Biography
        {
            get { return _biography; }
            set
            {
                MarkAsDirty();
                _biography = value;
            }
        }

        [Column(CanBeNull = false)]
        public string Twitter
        {
            get { return _twitter; }
            set
            {
                MarkAsDirty();
                _twitter = value;
            }
        }

        [Column(CanBeNull = false)]
        public string BlogUrl
        {
            get { return _blogUrl; }
            set
            {
                MarkAsDirty();
                _blogUrl = value;
            }
        }
    }
}
