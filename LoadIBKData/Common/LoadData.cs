using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadIBKData.Common
{
    public static class LoadTestData
    {
        public static string LoadData(string fileName)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            System.IO.DirectoryInfo directoryInfo = System.IO.Directory.GetParent(path).Parent.Parent;
            string file = directoryInfo.FullName + "\\APIData\\" + fileName;
            return file;
        }
    }
}
