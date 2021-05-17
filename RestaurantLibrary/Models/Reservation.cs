using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Guest Guest { get; set; }
        public Area Area { get; set; }
        public int WantedSeats { get; set; }
        public string ArrivalStatus { get; set; }
        public List<DinnerTable> Tables { get; set; } = new List<DinnerTable>();

        private int _totalSeats;
        public int TotalSeats 
        { 
            get
            {
                return _totalSeats;
            }
            private set
            {
                _totalSeats = 0;
                if (Tables.Any())
                {
                    foreach (var table in Tables)
                    {
                        _totalSeats += table.Seats;
                    }
                }
            }
        }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }        
    }
}
