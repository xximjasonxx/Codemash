using Codemash.Client.Common;
using Codemash.Client.Components;
using Windows.UI.Xaml;

namespace Codemash.Client.Views
{
    public sealed partial class SearchResultsView : LayoutAwarePage
    {
        public SearchResultsView()
        {
            this.InitializeComponent();
        }

        protected override void HandleLayoutChange(DisplayModeType displayType)
        {
            SnappedListView.Visibility = Visibility.Collapsed;
            FullGridView.Visibility = Visibility.Visible;
            GoBack.Margin = new Thickness(36, 0, 36, 0);
            PageTitle.Style = (Style)App.Current.Resources["PageTitle"];
            PageTitle.TextWrapping = TextWrapping.NoWrap;
            PageTitle.Margin = new Thickness(20, 0, 0, 7);
            NoResultsText.Margin = new Thickness(100, 7, 5, 5);

            if (displayType == DisplayModeType.Snapped)
            {
                SnappedListView.Visibility = Visibility.Visible;
                FullGridView.Visibility = Visibility.Collapsed;
                GoBack.Margin = new Thickness(5, 0, 5, 0);
                PageTitle.Margin = new Thickness(10, 0, 0, 0);
                PageTitle.Style = (Style)App.Current.Resources["SnappedPageTitle"];
                PageTitle.TextWrapping = TextWrapping.Wrap;
                NoResultsText.Margin = new Thickness(10, 7, 5, 5);
            }
        }
    }
}
