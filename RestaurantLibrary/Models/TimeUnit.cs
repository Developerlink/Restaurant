using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Models
{
    public class TimeUnit : INotifyPropertyChanged
    {
        private int _Hour;
        public int Hour
        {
            get { return _Hour; }
            set
            {
                if (value < 0 || 23 < value)
                {
                    _Hour = value;
                }
                else
                {
                    _Hour = 0;
                }
                OnPropertyChanged("Hour");
            }
        }

        private int _Minute;
        public int Minute
        {
            get { return _Minute; }
            set
            {
                if (value < 0 || 59 < value)
                {
                    _Minute = value;
                }
                else
                {
                    _Minute = 0;
                }
                OnPropertyChanged("Minute");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
