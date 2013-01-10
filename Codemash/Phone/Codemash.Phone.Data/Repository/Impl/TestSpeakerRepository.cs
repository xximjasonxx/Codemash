using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class TestSpeakerRepository : ISpeakerRepository
    {
        public void Load()
        {
            if (LoadCompleted != null)
                LoadCompleted(this, new EventArgs());
        }

        public event EventHandler LoadCompleted;
        public Speaker Get(long id)
        {
            return new Speaker() {Name = "Some Speaker"};
        }

        public void Add(Speaker item)
        {
            
        }

        public void SaveChanges()
        {
            
        }
    }
}
