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
        public int AreaId { get; set; } = 2;
        public string Name { get; set; }
        public int Seats { get; set; } = 2;
        public string TableType { get; set; }

        public DinnerTable()
        {

        }

        public DinnerTable(string name)
        {
            Name = name;
        }
    }
}
