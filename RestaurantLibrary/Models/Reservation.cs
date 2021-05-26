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

        private int _AreaId;
        public int AreaId
        {
            get { return _AreaId; }
            set
            {
                _AreaId = value;
                OnPropertyChanged("AreaId");
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

        private ArrivalStatus _ArrivalStatus;
        public ArrivalStatus ArrivalStatus
        {
            get { return _ArrivalStatus; }
            set
            {
                _ArrivalStatus = value;
                OnPropertyChanged("ArrivalStatus");
            }
        }

        public List<DinnerTable> Tables { get; set; } = new List<DinnerTable>();

        private int TotalSeats
        {
            get 
            {                
                return Tables.Sum(table => table.Seats);
            }
        }

        public int SeatBalance 
        {
            get
            {
                return TotalSeats - WantedSeats;
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

        private DateTime? _TimeOut;
        public DateTime? TimeOut
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
