using System;
using Windows.UI.Text;
using Windows.UI.Xaml.Data;

namespace Codemash.Client.Code
{
    public class BooleanToFontWeightConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var boolVal = (bool) value;
            return boolVal ? FontWeights.Bold : FontWeights.Light;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
