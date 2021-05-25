using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using RestaurantLibrary;
using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;

namespace RestaurantWPF.ViewModels
{
    //public BindableCollection<DinnerTable> Tables { get; set; }
    public class ReservationManagerViewModel : INotifyPropertyChanged
    {
        // PROPERTIES
        private Restaurant _SelectedRestaurant;
        public Restaurant SelectedRestaurant
        {
            get { return _SelectedRestaurant; }
            set
            {
                _SelectedRestaurant = value;
                OnPropertyChanged("SelectedRestaurant");
            }
        }

        private DateTime _SelectedDate = DateTime.Now.ChangeTime();
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private Area _SelectedArea;
        public Area SelectedArea
        {
            get { return _SelectedArea; }
            set
            {
                _SelectedArea = value;
                OnPropertyChanged("SelectedArea");
            }
        }

        private ObservableCollection<Reservation> _SelectedDateReservations;
        public ObservableCollection<Reservation> SelectedDateReservations
        {
            get { return _SelectedDateReservations; }
            set
            {
                _SelectedDateReservations = value;
                OnPropertyChanged("SelectedDateReservations");
            }
        }

        // CONSTRUCTOR
        public ReservationManagerViewModel()
        {
            SqlConnector conn = new SqlConnector();
            // Restaurant Id is hardcoded because I only have 1 restaurant, else it should have been selected by the user.
            int restaurantId = 1;
            SelectedRestaurant = conn.GetRestaurant(restaurantId);
            SelectedArea = SelectedRestaurant.Areas[0];
            UpdateSelectedDateReservations(SelectedDate, SelectedArea);
        }

        // METHODS
        private void UpdateSelectedDateReservations(DateTime selectedDate, Area selectedArea)
        {
            SqlConnector conn = new SqlConnector();
            SelectedDateReservations = conn.GetReservations(SelectedDate, SelectedArea).ToObservableCollection();
        }



        // INotifyPropertyChanged INTERFACE IMPLEMENTATION.
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
