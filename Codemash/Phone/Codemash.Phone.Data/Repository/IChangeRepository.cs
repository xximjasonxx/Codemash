using System;
using System.Collections.Generic;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository
{
    public interface IChangeRepository
    {
        /// <summary>
        /// Get the number of speaker changes within the repository
        /// </summary>
        IList<Change> SpeakerChanges { get; }

        /// <summary>
        /// Get the number of session changes within the repository
        /// </summary>
        IList<Change> SessionChanges { get; }

        /// <summary>
        /// All changes in the custom change list instance
        /// </summary>
        ChangeList AllChanges { get; }

        /// <summary>
        /// Loads the repository from whatever backing data store is most appropriate
        /// </summary>
        void Load();

        /// <summary>
        /// Event to indicate the load of the repository data is complete
        /// </summary>
        event EventHandler LoadCompleted;
    }
}
