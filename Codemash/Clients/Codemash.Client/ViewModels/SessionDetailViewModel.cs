
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
                    StartsAt = session.Starts.AsDateTimeDisplay(),
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

        private void NotifyContentDisplayChanged()
        {
            NotifyOfPropertyChange("CanShowAbstract");
            NotifyOfPropertyChange("CanShowSpeaker");
        }
    }
}
