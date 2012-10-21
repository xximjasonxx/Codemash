using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Codemash.Client.Core;
using Codemash.Client.Data.Repository;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void ColorAnimation_Completed_1(object sender, object e)
        {
            var dialog = new MessageDialog("Navigating to this URL will take you out of the App. Are you sure?", "Codemash 2.0.1.3");
            dialog.ShowAsync();
        }
    }
}
