using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Codemash.Phone.Shared.DataModels;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            // you cannot go back from this page
            navigationService.RemoveBackEntry();
        }

        // attributes
        public ObservableCollection<SessionListView> UpcomingSessions
        {
            get
            {
                var upcoming = SessionRepository.GetUpcomingSessions().OrderBy(s => s.Title);
                return new ObservableCollection<SessionListView>(upcoming.Select(s => new SessionListView
                                                                                          {
                                                                                              SessionId = s.SessionId,
                                                                                              Title = s.Title,
                                                                                              StartsAt = s.Starts.AsDateTime().AsTimeDisplay()
                                                                                          }));
            }
        }

        // behaviors
        public void SelectionChanged(SelectionChangedEventArgs ev)
        {
            if (ev.AddedItems.Count > 0)
            {
                var selectedSessionView = ev.AddedItems[0] as SessionListView;
                if (selectedSessionView != null)
                {
                    NavigationService.UriFor<SessionViewModel>().WithParam(sv => sv.IncomingSession,
                                                                           selectedSessionView.SessionId)
                        .Navigate();
                }
            }
        }

        public void AllByName()
        {
            NavigationService.UriFor<ListViewModel>().WithParam(l => l.GroupingType, SessionGroupType.ByName)
                .Navigate();
        }

        public void AllByBlock()
        {
            NavigationService.UriFor<ListViewModel>().WithParam(l => l.GroupingType, SessionGroupType.ByBlock)
                .Navigate();
        }

        public void AllByTech()
        {
            NavigationService.UriFor<ListViewModel>().WithParam(l => l.GroupingType, SessionGroupType.ByTech)
                .Navigate();
        }

        public void AllByRoom()
        {
            NavigationService.UriFor<ListViewModel>().WithParam(l => l.GroupingType, SessionGroupType.ByRoom)
                .Navigate();
        }

        public void SearchSessions()
        {
            NavigationService.UriFor<SearchViewModel>().Navigate();
        }
    }
}
