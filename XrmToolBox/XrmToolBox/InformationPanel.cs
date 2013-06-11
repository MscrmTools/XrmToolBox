// PROJECT : XrmToolBox
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace XrmToolBox
{
    /// <summary>
    /// Class that implements methods to display an information panel
    /// </summary>
    public class InformationPanel
    {
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
                                    (parentControl.Width - width)/2,
                                    (parentControl.Height - height)/2),
                                    BackColor = Color.FromArgb(255,255,192),
                                    BorderStyle = BorderStyle.FixedSingle
                            };

            var label = new Label
                            {
                                AutoEllipsis = true,
                                AutoSize = false,
                                TextAlign = ContentAlignment.MiddleCenter,
                                Width = panel.Width,
                                Height = panel.Height/2,
                                Text = message,
                                Location = new Point(0, 10),
                                Font = new Font("Segoe UI", 11F)
                            };


            var assembly = Assembly.GetExecutingAssembly();
            var file = assembly.GetManifestResourceStream("XrmToolBox.Images.progress.gif");
            if (file != null)
            {
                var pBox = new PictureBox
                               {
                                   Height = 32,
                                   Width = 32,
                                   Location = new Point(
                                       (panel.Width - 32)/2,
                                       (panel.Height - 32)/4*3),
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

        /// <summary>
        /// Updates the message of an existing panel
        /// </summary>
        /// <param name="informationPanel">Panel to update</param>
        /// <param name="message">Message to display</param>
        public static void ChangeInformationPanelMessage(Panel informationPanel, string message)
        {
            foreach (var label in informationPanel.Controls.OfType<Label>())
            {
                label.Text = message;
            }
        }
        
        /// <summary>
        /// Adjusts location of the panel when the parent container is resized
        /// </summary>
        /// <param name="sender">Parent container</param>
        /// <param name="e">Event arguments</param>
        private static void ParentControlResize(object sender, EventArgs e)
        {
            foreach (var ctrl in ((Control) sender).Controls.Cast<object>().OfType<Panel>().Where(ctrl => ctrl.Name == "informationPanel"))
            {
                ctrl.Location = new Point(
                    (((Control) sender).Width - ctrl.Width)/2,
                    (((Control) sender).Height - ctrl.Height)/2);
            }
        }
    }
}
