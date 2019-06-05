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
        private string fileContent = string.Empty;
        private string filePath = string.Empty;
        private readonly DataSet DS = new DataSet("SourceList");
        private readonly DataTable DT = new DataTable("SourceItem");

        public MainForm()
        {
            InitializeComponent();
            EnableControls(false);
            lblStatus.Text = "No source loaded";
            SourceListView.MultiSelect = false;
            SourceListView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            SourceListView.Enabled = hasListLoaded;
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
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

                    DS.ReadXml(filePath);

                    SourceListView.Columns.RemoveAt(0);                    
                    SourceListView.DataSource = DS.Tables[0];
                }
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            
            EnableControls(true);

            do
            {
                foreach (DataGridViewRow row in SourceListView.Rows)
                {
                    try
                    {
                        SourceListView.Rows.Remove(row);
                    }
                    catch (Exception) { }
                }
            } while (SourceListView.Rows.Count > 1);

                    SourceListView.Focus();
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
                string resource = inputBox.data;
                SourceListView.Rows.Add(resource);
            }
            
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceListView.SelectedRows.Count > 0)
                {
                    SourceListView.Rows.RemoveAt(SourceListView.SelectedRows[0].Index);
                }
            }
            catch { }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                DS.Tables.RemoveAt(0);
            }
            
            foreach (DataGridViewColumn col in SourceListView.Columns)
            {
                DT.Columns.Add(col.Name);
            }

            foreach (DataGridViewRow row in SourceListView.Rows)
            {
                DataRow dRow = DT.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    dRow[cell.ColumnIndex] = cell.Value;
                }
                DT.Rows.Add(dRow);
            }

            DS.Tables.Add(DT);

            if (string.IsNullOrEmpty(filePath))
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.Filter = "Source List Files (*.list)|*.list";
                saveFile.FilterIndex = 1;
                saveFile.AddExtension = true;

                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    DS.AcceptChanges();
                    filePath = saveFile.FileName;
                    txtPath.Text = filePath;
                    DS.WriteXml(filePath);

                    lblStatus.Text = "The source list file was saved";
                    MessageBox.Show("The source list file was saved", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else
            {
                DS.AcceptChanges();
                DS.WriteXml(filePath);

                lblStatus.Text = "The changes has been saved";
                MessageBox.Show("The changes has been saved", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void BtnClone_Click(object sender, EventArgs e)
        {
            
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C git clone " + SourceListView.Rows[0].Cells[0].Value.ToString();
            process.StartInfo = startInfo;
            process.Start();
        }

        private void BtnAbout_Click(object sender, EventArgs e)
        {
            AboutBox showAbout = new AboutBox();
            showAbout.ShowDialog();
        }

        private bool CompressFiles()
        {
            return true;
        }

        private bool DeleteTempFolder()
        {
            return true;
        }
    }
}
