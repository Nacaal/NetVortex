using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Web.WcfRestAlone
{
    [ServiceContract]
    interface IService
    {
        [OperationContract]
        [Description("Gets the status the service")]
        [WebGet(UriTemplate = "status")]
        ServerStatus GetStatus();

        [OperationContract]
        [Description("Gets the user profile using the supplied id")]
        [WebGet(UriTemplate = "profile/{id}")]
        UserProfile GetProfile(string id);

        [OperationContract]
        [Description("Creates a new user profile and returns the associated id")]
        [WebInvoke(Method = "POST", UriTemplate = "profile")]
        int CreateProfile(UserProfile profile);

        [OperationContract]
        [Description("Copy file to another dimension")]
        [WebInvoke(Method = "PUT", UriTemplate = "copy/{filename}")]
        void CopyFile(string fileName, Stream fileData);
    }
}
