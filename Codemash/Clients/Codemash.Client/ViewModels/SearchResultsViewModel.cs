using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class SearchResultsViewModel : ViewModelBase
    {
        public SearchTextParameter Parameter { get; set; }
        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SearchResultsViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
            : base(navigationService)
        {
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
        }

        // attributes
        public string AppName { get { return "Jason Farrell"; } }
    }
}
