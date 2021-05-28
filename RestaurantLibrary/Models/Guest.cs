using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Guest : Person
    {
        public int TotalVisits { get; set; }
        public List<DateTime> VisitHistory { get; set; } = new List<DateTime>();

        public Guest()
        {

        }

        public Guest(string firstName, string lastName, int phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
        }

        public override string GetInfo()
        {
            return $"Tlf: {PhoneNumber}";
        }
    }
}
