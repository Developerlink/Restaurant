using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RestaurantLibrary;
using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;
using RestaurantWPF.ViewModels.Commands;

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

        public int MyProperty { get; set; }

        private Reservation _SelectedReservation;
        public Reservation SelectedReservation
        {
            get { return _SelectedReservation; }
            set
            {
                _SelectedReservation = value;
                OnPropertyChanged("SelectedReservation");
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
        public List<ArrivalStatus> ArrivalStatuses { get; set; }

        public ICommand UpdateReservationOverviewCommand { get; set; }
        public ICommand ChangeArrivalStatusCommand { get; set; }

        // CONSTRUCTOR
        public ReservationManagerViewModel()
        {
            SqlConnector conn = new SqlConnector();
            // Restaurant Id is hardcoded because I only have 1 restaurant, else it should have been selected by the user.
            int restaurantId = 1;
            SelectedRestaurant = conn.GetRestaurant(restaurantId);
            SelectedArea = SelectedRestaurant.Areas[0];
            ArrivalStatuses = conn.GetArrivalStatuses();
            UpdateSelectedDateReservations();
            LoadCommands();
        }

        // METHODS
        private void LoadCommands()
        {
            UpdateReservationOverviewCommand = new SimpleExecuteCommand(UpdateSelectedDateReservations);
            ChangeArrivalStatusCommand = new SimpleExecuteCommand(UpdateSelectedDateReservations);
        }
        private void UpdateSelectedDateReservations()
        {
            SqlConnector conn = new SqlConnector();
            SelectedDateReservations = conn.GetReservations(SelectedDate, SelectedArea).ToObservableCollection();
        }

        private void ChangeArrivalStatusForSelectedReservation(ArrivalStatus arrivalStatus)
        {
            SqlConnector conn = new SqlConnector();
            SelectedReservation.ArrivalStatus = arrivalStatus;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
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
