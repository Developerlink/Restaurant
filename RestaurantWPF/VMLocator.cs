using RestaurantWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantWPF
{
    public class VMLocator
    {
        private static CreateReservationViewModel _CreateReservationViewModel = new CreateReservationViewModel();
        
        public static CreateReservationViewModel CreateReservationViewModel
        {
            get
            {
                return _CreateReservationViewModel;
            }
        }
    }
}
