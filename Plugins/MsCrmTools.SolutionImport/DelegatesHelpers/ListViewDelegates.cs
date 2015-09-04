// PROJECT : MsCrmTools.SolutionImport
// This project was developed by Tanguy Touzard
// CODEPLEX: http://xrmtoolbox.codeplex.com
// BLOG: http://mscrmtools.blogspot.com

using System.Drawing;
using System.Windows.Forms;

namespace Tanguy.WinForm.Utilities.DelegatesHelpers
{
    public class ListViewDelegates
    {
        public static void AddColumn(ListView listview, ColumnHeader column)
        {
            MethodInvoker miAddColumn = delegate
            {
                listview.Columns.Add(column);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miAddColumn);
            }
            else
            {
                miAddColumn();
            }
        }

        public static void AddImageToImageList(ListView listview, Bitmap image)
        {
            MethodInvoker miAddImageToImageList = delegate
            {
                if (listview.SmallImageList == null)
                {
                    listview.SmallImageList = new ImageList();
                }

                listview.SmallImageList.Images.Add(image);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miAddImageToImageList);
            }
            else
            {
                miAddImageToImageList();
            }
        }

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

        public static void ClearColumns(ListView listview)
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

        public static void ClearItems(ListView listview)
        {
            MethodInvoker miClearItems = delegate
            {
                listview.Columns.Clear();
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

        public static int[] GetCheckedIndexes(ListView listview)
        {
            int[] checkedIndexes = new int[0];

            MethodInvoker miClearItems = delegate
            {
                checkedIndexes = new int[listview.CheckedIndices.Count];

                for (int i = 0; i < checkedIndexes.Length; i++)
                {
                    checkedIndexes[i] = listview.CheckedIndices[i];
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

            return checkedIndexes;
        }

        public static ListViewItem[] GetCheckedItems(ListView listview)
        {
            ListViewItem[] checkedItems = new ListViewItem[0];

            MethodInvoker miClearItems = delegate
            {
                checkedItems = new ListViewItem[listview.CheckedItems.Count];
                listview.CheckedItems.CopyTo(checkedItems, 0);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return checkedItems;
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

        public static int[] GetSelectedIndexes(ListView listview)
        {
            int[] selectedIndexes = new int[0];

            MethodInvoker miClearItems = delegate
            {
                selectedIndexes = new int[listview.SelectedIndices.Count];
                listview.SelectedIndices.CopyTo(selectedIndexes, 0);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return selectedIndexes;
        }

        public static ListViewItem[] GetSelectedItems(ListView listview)
        {
            ListViewItem[] selectedItems = new ListViewItem[0];

            MethodInvoker miGetSelectedItems = delegate
            {
                selectedItems = new ListViewItem[listview.SelectedItems.Count];
                listview.SelectedItems.CopyTo(selectedItems, 0);
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miGetSelectedItems);
            }
            else
            {
                miGetSelectedItems();
            }

            return selectedItems;
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

        public static void SetItemCheckStatus(ListView listview, ListViewItem item, bool isChecked)
        {
            MethodInvoker miSetItemCheckStatus = delegate
            {
                item.Checked = isChecked;
            };

            if (listview.InvokeRequired)
            {
                listview.Invoke(miSetItemCheckStatus);
            }
            else
            {
                miSetItemCheckStatus();
            }
        }
    }
}