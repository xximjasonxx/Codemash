using System.Linq;
using Codemash.Client.Common;
using Codemash.Client.Components;
using Windows.UI.Xaml;

namespace Codemash.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionsListView : LayoutAwarePage
    {
        public SessionsListView()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            GroupList.ItemsSource = SessionList.View.CollectionGroups.ToList();
        }

        protected override void HandleLayoutChange(DisplayModeType displayType)
        {
            SnappedListView.Visibility = Visibility.Collapsed;
            FullGridView.Visibility = Visibility.Visible;
            GoBack.Margin = new Thickness(36, 0, 36, 0);
            PageTitle.Style = (Style) App.Current.Resources["PageTitle"];
            PageTitle.TextWrapping = TextWrapping.NoWrap;
            PageTitle.Margin = new Thickness(20, 0, 0, 7);

            if (displayType == DisplayModeType.Snapped)
            {
                SnappedListView.Visibility = Visibility.Visible;
                FullGridView.Visibility = Visibility.Collapsed;
                GoBack.Margin = new Thickness(5, 0, 5, 0);
                PageTitle.Margin = new Thickness(10, 0, 0, 0);
                PageTitle.Style = (Style)App.Current.Resources["SnappedPageTitle"];
                PageTitle.TextWrapping = TextWrapping.Wrap;
            }
        }

        private void DismissAppBar(object sender, RoutedEventArgs e)
        {
            AppBar.IsOpen = false;
        }
    }
}
