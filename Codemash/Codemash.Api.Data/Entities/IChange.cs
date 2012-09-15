using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public interface IChange
    {
        int ID { set; }
        ChangeAction Action { set; }
        string Key { set; }
        string Value { set; }
    }
}
