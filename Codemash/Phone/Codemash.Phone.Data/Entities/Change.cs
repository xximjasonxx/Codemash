using System;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Entities
{
    public class Change : EntityBase
    {
        public int ChangeId { get; set; }
        public int Changeset { get; set; }
        public int EntityId { get; set; }
        public ActionType Action { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        #region Overrides of EntityBase

        internal override int PrimaryKey
        {
            get { return ChangeId; }
        }

        #endregion
    }
}
