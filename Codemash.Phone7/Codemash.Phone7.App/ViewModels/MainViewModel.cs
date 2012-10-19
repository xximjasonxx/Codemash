using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone7.App.DataModels;
using Codemash.Phone7.Data.Repository;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        [Inject]
        public ISpeakerRepository SpeakerRepository { get; set; }

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
                                                                                              Title = s.Title
                                                                                          }));
            }
        }

        // behaviors
        public void SelectionChanged(SelectionChangedEventArgs ev)
        {
            var selectedSessionView = ev.AddedItems[0] as SessionListView;
            if (selectedSessionView != null)
            {
                var session = SessionRepository.Get(selectedSessionView.SessionId);
                NavigationService.UriFor<SessionViewModel>().WithParam(sv => sv.IncomingSession, session)
                    .Navigate();
            }
        }

        public void AllByName()
        {
            
        }

        public void AllByBlock()
        {
            
        }

        public void AllByTech()
        {
            
        }
    }
}
