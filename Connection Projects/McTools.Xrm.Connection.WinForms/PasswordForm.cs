using System;
using System.Windows.Forms;

namespace McTools.Xrm.Connection.WinForms
{
    /// <summary>
    /// Formulaire Windows permettant de demander le mot de 
    /// passe d'un utilisateur
    /// </summary>
    public partial class PasswordForm : Form
    {
        #region Variables

        /// <summary>
        /// Login de l'utilisateur
        /// </summary>
        string userLogin;

        /// <summary>
        /// Nom de domaine pour l'utilisateur
        /// </summary>
        string userDomain;

        /// <summary>
        /// Mot de passe de l'utilisateur
        /// </summary>
        string userPassword;

        #endregion

        #region Constructeur

        /// <summary>
        /// Créé une nouvelle instance de la classe PasswordForm
        /// </summary>
        public PasswordForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Obtient ou définit le login de l'utilisateur
        /// </summary>
        public string UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; }
        }

        /// <summary>
        /// Obtient ou définit le nom de domaine pour l'utilisateur
        /// </summary>
        public string UserDomain
        {
            get { return userDomain; }
            set { userDomain = value; }
        }

        /// <summary>
        /// Obtient le mot de passe de l'utilisateur
        /// </summary>
        public string UserPassword
        {
            get { return userPassword; }
        }

        public bool SavePassword { get; set; }

        #endregion

        #region Méthodes

        protected override void OnLoad(EventArgs e)
        {
            this.tbUserLogin.Text = string.Format("{0}{1}{2}",
                userDomain,
                userDomain.Length > 0 ? "\\" : "",
                userLogin);

            base.OnLoad(e);
        }

        private void bValidate_Click(object sender, EventArgs e)
        {
            bool go = true;

            if (this.tbPassword.Text.Length == 0)
            {
                if (MessageBox.Show(this, "Are you sure you want to leave the password empty?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    go = false;
                }
            }

            if (go)
            {
                this.userPassword = this.tbPassword.Text;
                this.SavePassword = this.chkSavePassword.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                bValidate_Click(null, null);
            }
        }

        private void chkShowCharacters_CheckedChanged(object sender, EventArgs e)
        {
            this.tbPassword.PasswordChar = this.chkShowCharacters.Checked ? (char)0 : '•';
        }

        #endregion
    }
}