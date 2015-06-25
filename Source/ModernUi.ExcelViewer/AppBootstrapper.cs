using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using NetVortex.ModernUi.ExcelViewer.ViewModels;

namespace NetVortex.ModernUi.ExcelViewer
{
    class AppBootstrapper : CaliburnMetroAutofacBootstrapper<AppViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();
        }
    }
}
