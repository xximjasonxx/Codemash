using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities.Interfaces;

namespace Codemash.Api.Data.Entities
{
    public class SpeakerChange : EntityBase, IChange
    {
        [Key]
        public int SpeakerChangeID { get; set; }
        public int SpeakerID { get; set; }

        #region Implementation of IChange

        public int ChangeEntityID { set { SpeakerID = value; } }
        public ChangeAction Action { get; set; }

        [StringLength(100)]
        public string Key { set; get; }

        [StringLength(1000)]
        public string Value { set; get; }

        #endregion
    }
}
