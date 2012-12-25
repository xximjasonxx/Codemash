using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone.Core;
using Codemash.Phone.Data.Repository;
using Codemash.Phone.Shared.Common;
using Codemash.Phone.Shared.DataModels;
using Codemash.Phone.Shared.Grouping;
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

        [Inject]
        public ISettingsRepository SettingsRepository { get; set; }

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
                var result = dictionary.Select(d => new SessionGroup(d.Key, d.Value.Select(s => new SessionListView
                                                                                            {
                                                                                                Title = s.Title,
                                                                                                SessionId = s.SessionId,
                                                                                                StartsAt = s.Starts.AsDateTime().AsBlockDisplay()
                                                                                            }))).ToList();

                return result;
            }
        }

        public bool ShowGrid { get { return GroupingType == SessionGroupType.ByName; } }
        public bool ShowList { get { return GroupingType != SessionGroupType.ByName; } }

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
                    case SessionGroupType.ByRoom:
                        return "by room";
                }

                throw new InvalidOperationException("Unable to determine grouping type");
            }
        }

        // behaviors
        public void PageLoaded()
        {
            if (!SettingsRepository.HasSeenListPage)
            {
                MessageBox.Show("Touch the Group Headers to quickly move through the whole list");
                SettingsRepository.HasSeenListPage = true;
            }
        }

        public void SelectionChanged(SelectionChangedEventArgs args)
        {
            if (args.AddedItems.Count > 0)
            {
                var listItem = args.AddedItems[0] as SessionListView;
                if (listItem != null)
                {
                    NavigationService.UriFor<SessionViewModel>().WithParam(s => s.IncomingSession, listItem.SessionId)
                        .Navigate();
                }
            }
        }

        public void SearchSessions()
        {
            NavigationService.UriFor<SearchViewModel>().Navigate();
        }
    }
}