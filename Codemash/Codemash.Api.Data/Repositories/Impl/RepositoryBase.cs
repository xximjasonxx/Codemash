using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Api.Data.Repositories.Impl
{
    public abstract class RepositoryBase
    {
        /// <summary>
        /// For Change saves, we use a block notation to group things together. This is also so users can easily determine when they receive new data
        /// </summary>
        /// <returns></returns>
        protected string GetBlockId()
        {
            string rawBlock = Guid.NewGuid().ToString();
            return rawBlock.Split(new[] {'-'})[0];
        }
    }
}
