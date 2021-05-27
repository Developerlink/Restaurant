using RestaurantLibrary.DataAccess;
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
    public class ArrivalStatusIdToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string id = (string)value;
            string statusString = "";
            SqlConnector conn = new SqlConnector();
            List<ArrivalStatus> arrivalStatuses = conn.GetArrivalStatuses();
            foreach (var arrivalStatus in arrivalStatuses)
            {
                if (arrivalStatus.Id.ToString() == id)
                {
                    statusString = arrivalStatus.Status;
                }
            }
            return statusString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
