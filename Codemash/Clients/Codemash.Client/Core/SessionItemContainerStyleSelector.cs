using Codemash.Client.Code;
using Codemash.Client.DataModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Codemash.Client.Core
{
    public class SessionItemContainerStyleSelector : StyleSelector
    {
        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            var listItem = item as IListItem;
            if (listItem != null)
            {
                if (listItem.ItemType == ItemType.GroupHeading)
                    return App.Current.Resources["GroupHeaderItem"] as Style;
                return App.Current.Resources["GroupItem"] as Style;
            }

            return null;
        }
    }
}
