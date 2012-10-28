
using Codemash.Client.Code;
using Codemash.Client.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionDetailView : LayoutAwarePage
    {
        public SessionDetailView()
        {
            this.InitializeComponent();
        }

        protected override void HandleLayoutChange(Code.DisplayModeType displayType)
        {
            SnappedBorder.Visibility = Visibility.Collapsed;
            FullFilledGrid.Visibility = Visibility.Visible;

            if (displayType == DisplayModeType.Snapped)
            {
                SnappedBorder.Visibility = Visibility.Visible;
                FullFilledGrid.Visibility = Visibility.Collapsed;
            }
        }
    }
}
