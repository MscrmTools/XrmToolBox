// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    /// <summary>
    /// Class that implements methods to display an information panel
    /// </summary>
    public class InformationPanel
    {
        /// <summary>
        /// Updates the message of an existing panel
        /// </summary>
        /// <param name="informationPanel">Panel to update</param>
        /// <param name="message">Message to display</param>
        public static void ChangeInformationPanelMessage(Panel informationPanel, string message)
        {
            MethodInvoker mi = delegate
            {
                foreach (var label in informationPanel.Controls.OfType<Label>())
                {
                    if (label.Name == "InfoLabel")
                    {
                        label.Text = message;
                    }
                }
            };

            if (informationPanel.InvokeRequired)
            {
                informationPanel.Invoke(mi);
            }
            else
            {
                mi();
            }
        }

        /// <summary>
        /// Creates an information panel with a waiting animated gif and a message
        /// </summary>
        /// <param name="parentControl">Control where the panel will be added</param>
        /// <param name="message">Message to display</param>
        /// <param name="width">Panel width</param>
        /// <param name="height">Panel height</param>
        /// <returns>Panel created</returns>
        public static Panel GetInformationPanel(Control parentControl, string message, int width, int height)
        {
            var panel = new Panel
            {
                Name = "informationPanel",
                Width = width,
                Height = height,
                Location = new Point(
                                    (parentControl.Width - width) / 2,
                                    (parentControl.Height - height) / 2),
                BackColor = Color.FromArgb(255, 255, 224),
                BorderStyle = BorderStyle.FixedSingle
            };

            var label = new Label
            {
                AutoEllipsis = true,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = panel.Width,
                Height = panel.Height / 2,
                Text = message,
                Location = new Point(0, 10),
                Font = new Font("Segoe UI", 10F),
                Name = "InfoLabel"
            };

            var hyperlink = new LinkLabel
            {
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoEllipsis = true,
                AutoSize = false,
                Text = "Support MscrmTools, Donate. Click here!",
                TextAlign = ContentAlignment.MiddleCenter,
                Width = panel.Width,
                Location = new Point(0, panel.Height - 20),
                Font = new Font("Segoe UI", 9F)
            };

            hyperlink.Click += hyperlink_Click;
            panel.Controls.Add(hyperlink);

            var assembly = Assembly.GetExecutingAssembly();
            var file = assembly.GetManifestResourceStream("XrmToolBox.Extensibility.Images.progress.gif");
            if (file != null)
            {
                var pBox = new PictureBox
                {
                    Height = 36,
                    Width = 36,
                    Location = new Point(
                                       (panel.Width - 36) / 2,
                                       (panel.Height - 36) / 4 * 3),
                    Image = Image.FromStream(file)
                };

                panel.Controls.Add(pBox);
            }

            panel.Controls.Add(label);

            parentControl.Resize += ParentControlResize;
            parentControl.Controls.Add(panel);
            panel.BringToFront();

            return panel;
        }

        private static void hyperlink_Click(object sender, EventArgs e)
        {
            Process.Start("http://mscrmtools.blogspot.fr/p/xrmtoolbox-sponsoring.html");
        }

        /// <summary>
        /// Adjusts location of the panel when the parent container is resized
        /// </summary>
        /// <param name="sender">Parent container</param>
        /// <param name="e">Event arguments</param>
        private static void ParentControlResize(object sender, EventArgs e)
        {
            foreach (var ctrl in ((Control)sender).Controls.Cast<object>().OfType<Panel>().Where(ctrl => ctrl.Name == "informationPanel"))
            {
                ctrl.Location = new Point(
                    (((Control)sender).Width - ctrl.Width) / 2,
                    (((Control)sender).Height - ctrl.Height) / 2);
            }
        }
    }
}