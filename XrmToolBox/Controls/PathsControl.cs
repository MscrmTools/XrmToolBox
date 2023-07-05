using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace XrmToolBox.Controls
{
    public partial class PathsControl : UserControl

    {
        private Dictionary<string, Color> colors = new Dictionary<string, Color>
        {
            {"Logs", Color.Blue},
            { "Metadata", Color.Green },
            { "NugetPlugins", Color.Purple},
            { "Plugins", Color.Orange },
            { "Settings", Color.Yellow },
            { "ToolLogoCache", Color.Red }
        };

        private Dictionary<string, string> labels = new Dictionary<string, string>
        {
            {"Logs", "Logs"},
            { "Metadata", "Connections Metadata"},
            { "NugetPlugins", "Nuget package cache"},
            { "Plugins", "Tools" },
            { "Settings", "Settings"},
            { "ToolLogoCache", "Tool Library Logo cache"}
        };

        private Dictionary<string, long> sizes = new Dictionary<string, long>();
        private string[] subFolders = new[] { "Logs", "Metadata", "NugetPlugins", "Plugins", "Settings", "ToolLogoCache" };

        public PathsControl()
        {
            InitializeComponent();

            var folder = Paths.XrmToolBoxPath;

            foreach (var subFolder in Directory.GetDirectories(folder))
            {
                if (!subFolders.Contains(subFolder.Split('\\').Last())) continue;

                sizes.Add(subFolder.Split('\\').Last(), DirSize(new DirectoryInfo(subFolder)));
            }

            lblChangePathDescription.Text = string.Format(lblChangePathDescription.Text, folder);
        }

        private static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        private void llOpenRootFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.FullName);
        }

        private void llOpenStorageFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Paths.XrmToolBoxPath);
        }

        private void PathsControl_Resize(object sender, EventArgs e)
        {
            pnlStorageGraphics.Invalidate();
            pnlLegend.Invalidate();
        }

        private void pnlLegend_Paint(object sender, PaintEventArgs e)
        {
            var x = (long)e.ClipRectangle.X + 10;
            var blackBrush = new SolidBrush(Color.Black);
            foreach (var subFolder in subFolders)
            {
                if (!sizes.ContainsKey(subFolder)) continue;

                var text = $"{labels[subFolder]} ({sizes[subFolder] / 1024:G} KB)";
                var textSize = TextRenderer.MeasureText(text, Font);

                using (var brush = new SolidBrush(colors[subFolder]))
                {
                    e.Graphics.FillRectangle(brush, x, e.ClipRectangle.Y + 8, 10, 16);
                    x += 20;
                    e.Graphics.DrawString(text, Font, blackBrush, x, 10);
                    x += textSize.Width + 20;
                }
            }
        }

        private void pnlStorageGraphics_Paint(object sender, PaintEventArgs e)
        {
            var total = sizes.Sum(a => a.Value);

            var x = (long)e.ClipRectangle.X;
            foreach (var subFolder in subFolders)
            {
                if (!sizes.ContainsKey(subFolder)) continue;

                var width = e.ClipRectangle.Width * sizes[subFolder] / total;
                using (var brush = new SolidBrush(colors[subFolder]))
                {
                    e.Graphics.FillRectangle(brush, x, e.ClipRectangle.Y, width, e.ClipRectangle.Height);
                }

                x += width;
            }
        }
    }
}