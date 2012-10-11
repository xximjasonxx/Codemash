using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Repository;

namespace Codemash.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; set; }

        public MainViewModel(INavigationService navigationService, ISessionRepository sessionRepository)
            : base(navigationService)
        {
            SessionRepository = sessionRepository;
        }

        // attributes
        public IList<Session> UpcomingSessions { get { return SessionRepository.GetUpcomingSessions(); } }

        // behaviors
        public void ShowAllSessions()
        {
            return;
        }

        public void ShowFavorites()
        {
            return;
        }

        public void ShowSesionsByBlock()
        {
            return;
        }

        public void ShowSessionsByTrack()
        {
            return;
        }
    }
}
