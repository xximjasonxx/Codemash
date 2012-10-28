using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using Codemash.Client.Components;
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

        public SearchResultsViewModel(INavigationService navigationService, ISessionRepository sessionRepository, IAppService appService)
            : base(navigationService, appService.CanGoBack)
        {
            SessionRepository = sessionRepository;
        }

        // attributes
        public string SearchTitle { get { return string.Format("Search Results for '{0}'", Parameter.Value); } }
        public ObservableCollection<SessionView> Results
        {
            get
            {
                return new ObservableCollection<SessionView>(
                    SessionRepository.SearchSessions(Parameter.Value).Select(s => new SessionView
                                                                                      {
                                                                                          SessionId = s.SessionId,
                                                                                          Title = s.Title
                                                                                      }));
            }
        }

        public bool HasNoResults { get { return Results.Count == 0; } }

        // behaviors
        public void SessionClick(ItemClickEventArgs args)
        {
            var item = args.ClickedItem as SessionView;
            if (item != null)
            {
                var session = SessionRepository.Get(item.SessionId);
                NavigationService.NavigateToViewModel<SessionDetailViewModel>(new SessionParameter(session));
            }
        }
    }
}
