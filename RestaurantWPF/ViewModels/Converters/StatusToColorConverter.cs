using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantWPF.ViewModels.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ArrivalStatus arrivalStatus = (ArrivalStatus)value;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
