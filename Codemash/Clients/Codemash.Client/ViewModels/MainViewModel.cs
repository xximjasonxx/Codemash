using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Common;
using Codemash.Client.Common.Models;
using Codemash.Client.Common.Services;
using Codemash.Client.Data.Repository;
using Codemash.Client.Parameters;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public MainViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository, IAppService appService)
            : base(navigationService, false)
        {
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
        }

        // attributes
        public IList<SessionGroup> UpcomingSessions
        {
            get
            {
                var grouping = new List<SessionGroup>();
                grouping.Add(new SessionGroup("Upcoming", SessionRepository.GetUpcomingSessions()));

                return grouping;
            }
        }

        // behaviors
        public void ShowAllSessions()
        {
            NavigationService.Navigate(typeof(Views.SessionsListView), new GroupingParameter(GroupingType.Alphabetical));
        }

        public void ShowSessionsByBlock()
        {
            NavigationService.Navigate(typeof(Views.SessionsListView), new GroupingParameter(GroupingType.Block));
        }

        public void ShowSessionsByTrack()
        {
            NavigationService.Navigate(typeof(Views.SessionsListView), new GroupingParameter(GroupingType.Track));
        }

        public void ShowSessionsByRoom()
        {
            NavigationService.Navigate(typeof (Views.SessionsListView), new GroupingParameter(GroupingType.Room));
        }

        public void SessionClick(ItemClickEventArgs args)
        {
            var tileData = (SessionView) args.ClickedItem;
            var session = SessionRepository.Get(tileData.SessionId);
            NavigationService.Navigate(typeof(Views.SessionDetailView), new SessionParameter(session));
        }
    }
}
