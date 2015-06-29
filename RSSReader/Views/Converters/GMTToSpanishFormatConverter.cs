using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace RSSReader.Views.Converters
{
    public class GMTToSpanishFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result = "";
            string valueResult = (string)value;
            DateTime dateValue = DateTime.Parse(valueResult);
            result = dateValue.ToString("D",new CultureInfo("es-ES"));
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
