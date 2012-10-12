using Codemash.Client.Code;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Core
{
    public class SessionItemTemplateSelector : DataTemplateSelector
    {
        protected override Windows.UI.Xaml.DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            IListItem listItem = (IListItem) item;

            if (listItem.ItemType == ItemType.GroupHeading)
                return App.Current.Resources["sessionGroupItemTemplate"] as DataTemplate;
            return App.Current.Resources["sessionItemTemplate"] as DataTemplate;
        }
    }
}
