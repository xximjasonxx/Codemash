using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codemash.Phone7.Data.Repository
{
    public interface ISettingsRepository
    {
        /// <summary>
        /// Indicates whether the user has seen the Session List page
        /// </summary>
        bool HasSeenListPage { get; set; }

        /// <summary>
        /// Save the current settings
        /// </summary>
        void Save();
    }
}
