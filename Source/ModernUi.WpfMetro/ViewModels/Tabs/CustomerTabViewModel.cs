using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NetVortex.ModernUi.WpfMetro.ViewModels.Tabs
{
    class CustomerTabViewModel : Screen
    {
        public CustomerTabViewModel()
        {
            base.DisplayName = "Customer";
        }

        private XmlElement _selectedItem;
        public XmlElement SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    NotifyOfPropertyChange();
                }
            }
        }
    }
}
