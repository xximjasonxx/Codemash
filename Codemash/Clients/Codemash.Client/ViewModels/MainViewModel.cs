using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Core;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Repository;
using Codemash.Client.DataModels;
using Codemash.Client.Parameters;
using Windows.UI.Xaml.Controls;

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
                                                                                   SessionId = s.SessionId,
                                                                                   Title = s.Title,
                                                                                   Room = s.Room,
                                                                                   Technology = s.Technology,
                                                                                   StartsAt = s.Starts.AsTimeDisplay()
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

        public void SessionClick(ItemClickEventArgs args)
        {
            var tileData = (SessionTileView) args.ClickedItem;
            var session = SessionRepository.Get(tileData.SessionId);
            NavigationService.NavigateToViewModel<SessionViewModel>(new SessionParameter(session));
        }
    }
}
