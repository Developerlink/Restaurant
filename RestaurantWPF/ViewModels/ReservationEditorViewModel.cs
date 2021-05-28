﻿using RestaurantLibrary;
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

        private ObservableCollection<DinnerTable> _CurrentReservedTables;
        public ObservableCollection<DinnerTable> CurrentReservedTables
        {
            get { return _CurrentReservedTables; }
            set
            {
                _CurrentReservedTables = value;
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
            CurrentReservedTables = new ObservableCollection<DinnerTable>();
            SelectedTableFromFreeTables = new DinnerTable();
            SelectedTableFromReservationTables = new DinnerTable();
            CurrentReservation = new Reservation();            
            SelectedArea = new Area();
            LoadData();
            LoadCommands();
        }

        // Properties
        private void LoadData()
        {
            SqlConnector conn = new SqlConnector();
            // Restaurant Id is hardcoded because I only have 1 restaurant, else it should have been selected by the user.
            int restaurantId = 1;
            Restaurant = conn.GetRestaurant(restaurantId);
            SelectedArea = Restaurant.Areas[0];
            CurrentFreeTables = SelectedArea.DinnerTables.ToObservableCollection();
            ArrivalStatuses = conn.GetArrivalStatuses().ToObservableCollection();
            SelectedArrivalStatus = ArrivalStatuses[0];
            if (CurrentReservation.TimeIn != null)
            {
                SelectedDate = CurrentReservation.TimeIn;
                
            }
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
            if (string.IsNullOrEmpty(CurrentReservation.Guest.FirstName))
            {
                CurrentReservation.Guest.FirstName = "Walk-in";
            }
            MessageBox.Show(CurrentReservation.Guest.FirstName);


            //MessageBox.Show("Your reservation has been saved!");
        }

        private bool CanCreateReservation(object obj)
        {
            bool result = true;
            if (CurrentReservation.WantedSeats == 0)
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
            if (CurrentReservation.Tables.Any())
            {
                foreach (var t in CurrentReservation.Tables)
                {
                    CurrentFreeTables.Remove(t);
                }
            }
        }

        private void AddTableToCurrentReservation()
        {
            if (_SelectedTableFromFreeTables != null)
            {
                CurrentReservation.Tables.Add(SelectedTableFromFreeTables);                
                UpdateCurrentFreeTables();
            }
        }

        private void RemoveTableFromCurrentReservation()
        {   
                CurrentReservation.Tables.Remove(SelectedTableFromReservationTables);
                UpdateCurrentFreeTables();            
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