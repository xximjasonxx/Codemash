using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Classes
{
    public interface IListItem
    {
        string Display { get; }
        ItemType ItemType { get; }
    }
}
