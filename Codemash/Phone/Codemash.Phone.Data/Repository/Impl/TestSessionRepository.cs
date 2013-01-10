using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Codemash.Phone.Data.Entities;
using Codemash.Phone.Data.Repository;

namespace Codemash.Phone.Data.Repository.Impl
{
    public class TestSessionRepository : ISessionRepository
    {
        public void Load()
        {
            if (LoadCompleted != null)
                LoadCompleted(this, new EventArgs());
        }

        public event EventHandler LoadCompleted;
        public Session Get(long id)
        {
            return new Session() {Title = "Test"};
        }

        public void Add(Session item)
        {
            
        }

        public IList<Session> GetUpcomingSessions()
        {
            return new List<Session>()
                       {
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"}
                       };
        }

        public IList<Session> GetAllSessions()
        {
            return new List<Session>()
                       {
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"}
                       };
        }

        public IList<Session> FindSessions(string searchTerm)
        {
            return new List<Session>()
                       {
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"}
                       };
        }

        public IList<Session> GetFavoriteSessions()
        {
            return new List<Session>()
                       {
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"},
                           new Session() {Title = "Test"}
                       };
        }

        public void UpdateFavoriteStatus(long sessionId, bool favoriteStatus)
        {
            
        }

        public void SaveChanges()
        {
            
        }
    }
}
