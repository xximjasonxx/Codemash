using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Repository;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public MainViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
            : base(navigationService)
        {
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
        }

        // attributes
        public IList<SessionTileView> UpcomingSessions
        {
            get
            {
                return SessionRepository.GetUpcomingSessions().Select(s => new SessionTileView
                                                                               {
                                                                                   Title = s.Title,
                                                                                   SpeakerName = SpeakerRepository.Get(s.SpeakerId).FullName,
                                                                                   Room = s.Room,
                                                                                   Track = s.Track
                                                                               }).ToList();
            }
        }

        // behaviors
        public void ShowAllSessions()
        {
            NavigationService.NavigateToViewModel<SessionsListViewModel>(new GroupingParameter(GroupingType.Alphabetical));
        }

        public void ShowFavorites()
        {
            return;
        }

        public void ShowSessionsByBlock()
        {
            NavigationService.NavigateToViewModel<SessionsListViewModel>(new GroupingParameter(GroupingType.Block));
        }

        public void ShowSessionsByTrack()
        {
            NavigationService.NavigateToViewModel<SessionsListViewModel>(new GroupingParameter(GroupingType.Track));
        }
    }
}
