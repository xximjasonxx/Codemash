using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Api.Data.Entities;

namespace Codemash.Poller.Process
{
    public interface ISynchronize
    {
        /// <summary>
        /// Execute whatever process is being implemeted
        /// </summary>
        IList<Change> Synchronize();
    }
}
