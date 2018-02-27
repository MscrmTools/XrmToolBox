using System.Collections.Generic;
using System.Windows.Forms;

namespace XrmToolBox.New
{
    public partial class InvalidPluginsInfo : UserControl
    {
        private readonly Dictionary<string, string> validationErrors;

        public InvalidPluginsInfo(Dictionary<string, string> validationErrors)
        {
            InitializeComponent();

            this.validationErrors = validationErrors;
        }

        private void lblSecurityFilter_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var ipForm = new InvalidPluginsForm(validationErrors);
                ipForm.ShowDialog(ParentForm);
            }
        }
    }
}