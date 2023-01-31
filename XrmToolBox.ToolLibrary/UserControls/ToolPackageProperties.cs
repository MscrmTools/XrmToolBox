using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using XrmToolBox.ToolLibrary.AppCode;

namespace XrmToolBox.ToolLibrary.UserControls
{
    public partial class ToolPackageCtrl : UserControl
    {
        private ToolLibrary library;
        private XtbPlugin plugin;
        private ToolTip toolTip = new ToolTip();
        private Version xrmToolBoxLastBreakVersion;

        public ToolPackageCtrl(Version xrmToolBoxLastBreakVersion)
        {
            InitializeComponent();

            this.xrmToolBoxLastBreakVersion = xrmToolBoxLastBreakVersion;
        }

        public ToolPackageCtrl(XtbPlugin plugin, Version xrmToolBoxLastBreakVersion, ToolLibrary library)
        {
            this.library = library;
            this.plugin = plugin;
            this.xrmToolBoxLastBreakVersion = xrmToolBoxLastBreakVersion;

            InitializeComponent();
            SetControlsDisplay();
            GetVersions();
        }

        public event EventHandler<ToolOperationEventArgs> OnToolOperationRequested;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            OnToolOperationRequested?.Invoke(this, new ToolOperationEventArgs(false, plugin, ((XtbPluginVersion)cbbVersions.SelectedItem).Version, ((XtbPluginVersion)cbbVersions.SelectedItem).DownloadUrl));
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            var version = (XtbPluginVersion)cbbVersions.SelectedItem;
            plugin.RequiresXtbRestart = plugin.CurrentVersion != null && !version.Version.Equals(plugin.CurrentVersion);

            var ea = new ToolOperationEventArgs(true, plugin, version.Version, version.DownloadUrl);
            ea.OnOperationCompleted += (s, success) =>
            {
                if (success)
                {
                    btnInstall.Enabled = false;
                    btnDelete.Enabled = true;

                    version.SetIsCurrent();
                    cbbVersions.Items.Clear();
                    cbbVersions.Items.AddRange(plugin.Versions.OrderByDescending(v => v.Version).ToArray());

                    if (cbbVersions.Items.Count != 0)
                    {
                        cbbVersions.SelectedIndex = 0;
                        cbbVersions.Enabled = true;
                    }
                }
            };
            OnToolOperationRequested?.Invoke(this, ea);
        }

        private void cbbVersions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var version = (XtbPluginVersion)cbbVersions.SelectedItem;
            var releaseNotes = version.ReleaseNotes;
            var r = new Regex("([ ]{2,})");
            releaseNotes = releaseNotes != null ? r.Replace(releaseNotes.Replace("\t", ""), "") : "";
            rtbReleaseNotes.Text = releaseNotes;

            library.AnalyzePackage(plugin, version.Version);

