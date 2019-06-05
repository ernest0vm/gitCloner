using System;
using System.Windows.Forms;

namespace gitCloner
{
    public partial class InputBox : Form
    {
        public string data = string.Empty;
        public DialogResult result = DialogResult.None;
        public InputBox(string title)
        {
            InitializeComponent();
            Text = title;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            data = txtURL.Text;
            result = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            data = string.Empty;
            result = DialogResult.Cancel;
            Close();
        }
    }
}
