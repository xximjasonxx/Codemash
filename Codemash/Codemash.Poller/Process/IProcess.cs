using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Poller.Process
{
    public interface IProcess
    {
        /// <summary>
        /// Execute the represented process
        /// </summary>
        void Execute();
    }
}
