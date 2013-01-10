using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class TestSettingsRepository : ISettingsRepository
    {
        public bool HasSeenListPage { get; set; }
        public string PushChannelUri { get; set; }
    }
}
