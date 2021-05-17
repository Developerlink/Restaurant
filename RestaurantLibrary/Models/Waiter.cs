using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Waiter : Person, IEmployee
    {
        public string StaffType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime BirthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime HireDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime ResignationDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DoJob(string job)
        {
            Console.WriteLine($"Waiter {FirstName} {LastName} starts doing {job}");
        }

        public void DoJob(params string[] jobs)
        {
            Console.WriteLine($"Waiter {FirstName} {LastName} starts doing: ");
            foreach (string job in jobs)
            {
                Console.WriteLine(job);
            }
        }

        public override string GetInfo()
        {
            return $"Tlf: {PhoneNumber} Address: {Address}";
        }

        public override string ToString()
        {
            return $"Waiter: {FirstName} {LastName} ";
        }
    }
}
