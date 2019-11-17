using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLocalDeploy
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public string SourceFolderPath { get; private set; }

        private void BtnSource_Click(object sender, EventArgs e)
        {
            if (fbdSourceFolder.ShowDialog() == DialogResult.OK)
            {
                SourceFolderPath = fbdSourceFolder.SelectedPath;
                lblSource.Text = SourceFolderPath;
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
            SetProgresBar(directories.Length + files.Length);

            LogClear();

            //Now Create all of the directories
            //foreach (string dirPath in directories)
            //{
            //    Directory.CreateDirectory(dirPath.Replace(sourceFolder, pathToDeploy));
            //    pbDeploy.Value++;
            //    Log($"Copy folder {dirPath}");
            //}

            Task.Run(() => Parallel.ForEach(directories, dirPath =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourceFolder, pathToDeploy));
                pbDeploy.Value++;
                Log($"Copy folder {dirPath}");
            }));

            //Copy all the files & Replaces any files with the same name
            //foreach (string newPath in files)
            //{
            //    if (cbSkipConfigFiles.Checked && newPath.EndsWith("web.config"))
            //    {
            //        pbDeploy.Value++;
            //        continue;
            //    }

            //    File.Copy(newPath, newPath.Replace(sourceFolder, pathToDeploy), true);
            //    pbDeploy.Value++;
            //    Log($"Copy file {newPath}");
            //}

            Task.Run(() => Parallel.ForEach(files, newPath =>
            {
                if (cbSkipConfigFiles.Checked && newPath.EndsWith("web.config"))
                {
                    pbDeploy.Value++;
                    return;
                }

                File.Copy(newPath, newPath.Replace(sourceFolder, pathToDeploy), true);
                pbDeploy.Value++;
                Log($"Copy file {newPath}");
            }));
        }

        private void SetProgresBar(int steps)
        {
            pbDeploy.Maximum = steps;
            pbDeploy.Value = 0;
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