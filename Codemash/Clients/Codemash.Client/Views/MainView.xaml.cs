using Codemash.Client.Common;
using Codemash.Client.Components;
using Windows.UI.Xaml;

namespace Codemash.Client.Views
{
    public sealed partial class MainView : LayoutAwarePage
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        protected override void HandleLayoutChange(DisplayModeType displayType)
        {
            FilledGridView.Visibility = Visibility.Visible;
            SnappedListView.Visibility = Visibility.Collapsed;
            PageTitle.Style = (Style) App.Current.Resources["PageTitle"];
            PageTitle.Margin = new Thickness(20,0,0,7);
            AppBar.Visibility = Visibility.Visible;

            if (displayType == DisplayModeType.Snapped)
            {
                FilledGridView.Visibility = Visibility.Collapsed;
                SnappedListView.Visibility = Visibility.Visible;
                PageTitle.Style = (Style) App.Current.Resources["SnappedPageTitle"];
                PageTitle.Margin = new Thickness(10, 0, 0, 7);
                AppBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
