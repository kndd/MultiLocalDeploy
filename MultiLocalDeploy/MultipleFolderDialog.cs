using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MultiLocalDeploy.Extensions;

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
            foreach (var drive in drives)
            {
                tvFolders.LoadDirectory(drive, true);
            }

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

            e.Node.LoadSubDirectories(true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (SelectedNodePath == null) return;

            lvFolders.Items.Add(SelectedNodePath);
            FolderList.Add(SelectedNodePath);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}