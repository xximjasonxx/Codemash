using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using Codemash.Client.Common;
using Codemash.Client.Common.Grouping;
using Codemash.Client.Common.Models;
using Codemash.Client.Data.Repository;
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
                var grouper = GroupSessionFactory.GetSessionGrouperInstance(Parameter.GroupingType);
                return new ObservableCollection<SessionGroup>(grouper.GetGroupedSessions(SessionRepository.GetAll()));
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
                    case GroupingType.Room:
                        return "All Sessions by Room";
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
                NavigationService.Navigate(typeof(Views.SessionDetailView), new SessionParameter(session));
            }
        }

        public void ShowAllSessions()
        {
            if (Parameter.GroupingType != GroupingType.Alphabetical)
            {
                UpdateListing(GroupingType.Alphabetical);
            }
        }

        public void ShowSessionsByBlock()
        {
            if (Parameter.GroupingType != GroupingType.Block)
            {
                UpdateListing(GroupingType.Block);
            }
        }

        public void ShowSessionsByTrack()
        {
            if (Parameter.GroupingType != GroupingType.Track)
            {
                UpdateListing(GroupingType.Track);
            }
        }

        public void ShowSessionsByRoom()
        {
            if (Parameter.GroupingType != GroupingType.Room)
            {
                UpdateListing(GroupingType.Room);
            }
        }

        // methods
        private void UpdateListing(GroupingType groupingType)
        {
            Parameter = new GroupingParameter(groupingType);
            NotifyOfPropertyChange("PageTitle");
            NotifyOfPropertyChange("ListSource");
        }
    }
}
