using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace gitCloner
{
    public partial class MainForm : Form
    {
        private string itemsProgress = string.Empty;
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
            btnMultiClone.Enabled = hasListLoaded;
            chkCompress.Enabled = hasListLoaded;
            chkDelete.Enabled = hasListLoaded;
            chkMirror.Enabled = hasListLoaded;
            txtPath.Enabled = hasListLoaded;
            txtSavePath.Enabled = hasListLoaded;
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

                    lblItems.Text = $"Total: {SourceList.Items.Count}";

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

            BtnAdd_Click(null, null);
            lblItems.Text = $"Total: {SourceList.Items.Count}";
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

            lblItems.Text = $"Total: {SourceList.Items.Count}";
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceList.Items.Count > 0)
                {
                    SourceList.Items.RemoveAt(SourceList.SelectedIndex);
                }

                lblItems.Text = $"Total: {SourceList.Items.Count}";
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
                string clonePath = savePathFolder + repositoryName + (chkMirror.Checked ? $"\\{repositoryName}.git" : "");
                string command = "/C git clone " + (chkMirror.Checked ? "--mirror " : "") + SourceList.SelectedItem.ToString() + " " + clonePath;

                if (Directory.Exists(clonePath))
                {
                    MessageBox.Show("Already cloned.", "Cannot clone repository", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Directory.Delete(clonePath,true);
                }

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();
#if DEBUG
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;
#else
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
#endif
                startInfo.FileName = "cmd.exe";

                startInfo.Arguments = command;
                process.StartInfo = startInfo;
                process.Start();

                while (!process.HasExited)
                {
                    //wait for cloning.
                }

                if (chkMirror.Checked)
                {
                    Process Moveproc = new Process();
                    startInfo.Arguments = $"/C rename {clonePath} .git";
                    Moveproc.StartInfo = startInfo;    
                    Moveproc.Start();
                    while (!Moveproc.HasExited)
                    {
                        //wait for rename directory.
                    }

                    Process Configproc = new Process();
                    startInfo.WorkingDirectory = savePathFolder + repositoryName;
                    startInfo.Arguments = "/C git config --local --bool core.bare false";
                    Configproc.StartInfo = startInfo;
                    Configproc.Start();
                    while (!Configproc.HasExited)
                    {
                        //wait for repository config
                    }

                    Process Checkoutproc = new Process();
                    startInfo.WorkingDirectory = savePathFolder + repositoryName;
                    startInfo.Arguments = "/C git checkout master";
                    Checkoutproc.StartInfo = startInfo;
                    Checkoutproc.Start();
                    while (!Checkoutproc.HasExited)
                    {
                        //wait for repository config
                    }
                }

                lblStatus.Text = repositoryName + " has been cloned.";
                progressBar1.Value = 100;

                if (chkCompress.Checked)
                {
                    if (chkDelete.Checked)
                    {
                        CompressFiles(true);
                    }
                    else
                    {
                        CompressFiles();
                    }

                    lblStatus.Text = repositoryName + " has been compressed.";
                }

                Text = "gitCloner v" + Application.ProductVersion;
                SourceList.Enabled = true;
                progressBar1.Value = 0;
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

        private void CompressFiles(bool delete = false)
        {

            string compressCommand = $"/C 7z.exe a {savePathFolder}gitBackup[{DateTime.UtcNow.ToString("dd-MM-yyyy")}].zip {savePathFolder} {(delete ? "-sdel" : "")}";

            Text = $"gitCloner v{Application.ProductVersion} [Compressing files]";

            Process process = new Process();
            ProcessStartInfo startCompressInfo = new ProcessStartInfo
            {
#if DEBUG
                WindowStyle = ProcessWindowStyle.Minimized,
#else
                WindowStyle = ProcessWindowStyle.Hidden,
#endif
                FileName = "cmd.exe",

                Arguments = compressCommand
            };

            process.StartInfo = startCompressInfo;

            process.Start();

            while (!process.HasExited)
            {
                //wait infinite time for the end of process.
            }

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

        private void BtnMultiClone_Click(object sender, EventArgs e)
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
                    string clonePath = savePathFolder + repositoryName + (chkMirror.Checked ? $"\\{repositoryName}.git" : "");
                    string command = "/C git clone " + (chkMirror.Checked ? "--mirror " : "") + SourceList.SelectedItem.ToString() + " " + clonePath;

                    Text = $"gitCloner v{Application.ProductVersion} [Cloning {repositoryName}]";

                    if (Directory.Exists(clonePath))
                    {
                        lblStatus.Text = repositoryName + " already cloned.";
                        //Directory.Delete(clonePath,true);
                    }

                    Process process = new Process();
                    ProcessStartInfo startInfo = new ProcessStartInfo();
#if DEBUG
                    startInfo.WindowStyle = ProcessWindowStyle.Minimized;
#else
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
#endif
                    startInfo.FileName = "cmd.exe";

                    startInfo.Arguments = command;
                    process.StartInfo = startInfo;

                    process.Start();

                    while (!process.HasExited)
                    {
                        //wait infinite time for the end of process.
                    }

                    if (chkMirror.Checked)
                    {
                        Process Moveproc = new Process();
                        startInfo.Arguments = $"/C rename {clonePath} .git";
                        Moveproc.StartInfo = startInfo;
                        Moveproc.Start();
                        while (!Moveproc.HasExited)
                        {
                            //wait for rename directory.
                        }

                        Process Configproc = new Process();
                        startInfo.WorkingDirectory = savePathFolder + repositoryName;
                        startInfo.Arguments = "/C git config --local --bool core.bare false";
                        Configproc.StartInfo = startInfo;
                        Configproc.Start();
                        while (!Configproc.HasExited)
                        {
                            //wait for repository config
                        }

                        Process Checkoutproc = new Process();
                        startInfo.WorkingDirectory = savePathFolder + repositoryName;
                        startInfo.Arguments = "/C git checkout master";
                        Checkoutproc.StartInfo = startInfo;
                        Checkoutproc.Start();
                        while (!Checkoutproc.HasExited)
                        {
                            //wait for repository config
                        }
                    }

                    lblStatus.Text = repositoryName + " has been cloned.";

                    itemsProgress = $"Cloned: {item++} / Remain: {(SourceList.Items.Count + 1) - item} / Total: {SourceList.Items.Count}";
                    lblItems.Text = itemsProgress;

                    int dinamicValue = (100 / SourceList.Items.Count) * item;
                    if (dinamicValue > 100) { dinamicValue = 100; } else { dinamicValue = (100 / SourceList.Items.Count) * item; }
                    progressBar1.Value = dinamicValue;

                }

                
                lblStatus.Text = "All repositories has been cloned.";
                itemsProgress = $"Cloned: {item - 1} / Total: {SourceList.Items.Count}";
                lblItems.Text = itemsProgress;

                if (chkCompress.Checked)
                {
                    if (chkDelete.Checked)
                    {
                        CompressFiles(true);
                    }
                    else
                    {
                        CompressFiles();
                    }

                    lblStatus.Text = "All repositories has been compressed.";
                }

                Text = "gitCloner v" + Application.ProductVersion;
                SourceList.Enabled = true;
                progressBar1.Value = 0;
            }
            catch
            {
                MessageBox.Show("please select an item on the list", "Item no selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
