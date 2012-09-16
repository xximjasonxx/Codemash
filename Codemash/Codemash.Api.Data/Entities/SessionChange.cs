
using System;
using System.ComponentModel.DataAnnotations;
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
        public int SessionChangeID { get; set; }

        public int SessionID { get; set; }
        public ChangeAction Action { get; set; }

        [StringLength(100)]
        public string Key { get; set; }

        [StringLength(1000)]
        public string Value { get; set; }

        #region IChange Implementation

        public int ChangeEntityID { set { SessionID = value; } }

        #endregion
    }
}
