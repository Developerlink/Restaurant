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
        private static ReservationEditorViewModel _CreateReservationViewModel = new ReservationEditorViewModel();
        
        public static ReservationEditorViewModel CreateReservationViewModel
        {
            get
            {
                return _CreateReservationViewModel;
            }
        }
    }
}
