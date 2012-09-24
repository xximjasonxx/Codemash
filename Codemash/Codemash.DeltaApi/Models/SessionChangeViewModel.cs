using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codemash.DeltaApi.Models
{
    public class SessionChangeViewModel
    {
        public string Action { get; set; }

        public int SessionId { get; set; }

        public int Version { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}