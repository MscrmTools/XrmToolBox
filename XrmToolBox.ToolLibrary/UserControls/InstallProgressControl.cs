using System;
using System.Windows.Forms;

namespace XrmToolBox.ToolLibrary.UserControls
{
    public partial class InstallProgressControl : UserControl
    {
        public InstallProgressControl()
        {
            InitializeComponent();
        }

        public event EventHandler OnCloseRequested;

        public bool IsSuccess { get; private set; } = true;

        public void AddDownloadStep(string toolName)
        {
            var step = new ProgressStepControl($"Downloading {toolName}", Resource.Download32);
            step.Dock = DockStyle.Top;
            step.Height = 52;

            pnlMain.Controls.Add(step);
            pnlMain.Controls.SetChildIndex(step, 0);

            pnlMain.ScrollControlIntoView(step);
        }

        public void AddInstallStep(string toolName)
        {
            var step = new ProgressStepControl($"Installing {toolName}", Resource.Install32);
            step.Dock = DockStyle.Top;
            step.Height = 52;

            pnlMain.Controls.Add(step);
            pnlMain.Controls.SetChildIndex(step, 0);

            pnlMain.ScrollControlIntoView(step);
        }

        public void End()
        {
            btnClose.Enabled = true;
        }

        public void SetError(string message)
        {
            ((ProgressStepControl)pnlMain.Controls[0]).SetFailure(message);
            IsSuccess = false;
        }

        public void SetSuccess(bool pending = false)
        {
            ((ProgressStepControl)pnlMain.Controls[0]).SetSuccess(pending);

            btnRestart.Visible = pending;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            OnCloseRequested?.Invoke(this, new EventArgs());
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}