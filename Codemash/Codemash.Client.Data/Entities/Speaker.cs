using SQLite;

namespace Codemash.Client.Data.Entities
{
    public class Speaker : EntityBase
    {
        private string _name;
        private string _twitter;
        private string _biography;
        private string _blogUrl;

        public string Name
        {
            get { return _name; }
            internal set
            {
                MarkDirty();
                _name = value;
            }
        }

        public string Twitter
        {
            get { return _twitter; }
            internal set
            {
                MarkDirty();
                _twitter = value;
            }
        }

        public string Biography
        {
            get { return _biography; }
            internal set
            {
                MarkDirty();
                _biography = value;
            }
        }

        public string BlogUrl
        {
            get { return _blogUrl; }
            internal set
            {
                MarkDirty();
                _blogUrl = value;
            }
        }

        [PrimaryKey]
        public long SpeakerId { get; internal set; }
    }
}
