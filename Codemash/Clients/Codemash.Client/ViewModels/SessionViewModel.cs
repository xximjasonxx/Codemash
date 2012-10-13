using Caliburn.Micro;
using Codemash.Client.Data.Repository;
using Codemash.Client.DataModels;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        public ISpeakerRepository SpeakerRepository { get; set; }
        public SessionParameter Parameter { get; set; }

        public SessionViewModel(INavigationService navigationService, ISpeakerRepository speakerRepository) : base(navigationService)
        {
            SpeakerRepository = speakerRepository;
        }

        // Attributes
        public SessionDetailView Session
        {
            get
            {
                var detailView = new SessionDetailView(Parameter.Value);
                detailView.SpeakerName = SpeakerRepository.Get(detailView.SpeakerId).FullName;

                return detailView;
            }
        }

        // behaviors
        public void ViewSpeaker()
        {
            var speaker = SpeakerRepository.Get(Parameter.Value.SpeakerId);
            NavigationService.NavigateToViewModel<SpeakerViewModel>(new SpeakerParameter(speaker));
        }
    }
}
