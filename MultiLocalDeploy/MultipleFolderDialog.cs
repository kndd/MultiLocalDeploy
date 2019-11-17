using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MultiLocalDeploy
{
    public partial class MultipleFolderDialog : Form
    {
        public MultipleFolderDialog()
        {
            InitializeComponent();
        }

        public string SelectedNodePath { get; set; }
        public List<string> FolderList { get; set; } = new List<string>();

        private void MultipleFolderDialog_Load(object sender, EventArgs e)
        {
            var drives = Environment.GetLogicalDrives();
            foreach (var drive in drives) LoadDirectory(drive);

            tvFolders.AfterExpand += TreeView1OnAfterExpand;
            tvFolders.NodeMouseClick += TreeView1OnNodeMouseClick;
        }

        private void TreeView1OnNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Tag != null) SelectedNodePath = e.Node.Tag.ToString();
        }

        private void TreeView1OnAfterExpand(object sender, TreeViewEventArgs e)
        {
            e.Node.Nodes.RemoveAt(0);

            if (e.Node.Nodes.Count != 0) return;

            LoadSubDirectories(e.Node.Tag.ToString(), e.Node);
        }

        public void LoadDirectory(string dir)
        {
            var di = new DirectoryInfo(dir);
            var tds = tvFolders.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.StateImageIndex = 0;
            tds.Nodes.Add(new TreeNode());
            //LoadFiles(dir, tds);
            //LoadSubDirectories(dir, tds);
        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            try
            {
                // Get all subdirectories  
                var subdirectoryEntries = Directory.GetDirectories(dir);
                // Loop through them to see if they have any other subdirectories  
                foreach (var subdirectory in subdirectoryEntries)
                {
                    var di = new DirectoryInfo(subdirectory);
                    var tds = td.Nodes.Add(di.Name);
                    tds.StateImageIndex = 0;
                    tds.Tag = di.FullName;
                    tds.Nodes.Add(new TreeNode());
                    //LoadFiles(subdirectory, tds);
                    //LoadSubDirectories(subdirectory, tds);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            var Files = Directory.GetFiles(dir, "*.*");

            // Loop through them to see files  
            foreach (var file in Files)
            {
                var fi = new FileInfo(file);
                var tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedNodePath != null)
            {
                lvFolders.Items.Add(SelectedNodePath);
                FolderList.Add(SelectedNodePath);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}