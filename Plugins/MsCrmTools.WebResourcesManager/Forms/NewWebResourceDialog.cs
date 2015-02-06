// PROJECT : MsCrmTools.WebResourcesManager
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class NewWebResourceDialog : Form
    {
        readonly Regex inValidWrNameRegex = new Regex("[^a-z0-9A-Z_\\./]|[/]{2,}", (RegexOptions.Compiled | RegexOptions.CultureInvariant));
        
        private string _webResourceName;

        private readonly string _extension;

        public NewWebResourceDialog(string extension)
        {
            InitializeComponent();

            _extension = extension;
        }

        private void TxtWebResourceNameTextChanged(object sender, EventArgs e)
        {
            label2.Text = string.Format("Final file name: {0}.{1}", txtWebResourceName.Text, _extension);
        }

        public string WebResourceName { get { return _webResourceName; } }

        private void BtnValidateClick(object sender, EventArgs e)
        {
            if (txtWebResourceName.Text.Length > 0 
                && !inValidWrNameRegex.IsMatch(txtWebResourceName.Text)
                && txtWebResourceName.Text.Split('/').All(x=>x.Length != 0))
            {
                _webResourceName = txtWebResourceName.Text;

                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Please provide a valid web resource name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void TxtWebResourceNameKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnValidateClick(null, null);
            }
        }
    }
}
