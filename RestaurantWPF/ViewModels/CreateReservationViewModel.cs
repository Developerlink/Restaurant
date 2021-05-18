using RestaurantLibrary;
using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWPF.ViewModels
{
    public class CreateReservationViewModel : INotifyPropertyChanged
    {
        private Restaurant _Restaurant;
        public Restaurant Restaurant
        {
            get { return _Restaurant; }
            set
            {
                _Restaurant = value;
                OnPropertyChanged("Restaurant");
            }
        }

        private Area _CurrentArea;
        public Area CurrentArea
        {
            get { return _CurrentArea; }
            set
            {
                _CurrentArea = value;
                OnPropertyChanged("CurrentArea");
            }
        }


        private Guest _CurrentGuest;
        public Guest CurrentGuest
        {
            get { return _CurrentGuest; }
            set
            {
                _CurrentGuest = value;
                OnPropertyChanged("CurrentGuest");
            }
        }


        private Reservation _CurrentReservation;
        public Reservation CurrentReservation
        {
            get { return _CurrentReservation; }
            set
            {
                _CurrentReservation = value;
                OnPropertyChanged("CurrentReservation");
            }
        }

        public string FirstName { get; set; } = "Peter";


        public ObservableCollection<DinnerTable> CurrentFreeTables;

        public List<ArrivalStatus> ArrivalStatuses { get; set; }

        private ArrivalStatus _SelectedArrivalStatus;
        public ArrivalStatus CurrentArrivalStatus
        {
            get { return _SelectedArrivalStatus; }
            set
            {
                _SelectedArrivalStatus = value;
                OnPropertyChanged("CurrentArrivalStatus");
            }
        }



        public CreateReservationViewModel()
        {
            ArrivalStatuses = new List<ArrivalStatus>();
            LoadData();
        }


        private void LoadData()
        {
            SqlConnector conn = new SqlConnector();
            ArrivalStatuses = conn.GetArrivalStatuses();
        }

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
