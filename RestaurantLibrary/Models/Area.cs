using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class Area
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DinnerTable> DinnerTables { get; set; } = new List<DinnerTable>();
    }
}
