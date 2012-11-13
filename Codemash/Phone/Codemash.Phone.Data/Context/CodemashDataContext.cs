using System.Data.Linq;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Context
{
    public class CodemashDataContext : DataContext
    {
        public CodemashDataContext(string connectionString) : base(connectionString)
        {
            
        }

        public Table<Session> Sessions
        {
            get { return GetTable<Session>(); }
        }

        public Table<Speaker> Speakers
        {
            get { return GetTable<Speaker>(); }
        }
    }
}
