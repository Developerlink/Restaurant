using RestaurantLibrary;
using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;
using RestaurantWPF.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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

        public ObservableCollection<DinnerTable> CurrentFreeTables { get; set; }

        public List<ArrivalStatus> ArrivalStatuses { get; set; }

        private ArrivalStatus _SelectedArrivalStatus;
        public ArrivalStatus SelectedArrivalStatus
        {
            get { return _SelectedArrivalStatus; }
            set
            {
                _SelectedArrivalStatus = value;
                OnPropertyChanged("SelectedArrivalStatus");
            }
        }

        //public ICommand SelectAreaCommand { get; set; }
        public ICommand CreateReservationCommand { get; set; }

        // CONSTRUCTOR
        public CreateReservationViewModel()
        {
            ArrivalStatuses = new List<ArrivalStatus>();
            CurrentFreeTables = new ObservableCollection<DinnerTable>();
            SelectedArea = new Area();            
            LoadData();
            CreateReservationCommand = new CreateReservatinCommand(CreateReservation, CanCreateReservation);
            CurrentReservation = new Reservation();
        }        

        // Properties
        private void LoadData()
        {
            SqlConnector conn = new SqlConnector();
            Restaurant = conn.GetRestaurant(1);
            Restaurant.Areas = conn.GetAreas(Restaurant.Id);
            foreach (var area in Restaurant.Areas)
            {
                area.DinnerTables = conn.GetTables(area.Id);
            }
            SelectedArea = Restaurant.Areas[0];
            CurrentFreeTables = SelectedArea.DinnerTables.ToObservableCollection();
            ArrivalStatuses = conn.GetArrivalStatuses();
        }

        // Commands
        private void CreateReservation(object obj)
        {

        }

        private bool CanCreateReservation(object obj)
        {
            if(CurrentReservation != null)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }


        private void LoadCommands()
        {
        }

        // Methods
        private bool CanExecute()
        {
            return false;
        }

        // Interface implementation
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
