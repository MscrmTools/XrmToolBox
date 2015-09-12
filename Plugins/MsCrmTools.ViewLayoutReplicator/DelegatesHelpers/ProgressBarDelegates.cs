// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Drawing;
using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ProgressBarDelegates
    {
        public static void PerformStep(ProgressBar progressBar)
        {
            MethodInvoker miPerformStep = delegate
            {
                progressBar.PerformStep();
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miPerformStep);
            }
            else
            {
                miPerformStep();
            }
        }

        public static void SetForeColor(ProgressBar progressBar, Color color)
        {
            MethodInvoker miSetForeColor = delegate
            {
                progressBar.ForeColor = color;
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miSetForeColor);
            }
            else
            {
                miSetForeColor();
            }
        }

        public static void SetMaximum(ProgressBar progressBar, int maximum)
        {
            MethodInvoker miSetMaximum = delegate
            {
                progressBar.Maximum = maximum;
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miSetMaximum);
            }
            else
            {
                miSetMaximum();
            }
        }

        public static void SetMinimum(ProgressBar progressBar, int minimum)
        {
            MethodInvoker miSetMinimum = delegate
            {
                progressBar.Maximum = minimum;
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miSetMinimum);
            }
            else
            {
                miSetMinimum();
            }
        }

        public static void SetStep(ProgressBar progressBar, int step)
        {
            MethodInvoker miSetStep = delegate
            {
                progressBar.Step = step;
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miSetStep);
            }
            else
            {
                miSetStep();
            }
        }

        public static void SetValue(ProgressBar progressBar, int value)
        {
            MethodInvoker miSetValue = delegate
            {
                progressBar.Value = value;
            };

            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(miSetValue);
            }
            else
            {
                miSetValue();
            }
        }
    }
}