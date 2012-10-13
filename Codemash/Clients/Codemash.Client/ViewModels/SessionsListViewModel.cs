using System.Collections.ObjectModel;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Data.Repository;
using Codemash.Client.Grouping;
using Codemash.Client.Parameters;
using Windows.UI.Xaml.Controls;

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

        // behaviors
        public void SessionClick(ItemClickEventArgs args)
        {
            var listItem = (IListItem) args.ClickedItem;
            if (listItem.Id > 0)
            {
                var session = SessionRepository.Get(listItem.Id);
                NavigationService.NavigateToViewModel<SessionViewModel>(new SessionParameter(session));
            }
        }
    }
}
