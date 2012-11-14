
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

            CanShowAbstract = false;
            CanShowSpeaker = true;
        }

        // attributes
        public SessionDetailModel Session
        {
            get
            {
                var session = Parameter.Value;
                var speaker = SpeakerRepository.Get(session.SpeakerId);
                return new SessionDetailModel
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
            }
        }
        public bool CanShowAbstract { get; private set; }
        public bool CanShowSpeaker { get; private set; }

        // behaviors
        public void ShowAbstract()
        {
            CanShowAbstract = false;
            CanShowSpeaker = true;
            NotifyContentDisplayChanged();
        }

        public void ShowSpeaker()
        {
            CanShowAbstract = true;
            CanShowSpeaker = false;
            NotifyContentDisplayChanged();
        }

        public void ViewMap()
        {
            NavigationService.Navigate(typeof(Views.MapView));
        }

        public void GoBlog()
        {
            if (Session.Speaker.HasBlog)
            {
                string url = Session.Speaker.BlogUrl;
                NavigateToUrl(url);
            }
        }

        public void GoTwitter()
        {
            if (Session.Speaker.HasTwitter)
            {
                string url = string.Format("http://www.twitter.com/{0}", Session.Speaker.Twitter);
                NavigateToUrl(url);
            }
        }

        // methods
        private void NotifyContentDisplayChanged()
        {
            NotifyOfPropertyChange("CanShowAbstract");
            NotifyOfPropertyChange("CanShowSpeaker");
        }

        private void NavigateToUrl(string url)
        {
            Launcher.LaunchUriAsync(new Uri(url, UriKind.Absolute));
        }
    }
}
