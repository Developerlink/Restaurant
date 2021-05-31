using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Reservation : INotifyPropertyChanged, IEntity
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

        public List<DinnerTable> Tables { get; set; }

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


        public Reservation()
        {
            Guest = new Guest();
            Area = new Area();
            ArrivalStatus = new ArrivalStatus() {Id=1};
            Tables = new List<DinnerTable>();
            TimeIn = new DateTime();
        }

        // Methods
        public string IsValid()
        {
            string result = "";
            StringBuilder sb = new StringBuilder();
            if (WantedSeats == 0)
            {
                sb.Append("Number of seats has not been provided.\n");
            }
            if (ArrivalStatus.Id == 0)
            {
                sb.Append("Arrival status has not been provided.\n");
            }
            if (TimeIn == null)
            {
                sb.Append("A date and time for arrival has not been provided.\n");
            }
            if (sb != null)
            {
                result = "true";
            }
            return result;
        }


        private Guest CreateDummyWithGuestId()
        {
            Guest guest = new Guest()
            {
                Id = 1,
            };

            return guest;
        }




        // INotifyPropertyChanged implementation.
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
