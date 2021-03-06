﻿using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Data.Entities
{
    public class Change : EntityBase
    {
        public int Changeset { get; set; }
        public long EntityId { get; set; }
        public ActionType Action { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public string EntityType { get; set; }
    }
}