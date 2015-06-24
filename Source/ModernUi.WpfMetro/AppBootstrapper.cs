using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Caliburn.Metro.Autofac;
using Caliburn.Micro;
using NetVortex.ModernUi.WpfMetro.ViewModels;

namespace NetVortex.ModernUi.WpfMetro
{
    class AppBootstrapper : CaliburnMetroAutofacBootstrapper<AppViewModel>
    {
        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<AppWindowManager>().As<IWindowManager>().SingleInstance();

            //var assembly = typeof(AppViewModel).Assembly;
            //builder.RegisterAssemblyTypes(assembly)
            //    .Where(item => item.Name.EndsWith("ViewModel") && item.IsAbstract == false)
            //    .AsSelf()
            //    .SingleInstance();
        }
    }
}
