using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public class SpeakerChange : EntityBase, IChange
    {
        public int SpeakerChangeId { get; set; }

        #region Implementation of IChange

        public int ID { set { SpeakerChangeId = value; } }
        public ChangeAction Action { get; set; }
        public string Key { set; get; }
        public string Value { set; get; }

        #endregion
    }
}