            btnInstall.Enabled = plugin.Action == PackageInstallAction.Install || !(plugin.CurrentVersion?.Simplify().Equals(version.Version.Simplify()) ?? false);
            btnDelete.Enabled = plugin.Action == PackageInstallAction.None || plugin.Action == PackageInstallAction.Update;
            lblIncompatibleReason.Text = version.GetIncompatibleReason();
        }

        private void GetVersions()
        {
            var bw = new BackgroundWorker();
            bw.DoWork += (s, evt) =>
            {
                var httpClient = new HttpClient();
                var data = httpClient.GetAsync($"https://api-v2v3search-0.nuget.org/query?q={plugin.NugetId}").GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var jo = JObject.Parse(data);

                var registrationData = httpClient.GetAsync(((JArray)jo["data"]).FirstOrDefault()["registration"].ToString()).GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult();
                var rd = JObject.Parse(registrationData);
                var versions = ((JArray)rd["items"]).SelectMany(va => (JArray)va["items"]);

                plugin.Versions = new List<XtbPluginVersion>();

                foreach (var versionInfo in versions)
                {
                    var catalogEntry = versionInfo["catalogEntry"];

                    var version = new Version(catalogEntry["version"]?.ToString().Split('-')[0]);
                    var infoUrl = versionInfo["@id"]?.ToString();
                    var isCurrentVersion = plugin.CurrentVersion?.Simplify().Equals(new Version(catalogEntry["version"]?.ToString() ?? "0.0.0.0").Simplify()) ?? false;

                    try
                    {
                        if (catalogEntry["dependencyGroups"] == null)
                        {
                            continue;
                        }

                        var xtbDependency = catalogEntry["dependencyGroups"].SelectMany(dp => dp["dependencies"]).FirstOrDefault(d => d["id"]?.ToString().ToLower() == "xrmtoolbox" || d["id"]?.ToString().ToLower() == "xrmtoolboxpackage")?["range"].ToString();

                        xtbDependency = xtbDependency.StartsWith("[") ? xtbDependency.Remove(0, 1) : xtbDependency;
                        xtbDependency = xtbDependency.Split(',')[0];

                        var minXrmToolBoxVersionSupport = new Version(xtbDependency);

                        plugin.Versions.Add(new XtbPluginVersion(version, infoUrl, minXrmToolBoxVersionSupport, isCurrentVersion, xrmToolBoxLastBreakVersion));
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                }
            };
            bw.RunWorkerCompleted += (s, evt) =>
            {
                cbbVersions.Items.Clear();
                cbbVersions.Items.AddRange(plugin.Versions.OrderByDescending(v => v.Version).ToArray());

                if (cbbVersions.Items.Count != 0)
                {
                    cbbVersions.SelectedIndex = 0;
                    cbbVersions.Enabled = true;
                }
                else
                {
                    MessageBox.Show(this, "No XrmToolBox dependency info have been found for this tool. Please contact the developer", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };
            bw.RunWorkerAsync();
        }

        private void llRateThisTool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start($"https://www.xrmtoolbox.com/plugins/plugininfo/rating/?pvid={plugin.LatestReleaseId}&id={plugin.Id}");
        }

        private void llToolProjectUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(llToolProjectUrl.Tag.ToString());
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            if (image == null) throw new NullReferenceException("Image is null for the tool");

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void rtbDescription_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            var maxAvailableHeight = richTextBox.Parent.ClientSize.Height - richTextBox.Parent.Controls.Cast<Control>().Except(new[] { richTextBox, rtbReleaseNotes }).Sum(c => c.Height) - 50;
            if (maxAvailableHeight < 50)
                maxAvailableHeight = 50;
            richTextBox.ClientSize = new Size(richTextBox.Parent.Width, Math.Min(maxAvailableHeight, e.NewRectangle.Height) + 1);
        }

        private void rtbReleaseNotes_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void SetControlsDisplay()
        {
            var r = new Regex("([ ]{2,})");
            var releaseNotes = plugin.LatestReleaseNote != null ? r.Replace(plugin.LatestReleaseNote.Replace("\t", ""), "") : "";

            lblToolName.Text = plugin.Name;
            lblToolAuthors.Text = plugin.Authors;
            rtbDescription.Text = plugin.Description;
            lblToolFirstRelease.Text = plugin.FirstReleaseDate.Value.ToString("d");
            lblToolLastRelease.Text = plugin.LatestReleaseDate.Value.ToString("d");
            lblToolDownload.Text = plugin.TotalDownloadCount.ToString("N0");
            llToolProjectUrl.Tag = plugin.ProjectUrl;
            rtbReleaseNotes.Text = releaseNotes;
            lblToolRating.Text = $"{plugin.AverageFeedbackRating:F2} ({plugin.TotalFeedbackRating:N0})";

            llRateThisTool.Visible = !plugin.IsFromCustomRepo;

            toolTip.SetToolTip(llToolProjectUrl, plugin.ProjectUrl);

            var leftColumnSize = TextRenderer.MeasureText(lblTitleLatestRelease.Text, lblTitleLatestRelease.Font);
            tlpMain.ColumnStyles[0].SizeType = SizeType.Absolute;
            tlpMain.ColumnStyles[0].Width = leftColumnSize.Width + 20;

            try
            {
                pbLogo.Image = ResizeImage(plugin.Logo, 60, 60);
            }
            catch (Exception error)
            {
                pbLogo.Image = ResizeImage(Resource.NoLogo100, 60, 60);

                toolTip.SetToolTip(pbLogo, $"Logo url:{plugin.LogoUrl}\r\nError:{error.Message}");
            }

            btnInstall.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}