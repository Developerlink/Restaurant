using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class OpeningTime
    {
        public int WeekDay { get; set; }
        public string WeekDayName { get; set; }
        public int OpeningHour { get; set; }
        public int OpeningMinute { get; set; }
        public int ClosingHour { get; set; }
        public int ClosingMinute { get; set; }
        public DateTime SpecialDate { get; set; }
    }
}
