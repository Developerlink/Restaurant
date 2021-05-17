using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    interface IEmployee
    {
        DateTime BirthDate { get; set; }
        DateTime HireDate { get; set; }
        DateTime ResignationDate { get; set; }
        string StaffType { get; set; }
        void DoJob(string job);
    }
}
