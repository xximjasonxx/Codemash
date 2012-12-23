using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone.Data.Common;
using Codemash.Phone.Data.Entities;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Codemash.Phone.Shared.DataModels;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class ChangesViewModel : ViewModelBase
    {
        [Inject]
        public IChangeRepository ChangeRepository { get; set; }

        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public ChangesViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        // attributes
        public string SessionsHeader
        {
            get { return string.Format("sessions ({0})", ChangeRepository.SessionChanges.Distinct(new ChangeDistinctComparer()).Count()); }
        }

        public string SpeakersHeader
        {
            get { return string.Format("speakers ({0})", ChangeRepository.SpeakerChanges.Distinct(new ChangeDistinctComparer()).Count()); }
        }

        public IList<ChangeView> SessionChanges
        {
            get
            {
                return ChangeRepository.SessionChanges.Distinct(new ChangeDistinctComparer())
                                       .Select(s => new ChangeView()
                                                        {
                                                            EntityId = s.EntityId,
                                                            EntityChangeAction = s.Action,
                                                            EntityType = typeof(Session),
                                                            EntityDisplay = "Session Title"
                                                        }).ToList();
            }
        }

        public bool HasSessionChanges
        {
            get { return ChangeRepository.SessionChanges.Count > 0; }
        }

        public bool HasNoSessionChanges
        {
            get { return !HasSessionChanges; }
        }

        // methods
        public void SelectionChanged(SelectionChangedEventArgs ev)
        {
            var changeRecord = (ChangeView) ev.AddedItems[0];
            if (changeRecord.EntityChangeAction == ActionType.Delete)
            {
                MessageBox.Show("This item has been deleted and cannot be viewed");
            }
            else
            {
                var selectedSessionView = SessionRepository.Get(changeRecord.EntityId);
                if (selectedSessionView != null)
                {
                    NavigationService.UriFor<SessionViewModel>()
                        .WithParam(sv => sv.IncomingSession, selectedSessionView.SessionId)
                        .Navigate();
                }
            }
        }
    }
}
