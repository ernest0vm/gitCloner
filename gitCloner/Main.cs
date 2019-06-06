using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gitCloner
{
    public partial class MainForm : Form
    {

        private string filePath = string.Empty;
        private string savePathFolder = string.Empty;

        public MainForm()
        {
            InitializeComponent();
            EnableControls(false);
            lblStatus.Text = "Without source list";
            lblItems.Text = string.Empty;
            Text = "gitCloner v" + Application.ProductVersion;
        }

        private void EnableControls(bool hasListLoaded)
        {
            btnAdd.Enabled = hasListLoaded;
            btnDelete.Enabled = hasListLoaded;
            btnSave.Enabled = hasListLoaded;
            btnClone.Enabled = hasListLoaded;
            chkCompress.Enabled = hasListLoaded;
            chkDelete.Enabled = hasListLoaded;
            txtPath.Enabled = hasListLoaded;
            SourceList.Enabled = hasListLoaded;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                SourceList.Items.Clear();
                openFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                openFileDialog.Filter = "Source List Files (*.list)|*.list";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    txtPath.Text = filePath;
                    lblStatus.Text = "Source list loaded";

                    EnableControls(true);

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            SourceList.Items.Add(line);
                        }
                    }
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            SourceList.Items.Clear();
            EnableControls(true);

            lblStatus.Text = "New source list was created";
            txtPath.Text = string.Empty;
            filePath = string.Empty;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            InputBox inputBox = new InputBox("Add source");
            inputBox.ShowDialog();

            if (inputBox.result == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(inputBox.data))
                    SourceList.Items.Add(inputBox.data);
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceList.Items.Count > 0)
                {
                    SourceList.Items.RemoveAt(SourceList.SelectedIndex);
                }
            }
            catch
            {
                MessageBox.Show("please select an item on the list", "Item no selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(filePath))
            {
                SaveFileDialog saveFile = new SaveFileDialog
                {
                    Filter = "Source List Files (*.list)|*.list",
                    FilterIndex = 1,
                    AddExtension = true
                };

                if (saveFile.ShowDialog() == DialogResult.OK)
                {

                    filePath = saveFile.FileName;
                    txtPath.Text = filePath;

                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (object item in SourceList.Items)
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }

                    lblStatus.Text = "The source list file was saved";
                    MessageBox.Show("The source list file was saved", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    foreach (object item in SourceList.Items)
                    {
                        sw.WriteLine(item.ToString());
                    }
                }

                lblStatus.Text = "The changes has been saved";
                MessageBox.Show("The changes has been saved", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BtnClone_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;

                if (string.IsNullOrEmpty(savePathFolder))
                {
                    MessageBox.Show("please select a path for save the repositories", "Without path for save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string[] tempString = SourceList.SelectedItem.ToString().Split(Convert.ToChar("/"));
                string repositoryName = tempString.LastOrDefault().Replace(".git", "");
                string clonePath = savePathFolder + repositoryName;
                string command = "/C git clone " + SourceList.SelectedItem.ToString() + " " + clonePath;

                if (Directory.Exists(clonePath))
                {
                    MessageBox.Show("Already cloned.", "Cannot clone repository", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Directory.Delete(clonePath,true);
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
#if DEBUG
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
#else
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
#endif
                startInfo.FileName = "cmd.exe";

                startInfo.Arguments = command;
                process.StartInfo = startInfo;
                process.Start();

                lblStatus.Text = repositoryName + " has been cloned.";
                progressBar1.Value = 100;
            }
            catch
            {
                MessageBox.Show("please select an item on the list", "Item no selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            AboutBox showAbout = new AboutBox();
            showAbout.ShowDialog();
        }

#pragma warning disable IDE0051
        private bool CompressFiles()
        {
            return true;
        }

        private bool DeleteTempFolder()
        {
            return true;
        }

        private void BtnSavePath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    savePathFolder = folderBrowser.SelectedPath + "\\";
                    txtSavePath.Text = savePathFolder;
                }
            }
        }

        private void BtnMultiCLone_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Value = 0;
                SourceList.Enabled = false;
                if (string.IsNullOrEmpty(savePathFolder))
                {
                    MessageBox.Show("please select a path for save the repositories", "Without path for save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                int item = 1;
                for (int a = 0; a <= SourceList.Items.Count - 1; a++)
                {
                    SourceList.SelectedIndex = a;
                    string[] tempString = SourceList.SelectedItem.ToString().Split(Convert.ToChar("/"));
                    string repositoryName = tempString.LastOrDefault().Replace(".git", "");
                    string clonePath = savePathFolder + repositoryName;
                    string command = "/C git clone " + SourceList.SelectedItem.ToString() + " " + clonePath;

                    if (Directory.Exists(clonePath))
                    {
                        //MessageBox.Show(repositoryName + " already cloned.", "Cannot clone repository", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Directory.Delete(clonePath,true);
                    }

                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
#if DEBUG
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
#else
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
#endif
                    startInfo.FileName = "cmd.exe";

                    startInfo.Arguments = command;
                    process.StartInfo = startInfo;
                    process.Start();

                    lblStatus.Text = repositoryName + " has been cloned.";
                                       
                    string itemsProgress = $"Cloned: {item++} / Remain: {SourceList.Items.Count - item} / Total: {SourceList.Items.Count}";
                    lblItems.Text = itemsProgress;

                    progressBar1.Value = (100 / SourceList.Items.Count) * item;


                }

                SourceList.Enabled = true;
            }
            catch
            {
                MessageBox.Show("please select an item on the list", "Item no selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
