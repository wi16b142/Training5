﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media; //for the colors

namespace Training5.Converter
{
    public class BoolToColorConverter : IValueConverter //very important! don't forget to make it public and inherit from IValueConverter
    {
        SolidColorBrush green = new SolidColorBrush(Colors.Green); //define the color green
        SolidColorBrush red = new SolidColorBrush(Colors.Red); //define the color red


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) //autogenerated code from the interface
        {
            bool state = (bool)value; //save the value from the parameter into a variable of the targettype (bool)

            if(state == true)
            {
                return green; //return green if the state is true
            }

            return red; //return red if the satate is false
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) //autogenerated code from the interface
        {
            throw new NotImplementedException();
        }
    }
}
