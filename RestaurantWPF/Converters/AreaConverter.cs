using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantWPF.Converters
{
    public class AreaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int AreaId = (int)value;

            if(AreaId == 1)
            {
                return "Buffet";
            }
            else
            {
                return "Sushi";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;  
        }
    }
}
