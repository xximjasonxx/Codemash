using System;
using Caliburn.Micro;
using Codemash.Client.Data.Entities;
using Codemash.Client.Parameters;
using Windows.System;
using Windows.UI.Popups;

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

        // behaviors
        public void ViewBlog()
        {
            var dialog = new MessageDialog("Navigating to this URL will take you out of the App. Are you sure?", "Codemash 2.0.1.3");
            dialog.Commands.Add(new UICommand("Yes", command =>
                                                         {
                                                             var url = Speaker.BlogUrl;
                                                             Launcher.LaunchUriAsync(new Uri(url, UriKind.Absolute));
                                                         }));

            dialog.Commands.Add(new UICommand("No"));
            dialog.ShowAsync();
        }
    }
}
