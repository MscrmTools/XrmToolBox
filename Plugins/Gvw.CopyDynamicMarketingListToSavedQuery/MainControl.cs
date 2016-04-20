using Gvw.CopyDynamicMarketingListToSavedQuery.Helpers;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Gvw.CopyDynamicMarketingListToSavedQuery
{
    public partial class MainControl : PluginControlBase, IHelpPlugin
    {
        #region Variables

        private ListViewItem[] listViewItemsCache;

        private Guid selectedListId;
        private Entity selectedList;

        #endregion Variables

        public MainControl()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void retrieveListsButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(LoadLists);
        }
        
        private void listsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            selectedListId = (Guid)e.Item.Tag;
            ExecuteMethod(RetrieveList);
        }

        private void createViewButton_Click(object sender, EventArgs e)
        {
            ExecuteMethod(CreateView);
        }

        private void LoadLists()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Loading lists...",
                Work = (bw, e) =>
                {
                    e.Result = ListHelper.RetrieveLists(Service);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(ParentForm, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        listsListView.Items.Clear();
                        var list = new List<ListViewItem>();
                        foreach (Entity entity in (List<Entity>)e.Result)
                        {
                            var item = new ListViewItem { Text = entity.GetAttributeValue<string>("listname"), Tag = entity.Id };
                            item.SubItems.Add(CreatedFromCodeToString(entity.GetAttributeValue<OptionSetValue>("createdfromcode")).ToUpper());
                            list.Add(item);
                        }

                        listViewItemsCache = list.ToArray();
                        listsListView.Items.AddRange(listViewItemsCache);
                    }
                }
            });
        }

        private void RetrieveList()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Retrieving list...",
                Work = (bw, e) =>
                {
                    e.Result = ListHelper.RetrieveList(Service, selectedListId);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(ParentForm, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        selectedList = (Entity)e.Result;
                        
                        nameTextBox.Text = selectedList.GetAttributeValue<string>("listname");
                        descriptionTextBox.Text = selectedList.GetAttributeValue<string>("description");
                        entityTextBox.Text = CreatedFromCodeToString(selectedList.GetAttributeValue<OptionSetValue>("createdfromcode"));
                        cbViewType.SelectedIndex = 0;
                        createViewButton.Enabled = true;
                    }
                }
            });
        }

        private string CreatedFromCodeToString(OptionSetValue createdFromCode)
        {
            if (createdFromCode != null)
            {
                if (createdFromCode.Value == 4)
                    return "lead";
                if (createdFromCode.Value == 2)
                    return "contact";
                if (createdFromCode.Value == 1)
                    return "account";
            }

            throw new Exception("Something went wrong retrieving the list. Try again.");
        }

        private void CreateView()
        {
            var viewType = (ViewType)cbViewType.SelectedIndex;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Creating view...",
                Work = (bw, e) =>
                {
                    e.Result = ViewHelper.CreateView(Service, nameTextBox.Text, descriptionTextBox.Text, entityTextBox.Text, selectedList, viewType);
                },
                PostWorkCallBack = e =>
                {
                    if (e.Error != null)
                    {
                        MessageBox.Show(ParentForm, e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(ParentForm, (string)e.Result, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            });
        }

        #region IHelpPlugin

        public string HelpUrl => "https://msdn.microsoft.com/nl-nl/library/gg328457.aspx#BKMK_CreateViews";

        #endregion IHelpPlugin
    }
}
