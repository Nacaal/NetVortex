using System;
using System.Collections.Generic;
using System.IO;

namespace Harlinn.Depends4Net.Utils.Paths
{
    public class SearchPath : List<DirectoryInfo>
    {
        public SearchPath()
        { }


        public void Initialize()
        {
            string searchPath = Environment.GetEnvironmentVariable("PATH");
            string[] pathElements = searchPath.Split(';');
            Add(pathElements);
        }


        public void Add(string pathElement)
        {
            if (Directory.Exists(pathElement))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(pathElement);
                base.Add(directoryInfo);
            }
        }

        public void Add(string[] pathElements)
        {
            foreach (string pathElement in pathElements)
            {
                Add(pathElement);
            }
        }


        public bool Contains(string pathElement)
        {
            DirectoryInfo pathElementDirectoryInfo = new DirectoryInfo(pathElement);
            string fullName = pathElementDirectoryInfo.FullName;
            foreach (DirectoryInfo directoryInfo in this)
            {
                if (string.Compare( pathElementDirectoryInfo.FullName,fullName,true) == 0)
                {
                    return true;
                }
            }
            return false;
        }


        public string Search(string filename)
        {
            string result = null;
            foreach(DirectoryInfo directoryInfo in this)
            {
                string foundFile;
                if(Find(filename,directoryInfo,out foundFile))
                {
                    result = foundFile;
                    break;
                }
            }
            return result;
        }


        private static bool Find(string filename,DirectoryInfo directoryInfo, out string foundFile)
        {
            bool result = false;
            string s = System.IO.Path.Combine(directoryInfo.FullName, filename);
            if (System.IO.File.Exists(s))
            {
                foundFile = s;
                result = true;
            }
            else
            {
                foundFile = null;
            }
            return result;
        }



    }
}
