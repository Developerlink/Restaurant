using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
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

        public ArrivalStatus SelectedArrivalStatus { get; set; }

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
        //public List<Button> ArrivalStatusButtons { get; set; }

        public ICommand UpdateReservationOverviewCommand { get; set; }
        public ICommand ChangeStatusToAwaitingCommand { get; set; }
        public ICommand ChangeStatusToArrivedCommand { get; set; }
        public ICommand ChangeStatusToSeatedCommand { get; set; }
        public ICommand ChangeStatusToOrderTakenCommand { get; set; }
        public ICommand ChangeStatusToFinishedCommand { get; set; }
        public ICommand ChangeStatusToCancelledCommand { get; set; }
        public ICommand ChangeStatusToNoShowCommand { get; set; }
        public ICommand CreateNewReservationCommand { get; set; }


        // CONSTRUCTOR
        public ReservationManagerViewModel()
        {
            SqlConnector conn = new SqlConnector();
            // Restaurant Id is hardcoded because I only have 1 restaurant, else it should have been selected by the user.
            int restaurantId = 1;
            SelectedRestaurant = conn.GetRestaurant(restaurantId);
            SelectedArea = SelectedRestaurant.Areas[0];
            ArrivalStatuses = conn.GetArrivalStatuses();
            //ArrivalStatusButtons = new List<Button>();         
            SelectedArrivalStatus = new ArrivalStatus();
            SelectedDateReservations = new ObservableCollection<Reservation>();
            UpdateSelectedDateReservations();
            LoadCommands();

            //MessageBox.Show(SelectedRestaurant.ToString());
        }

        // METHODS
        private void LoadCommands()
        {
            UpdateReservationOverviewCommand = new SimpleExecuteCommand(UpdateSelectedDateReservations);
            ChangeStatusToAwaitingCommand = new SimpleExecuteCommand(ChangeStatusToAwaiting);
            ChangeStatusToArrivedCommand = new SimpleExecuteCommand(ChangeStatusToArrived);
            ChangeStatusToSeatedCommand = new SimpleExecuteCommand(ChangeStatusToSeated);
            ChangeStatusToOrderTakenCommand = new SimpleExecuteCommand(ChangeStatusToOrderTaken);
            ChangeStatusToFinishedCommand = new SimpleExecuteCommand(ChangeStatusToFinished);
            ChangeStatusToCancelledCommand = new SimpleExecuteCommand(ChangeStatusToCancelled);
            ChangeStatusToNoShowCommand = new SimpleExecuteCommand(ChangeStatusToNoShow);
        }
        private void UpdateSelectedDateReservations()
        {
            SqlConnector conn = new SqlConnector();
            SelectedDateReservations = conn.GetReservations(SelectedDate, SelectedArea).ToObservableCollection();
        }

        //private void ChangeArrivalStatusForSelectedReservation(ArrivalStatus arrivalStatus)
        //{
        //    SqlConnector conn = new SqlConnector();

        //    SelectedReservation.ArrivalStatus = arrivalStatus;
        //    conn.UpdateReservation(SelectedReservation);
        //    UpdateSelectedDateReservations();
        //}

        private void CreateNewReservation()
        {

        }


        private void ChangeStatusToAwaiting()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Awaiting");
            SelectedReservation.ArrivalStatus.Id = 1;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToArrived()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Arrived");
            SelectedReservation.ArrivalStatus.Id = 2;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToSeated()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Seated");
            SelectedReservation.ArrivalStatus.Id = 3;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToOrderTaken()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Ordered");
            SelectedReservation.ArrivalStatus.Id = 4;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToFinished()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Finished");
            SelectedReservation.ArrivalStatus.Id = 5;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToCancelled()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("Cancelled");
            SelectedReservation.ArrivalStatus.Id = 6;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        private void ChangeStatusToNoShow()
        {
            SqlConnector conn = new SqlConnector();
            //MessageBox.Show("No show");
            SelectedReservation.ArrivalStatus.Id = 7;
            conn.UpdateReservation(SelectedReservation);
            UpdateSelectedDateReservations();
        }

        //private void AddButton(ArrivalStatus arrivalStatus)
        //{
        //    Button btn = new Button();
        //    btn.Content = arrivalStatus.Status;
        //    btn.Command = ChangeArrivalStatusCommand;
        //    btn.CommandParameter = arrivalStatus.Id;
        //    btn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(arrivalStatus.Color);
        //    btn.FontSize = 20;
        //    btn.Margin = new Thickness(5, 5, 5, 5);
        //    ArrivalStatusButtons.Add(btn);
        //}

        //public RelayCommand _ChangeArrivalStatusCommand;
        //public ICommand ChangeArrivalStatusCommand
        //{
        //    get
        //    {
        //        if (_ChangeArrivalStatusCommand == null)
        //            _ChangeArrivalStatusCommand = new RelayCommand(param => this.ChangeArrivalStatusCommand(param);
        //        return _ChangeArrivalStatusCommand;
        //    }
        //}

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
