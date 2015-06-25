using System;
using System.Collections.Generic;
using System.Linq;

namespace Harlinn.Depends4Net.Types
{
    public class Named : Base
    {
        string name;



        public Named()
        { 
        }

        public Named(string name)
        {
            this.name = name;
        }


        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public override string ToString()
        {
            return Name;
        }


    }
}
