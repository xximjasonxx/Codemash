using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public class SessionChange
    {
        public int SessionChangeId { get; set; }
        public int SessionId { get; set; }
        public SessionChangeAction Action { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
