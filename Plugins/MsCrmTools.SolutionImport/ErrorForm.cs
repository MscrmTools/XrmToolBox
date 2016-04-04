// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Drawing;
using System.Windows.Forms;

namespace MsCrmTools.SolutionImport
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        public ErrorForm(string message)
            : this()
        {
            richTextBox1.Text = message;
            int start = richTextBox1.Find("<<<<<ERROR LOCATION>>>>>");
            if (start >= 0)
            {
                richTextBox1.Select(start, 24);
                richTextBox1.SelectionColor = Color.Red;
            }
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}