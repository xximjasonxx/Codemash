using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codemash.DeltaApi.Models
{
    public class ChangeViewModel
    {
        public string Action { get; set; }

        public long EntityId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        public int Changeset { get; set; }

        public string EntityType { get; set; }
    }
}