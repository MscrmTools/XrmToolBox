// PROJECT : MsCrmTools.ViewLayoutReplicator
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ComboboxDelegates
    {
        public static void AddItem(ComboBox combobox, object item)
        {
            MethodInvoker miAddItem = delegate
            {
                combobox.Items.Add(item);
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miAddItem);
            }
            else
            {
                miAddItem();
            }
        }

        public static void ClearItems(ComboBox combobox)
        {
            MethodInvoker miClearItems = delegate
            {
                combobox.Items.Clear();
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }
        }

        public static object GetItem(ComboBox combobox, int index)
        {
            object item = null;

            MethodInvoker miRemoveItem = delegate
            {
                item = combobox.Items[index];
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }

            return item;
        }

        public static object GetSelectedItem(ComboBox combobox)
        {
            object selectedItem = null;

            MethodInvoker miClearItems = delegate
            {
                selectedItem = combobox.SelectedItem;
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return selectedItem;
        }

        public static string GetText(ComboBox combobox)
        {
            string text = string.Empty;

            MethodInvoker miGetText = delegate
            {
                text = combobox.Text;
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miGetText);
            }
            else
            {
                miGetText();
            }

            return text;
        }

        public static void InsertItem(ComboBox combobox, int index, object item)
        {
            MethodInvoker miInsertItem = delegate
            {
                combobox.Items.Insert(index, item);
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miInsertItem);
            }
            else
            {
                miInsertItem();
            }
        }

        public static void RemoveItem(ComboBox combobox, object item)
        {
            MethodInvoker miRemoveItem = delegate
            {
                combobox.Items.Remove(item);
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }
        }

        public static void RemoveItemAt(ComboBox combobox, int index)
        {
            MethodInvoker miRemoveItem = delegate
            {
                combobox.Items.RemoveAt(index);
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miRemoveItem);
            }
            else
            {
                miRemoveItem();
            }
        }

        public static void SetEnableState(ComboBox combobox, bool enabled)
        {
            CommonDelegates.SetEnableState(combobox, enabled);
        }
    }
}