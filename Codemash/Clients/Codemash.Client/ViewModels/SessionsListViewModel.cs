using System.Collections.ObjectModel;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Data.Repository;
using Codemash.Client.Grouping;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class SessionsListViewModel : ViewModelBase
    {
        // parameter
        public GroupingParameter Parameter { get; set; }

        // deps
        public ISessionRepository SessionRepository { get; set; }
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SessionsListViewModel(INavigationService navigationService, ISessionRepository sessionRepository, ISpeakerRepository speakerRepository)
            : base(navigationService)
        {
            SessionRepository = sessionRepository;
            SpeakerRepository = speakerRepository;
        }

        // attributes
        public ObservableCollection<IListItem> ListSource
        {
            get
            {
                var grouper = GroupSessionFactory.GetSessionGrouperInstance(SessionRepository.GetAll(), Parameter.GroupingType);
                return new ObservableCollection<IListItem>(grouper.GetGroupedList());
            }
        }
    }
}
