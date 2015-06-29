using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace NetVortex.Web.WcfRestAlone
{
    class Service : IService
    {
        public ServerStatus GetStatus()
        {
            Program.HostRequestCount++;
            TimeSpan runtime = DateTime.Now - Program.HostStartTime.GetValueOrDefault(DateTime.Now);

            return new ServerStatus()
            {
                StartDate = Program.HostStartTime.GetValueOrDefault(DateTime.Now),
                Uptime = string.Format("{0} hours, {1} minutes and {2} seconds", runtime.Hours, runtime.Minutes, runtime.Seconds),
                RequestCount = Program.HostRequestCount
            };
        }

        public UserProfile GetProfile(string id)
        {
            int tmpId = 0;
            if (!int.TryParse(id, out tmpId))
                throw new WebFaultException<string>(string.Format("Supplied id {0} is not an integer", id), HttpStatusCode.BadRequest);

            UserProfile profile = Program.ProfileList.FirstOrDefault(x => x.Id == tmpId);

            if (profile == null)
                throw new WebFaultException<string>(string.Format("Profile with id {0} does not exist.", id), HttpStatusCode.NotFound);

            return profile;
        }

        public int CreateProfile(UserProfile profile)
        {
            if (profile == null)
                throw new WebFaultException<string>("No valid user profile supplied!", HttpStatusCode.BadRequest);

            int maxid = Program.ProfileList.Max<UserProfile>(x => x.Id);

            profile.Id = ++maxid;

            Program.ProfileList.Add(profile);

            return profile.Id;
        }

        public void CopyFile(string fileName, Stream fileData)
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

            using (FileStream fstream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                int bytesRead = 0, offset = 0;
                byte[] buffer = new byte[1000];

                do
                {
                    bytesRead = fileData.Read(buffer, offset, buffer.Length);
                    offset += bytesRead;

                } while (bytesRead > 0);
            }
        }
    }
}
