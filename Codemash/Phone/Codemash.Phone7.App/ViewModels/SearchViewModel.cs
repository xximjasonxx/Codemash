using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.DataModels;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public SearchViewModel(INavigationService navigationService) : base(navigationService)
        {
            ShowNoResults = false;
            ShowResults = false;
        }

        // attributes
        public ObservableCollection<SessionListView> SearchResults { get; private set; }
        public string SearchTerm { get; set; }
        public bool ShowResults { get; private set; }
        public bool ShowNoResults { get; private set; }

        // beahviors
        public void DoSearch()
        {
            var results = SessionRepository.FindSessions(SearchTerm);
            SearchResults = new ObservableCollection<SessionListView>(results.Select(s => new SessionListView
                                                                                                  {
                                                                                                      SessionId = s.SessionId,
                                                                                                      Title = s.Title
                                                                                                  }));

            UpdateViewModel();
        }

        public void SelectionChanged(SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                var item = args.AddedItems[0] as SessionListView;
                if (item != null)
                {
                    NavigationService.UriFor<SessionViewModel>().WithParam(s => s.IncomingSession, item.SessionId)
                        .Navigate();
                }
            }
        }

        private void UpdateViewModel()
        {
            NotifyOfPropertyChange("SearchResults");

            ShowResults = SearchResults.Count > 0;
            NotifyOfPropertyChange("ShowResults");

            ShowNoResults = SearchResults.Count == 0;
            NotifyOfPropertyChange("ShowNoResults");
        }
    }
}
