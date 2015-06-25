using System.Collections.ObjectModel;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetVortex.ModernUi.ExcelViewer.Model;

namespace NetVortex.ModernUi.ExcelViewer.ViewModels
{
    class SheetViewModel : PropertyChangedBase, IHaveDisplayName
    {
        public ObservableCollection<SheetRow> SheetRows { get; private set; }
        public string DisplayName { get; set; }

        public SheetViewModel(Sheet sheet)
        {
            DisplayName = sheet.Name;
            SheetRows = new ObservableCollection<SheetRow>(sheet);
        }
    }
}
