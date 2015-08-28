using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;
using XrmToolBox.Extensibility.UserControls;
using XrmToolBox.Forms;

namespace XrmToolBox
{
    partial class MainForm
    {
        /// <summary>
        /// List of plugins tiles
        /// </summary>
        private readonly List<PluginModel> pluginsModels;

        private void CreateModel<T>(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin, ref int top, int width, int count)
             where T : PluginModel
        {
            var type = plugin.Value.GetMyType();
            //var pm = (T)pManager.PluginsControls.FirstOrDefault(t => ((Type)t.Tag).FullName == type && t is T);

            var pm = (T)pluginsModels.FirstOrDefault(t => ((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)t.Tag).Value.GetType().FullName == type && t is T);
            var small = (typeof(T) == typeof(SmallPluginModel));

            if (pm == null)
            {
                var title = plugin.Metadata.Name;
                var desc = plugin.Metadata.Description;

                var author = plugin.Value.GetCompany();
                var version = plugin.Value.GetVersion();

                var backColor = ColorTranslator.FromHtml(plugin.Metadata.BackgroundColor);
                var primaryColor = ColorTranslator.FromHtml(plugin.Metadata.PrimaryFontColor);
                var secondaryColor = ColorTranslator.FromHtml(plugin.Metadata.SecondaryFontColor);

                var args = new[]
            {
                    typeof(Image),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(Color),
                    typeof(Color),
                    typeof(Color),
                    typeof(int)
                };

                var vals = new object[]
                {
                    GetImage(small ? plugin.Metadata.SmallImageBase64 : plugin.Metadata.BigImageBase64, small),
                    title,
                    desc,
                    author,
                    version,
                    backColor,
                    primaryColor,
                    secondaryColor,
                    count
                };

                var ctor = typeof(T).GetConstructor(args);
                if (ctor != null)
                {
                    pm = (T)ctor.Invoke(vals);

                    pm.Tag = plugin;
                    pm.Clicked += PluginClicked;

                    pluginsModels.Add(pm);
                }
            }

            if (pm == null) { return; }

            var localTop = top;

            Invoke(new Action(() =>
            {
                pm.Left = 4;
                pm.Top = localTop;
                pm.Width = width;
            }));
            top += pm.Height + 4;
        }

        private void DisplayOnePlugin(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin, ref int top, int width, int count = -1)
        {
            if (currentOptions.DisplayLargeIcons)
            {
                CreateModel<LargePluginModel>(plugin, ref top, width, count);
            }
            else
            {
                CreateModel<SmallPluginModel>(plugin, ref top, width, count);
            }
        }

