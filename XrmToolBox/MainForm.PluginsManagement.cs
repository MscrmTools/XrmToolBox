using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk.Client;
using XrmToolBox.Attributes;
using XrmToolBox.Forms;
using XrmToolBox.UserControls;

namespace XrmToolBox
{
    partial class MainForm
    {
        private void DisplayPlugins(object filter = null)
        {
            if (pManager.Plugins.Count == 0)
            {
                this.Invoke(new Action(() =>
                {
                    this.pnlHelp.Visible = true;
                }));

                return;
            }

            var top = 4;
            int lastWidth = HomePageTab.Width - 28;

            var filteredPlugins = (filter != null
                ? pManager.Plugins.Where(p
                    => p.GetTitle().ToLower().Contains(filter.ToString().ToLower())
                    || p.GetCompany().ToLower().Contains(filter.ToString().ToLower()))
                : pManager.Plugins).ToList();

            if (currentOptions.DisplayMostUsedFirst)
            {
                foreach (var item in currentOptions.MostUsedList.OrderByDescending(i => i.Count).ThenBy(i => i.Name))
                {
                    var plugin = filteredPlugins.FirstOrDefault(x => x.FullName == item.Name);
                    if (plugin != null && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.GetTitle())))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth, item.Count);
                    }
                }

                foreach (var plugin in filteredPlugins.OrderBy(p => p.GetTitle()))
                {
                    if (currentOptions.MostUsedList.All(i => i.Name != plugin.FullName) && (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.GetTitle())))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }
            else
            {
                foreach (var plugin in filteredPlugins.OrderBy(p => p.GetTitle()))
                {
                    if (currentOptions.HiddenPlugins == null || !currentOptions.HiddenPlugins.Contains(plugin.GetTitle()))
                    {
                        DisplayOnePlugin(plugin, ref top, lastWidth);
                    }
                }
            }

            this.Invoke(new Action(() =>
            {
                HomePageTab.Controls.Clear();

                foreach (UserControl ctrl in pManager.PluginsControls.Where(p => filteredPlugins.Contains(p.Tag)))
                {
                    ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    HomePageTab.Controls.Add(ctrl);
                }

                AdaptPluginControlSize();
            }));
        }

        private Image GetImage(Type plugin, bool small = false)
        {
            // Default logo (no-logo)
            var thisAssembly = Assembly.GetExecutingAssembly();
            var logoStream = thisAssembly.GetManifestResourceStream(small ? "XrmToolBox.Images.nologo32.png" : "XrmToolBox.Images.nologo.png");
            if (logoStream == null)
            {
                throw new Exception("Unable to find no-logo stream!");
            }

            var logo = Image.FromStream(logoStream);

            // Old method
            var pluginControl = (IMsCrmToolsPluginUserControl)PluginManager.CreateInstance(plugin.Assembly.Location, plugin.FullName);
            if (pluginControl.PluginLogo != null)
                logo = pluginControl.PluginLogo;

            // Replace by new method if available
            var b64 = AssemblyAttributeHelper.GetStringAttributeValue(plugin.Assembly, small ? "SmallBase64Image" : "BigBase64Image");
            if (b64.Length > 0)
            {
                var bytes = Convert.FromBase64String(b64);
                var ms = new MemoryStream(bytes, 0, bytes.Length);
                ms.Write(bytes, 0, bytes.Length);
                logo = Image.FromStream(ms);
                ms.Close();
            }

            return logo;
        }

        private void DisplayOnePlugin(Type plugin, ref int top, int width, int count = -1)
        {
            PluginModel pm;

            if (currentOptions.DisplayLargeIcons)
            {
                pm = this.CreateModel<LargePluginModel>(plugin, ref top, width, count);
            }
            else
            {
                pm = this.CreateModel<SmallPluginModel>(plugin, ref top, width, count);
            }
        }

        private PluginModel CreateModel<T>(Type plugin, ref int top, int width, int count)
            where T : PluginModel
        {
            var pm = (T)this.pManager.PluginsControls.FirstOrDefault(t => (Type)t.Tag == plugin && t is T);

            if (pm == null)
            {
                var title = plugin.GetTitle();
                var desc = plugin.GetDescription();
                var author = plugin.GetCompany();
                var version = plugin.Assembly.GetName().Version.ToString();

                var backColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(BackgroundColorAttribute));
                var primaryColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(PrimaryFontColorAttribute));
                var secondaryColor = AssemblyAttributeHelper.GetColor(plugin.Assembly, typeof(SecondaryFontColorAttribute));

                var args = new Type[] 
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
                    GetImage(plugin), 
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
                pm = (T)ctor.Invoke(vals);

                pm.Tag = plugin;
                pm.Clicked += PluginClicked;

                this.pManager.PluginsControls.Add(pm);
            }

            var localTop = top;

            this.Invoke(new Action(() =>
            {
                pm.Left = 4;
                pm.Top = localTop;
                pm.Width = width;
            }));
            top += pm.Height + 4;

            return pm;
        }


        private int DisplayPluginControl(UserControl plugin)
        {
            var tabIndex = 0;

            try
            {
                var controlType = (Type)plugin.Tag;
                var pluginControl = (UserControl)PluginManager.CreateInstance(controlType.Assembly.Location, controlType.FullName);

                if (service != null)
                {
                    var clonedService = (OrganizationService)currentConnectionDetail.GetOrganizationService();
                    ((OrganizationServiceProxy)clonedService.InnerService).SdkClientVersion = currentConnectionDetail.OrganizationVersion;

                    ((IMsCrmToolsPluginUserControl)pluginControl).UpdateConnection(clonedService,
                        currentConnectionDetail);
                }

                if (pluginControl is IMessageBusHost)
                {
                    ((IMessageBusHost)pluginControl).OnOutgoingMessage += MainForm_MessageBroker;
                }

                ((IMsCrmToolsPluginUserControl)pluginControl).OnRequestConnection += MainForm_OnRequestConnection;
                ((IMsCrmToolsPluginUserControl)pluginControl).OnCloseTool += MainForm_OnCloseTool;

                string name = string.Format("{0} ({1})", pluginControl.GetType().GetTitle(),
                    currentConnectionDetail != null
                        ? currentConnectionDetail.ConnectionName
                        : "Not connected");

                var newTab = new TabPage(name);
                tabControl1.TabPages.Add(newTab);

                pluginControl.Dock = DockStyle.Fill;
                pluginControl.Width = newTab.Width;
                pluginControl.Height = newTab.Height;

                newTab.Controls.Add(pluginControl);

                tabIndex = tabControl1.TabPages.Count - 1;

                tabControl1.SelectTab(tabIndex);

                var pluginInOption =
                    currentOptions.MostUsedList.FirstOrDefault(i => i.Name == pluginControl.GetType().FullName);
                if (pluginInOption == null)
                {
                    pluginInOption = new PluginUseCount { Name = pluginControl.GetType().FullName, Count = 0 };
                    currentOptions.MostUsedList.Add(pluginInOption);
                }

                pluginInOption.Count++;

                var p1 = plugin as SmallPluginModel;
                if (p1 != null)
                    p1.UpdateCount(pluginInOption.Count);
                else
                {
                    var p2 = plugin as LargePluginModel;
                    if (p2 != null)
                    {
                        p2.UpdateCount(pluginInOption.Count);
                    }
                }

                if (currentOptions.LastAdvertisementDisplay == new DateTime() ||
                    currentOptions.LastAdvertisementDisplay > DateTime.Now ||
                    currentOptions.LastAdvertisementDisplay.AddDays(7) < DateTime.Now)
                {
                    bool displayAdvertisement = true;
                    try
                    {
                        var assembly =
                            Assembly.LoadFile(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory +
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

                currentOptions.Save();
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "An error occured when trying to display this plugin: " + error.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return tabIndex;
        }

    }
}
