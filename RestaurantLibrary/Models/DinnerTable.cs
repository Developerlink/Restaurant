using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class DinnerTable
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public string TableType { get; set; }

        public DinnerTable()
        {

        }

        public DinnerTable(string name, int seats = 2)
        {
            Name = name;
            Seats = seats;
        }
    }
}
