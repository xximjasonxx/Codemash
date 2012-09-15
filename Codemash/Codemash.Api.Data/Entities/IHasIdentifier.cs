using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Entities
{
    public interface IHasIdentifier
    {
        int ID { get; }
    }
}
