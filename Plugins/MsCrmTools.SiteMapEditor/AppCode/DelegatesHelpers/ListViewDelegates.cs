// PROJECT : MsCrmTools.SiteMapEditor
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Collections.Generic;
using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ListViewDelegates
    {
        public static void AddItem(ListView listview, ListViewItem item)
        {
            MethodInvoker miAddItem = delegate
            {
                listview.Items.Add(item);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miAddItem);
            }
            else
            {
                miAddItem();
            }
        }

        public static void ClearItems(ListView listview)
        {
            MethodInvoker miClearItems = delegate
            {
                listview.Items.Clear();
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }
        }

        public static List<ListViewItem> GetCheckedIndexes(ListView listview)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            MethodInvoker miClearItems = delegate
            {
                foreach (ListViewItem item in listview.CheckedIndices)
                {
                    items.Add((ListViewItem)item.Clone());
                }
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return items;
        }

        public static List<ListViewItem> GetCheckedItems(ListView listview)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            MethodInvoker miClearItems = delegate
            {
                foreach (ListViewItem item in listview.CheckedItems)
                {
                    items.Add((ListViewItem)item.Clone());
                }
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return items;
        }

        public static ListViewItem GetItem(ListView listview, int index)
        {
            ListViewItem item = null;

            MethodInvoker miRemoveItem = delegate
            {
                item = listview.Items[index];
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }

            return item;
        }

        public static ListViewItem GetItem(ListView listview, string key)
        {
            ListViewItem item = null;

            MethodInvoker miRemoveItem = delegate
            {
                item = listview.Items[key];
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }

            return item;
        }

        public static List<ListViewItem> GetItems(ListView listview)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            MethodInvoker miClearItems = delegate
            {
                foreach (ListViewItem item in listview.Items)
                {
                    items.Add((ListViewItem)item.Clone());
                }
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return items;
        }

        public static List<ListViewItem> GetSelectedIndexes(ListView listview)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            MethodInvoker miClearItems = delegate
            {
                foreach (ListViewItem item in listview.SelectedIndices)
                {
                    items.Add((ListViewItem)item.Clone());
                }
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return items;
        }

        public static List<ListViewItem> GetSelectedItems(ListView listview)
        {
            List<ListViewItem> items = new List<ListViewItem>();

            MethodInvoker miClearItems = delegate
            {
                foreach (ListViewItem item in listview.SelectedItems)
                {
                    items.Add((ListViewItem)item.Clone());
                }
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return items;
        }

        public static void InsertItem(ListView listview, int index, ListViewItem item)
        {
            MethodInvoker miInsertItem = delegate
            {
                listview.Items.Insert(index, item);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miInsertItem);
            }
            else
            {
                miInsertItem();
            }
        }

        public static void RemoveItem(ListView listview, ListViewItem item)
        {
            MethodInvoker miRemoveItem = delegate
            {
                listview.Items.Remove(item);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }
        }

        public static void RemoveItemAt(ListView listview, int index)
        {
            MethodInvoker miRemoveItem = delegate
            {
                listview.Items.RemoveAt(index);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }
        }

        public static void SetEnableState(ListView listview, bool enabled)
        {
            CommonDelegates.SetEnableState(listview, enabled);
        }

        public static void Sort(ListView listview)
        {
            MethodInvoker miSort = delegate
            {
                listview.Sort();
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miSort);
            }
            else
            {
                miSort();
            }
        }
    }
}