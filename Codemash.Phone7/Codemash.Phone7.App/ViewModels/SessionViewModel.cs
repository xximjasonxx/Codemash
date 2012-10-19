using Caliburn.Micro;
using Codemash.Phone7.App.DataModels;
using Codemash.Phone7.Data.Entities;
using Codemash.Phone7.Data.Repository;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        private SessionDetailView _sessionDetailView;

        public int IncomingSession { get; set; }

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public SessionViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        // attributes
        public SessionDetailView Session
        {
            get
            {
                if (_sessionDetailView == null)
                {
                    var session = SessionRepository.Get(IncomingSession);
                    _sessionDetailView = new SessionDetailView
                                             {
                                                 Title = session.Title
                                             };
                }

                return _sessionDetailView;
            }
        }
    }
}
