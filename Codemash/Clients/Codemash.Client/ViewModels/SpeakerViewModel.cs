using Caliburn.Micro;
using Codemash.Client.Data.Entities;
using Codemash.Client.Parameters;

namespace Codemash.Client.ViewModels
{
    public class SpeakerViewModel : ViewModelBase
    {
        public SpeakerParameter Parameter { get; set; }

        public SpeakerViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        // attributes
        public Speaker Speaker
        {
            get { return Parameter.Value; }
        }
    }
}
