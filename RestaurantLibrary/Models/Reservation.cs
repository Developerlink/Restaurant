using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Reservation : INotifyPropertyChanged
    {
        public int Id { get; set; }
        
        private Guest _Guest;
        public Guest Guest 
        {
            get
            {
                return _Guest;
            }
            set
            {
                _Guest = value;
                OnPropertyChanged("Guest");
            }
        }

        private Area _Area;
        public Area Area
        {
            get { return _Area; }
            set
            {
                _Area = value;
                OnPropertyChanged("Area");
            }
        }

        private int _WantedSeats;
        public int WantedSeats
        {
            get { return _WantedSeats; }
            set
            {
                _WantedSeats = value;
                OnPropertyChanged("WantedSeats");
            }
        }

        private string _ArrivalStatus;
        public string ArrivalStatus
        {
            get { return _ArrivalStatus; }
            set
            {
                _ArrivalStatus = value;
                OnPropertyChanged("ArrivalStatus");
            }
        }

        public ObservableCollection<DinnerTable> Tables { get; set; } = new ObservableCollection<DinnerTable>();

        private int _TotalSeats;
        public int TotalSeats
        {
            get { return _TotalSeats; }
            set
            {
                _TotalSeats = value;
                OnPropertyChanged("TotalSeats");
            }
        }

        private DateTime _TimeIn;
        public DateTime TimeIn
        {
            get { return _TimeIn; }
            set
            {
                _TimeIn = value;
                OnPropertyChanged("TimeIn");
            }
        }

        private DateTime _TimeOut;
        public DateTime TimeOut
        {
            get { return _TimeOut; }
            set
            {
                _TimeOut = value;
                OnPropertyChanged("TimeOut");
            }
        }

        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }        

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
