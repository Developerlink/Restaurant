using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Area> Areas { get; set; } = new List<Area>();
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Waiter> Employees { get; set; } = new List<Waiter>();
        //public List<OpeningTime> NormalOpeningTimes { get; set; } = new List<OpeningTime>();
        //public List<OpeningTime> SpecialOpeningTimes { get; set; } = new List<OpeningTime>();
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }

        public override string ToString()
        {
            return $"Restaurant name: {Name} - Areas: {AreasToString()}- Phone number: {PhoneNumber}";
        }

        private string AreasToString()
        {
            string result = "";
            foreach (var a in Areas)
            {
                result += a.Name + ", ";
            }

            return result;
        }
    }
}
