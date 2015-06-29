using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Web.WcfRestAlone
{
    class Program
    {
        public static DateTime? HostStartTime = null;
        public static int HostRequestCount = 0;
        public static List<UserProfile> ProfileList { get; private set; }

        static void Main(string[] args)
        {
            ProfileList = new List<UserProfile>();

            ProfileList.Add(new UserProfile()
            {
                Id = 1,
                FirstName = "Net",
                LastName = "Vortex",
                BirthDate = new DateTime(1955, 9, 11)
            });

            Uri baseAddress = new Uri("http://localhost:8080");

            ServiceHost host = new ServiceHost(typeof(Service), baseAddress);
            host.AddServiceEndpoint(typeof(IService), new WebHttpBinding(), "REST")
                .Behaviors.Add(new WebHttpBehavior() { HelpEnabled = true });

            host.Open();
            HostStartTime = DateTime.Now;

            Console.WriteLine("Enter to terminate...");
            Console.ReadLine();

            host.Close();
        }
    }
}
