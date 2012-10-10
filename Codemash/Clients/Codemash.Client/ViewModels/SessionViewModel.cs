using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Codemash.Client.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        public SessionViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        // behaviors
        public void ViewSpeaker()
        {
            return;
        }
    }
}
