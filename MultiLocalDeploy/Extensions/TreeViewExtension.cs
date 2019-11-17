using System;
using System.IO;
using System.Windows.Forms;

namespace MultiLocalDeploy.Extensions
{
    public static class TreeViewExtension
    {
        public static void LoadDirectory(this TreeView tv, string dir, bool isExpandable)
        {
            var di = new DirectoryInfo(dir);
            var tds = tv.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            if (isExpandable)
                tds.Nodes.Add(new TreeNode());
        }

        public static void LoadSubDirectories(this TreeNode td, bool isExpandable)
        {
            try
            {
                // Get all subdirectories  
                var subdirectoryEntries = Directory.GetDirectories(td.Tag.ToString());
                // Loop through them to see if they have any other subdirectories  
                foreach (var subdirectory in subdirectoryEntries)
                {
                    var di = new DirectoryInfo(subdirectory);
                    var tds = td.Nodes.Add(di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    if (isExpandable)
                        tds.Nodes.Add(new TreeNode());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void LoadFiles(this TreeNode td)
        {
            var Files = Directory.GetFiles(td.Tag.ToString(), "*.*");

            // Loop through them to see files  
            foreach (var file in Files)
            {
                var fi = new FileInfo(file);
                var tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }

        public static void LoadAllStructure(this TreeView tv, string dir)
        {
            var di = new DirectoryInfo(dir);
            var tds = tv.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            tds.LoadSubDirectories(false);
            tds.LoadFiles();
        }
    }
}