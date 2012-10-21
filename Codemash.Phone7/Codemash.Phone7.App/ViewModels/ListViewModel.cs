using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;
using Codemash.Phone7.App.Common;
using Codemash.Phone7.App.DataModels;
using Codemash.Phone7.App.Grouping;
using Codemash.Phone7.Core;
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
                var result = dictionary.Select(d => new SessionGroup
                                                        {
                                                            Title = d.Key,
                                                            Items = d.Value.Select(s => new SessionListView
                                                                                            {
                                                                                                Title = s.Title,
                                                                                                SessionId = s.SessionId,
                                                                                                Duration = s.Duration.AsDurationString()
                                                                                            }).ToList()
                                                        }).ToList();

                return result.OrderBy(r => r.Title);
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
        public void PageLoaded()
        {
            if (!SettingsRepository.HasSeenListPage)
            {
                MessageBox.Show("Touch the Group Headers to quickly move through the whole list");
                SettingsRepository.HasSeenListPage = true;
                SettingsRepository.Save();
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