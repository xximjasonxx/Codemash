
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Codemash.Api.Data.Entities.Interfaces;

namespace Codemash.Api.Data.Entities
{
    public class SessionChange : EntityBase, IChange
    {
        private int _sessionId;
        private ChangeAction _action;
        private string _key;
        private string _value;

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

        public DateTime DateCreated { get; internal set; }

        #region IChange Implementation

        public int ChangeEntityId { set { SessionId = value; } }

        #endregion
    }
}
