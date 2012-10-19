using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;
using Codemash.Phone7.Data.Entities;

namespace Codemash.Phone7.App.ViewModels
{
    public class SessionViewModel : ViewModelBase
    {
        public Session IncomingSession { get; set; }

        public SessionViewModel(INavigationService navigationService) : base(navigationService)
        {
        }
    }
}
