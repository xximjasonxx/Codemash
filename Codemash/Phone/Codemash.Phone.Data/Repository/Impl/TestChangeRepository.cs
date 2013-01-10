using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class TestChangeRepository : IChangeRepository
    {
        public IList<Change> SpeakerChanges { get { return new List<Change>(); } }
        public IList<Change> SessionChanges { get { return new List<Change>(); } }
        public ChangeList AllChanges { get { return new ChangeList(); } }
        public void Load()
        {
            if (LoadCompleted != null)
                LoadCompleted(this, new EventArgs());
        }

        public event EventHandler LoadCompleted;
    }
}
