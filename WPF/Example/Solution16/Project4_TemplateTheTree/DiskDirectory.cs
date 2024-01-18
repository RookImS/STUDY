using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Documents;

namespace Project4_TemplateTheTree
{
    public class DiskDirectory
    {
        DirectoryInfo dirInfo;

        public DiskDirectory(DirectoryInfo dirInfo)
        {
            this.dirInfo = dirInfo;
        }

        public string Name
        {
            get { return dirInfo.Name; }
        }

        public List<DiskDirectory> Subdirectories
        {
            get
            {
                List<DiskDirectory> dirs = new List<DiskDirectory>();
                DirectoryInfo[] subdirs;

                try
                {
                    subdirs = dirInfo.GetDirectories();
                }
                catch
                {
                    return dirs;
                }

                foreach (DirectoryInfo subdir in subdirs)
                    dirs.Add(new DiskDirectory(subdir));

                return dirs;                
            }
        }
    }
}
