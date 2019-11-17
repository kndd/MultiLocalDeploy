using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiLocalDeploy.Extensions;
using MultiLocalDeploy.Properties;

namespace MultiLocalDeploy
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public string SourceFolderPath { get; private set; }
        public List<string> TargetFolderList { get; set; }

        private void Main_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SourceFolder))
            {
                SourceFolderPath = Properties.Settings.Default.SourceFolder;

                tvSourceFolder.LoadAllStructure(SourceFolderPath);
                tvSourceFolder.ExpandAll();
            }

            if (Properties.Settings.Default.TargetFolderList != null)
            {
                TargetFolderList = Properties.Settings.Default.TargetFolderList.Cast<string>().ToList();
                lbFolders.Items.AddRange(TargetFolderList.ToArray());
            }
        }
        
        private void BtnSource_Click(object sender, EventArgs e)
        {
            if (fbdSourceFolder.ShowDialog() == DialogResult.OK)
            {
                SourceFolderPath = fbdSourceFolder.SelectedPath;
            }
        }

        private void BtnTarget_Click(object sender, EventArgs e)
        {
            var mfd = new MultipleFolderDialog();
            if (mfd.ShowDialog(this) == DialogResult.OK) lbFolders.Items.AddRange(mfd.FolderList.ToArray());
        }

        private void BtnDeploy_Click(object sender, EventArgs e)
        {
            if (!ValidateDeploy())
                return;

            var result = MessageBox.Show($"Do you want to deploy {lbFolders.SelectedItem}",
                "Warning!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            var pathToDeploy = lbFolders.SelectedItem.ToString();
            LogClear();
            DeployFiles(SourceFolderPath, pathToDeploy);
            Log("******************");
            Log("DONE!");
            Log("******************");
        }

        private void DeployFiles(string sourceFolder, string pathToDeploy)
        {
            var directories = Directory.GetDirectories(sourceFolder, "*",
                SearchOption.AllDirectories);
            var files = Directory.GetFiles(sourceFolder, "*.*",
                SearchOption.AllDirectories);
            
            Parallel.ForEach(directories, dirPath =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourceFolder, pathToDeploy));
            });
            
            Parallel.ForEach(files, newPath =>
            {
                if (cbSkipConfigFiles.Checked && newPath.EndsWith("web.config"))
                {
                    return;
                }

                File.Copy(newPath, newPath.Replace(sourceFolder, pathToDeploy), true);
            });
        }
        
        private bool ValidateDeploy()
        {
            return !string.IsNullOrEmpty(SourceFolderPath) && lbFolders.SelectedItem != null;
        }

        private void Log(string message)
        {
            tbLog.Text += message + Environment.NewLine;
            tbLog.Refresh();
        }

        private void LogClear()
        {
            tbLog.Clear();
        }

        private void lbFolders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete) lbFolders.Items.RemoveAt(lbFolders.SelectedIndex);
        }

        private void lbFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFolders.SelectedIndex >= 0) LogClear();
        }
    }
}