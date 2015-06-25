using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace NetVortex.ModernUi.ExcelViewer.Model
{
    class Sheet : List<SheetRow>
    {
        public string Name { get; set; }

        public Sheet(string sheetName)
        {
            Name = sheetName;
        }
    }
}