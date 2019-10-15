using MultiLocalDeploy.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLocalDeploy
{
    public partial class Form1 : Form
    {
        public Form1()
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
            fbdTargetFolders.RootFolder = Environment.SpecialFolder.MyComputer;
            if (fbdTargetFolders.ShowDialog() == DialogResult.OK)
            {
                lbFolders.Items.AddRange(Directory.GetDirectories(fbdTargetFolders.SelectedPath));
            }
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

            Parallel.ForEach(directories, (dirPath) =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourceFolder, pathToDeploy));
                //pbDeploy.Value++;
                //Log($"Copy folder {dirPath}");
            });

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

            Parallel.ForEach(files, (newPath) =>
            {
                if (cbSkipConfigFiles.Checked && newPath.EndsWith("web.config"))
                {
                    pbDeploy.Value++;
                    return;
                }

                File.Copy(newPath, newPath.Replace(sourceFolder, pathToDeploy), true);
                //pbDeploy.Value++;
                //Log($"Copy file {newPath}");
            });
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
            if (e.KeyCode == Keys.Delete)
            {
                lbFolders.Items.RemoveAt(lbFolders.SelectedIndex);
            }
        }
    }
}
