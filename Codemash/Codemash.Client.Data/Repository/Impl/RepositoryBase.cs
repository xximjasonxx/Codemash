using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codemash.Client.Data.Entities;

namespace Codemash.Client.Data.Repository.Impl
{
    public class RepositoryBase<T> where T : EntityBase
    {
        private IList<T> _repository;  
        protected IList<T> Repository
        {
            get { return _repository ?? (_repository = new List<T>()); }
        }

        protected void AddRange(IEnumerable<T> enumerable)
        {
            foreach (var e in enumerable)
            {
                Repository.Add(e);
            }
        }
    }
}
