using RestaurantLibrary;
using RestaurantLibrary.DataAccess;
using RestaurantLibrary.Models;
using RestaurantWPF.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RestaurantWPF.ViewModels
{
    public class ReservationEditorViewModel : INotifyPropertyChanged
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

        private DinnerTable _SelectedTableFromFreeTables;
        public DinnerTable SelectedTableFromFreeTables
        {
            get { return _SelectedTableFromFreeTables; }
            set
            {
                _SelectedTableFromFreeTables = value;
                OnPropertyChanged("SelectedTableFromFreeTables");
            }
        }

        private DinnerTable _SelectedTableFromReservationTables;
        public DinnerTable SelectedTableFromReservationTables
        {
            get { return _SelectedTableFromReservationTables; }
            set
            {
                _SelectedTableFromReservationTables = value;
                OnPropertyChanged("SelectedTableFromReservationTables");
            }
        }

        private Guest _SelectedGuets;
        public Guest SelectedGuest
        {
            get { return _SelectedGuets; }
            set
            {
                _SelectedGuets = value;
                OnPropertyChanged("SelectedGuest");
            }
        }

        private int _SelectedWantedSeats;
        public int SelectedWantedSeats
        {
            get { return _SelectedWantedSeats; }
            set
            {
                _SelectedWantedSeats = value;
                OnPropertyChanged("SelectedWantedSeats");
            }
        }

        private DateTime _SelectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private TimeUnit _SelectedTimeIn;
        public TimeUnit SelectedTimeIn
        {
            get { return _SelectedTimeIn; }
            set
            {
                _SelectedTimeIn = value;
                OnPropertyChanged("CurrentTimeIn");
            }
        }

        private TimeUnit _SelectedTimeOut;
        public TimeUnit SelectedTimeOut
        {
            get { return _SelectedTimeOut; }
            set
            {
                _SelectedTimeOut = value;
                OnPropertyChanged("CurrentTTimeOut");
            }
        }

        private ObservableCollection<DinnerTable> _CurrentFreeTables;
        public ObservableCollection<DinnerTable> CurrentFreeTables
        {
            get { return _CurrentFreeTables; }
            set
            {
                _CurrentFreeTables = value;
                OnPropertyChanged("CurrentFreeTables");
            }
        }

        private ObservableCollection<DinnerTable> _SelectedReservedTables;
        public ObservableCollection<DinnerTable> SelectedReservedTables
        {
            get { return _SelectedReservedTables; }
            set
            {
                _SelectedReservedTables = value;
                OnPropertyChanged("CurrentReservedTables");
            }
        }

        private ObservableCollection<ArrivalStatus> _ArrivalStatuses;
        public ObservableCollection<ArrivalStatus> ArrivalStatuses
        {
            get { return _ArrivalStatuses; }
            set
            {
                _ArrivalStatuses = value;
                OnPropertyChanged("ArrivalStatuses");
            }
        }

        public ICommand SelectAreaCommand { get; set; }
        public ICommand CreateReservationCommand { get; set; }
        public ICommand AddTableToReservationCommand { get; set; }
        public ICommand RemoveTableFromReservationCommand { get; set; }

        // CONSTRUCTOR
        public ReservationEditorViewModel()
        {
            ArrivalStatuses = new ObservableCollection<ArrivalStatus>();
            CurrentFreeTables = new ObservableCollection<DinnerTable>();
            SelectedReservedTables = new ObservableCollection<DinnerTable>();
            SelectedTableFromFreeTables = new DinnerTable();
            SelectedTableFromReservationTables = new DinnerTable();
            CurrentReservation = new Reservation();
            SelectedArea = new Area();
            SelectedTimeIn = new TimeUnit();
            SelectedTimeOut = new TimeUnit();
            SelectedGuest = new Guest();
            LoadRestaurantData();
            LoadCommands();
            LoadDummyData();
        }

        private void LoadRestaurantData()
        {
            SqlConnector conn = new SqlConnector();
            // Restaurant Id is hardcoded because I only have 1 restaurant, else it should have been selected by the user.
            int restaurantId = 1;
            Restaurant = conn.GetRestaurant(restaurantId);
            SelectedArea = Restaurant.Areas[0];
            if (CurrentReservation.Tables.Any())
            {
                SelectedReservedTables = CurrentReservation.Tables.ToObservableCollection();
            }
            CurrentFreeTables = SelectedArea.DinnerTables.ToObservableCollection();
            ArrivalStatuses = conn.GetArrivalStatuses().ToObservableCollection();
            SelectedArrivalStatus = ArrivalStatuses[0];

            if (CurrentReservation.Id != 0)
            {
                LoadCurrentReservationData();
            }

        }

        private void LoadDummyData()
        {
            SelectedGuest.FirstName = "Diana";
            SelectedGuest.LastName = "Prince";
            SelectedGuest.PhoneNumber = 87698769;
            SelectedWantedSeats = 4;
            SelectedTimeIn.Hour = 18;
            SelectedTimeIn.Minute = 30;
            SelectedReservedTables.Add(CurrentFreeTables[0]);
            SelectedReservedTables.Add(CurrentFreeTables[1]);
            UpdateCurrentFreeTables();
        }

        private void LoadCurrentReservationData()
        {
            // If guest is not empty then transfer the data into the VM's binded guest object.
            if (CurrentReservation.Guest != null)
            {
                SelectedGuest = CurrentReservation.Guest;
            }
            // If there are tables then transfer them to the VM's observablecollection of reserved tables.
            if (CurrentReservation.Tables.Any())
            {
                SelectedReservedTables = CurrentReservation.Tables.ToObservableCollection();
                // Find the area of the first table and set the VM's selected area to match it. 
                foreach (var area in Restaurant.Areas)
                {
                    if (area.Id == SelectedReservedTables[0].AreaId)
                    {
                        SelectedArea = area;
                    }
                }
                UpdateCurrentFreeTables();
            }
            SelectedDate = CurrentReservation.TimeIn;
            SelectedTimeIn.Hour = CurrentReservation.TimeIn.Hour;
            SelectedTimeIn.Minute = CurrentReservation.TimeIn.Minute;
            SelectedArrivalStatus = CurrentReservation.ArrivalStatus;
        }

        // Commands
        private void LoadCommands()
        {
            CreateReservationCommand = new CreateReservationCommand(CreateReservation, CanCreateReservation);
            SelectAreaCommand = new SelectAreaUpdateFreeTablesCommand(UpdateCurrentFreeTables);
            AddTableToReservationCommand = new AddTableToReservationCommand(AddTableToCurrentReservation);
            RemoveTableFromReservationCommand = new RemoveTableFromReservationCommand(RemoveTableFromCurrentReservation);
        }

        // Methods
        private void CreateReservation(object obj)
        {
            CurrentReservation.Guest = SelectedGuest;
            CurrentReservation.Area = SelectedArea;
            CurrentReservation.ArrivalStatus = SelectedArrivalStatus;
            CurrentReservation.WantedSeats = SelectedWantedSeats;
            foreach (var table in SelectedReservedTables)
            {
                CurrentReservation.Tables.Add(table);
            }         
            CurrentReservation.TimeIn = SelectedDate.ChangeTime(SelectedTimeIn.Hour, SelectedTimeIn.Minute);
            if (SelectedTimeOut.Hour != 0 && SelectedTimeOut.Minute != 0)
            {
                CurrentReservation.TimeOut = new DateTime();
                CurrentReservation.TimeOut = SelectedDate.ChangeTime(SelectedTimeOut.Hour, SelectedTimeOut.Minute);
            }
            if (!string.IsNullOrEmpty(SelectedGuest.FirstName) || !string.IsNullOrEmpty(SelectedGuest.LastName))
            {
                CurrentReservation.Guest = SelectedGuest;
            }

            string result = CurrentReservation.IsValid();
            if (result == "true")
            {
                SqlConnector conn = new SqlConnector();
                // If guest is not null then insert or update in people table.
                if (CurrentReservation.Guest != null)
                {
                    conn.CreateOrUpdateGuest(CurrentReservation.Guest);
                }
                // Whether guest is null or not does not affect the reservation itself. 
                conn.CreateReservation(CurrentReservation);
                MessageBox.Show("Your reservation has been saved!");
            }
            else
            {
                MessageBox.Show(result);
            }

        }

        private bool CanCreateReservation(object obj)
        {
            bool result = true;
            if (SelectedWantedSeats == 0)
            {
                result = false;
            }
            return result;
        }

        private void UpdateCurrentFreeTables()
        {
            CurrentFreeTables.Clear();
            foreach (var t in SelectedArea.DinnerTables)
            {
                CurrentFreeTables.Add(t);
            }
            if (SelectedReservedTables.Any())
            {
                foreach (var t in SelectedReservedTables)
                {
                    CurrentFreeTables.Remove(t);
                }
            }
        }

        private void AddTableToCurrentReservation()
        {
            if (SelectedTableFromFreeTables != null)
            {
                SelectedReservedTables.Add(SelectedTableFromFreeTables);
                CurrentFreeTables.Remove(SelectedTableFromFreeTables);
                //UpdateCurrentFreeTables();
                SelectedTableFromFreeTables = null;
            }
        }

        private void RemoveTableFromCurrentReservation()
        {
            if (SelectedTableFromReservationTables != null)
            {
                SelectedReservedTables.Remove(SelectedTableFromReservationTables);
                //CurrentFreeTables.Add(SelectedTableFromReservationTables);
                UpdateCurrentFreeTables();
                SelectedTableFromReservationTables = null;
            }
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
