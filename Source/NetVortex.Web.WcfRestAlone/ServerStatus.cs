using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Web.WcfRestAlone
{
    [DataContract(Namespace = "http://NetVortex/RESTAlone", Name = "server_status")]
    class ServerStatus
    {
        [DataMember(Name = "host_startdate")]
        public DateTime StartDate { get; set; }

        [DataMember(Name = "host_uptime")]
        public string Uptime { get; set; }

        [DataMember(Name = "host_requests")]
        public long RequestCount { get; set; }
    }
}
