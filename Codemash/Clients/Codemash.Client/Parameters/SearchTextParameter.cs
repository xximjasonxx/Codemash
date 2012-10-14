using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Parameters
{
    public class SearchTextParameter
    {
        public string Value { get; private set; }

        public SearchTextParameter(string value)
        {
            Value = value;
        }
    }
}
