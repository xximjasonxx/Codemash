using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Codemash.Phone7.App.Views
{
    public partial class MainView : PhoneApplicationPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode != NavigationMode.Back)
            {
                MessageBox.Show("This is an alpha release. This app does not use the real Codemash REST API. Please submit UI feedback to imjason@gmail.com");
            }
        }
    }
}