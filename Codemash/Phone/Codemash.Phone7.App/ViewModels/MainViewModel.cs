using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Caliburn.Micro;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Provider;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Codemash.Phone.Shared.DataModels;
using Codemash.Phone.Shared.Grouping;
using Codemash.Phone.Shared.Services;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public IChangeRepository ChangeRepository { get; set; }

        [Inject]
        public IChangeProvider ChangeProvider { get; set; }

        [Inject]
        public IAppService ApplicationService { get; set; }

        private bool _mySessionsLoaded;

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

        public IEnumerable<SessionGroup> MySessions
        {
            get
            {
                var grouper = GroupingFactory.GetGroupInstance(SessionGroupType.ByBlock);
                var dictionary = grouper.Group(SessionRepository.GetFavoriteSessions());
                var result = dictionary.Select(d => new SessionGroup(d.Key, d.Value.Select(s => new SessionListView
                {
                    Title = s.Title,
                    SessionId = s.SessionId,
                    StartsAt = s.Starts.AsDateTime().AsTimeDisplay()
                }))).ToList();

                _mySessionsLoaded = true;
                return result.OrderBy(r => r.Key);
            }
        }

        public bool MySessionsEmpty { get { return !MySessions.Any(); } }

        // behaviors
        public void SelectionChanged(SelectionChangedEventArgs ev)
        {
            if (ev.AddedItems.Count > 0)
            {
                var selectedSessionView = ev.AddedItems[0] as SessionListView;
                if (selectedSessionView != null)
                {
                    NavigationService.UriFor<SessionViewModel>()
                        .WithParam(sv => sv.IncomingSession, selectedSessionView.SessionId)
                        .Navigate();
                }
            }
        }

        public void PageLoaded()
        {
            ApplicationService.ShowProgressMessage("Checking for Changes...");
            ChangeRepository.LoadCompleted += ChangeRepository_LoadCompleted;
            ChangeRepository.Load();

            if (_mySessionsLoaded)
            {
                NotifyOfPropertyChange("MySessions");
                NotifyOfPropertyChange("MySessionsEmpty");
            }
        }

        void ChangeRepository_LoadCompleted(object sender, System.EventArgs e)
        {
            var changes = ChangeRepository.AllChanges;
            if (changes.Count > 0)
            {
                ChangeProvider.ApplyChanges(changes);
                if (ChangeRepository.SessionChanges.Count > 0)
                    ApplicationService.ShowToast("Changes Applied", "Click to see Changes", ToastTap);
            }
            ApplicationService.HideProgressMessage();
        }

        private void ToastTap()
        {
            NavigationService.UriFor<ChangesViewModel>().Navigate();
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
