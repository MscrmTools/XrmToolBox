using OrderedPropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Controls;
using XrmToolBox.Properties;

namespace XrmToolBox.Forms
{
    public partial class SettingsForm : DockContent
    {
        public SettingsForm()
        {
            InitializeComponent();

            SetStyle(
               ControlStyles.AllPaintingInWmPaint |
               ControlStyles.UserPaint |
               ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.DoubleBuffer,
               true);

            PrepareTabsAndContent();
        }

        public event EventHandler OnProxySettingsChanged;

        private void advancedControl1_OnTabsOrderChanged(object sender, EventArgs e)
        {
            var ctrls = new Control[pnlNavLeft.Controls.Count];
            pnlNavLeft.Controls.CopyTo(ctrls, 0);
            pnlNavLeft.Controls.Clear();

            foreach (var tabName in Options.Instance.OrderForSettingsTab)
            {
                var ctrl = ctrls.First(c => c.Text == tabName);
                pnlNavLeft.Controls.Add(ctrl);
                pnlNavLeft.Controls.SetChildIndex(ctrl, 0);
            }

            pnlNavLeft.Controls.Add(ctrls.First(c => c.Text == "Advanced"));
            pnlNavLeft.Controls.SetChildIndex(ctrls.First(c => c.Text == "Advanced"), 0);
        }

        private void Ctrl_OnSettingsPropertyChanged(object sender, SettingsPropertyEventArgs e)
        {
            var t = typeof(Options);
            t.GetProperty(e.PropertyName).SetValue(Options.Instance, e.Value);

            Options.Instance.ReportSettingsChange(e);

            var sd = new SuccessDialog();

            sd.Location = new Point(ParentForm.Location.X + Location.X + pnlNavLeft.Width + 20, ParentForm.Location.Y + pnlNavMain.Location.Y + 90);
            sd.Show(this);

            var timer = new Timer();
            timer.Interval = 200;
            timer.Tick += (s, evt) =>
            {
                sd.Opacity -= 0.05;
                if (sd.Opacity == 0)
                {
                    sd.Close();
                    sd.Dispose();
                    timer.Stop();
                }
            };
            timer.Start();
        }

        private void Menu_OnSelectedChanged(object sender, EventArgs e)
        {
            var item = (NavLeftItem)sender;
            if (item.Selected)
            {
                foreach (var menu in pnlNavLeft.Controls.OfType<NavLeftItem>())
                {
                    if (menu != sender)
                    {
                        menu.Selected = false;
                    }
                }

                foreach (var p in pnlNavMain.Controls.OfType<Control>().Where(p => p.Tag.ToString() != item.Text))
                {
                    p.Visible = false;
                }

                var panel = pnlNavMain.Controls.OfType<Control>().First(p => p.Tag.ToString() == item.Text);
                panel.Dock = DockStyle.Fill;
                panel.Visible = true;
                panel.Invalidate();
            }
        }

