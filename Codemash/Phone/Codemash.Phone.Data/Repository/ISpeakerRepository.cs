using Codemash.Phone.Data.Entities;

namespace Codemash.Phone.Data.Repository
{
    public interface ISpeakerRepository : IRepository<Speaker>
    {
        /// <summary>
        /// Save the current state of the Speaker repository
        /// </summary>
        void SaveChanges();
    }
}
