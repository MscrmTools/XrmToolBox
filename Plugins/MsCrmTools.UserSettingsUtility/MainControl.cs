using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Forms;
using MsCrmTools.UserSettingsUtility.AppCode;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.UserSettingsUtility
{
    public partial class MainControl : PluginControlBase
    {
        private List<string> areas;
        private List<Tuple<string,string>> subAreas; 

        public MainControl()
        {
            InitializeComponent();
        }

        private void TsbCloseClick(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbLoadCrmItems_Click(object sender, EventArgs e)
        {
           ExecuteMethod(LoadCrmItems);
        }

        private void LoadCrmItems()
        {
            cbbSiteMapArea.Items.Clear();
            cbbSiteMapSubArea.Items.Clear();
            cbbTimeZones.Items.Clear();
            userSelector1.Service = Service;
            userSelector1.LoadViews();

            WorkAsync("Initializing...",
                (w, e) =>
                {
                    var sc = new SettingsCollection();

                    w.ReportProgress(0, "Loading Available languages...");
                    var ush = new UserSettingsHelper(Service);
                    sc.Languages = ush.RetrieveAvailableLanguages();

                    w.ReportProgress(0, "Loading Currencies...");
                    ush = new UserSettingsHelper(Service);
                    sc.Currencies = ush.RetrieveCurrencies();

                    w.ReportProgress(0, "Loading Time Zones...");
                    ush = new UserSettingsHelper(Service);
                    sc.TimeZones = ush.RetrieveTimeZones();

                    w.ReportProgress(0, "Loading SiteMap elements...");
                    var smm = new SiteMapManager(Service);
                    areas = smm.GetAreaList();
                    subAreas = smm.GetSubAreaList();

                    e.Result = sc;
                },
                e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + e.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        cbbTimeZones.Items.Clear();
                        cbbHelpLanguage.Items.Clear();
                        cbbUiLanguage.Items.Clear();
                        cbbSiteMapArea.Items.Clear();
                        cbbSiteMapSubArea.Items.Clear();
                        cbbCurrencies.Items.Clear();

                        var sc = (SettingsCollection)e.Result;

                        // TimeZones
                        cbbTimeZones.Items.Add(new AppCode.TimeZone
                        {
                            Code = -1,
                            Name = "No change"
                        });
                        cbbTimeZones.Items.AddRange(
                            sc.TimeZones.Entities.ToList()
                                .Select(
                                    t =>
                                        new AppCode.TimeZone
                                        {
                                            Code = t.GetAttributeValue<int>("timezonecode"),
                                            Name = t.GetAttributeValue<string>("userinterfacename")
                                        })
                                .Cast<object>().ToArray());
                        cbbTimeZones.SelectedIndex = 0;

                        // Language
                        cbbHelpLanguage.Items.Add("No change");
                        cbbHelpLanguage.Items.AddRange(sc.Languages.ToArray());
                        cbbUiLanguage.Items.Add("No change");
                        cbbUiLanguage.Items.AddRange(sc.Languages.ToArray());

                        // Currencies
                        cbbCurrencies.Items.Add("No change");
                        cbbCurrencies.Items.AddRange(sc.Currencies.ToArray());

                        // SiteMap
                        cbbSiteMapArea.Items.Add("No change");
                        cbbSiteMapArea.Items.AddRange(areas.ToArray());
                        cbbSiteMapSubArea.Items.Add("No change");
                        cbbSiteMapSubArea.Items.AddRange(subAreas.Select(t => t.Item1).ToArray());
                        cbbSiteMapSubArea.Enabled = false;

                        foreach (var ctrl in panel1.Controls)
                        {
                            var gb = ctrl as GroupBox;
                            if (gb != null)
                            {
                                foreach (var ctrl2 in gb.Controls)
                                {
                                    var cbb = ctrl2 as ComboBox;
                                    if (cbb != null && cbb.Items.Count > 0)
                                    {
                                        cbb.SelectedIndex = 0;
                                    }
                                }
                            }
                            
                        }

                        panel1.Enabled = true;
                    }
                },
                e =>
                {
                    SetWorkingMessage(e.UserState.ToString());
                });
        }
  
        private void cbbSiteMapArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbSiteMapSubArea.Enabled = cbbSiteMapArea.SelectedIndex != 0;
            cbbSiteMapSubArea.Items.Clear();
            cbbSiteMapSubArea.Items.Add("No change");
            cbbSiteMapSubArea.Items.AddRange(subAreas.Where(t => t.Item2 == cbbSiteMapArea.SelectedItem.ToString()).Select(t => t.Item1).ToArray());
            cbbSiteMapSubArea.SelectedIndex = 0;
        }

        private void tsbUpdateUserSettings_Click(object sender, EventArgs e)
        {
            if (userSelector1.SelectedItems.Count == 0)
            {
                MessageBox.Show(this, "No user has been selected!", "Warning", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            #region Initialisation des données à mettre à jour

            var setting = new UserSettings
            {
                AdvancedFindStartupMode = cbbAdvancedFindMode.SelectedIndex,
                AutoCreateContactOnPromote = cbbCreateRecords.SelectedIndex - 1,
                DefaultCalendarView = cbbCalendar.SelectedIndex - 1,
                IncomingEmailFilteringMethod = cbbTrackMessages.SelectedIndex - 1,
                ReportScriptErrors = cbbReportScriptErrors.SelectedIndex,
                HomePageArea = cbbSiteMapArea.SelectedItem.ToString(),
                HomePageSubArea = cbbSiteMapSubArea.SelectedItem.ToString(),
                UsersToUpdate = userSelector1.SelectedItems,
            };

            if (cbbSendAsAllowed.SelectedIndex != 0)
            {
                setting.IsSendAsAllowed = cbbSendAsAllowed.SelectedIndex == 2;
            }

            if (cbbPagingLimit.SelectedIndex != 0)
            {
                setting.PagingLimit = int.Parse(cbbPagingLimit.SelectedItem.ToString());
            }

            if (cbbTimeZones.SelectedIndex != 0)
            {
                setting.TimeZoneCode = ((AppCode.TimeZone) cbbTimeZones.SelectedItem).Code;
            }

            if (cbbWorkStartTime.SelectedIndex != 0 || cbbWorkStartTime.SelectedText != null)
            {
                setting.WorkdayStartTime = cbbWorkStartTime.SelectedItem.ToString();
                if (cbbWorkStartTime.SelectedIndex == 0)
                {
                    setting.WorkdayStartTime = cbbWorkStartTime.SelectedText;
                }
            }

            if (cbbWorkStopTime.SelectedIndex != 0 || cbbWorkStopTime.SelectedText != null)
            {
                setting.WorkdayStopTime = cbbWorkStopTime.SelectedItem.ToString();
                if (cbbWorkStopTime.SelectedIndex == 0)
                {
                    setting.WorkdayStopTime = cbbWorkStopTime.SelectedText;
                }
            }

            if (cbbHelpLanguage.SelectedIndex != 0)
            {
                setting.HelpLanguage = ((Language)cbbHelpLanguage.SelectedItem).Lcid;
            }

            if (cbbUiLanguage.SelectedIndex != 0)
            {
                setting.UiLanguage = ((Language)cbbUiLanguage.SelectedItem).Lcid;
            }

            if (cbbCurrencies.SelectedIndex != 0)
            {
                setting.Currency = ((Currency)cbbCurrencies.SelectedItem).CurrencyReference;
            }

            if (cbbStartupPane.SelectedIndex != 0)
            {
                setting.StartupPaneEnabled = cbbStartupPane.SelectedIndex == 2;
            }

            if (cbbUseCrmFormAppt.SelectedIndex != 0)
            {
                setting.UseCrmFormForAppointment = cbbUseCrmFormAppt.SelectedIndex == 2;
            }

            if (cbbUseCrmFormContact.SelectedIndex != 0)
            {
                setting.UseCrmFormForContact = cbbUseCrmFormContact.SelectedIndex == 2;
            }

            if (cbbUseCrmFormEmail.SelectedIndex != 0)
            {
                setting.UseCrmFormForEmail = cbbUseCrmFormEmail.SelectedIndex == 2;
            }

            if (cbbUseCrmFormTask.SelectedIndex != 0)
            {
                setting.UseCrmFormForTask = cbbUseCrmFormTask.SelectedIndex == 2;
            }

            #endregion

            WorkAsync("Initializing update...",
                (w, a) =>
                {
                    var settingArg = (UserSettings)a.Argument;
                    var ush = new UserSettingsHelper(Service);

                    foreach (var user in settingArg.UsersToUpdate)
                    {
                        w.ReportProgress(0, "Updating settings for user " + user.GetAttributeValue<string>("fullname"));
                        ush.UpdateSettings(user.Id, setting);
                    }
                },
                a =>
                {
                    if (a.Error != null)
                    {
                        MessageBox.Show(this, "An error occured: " + a.Error.Message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                },
                a =>
                {
                    SetWorkingMessage(a.UserState.ToString());
                },
                setting);
        }
    }
}
