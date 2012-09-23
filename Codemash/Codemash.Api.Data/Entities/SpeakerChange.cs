using System;
using System.ComponentModel.DataAnnotations;
using Codemash.Api.Data.Entities.Interfaces;

namespace Codemash.Api.Data.Entities
{
    public class SpeakerChange : EntityBase, IChange
    {
        [Key]
        public int SpeakerChangeId { get; set; }

        [Required]
        public int SpeakerId { get; set; }

        [Required]
        [StringLength(12)]
        public string Block { get; internal set; }

        #region Implementation of IChange

        public int ChangeEntityId { set { SpeakerId = value; } }

        [Required]
        public ChangeAction ActionType
        {
            get { return (ChangeAction) Action; }
            set { Action = (int) value; }
        }

        public int Action { get; private set; }

        [StringLength(50)]
        [Required]
        public string Key { set; get; }

        [Required]
        public string Value { set; get; }

        #endregion
    }
}
