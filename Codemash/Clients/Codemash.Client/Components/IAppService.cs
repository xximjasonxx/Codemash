using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codemash.Client.Components
{
    public interface IAppService
    {
        /// <summary>
        /// Application Service property indicating whether back is an option
        /// </summary>
        bool CanGoBack { get; }
    }
}
