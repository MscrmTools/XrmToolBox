using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox
{
    public class Worker
    {
        private Panel _infoPanel;

        public void WorkAsync(Control host, object argument, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, string message, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            var worker = new BackgroundWorker();

            worker.DoWork += (s, e) => work(e);

            worker.RunWorkerCompleted += (s, e) =>
            {
                if (host.Controls.Contains(_infoPanel))
                {
                    _infoPanel.Dispose();
                    host.Controls.Remove(_infoPanel);
                }
                callback(e);
            };
            worker.RunWorkerAsync(argument);
        }

        public void WorkAsync(Control host, object argument, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, string message, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            var worker = new BackgroundWorker {WorkerReportsProgress = progressChanged != null};

            worker.DoWork += (s, e) => work((BackgroundWorker)s, e);

            if (worker.WorkerReportsProgress && progressChanged != null)
            {
                worker.ProgressChanged += (s, e) => progressChanged(e);
            }

            worker.RunWorkerCompleted += (s, e) =>
            {
                if (host.Controls.Contains(_infoPanel))
                {
                    _infoPanel.Dispose();
                    host.Controls.Remove(_infoPanel);
                }
                callback(e);
            };
            worker.RunWorkerAsync(argument);
        }

        public void SetWorkingMessage(Control host, string message, int messageWidth = 340, int messageHeight = 150)
        {
            if (host.Controls.Contains(_infoPanel))
            {
                if (_infoPanel.Width != messageWidth || _infoPanel.Height != messageHeight)
                {
                    _infoPanel.Dispose();
                    host.Controls.Remove(_infoPanel);
                    _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);
                }
                else
                {
                    InformationPanel.ChangeInformationPanelMessage(_infoPanel, message);
                }
            }
            else
            {
                _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);
            }
        }
    }
}
