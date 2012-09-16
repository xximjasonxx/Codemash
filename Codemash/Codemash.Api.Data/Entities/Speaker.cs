
using System.ComponentModel.DataAnnotations;
using Codemash.Api.Data.Entities.Interfaces;
using Codemash.Server.Core.Attributes;

namespace Codemash.Api.Data.Entities
{
    public class Speaker : EntityBase, IHasIdentifier
    {
        private string _biography;
        private string _twitter;
        private string _emailAddress;
        private string _blogUrl;
        private string _firstName;
        private string _lastName;
        private string _company;

        [Key]
        public int SpeakerID { get; set; }

        [Comparable]
        [StringLength(1000)]
        public string Biography
        {
            get { return _biography; }
            set
            {
                ValueChanged();
                _biography = value;
            }
        }

        [Comparable]
        [StringLength(100)]
        public string Twitter
        {
            get { return _twitter; }
            set
            {
                ValueChanged();
                _twitter = value;
            }
        }

        [Comparable]
        [StringLength(255)]
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                ValueChanged();
                _emailAddress = value;
            }
        }

        [Comparable]
        [StringLength(100)]
        public string BlogUrl
        {
            get { return _blogUrl; }
            set
            {
                ValueChanged();
                _blogUrl = value;
            }
        }

        [Comparable]
        [StringLength(100)]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValueChanged();
                _firstName = value;
            }
        }

        [Comparable]
        [StringLength(100)]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValueChanged();
                _lastName = value;
            }
        }

        [Comparable]
        [StringLength(100)]
        public string Company
        {
            get { return _company; }
            set
            {
                ValueChanged();
                _company = value;
            }
        }

        public Speaker()
        {
            _biography = string.Empty;
            _twitter = string.Empty;
            _emailAddress = string.Empty;
            _blogUrl = string.Empty;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _company = string.Empty;
        }

        #region Implementation of IHasIdentifier

        public int ID { get { return SpeakerID; } }

        #endregion
    }
}
