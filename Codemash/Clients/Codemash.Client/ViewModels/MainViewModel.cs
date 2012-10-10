using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Codemash.Client.Data.Repository;

namespace Codemash.Client.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ISessionRepository SessionRepository { get; set; }

        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        // behaviors
        public void ShowAllSessions()
        {
            return;
        }

        public void ShowFavorites()
        {
            return;
        }

        public void ShowSesionsByBlock()
        {
            return;
        }

        public void ShowSessionsByTrack()
        {
            return;
        }
    }
}
