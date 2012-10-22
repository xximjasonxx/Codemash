
using Caliburn.Micro;
using Codemash.Client.DataModels;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class SessionDetailViewModel : ViewModelBase
    {
        // parameters
        public SessionParameter Parameter { get; set; }

        public SessionDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            SessionDetailsVisible = true;
            SpeakerDetailsVisible = false;
        }

        // attributes
        public SessionDetailModel Session { get; set; }
        public bool SessionDetailsVisible { get; set; }
        public bool SpeakerDetailsVisible { get; set; }

        // behaviors
        public void PageLoaded()
        {
            var session = Parameter.Value;
            Session = new SessionDetailModel
                          {
                              Title = session.Title
                          };

            NotifySessionUpdated();
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

        // methods
        private void NotifySessionUpdated()
        {
            NotifyOfPropertyChange("Session");
        }

        private void NotifyDetailsVisibleUpdated()
        {
            NotifyOfPropertyChange("SessionDetailsVisible");
            NotifyOfPropertyChange("SpeakerDetailsVisible");
        }
    }
}
