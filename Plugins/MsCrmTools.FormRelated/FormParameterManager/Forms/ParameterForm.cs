using Microsoft.Xrm.Sdk;
using MsCrmTools.FormParameterManager.AppCode;
using System;
using System.Windows.Forms;

namespace MsCrmTools.FormParameterManager.Forms
{
    public partial class ParameterForm : Form
    {
        private readonly IOrganizationService service;

        public ParameterForm(IOrganizationService service)
        {
            InitializeComponent();
            this.service = service;
            cbbTypes.SelectedIndex = 0;
        }

        public FormParameter Parameter { get; private set; }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Parameter = new FormParameter
                {
                    Name = txtName.Text,
                    Type = (FormParameterType)Enum.Parse(typeof(FormParameterType), cbbTypes.SelectedItem.ToString())
                };

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, error.Message, "Invalid Parameter Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var solution = (CrmSolution)comboBox1.SelectedItem;

            var name = txtName.Text;
            var nameParts = name.Split('_');
            nameParts[0] = solution.Prefix;
            txtName.Text = string.Join("_", nameParts);
        }

        private void ParameterForm_Load(object sender, EventArgs e)
        {
            var solutions = CrmSolution.GetUnmanagedSolutions(service);
            foreach (var solution in solutions)
            {
                comboBox1.Items.Add(solution);
            }

            comboBox1.SelectedIndex = 0;
        }
    }
}