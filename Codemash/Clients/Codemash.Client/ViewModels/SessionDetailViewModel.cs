
using System;
using Caliburn.Micro;
using Codemash.Client.Core;
using Codemash.Client.Data.Repository;
using Codemash.Client.DataModels;
using Codemash.Client.Parameters;
using Windows.System;
using Windows.UI.Popups;

namespace Codemash.Client.ViewModels
{
    public class SessionDetailViewModel : ViewModelBase
    {
        // parameters
        public SessionParameter Parameter { get; set; }

        // depts
        public ISpeakerRepository SpeakerRepository { get; set; }

        public SessionDetailViewModel(INavigationService navigationService, ISpeakerRepository speakerRepository) : base(navigationService)
        {
            SpeakerRepository = speakerRepository;

            SessionDetailsVisible = true;
            SpeakerDetailsVisible = false;
        }

        // attributes
        public SessionDetailModel Session { get; set; }
        public bool SessionDetailsVisible { get; set; }
        public bool SpeakerDetailsVisible { get; set; }
        public bool NotHasBlog { get; set; }
        public bool HasBlog { get; set; }

        // behaviors
        public void PageLoaded()
        {
            var session = Parameter.Value;
            var speaker = SpeakerRepository.Get(session.SpeakerId);
            Session = new SessionDetailModel
                          {
                              Title = session.Title,
                              Technology = session.Technology,
                              Difficulty = session.Difficulty,
                              Duration = session.Duration.AsDurationString(),
                              Room = session.Room,
                              StartsAt = session.Starts.AsTimeDisplay(),
                              Abstract = session.Abstract,
                              Speaker = new SpeakerDetailModel
                                            {
                                                Biography = speaker.Biography,
                                                BlogUrl = speaker.BlogUrl,
                                                Name = speaker.Name,
                                                Twitter = speaker.Twitter
                                            }
                          };

            NotifySessionUpdated();
            UpdateBlogVisibilityStatus();
        }

        public void ShowDetails()
        {
            SpeakerDetailsVisible = false;
            SessionDetailsVisible = true;
            NotifyDetailsVisibleUpdated();
        }

        public void ShowSpeaker()
        {
            SpeakerDetailsVisible = true;
            SessionDetailsVisible = false;
            NotifyDetailsVisibleUpdated();
        }

        public void ViewMap()
        {
            NavigationService.NavigateToViewModel<MapViewModel>();
        }

        // methods
        public void ShowBlog()
        {
            var dialog = new MessageDialog("Navigating to this URL will take you out of the App. Are you sure?", "Codemash 2.0.1.3");
            dialog.Commands.Add(new UICommand("Yes", command =>
                                                         {
                                                             var url = Session.Speaker.BlogUrl;
                                                             Launcher.LaunchUriAsync(new Uri(url, UriKind.Absolute));
                                                         }));

            dialog.Commands.Add(new UICommand("No"));
            dialog.ShowAsync();
        }

        private void NotifySessionUpdated()
        {
            NotifyOfPropertyChange("Session");
        }

        private void NotifyDetailsVisibleUpdated()
        {
            NotifyOfPropertyChange("SessionDetailsVisible");
            NotifyOfPropertyChange("SpeakerDetailsVisible");
        }

        private void UpdateBlogVisibilityStatus()
        {
            NotHasBlog = string.IsNullOrEmpty(Session.Speaker.BlogUrl);
            HasBlog = !string.IsNullOrEmpty(Session.Speaker.BlogUrl);

            NotifyOfPropertyChange("NotHasBlog");
            NotifyOfPropertyChange("HasBlog");
        }
    }
}
