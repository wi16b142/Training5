using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Training5.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        SolidColorBrush green = new SolidColorBrush(Colors.Green);
        SolidColorBrush red = new SolidColorBrush(Colors.Red);


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool state = (bool)value;

            if(state == true)
            {
                return green;
            }

            return red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
