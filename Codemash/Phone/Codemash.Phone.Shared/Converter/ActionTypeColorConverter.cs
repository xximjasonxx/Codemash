using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Codemash.Phone.Data.Common;

namespace Codemash.Phone.Shared.Converter
{
    public class ActionTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var actionType = (ActionType)value;
            switch (actionType)
            {
                case ActionType.Add:
                    return Color.FromArgb(255, 0, 155, 0);
                case ActionType.Modify:
                    return Color.FromArgb(255, 251, 129, 2);
                case ActionType.Delete:
                    return Color.FromArgb(255, 255, 0, 0);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
