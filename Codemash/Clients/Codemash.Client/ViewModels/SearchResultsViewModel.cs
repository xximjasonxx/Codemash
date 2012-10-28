using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Codemash.Client.Core.Ex;
using Codemash.Client.Data.Repository;
using Codemash.Client.DataModels;
using Codemash.Client.Parameters;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.ViewModels
{
    public class SearchResultsViewModel : ViewModelBase
    {
        public SearchTextParameter Parameter { get; set; }
        public ISessionRepository SessionRepository { get; set; }

        public SearchResultsViewModel(INavigationService navigationService, ISessionRepository sessionRepository)
            : base(navigationService)
        {
            SessionRepository = sessionRepository;
        }

        // attributes
        public string SearchSummary { get { return string.Format("Search Results for '{0}'", Parameter.Value); } }
        public ObservableCollection<SessionView> Results { get; set; }
        public bool ShowResultsGrid
        {
            get { return Results != null && Results.Count > 0; }
        }
        public bool ShowNoResults { get { return !ShowResultsGrid; } }

        // behaviors
        public void PageLoaded()
        {
            try
            {
                Results = new ObservableCollection<SessionView>(
                    SessionRepository.SearchSessions(Parameter.Value).Select(s => new SessionView
                                                                                      {
                                                                                          SessionId = s.SessionId,
                                                                                          Title = s.Title
                                                                                      }));
            }
            catch(BaseDataNotAvailableException)
            {
                // there is not local data available to search, return an empty list
                Results = new ObservableCollection<SessionView>();
            }

            NotifyResultsPropertyUpdated();
        }

        public void SessionClick(ItemClickEventArgs args)
        {
            var item = args.ClickedItem as SessionView;
            if (item != null)
            {
                var session = SessionRepository.Get(item.SessionId);
                NavigationService.NavigateToViewModel<SessionDetailViewModel>(new SessionParameter(session));
            }
        }

        // methods
        private void NotifyResultsPropertyUpdated()
        {
            NotifyOfPropertyChange("Results");
            NotifyOfPropertyChange("FormattedResultsCount");

            NotifyOfPropertyChange("ShowResultsGrid");
            NotifyOfPropertyChange("ShowNoResults");
        }
    }
}
