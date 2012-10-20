using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone7.App.Common;
using Codemash.Phone7.App.DataModels;
using Codemash.Phone7.App.Grouping;
using Codemash.Phone7.Data.Repository;
using Ninject;

namespace Codemash.Phone7.App.ViewModels
{
    public class ListViewModel : ViewModelBase
    {
        // parameters
        public SessionGroupType GroupingType { get; set; }

        // deps
        [Inject]
        public ISessionRepository SessionRepository { get; set; }

        public ListViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        // attributes
        public IEnumerable<SessionGroup> SessionList
        {
            get
            {
                var grouper = GroupingFactory.GetGroupInstance(GroupingType);
                var dictionary = grouper.Group(SessionRepository.GetAllSessions());
                var result = dictionary.Select(d => new SessionGroup
                                                        {
                                                            Title = d.Key,
                                                            Items = d.Value.Select(s => new SessionListView
                                                                                            {
                                                                                                Title = s.Title,
                                                                                                SessionId = s.SessionId
                                                                                            }).ToList()
                                                        }).ToList();

                return result;
            }
        }

        public string PageTitle
        {
            get
            {
                switch (GroupingType)
                {
                    case SessionGroupType.ByTech:
                        return "by tech";
                    case SessionGroupType.ByBlock:
                        return "by block";
                    case SessionGroupType.ByName:
                        return "by name";
                }

                throw new InvalidOperationException("Unable to determine grouping type");
            }
        }

        // behaviors
        public void SelectionChanged(SelectionChangedEventArgs args)
        {
            var listItem = args.AddedItems[0] as SessionListView;
            if (listItem != null)
            {
                NavigationService.UriFor<SessionViewModel>().WithParam(s => s.IncomingSession, listItem.SessionId)
                    .Navigate();
            }
        }
    }
}