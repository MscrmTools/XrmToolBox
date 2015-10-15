using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class Worker
    {
        private Panel _infoPanel;

        private BackgroundWorker worker;

        public void CancelWorker()
        {
            if (worker != null && worker.WorkerSupportsCancellation)
            {
                worker.CancelAsync();

                if (_infoPanel != null)
                {
                    _infoPanel.Parent.Controls.Remove(_infoPanel);
                    _infoPanel.Dispose();
                }
            }
        }

        public void SetWorkingMessage(Control host, string message, int width = 340, int height = 150)
        {
            if (host.Controls.Contains(_infoPanel))
            {
                if (_infoPanel.Width != width || _infoPanel.Height != height)
                {
                    _infoPanel.Dispose();
                    host.Controls.Remove(_infoPanel);
                    _infoPanel = InformationPanel.GetInformationPanel(host, message, width, height);
                }
                else
                {
                    InformationPanel.ChangeInformationPanelMessage(_infoPanel, message);
                }
            }
            else
            {
                _infoPanel = InformationPanel.GetInformationPanel(host, message, width, height);
            }
        }

        public void WorkAsync(Control host, string message, Action<DoWorkEventArgs> work, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker();

            worker.DoWork += (s, e) => work(e);

            worker.RunWorkerAsync(argument);
        }

        public void WorkAsync(Control host, string message, Action<DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker();

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

        public void WorkAsync(Control host, string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, object argument = null, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker { WorkerReportsProgress = progressChanged != null };

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

        public void WorkAsync(Control host, string message, Action<BackgroundWorker, DoWorkEventArgs> work, object argument = null, bool enableCancellation = false, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = enableCancellation;

            worker.DoWork += (s, e) => work((BackgroundWorker)s, e);

            worker.RunWorkerAsync(argument);
        }

        public void WorkAsync(Control host, string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, object argument = null, bool enableCancellation = false, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = enableCancellation;

            worker.DoWork += (s, e) => work((BackgroundWorker)s, e);

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

        public void WorkAsync(Control host, string message, Action<BackgroundWorker, DoWorkEventArgs> work, Action<RunWorkerCompletedEventArgs> callback, Action<ProgressChangedEventArgs> progressChanged, object argument = null, bool enableCancellation = false, int messageWidth = 340, int messageHeight = 150)
        {
            _infoPanel = InformationPanel.GetInformationPanel(host, message, messageWidth, messageHeight);

            worker = new BackgroundWorker { WorkerReportsProgress = progressChanged != null };
            worker.WorkerSupportsCancellation = enableCancellation;

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
    }
}