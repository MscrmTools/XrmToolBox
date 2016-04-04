// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ListBoxDelegates
    {
        public class ComboboxDelegates
        {
            public static void AddItem(ListBox listbox, object item)
            {
                MethodInvoker miAddItem = delegate
                {
                    listbox.Items.Add(item);
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miAddItem);
                }
                else
                {
                    miAddItem();
                }
            }

            public static void ClearItems(ListBox listbox)
            {
                MethodInvoker miClearItems = delegate
                {
                    listbox.Items.Clear();
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miClearItems);
                }
                else
                {
                    miClearItems();
                }
            }

            public static object GetItem(ListBox listbox, int index)
            {
                object item = null;

                MethodInvoker miRemoveItem = delegate
                {
                    item = listbox.Items[index];
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miRemoveItem);
                }
                else
                {
                    miRemoveItem();
                }

                return item;
            }

            public static object GetSelectedItem(ListBox listbox)
            {
                object selectedItem = null;

                MethodInvoker miClearItems = delegate
                {
                    selectedItem = listbox.SelectedItem;
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miClearItems);
                }
                else
                {
                    miClearItems();
                }

                return selectedItem;
            }

            public static string GetText(ListBox listbox)
            {
                string text = string.Empty;

                MethodInvoker miGetText = delegate
                {
                    text = listbox.Text;
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miGetText);
                }
                else
                {
                    miGetText();
                }

                return text;
            }

            public static void InsertItem(ListBox listbox, int index, object item)
            {
                MethodInvoker miInsertItem = delegate
                {
                    listbox.Items.Insert(index, item);
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miInsertItem);
                }
                else
                {
                    miInsertItem();
                }
            }

            public static void RemoveItem(ListBox listbox, object item)
            {
                MethodInvoker miRemoveItem = delegate
                {
                    listbox.Items.Remove(item);
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miRemoveItem);
                }
                else
                {
                    miRemoveItem();
                }
            }

            public static void RemoveItemAt(ListBox listbox, int index)
            {
                MethodInvoker miRemoveItem = delegate
                {
                    listbox.Items.RemoveAt(index);
                };

                if (listbox.InvokeRequired)
                {
                    listbox.Invoke(miRemoveItem);
                }
                else
                {
                    miRemoveItem();
                }
            }

            public static void SetEnableState(ListBox listbox, bool enabled)
            {
                CommonDelegates.SetEnableState(listbox, enabled);
            }
        }
    }
}