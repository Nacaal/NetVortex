using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Harlinn.Depends4Net.Types
{
    public class Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs ea = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, ea);
            }
        }

    }
}
