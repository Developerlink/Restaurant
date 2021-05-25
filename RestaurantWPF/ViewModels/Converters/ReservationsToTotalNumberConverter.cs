using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RestaurantWPF.ViewModels.Converters
{
    public class ReservationsToTotalNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Reservation> reservations = (ObservableCollection<Reservation>)value;
            int totalNumber = reservations.Sum(reservation => reservation.WantedSeats);

            return totalNumber;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
