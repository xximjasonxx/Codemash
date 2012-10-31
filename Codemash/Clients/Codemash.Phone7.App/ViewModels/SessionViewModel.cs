using Caliburn.Micro;
using Codemash.Phone7.App.DataModels;
using Codemash.Phone7.Core;
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

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

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
                                                 Title = session.Title,
                                                 Technology = session.Technology,
                                                 Starts = session.Starts.AsBlockDisplay(),
                                                 Duration = session.Duration.AsFullDurationString(),
                                                 Difficulty = session.Difficulty,
                                                 Room = session.Room,
                                                 Abstract = session.Abstract,
                                                 Speaker = new SpeakerDetailView(SpeakerRepository.Get(session.SpeakerId))
                                             };
                }

                return _sessionDetailView;
            }
        }

        // behaviors
        public void ShowMap()
        {
            NavigationService.UriFor<MapViewModel>().Navigate();
        }
    }
}
