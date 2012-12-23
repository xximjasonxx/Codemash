using System;
using System.Collections.Generic;
using System.Linq;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Common
{
    public class ChangeList : List<Change>
    {
        public IList<Change> SessionChanges
        {
            get { return this.Where(c => string.Compare("session", c.EntityType, StringComparison.OrdinalIgnoreCase) == 0).ToList(); }
        }

        public IList<Change> SpeakerChanges
        {
            get { return this.Where(c => string.Compare("speaker", c.EntityType, StringComparison.OrdinalIgnoreCase) == 0).ToList(); }
        }
    }
}