        private int DisplayPluginControl(Lazy<IXrmToolBoxPlugin, IPluginMetadata> plugin)
        {
            var tabIndex = 0;

            try
            {
                var pluginControl = (UserControl)plugin.Value.GetControl();

                if (service != null)
                {
                    var clonedService = (OrganizationService)currentConnectionDetail.GetOrganizationService();
                    ((OrganizationServiceProxy)clonedService.InnerService).SdkClientVersion = currentConnectionDetail.OrganizationVersion;

                    ((IXrmToolBoxPluginControl)pluginControl).UpdateConnection(clonedService, currentConnectionDetail);
                }

                // ReSharper disable once SuspiciousTypeConversion.Global
                var host = pluginControl as IMessageBusHost;
                if (host != null)
                {
                    host.OnOutgoingMessage += MainForm_MessageBroker;
                }

                ((IXrmToolBoxPluginControl)pluginControl).OnRequestConnection += MainForm_OnRequestConnection;
                ((IXrmToolBoxPluginControl)pluginControl).OnCloseTool += MainForm_OnCloseTool;

                string name = string.Format("{0} ({1})", plugin.Metadata.Name,
                    currentConnectionDetail != null
                        ? currentConnectionDetail.ConnectionName
                        : "Not connected");

                var newTab = new TabPage(name) { Tag = plugin };
                tabControl1.TabPages.Add(newTab);

                pluginControl.Dock = DockStyle.Fill;
                pluginControl.Width = newTab.Width;
                pluginControl.Height = newTab.Height;

                newTab.Controls.Add(pluginControl);

                tabIndex = tabControl1.TabPages.Count - 1;

                tabControl1.SelectTab(tabIndex);

                var pluginInOption = currentOptions.MostUsedList.FirstOrDefault(i => i.Name == plugin.Value.GetType().FullName);
                if (pluginInOption == null)
                {
                    pluginInOption = new PluginUseCount { Name = plugin.Value.GetType().FullName, Count = 0 };
                    currentOptions.MostUsedList.Add(pluginInOption);
                }

                pluginInOption.Count++;

                //var p1 = plugin as SmallPluginModel;
                //if (p1 != null)
                //    p1.UpdateCount(pluginInOption.Count);
                //else
                //{
                //    var p2 = plugin as LargePluginModel;
                //    if (p2 != null)
                //    {
                //        p2.UpdateCount(pluginInOption.Count);
                //    }
                //}

                if (currentOptions.LastAdvertisementDisplay == new DateTime() ||
                    currentOptions.LastAdvertisementDisplay > DateTime.Now ||
                    currentOptions.LastAdvertisementDisplay.AddDays(7) < DateTime.Now)
                {
                    bool displayAdvertisement = true;
                    try
                    {
                        var assembly = Assembly.LoadFile(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
                                              "\\McTools.StopAdvertisement.dll");
                        if (assembly != null)
                        {
                            Type type = assembly.GetType("McTools.StopAdvertisement.LicenseManager");
                            if (type != null)
                            {
                                MethodInfo methodInfo = type.GetMethod("IsValid");
                                if (methodInfo != null)
                                {
                                    object classInstance = Activator.CreateInstance(type, null);

                                    if ((bool)methodInfo.Invoke(classInstance, null))
                                    {
                                        displayAdvertisement = false;
                                    }
                                }
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                    }

                    if (displayAdvertisement)
                    {
                        var sc = new SupportScreen(currentReleaseNote);
                        sc.ShowDialog(this);
                        currentOptions.LastAdvertisementDisplay = DateTime.Now;
                    }
                }

                if (currentOptions.AllowLogUsage.HasValue && currentOptions.AllowLogUsage.Value)
                {
                    LogUsage.DoLog(plugin);
                }

                currentOptions.Save();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured when trying to display this plugin: " + error.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabIndex;
        }

        private void DisplayPlugins(object filter = null)
        {
            if (!pManager.Plugins.Any())
            {
                Invoke(new Action(() =>
                {
                    pnlHelp.Visible = true;
                }));

                return;
            }

            var top = 4;
            int lastWidth = HomePageTab.Width - 28;

            // Search with filter defined
            var filteredPlugins = (filter != null && filter.ToString().Length > 0
                ? pManager.Plugins.Where(p
                    => p.Metadata.Name.ToLower().Contains(filter.ToString().ToLower())
                    || p.Value.GetType().GetCompany().ToLower().Contains(filter.ToString().ToLower()))
                : pManager.Plugins).OrderBy(p => p.Metadata.Name).ToList();

            if (currentOptions.DisplayMostUsedFirst)
            {
                foreach (var item in currentOptions.MostUsedList.OrderByDescending(i => i.Count).ThenBy(i => i.Name))
                {
                    var plugin = filteredPlugins.FirstOrDefault(x => x.Value.GetType().FullName == item.Name);
                    if (plugin != null && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.GetType().GetTitle())))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth, item.Count);
                    }
                }

                foreach (var plugin in filteredPlugins.OrderBy(p => p.Metadata.Name))
                {
                    if (currentOptions.MostUsedList.All(i => i.Name != plugin.Value.GetType().FullName) && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.Value.GetType().GetTitle())))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }
            else
            {
                foreach (var plugin in filteredPlugins.OrderBy(p => p.Metadata.Name))
                {
                    if (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.Metadata.Name))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }

            Invoke(new Action(() =>
            {
                HomePageTab.Controls.Clear();

                foreach (PluginModel ctrl in pluginsModels.Where(p => filteredPlugins.Contains((Lazy<IXrmToolBoxPlugin, IPluginMetadata>)p.Tag)))
                //foreach (PluginModel ctrl in pluginsModels)
                {
                    ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    HomePageTab.Controls.Add(ctrl);
                }

                AdaptPluginControlSize();
            }));
        }

        /// <summary>
        /// Retrieves the logo to display in plugins list
        /// </summary>
        /// <param name="base64ImageContent">Base 64 content for the logo</param>
        /// <param name="small">Defines if the default logo requested should be small</param>
        /// <returns>Image</returns>
        private Image GetImage(string base64ImageContent, bool small = false)
        {
            // Default logo (no-logo)
            var thisAssembly = Assembly.GetExecutingAssembly();
            var logoStream = thisAssembly.GetManifestResourceStream(small ? "XrmToolBox.Images.nologo32.png" : "XrmToolBox.Images.nologo.png");
            if (logoStream == null)
            {
                throw new Exception("Unable to find no-logo stream!");
            }

            var logo = Image.FromStream(logoStream);

            // Replace by plugin logo if specified
            if (!string.IsNullOrEmpty(base64ImageContent))
            {
                var bytes = Convert.FromBase64String(base64ImageContent);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                logo = Image.FromStream(ms);
                ms.Close();
            }

            return logo;
        }

        private void pManager_PluginsListUpdated(object sender, EventArgs e)
        {
            if (DialogResult.Yes ==
                MessageBox.Show(this,
                    "A plugin has been added in Plugins directory, would you like to refresh the plugins list?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                pManager.Recompose();
                pluginsModels.Clear();
                DisplayPlugins(tstxtFilterPlugin.Text);
            }
        }
    }
}