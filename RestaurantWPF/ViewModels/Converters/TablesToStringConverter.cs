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
    public class TablesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<DinnerTable> tables = (List<DinnerTable>)value;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tables.Count; i++)
            {
                if (i != tables.Count - 1)
                {
                    sb.Append($"{tables[i].Name}, ");
                }
                else
                {
                    sb.Append($"{tables[i].Name}");
                }
            }

            return sb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
