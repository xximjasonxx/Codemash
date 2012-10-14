using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Core.Ex;
using Codemash.Client.Data.Entities;
using Codemash.Client.Data.Repository;
using Codemash.Client.Parameters;

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
        public string FormattedSearchTerm { get { return string.Format("'{0}'", Parameter.Value); } }
        public ObservableCollection<Session> Results { get; set; }
        public string FormattedResultsCount
        {
            get { return Results == null ? "(0)" : string.Format("({0})", Results.Count); }
        }
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
                Results = new ObservableCollection<Session>(SessionRepository.SearchSessions(Parameter.Value));
            }
            catch(BaseDataNotAvailableException)
            {
                // there is not local data available to search, return an empty list
                Results = new ObservableCollection<Session>();
            }

            NotifyResultsPropertyUpdated();
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
