using Codemash.Client.Code;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Core
{
    [Windows.Foundation.Metadata.WebHostHidden]
    public class LayoutAwarePage : Page
    {
        protected LayoutAwarePage()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;
            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            // get the view type
            var displayType = ApplicationView.Value.ToString().AsDisplayType();

            // call our handler
            HandleLayoutChange(displayType);
        }

        protected virtual void HandleLayoutChange(DisplayModeType displayType)
        {
            return;
        }
    }
}
