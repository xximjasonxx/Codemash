using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Codemash.Client.Code;
using Codemash.Client.Data.Repository;
using Codemash.Client.DataModels;
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
        public ObservableCollection<SessionGroup> ListSource
        {
            get
            {
                var grouper = GroupSessionFactory.GetSessionGrouperInstance(SessionRepository.GetAll(), Parameter.GroupingType);
                return new ObservableCollection<SessionGroup>(grouper.GetGroupedList());
            }
        } 

        public string PageTitle
        {
            get
            {
                switch (Parameter.GroupingType)
                {
                    case GroupingType.Alphabetical:
                        return "All Sessions by Name";
                    case GroupingType.Block:
                        return "All Sessions by Block";
                    case GroupingType.Track:
                        return "All Sessions by Tech";
                }

                throw new InvalidOperationException("Could not determine Grouping Type");
            }
        }

        // behaviors
        public void SessionClick(ItemClickEventArgs args)
        {
            var listItem = (SessionView) args.ClickedItem;
            if (listItem.SessionId != 0)
            {
                var session = SessionRepository.Get(listItem.SessionId);
                NavigationService.NavigateToViewModel<SessionDetailViewModel>(new SessionParameter(session));
            }
        }
    }
}
