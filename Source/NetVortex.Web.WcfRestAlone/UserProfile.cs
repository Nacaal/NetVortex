using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Web.WcfRestAlone
{
    [DataContract(Namespace = "http://NetVortex/REST/Data", Name = "user_profile")]
    class UserProfile
    {
        [DataMember(Name = "profile_id")]
        public int Id { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "birth_date")]
        public DateTime BirthDate { get; set; }
    }
}