        private void PrepareTabsAndContent()
        {
            string maxWidthText = "";

            var items = new List<NavLeftItem>();
            var type = typeof(Options);

            foreach (var pi in type.GetProperties())
            {
                var a = pi.GetCustomAttributes(typeof(CategoryAttribute), true).FirstOrDefault() as CategoryAttribute;
                if (a == null) continue;

                if (!Options.Instance.OrderForSettingsTab.Contains(a.Category))
                    Options.Instance.OrderForSettingsTab.Add(a.Category);
            }

            foreach (var category in new[] { "Proxy", "Paths", "Credits", "Application Protocol", "Assemblies", "Hidden Tools" })
                if (!Options.Instance.OrderForSettingsTab.Contains(category))
                    Options.Instance.OrderForSettingsTab.Add(category);

            int index = 0;
            foreach (var category in Options.Instance.OrderForSettingsTab)
            {
                if (items.All(i => i.Text != category))
                {
                    var parts = category.Split(' ');
                    var resourceName = "";
                    foreach (var part in parts)
                    {
                        resourceName += part[0].ToString().ToUpper() + part.Remove(0, 1);
                    }

                    var item = new NavLeftItem
                    {
                        Text = category,
                        Index = 4,
                        TabIndex = index,
                        Height = 40,
                        Image = (Bitmap)Resources.ResourceManager.GetObject(resourceName + "32"),
                        Dock = DockStyle.Top,
                        SelectedOnFocus = true,
                        UseCustomHighlightColor = true
                    };
                    item.OnSelectedChanged += Menu_OnSelectedChanged;
                    items.Add(item);

                    Panel p = new Panel { Tag = category, AutoScroll = true };
                    Label title = new Label
                    {
                        Text = category,
                        TextAlign = ContentAlignment.MiddleRight,
                        Dock = DockStyle.Top,
                        Height = 40,
                        Font = new Font(Font.FontFamily, 16)
                    };
                    p.Controls.Add(title);
                    pnlNavMain.Controls.Add(p);

                    index++;
                }

                if (maxWidthText.Length < category.Length)
                {
                    maxWidthText = category;
                }
            }

            var spitem = new NavLeftItem
            {
                Text = "Advanced",
                Index = 4,
                TabIndex = index,
                Height = 40,
                Image = (Bitmap)Resources.ResourceManager.GetObject("Settings32"),
                Dock = DockStyle.Top,
                SelectedOnFocus = true,
                UseCustomHighlightColor = true
            };
            spitem.OnSelectedChanged += Menu_OnSelectedChanged;

            items.Reverse();
            items.Insert(0, spitem);
            pnlNavLeft.Controls.AddRange(items.ToArray());

            var textSize = TextRenderer.MeasureText(maxWidthText, pnlNavLeft.Controls.OfType<NavLeftItem>().First().Font);
            pnlNavLeft.Width = textSize.Width + 70;
            pnlNavLeft.Controls.OfType<NavLeftItem>().Last().Selected = true;

            var listProperties = new List<SettingsPropertyInfo>();

            foreach (var pi in type.GetProperties())
            {
                var a = pi.GetCustomAttributes(typeof(CategoryAttribute), true).FirstOrDefault() as CategoryAttribute;
                if (a != null)
                {
                    var title = (pi.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault() as DisplayNameAttribute)?.DisplayName;
                    var desc = (pi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault() as DescriptionAttribute)?.Description;
                    var isReadonly = (pi.GetCustomAttributes(typeof(ReadOnlyAttribute), true).FirstOrDefault() as ReadOnlyAttribute)?.IsReadOnly ?? false;
                    var order = (pi.GetCustomAttributes(typeof(PropertyOrderAttribute), true).FirstOrDefault() as PropertyOrderAttribute)?.Order ?? 0;
                    var isMultiline = (pi.GetCustomAttributes(typeof(EditorAttribute), true).FirstOrDefault() as EditorAttribute)?.EditorTypeName == typeof(System.ComponentModel.Design.MultilineStringEditor).AssemblyQualifiedName;

                    listProperties.Add(new SettingsPropertyInfo
                    {
                        Category = a.Category,
                        Name = pi.Name,
                        Title = title,
                        Description = desc,
                        IsReadOnly = isReadonly,
                        Order = order,
                        Type = pi.PropertyType,
                        IsMultiline = isMultiline
                    });
                }
            }

            foreach (var pi in listProperties.OrderBy(p => p.Order))
            {
                var pnl = pnlNavMain.Controls.OfType<Panel>().FirstOrDefault(p => p.Tag?.ToString() == pi.Category);
                if (pnl != null)
                {
                    ISettingsControl ctrl = null;

                    if (pi.Type == typeof(bool))
                    {
                        ctrl = new SwitchSettingsControl
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            Checked = (bool)type.GetProperty(pi.Name).GetValue(Options.Instance, null),
                            Dock = DockStyle.Top
                        };
                    }
                    if (pi.Type == typeof(bool) || pi.Type == typeof(bool?))
                    {
                        ctrl = new SwitchSettingsControl
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            Checked = (bool?)type.GetProperty(pi.Name).GetValue(Options.Instance, null) ?? false,
                            Dock = DockStyle.Top
                        };
                    }
                    else if (pi.Type == typeof(string))
                    {
                        ctrl = new TextBoxSettingsControl<string>
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            Text = type.GetProperty(pi.Name).GetValue(Options.Instance, null)?.ToString(),
                            Dock = DockStyle.Top,
                            Readonly = pi.IsReadOnly,
                            Multiline = pi.IsMultiline
                        };

                        if (pi.IsMultiline)
                        {
                            ((Control)ctrl).Height = 200;
                        }
                    }
                    else if (pi.Type == typeof(int))
                    {
                        ctrl = new TextBoxSettingsControl<int>
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            ValidationRegEx = "^\\d+$",
                            Dock = DockStyle.Top,
                            Text = ((int)type.GetProperty(pi.Name).GetValue(Options.Instance, null)).ToString(CultureInfo.InvariantCulture),
                            Readonly = pi.IsReadOnly
                        };
                    }
                    else if (pi.Type == typeof(decimal))
                    {
                        ctrl = new TextBoxSettingsControl<decimal>
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            ValidationRegEx = "^\\d*\\.?\\d*$",
                            Dock = DockStyle.Top,
                            Text = ((decimal)type.GetProperty(pi.Name).GetValue(Options.Instance, null)).ToString(CultureInfo.InvariantCulture),
                            Readonly = pi.IsReadOnly
                        };
                    }
                    else if (pi.Type.BaseType == typeof(Enum))
                    {
                        ctrl = new DropdownSettingsControl
                        {
                            PropertyName = pi.Name,
                            Title = pi.Title,
                            Description = pi.Description,
                            EnumType = pi.Type,
                            Value = (Enum)type.GetProperty(pi.Name).GetValue(Options.Instance, null),
                            Dock = DockStyle.Top,
                        };
                    }

                    if (ctrl != null)
                    {
                        ctrl.OnSettingsPropertyChanged += Ctrl_OnSettingsPropertyChanged;
                        pnl.Controls.Add((Control)ctrl);
                        pnl.Controls.SetChildIndex((Control)ctrl, 0);
                    }
                }
            }
        }

        private void ProxyControl1_OnProxySettingsChanged(object sender, System.EventArgs e)
        {
            OnProxySettingsChanged?.Invoke(this, new EventArgs());
        }
    }
}