
using System;
using System.ComponentModel.DataAnnotations;
using Codemash.Api.Data.Entities.Interfaces;

namespace Codemash.Api.Data.Entities
{
    public class SessionChange : EntityBase, IChange
    {
        [Key]
        public int SessionChangeId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [Required]
        public ChangeAction ActionType
        {
            get { return (ChangeAction) Action; }
            set { Action = (int) value; }
        }

        public int Action { get; private set; }

        [StringLength(50)]
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [StringLength(12)]
        public string Block { get; internal set; }

        #region IChange Implementation

        public int ChangeEntityId { set { SessionId = value; } }

        #endregion
    }
}
