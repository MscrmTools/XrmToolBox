using McTools.Xrm.Connection;
using McTools.Xrm.Connection.AppCode;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using XrmToolBox.AppCode;
using XrmToolBox.Controls;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Args;
using XrmToolBox.Extensibility.Interfaces;

namespace XrmToolBox.New
{
    public partial class PluginForm : DockContent, IStatusBarMessenger
    {
        private readonly PluginControlBase pluginControlBase;
        private StatusBarMessageEventArgs lastStatusEventArgs;

        public PluginForm(UserControl control, string name, string pluginName)
        {
            InitializeComponent();

            Tag = control.Tag;
            Text = name;
            PluginTitle = pluginName;

            control.Dock = DockStyle.Fill;
            pnlMain.Controls.Add(control);
            pnlMain.Controls.SetChildIndex(control, 0);
            pluginControlBase = (PluginControlBase)control;
            pluginControlBase.OnCloseTool += PluginControlBase_OnCloseTool;
            Icon = pluginControlBase.PluginIcon;

            if (pluginControlBase is MultipleConnectionsPluginControlBase mcp)
            {
                mcp.AdditionalConnectionDetails.CollectionChanged += AdditionalConnectionDetails_CollectionChanged;
            }

            if (pluginControlBase is IStatusBarMessenger statusBarMessenger)
            {
                statusBarMessenger.SendMessageToStatusBar += StatusBarMessager_SendMessageToStatusBar;
            }

            if (pluginControlBase.ConnectionDetail != null)
            {
                pluginControlBase.ConnectionDetail.OnImpersonate += Detail_OnImpersonate;
                if (pluginControlBase.ConnectionDetail.ImpersonatedUserId != Guid.Empty)
                {
                    Detail_OnImpersonate(pluginControlBase.ConnectionDetail,
                        new ImpersonationEventArgs(pluginControlBase.ConnectionDetail.ImpersonatedUserId,
                            pluginControlBase.ConnectionDetail.ImpersonatedUserName));
                }
            }

            ThemeHelpers.ApplyThemeAndWatch(this);

            DisplayHighlight(pluginControlBase.ConnectionDetail);
        }

        public event EventHandler<StatusBarMessageEventArgs> SendMessageToStatusBar;

        public override sealed Color BackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        public IXrmToolBoxPluginControl Control => pluginControlBase;

        public string PluginName { get; internal set; }

        public string PluginTitle { get; }

        public override sealed string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public bool CloseWithReason(ToolBoxCloseReason reason, bool forceSilent = false)
        {
            PluginCloseInfo info = new PluginCloseInfo(reason);
            if (Options.Instance.CloseEachPluginSilently || forceSilent)
            {
                info.Silent = true;
            }

            pluginControlBase?.ClosingPlugin(info);
            if (info.Cancel) return false;

            FormClosing -= PluginForm_FormClosing;
            Close();
            return true;
        }

        public void SendIncomingBrokerMessage(MessageBusEventArgs message)
        {
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (pluginControlBase is IMessageBusHost host)
            {
                host.OnIncomingMessage(message);
            }
        }

        public void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            pluginControlBase.UpdateConnection(newService, detail, actionName, parameter);
            detail.OnImpersonate += Detail_OnImpersonate;

            if (detail.ImpersonatedUserId != Guid.Empty)
            {
                Detail_OnImpersonate(detail, new ImpersonationEventArgs(detail.ImpersonatedUserId, detail.ImpersonatedUserName));
            }

            if (actionName != "AdditionalOrganization")
                DisplayHighlight(detail);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Options.Instance.CycleTabsInRecentlyUsedOrder && MruTabSwitcher.HandleShortcut(DockPanel, keyData))
            {
                return true;
            }

            switch (keyData)
            {
                case Keys.Control | Keys.Tab:

                    if (TabIndex == DockPanel.Documents.OfType<DockContent>().Count() - 1)
                    {
                        DockPanel.Documents.OfType<DockContent>().FirstOrDefault(d => d.TabIndex == 0)?.Activate();
                        return true;
                    }

                    foreach (var document in DockPanel.Documents.OfType<DockContent>())
                    {
                        if (document.TabIndex == TabIndex + 1)
                        {
                            document.Activate();
                            return true;
                        }
                    }
                    break;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }

            return true;
        }

        private void AdditionalConnectionDetails_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var targets = ((MultipleConnectionsPluginControlBase)pluginControlBase).AdditionalConnectionDetails.ToList();

            if (targets.Count > 0)
            {
                DisplayHighlightForMultipleConnections(pluginControlBase.ConnectionDetail, targets);
            }
            else
            {
                DisplayHighlight(pluginControlBase.ConnectionDetail);
            }
        }

        private void btnResetImpersonate_Click(object sender, System.EventArgs e)
        {
            pluginControlBase.ConnectionDetail.RemoveImpersonation();
        }

        private void Detail_OnImpersonate(object sender, ImpersonationEventArgs e)
        {
            if (e.UserId == Guid.Empty)
            {
                pnlImpersonate.Visible = false;
            }
            else
            {
                lblImpersonation.Text = string.Format(lblImpersonation.Tag.ToString(), e.UserName, e.UserId);
                pnlImpersonate.Visible = true;
            }
        }

        private void DisplayHighlight(ConnectionDetail detail)
        {
            tlpHighlight.Visible = false;

            if ((detail?.IsEnvironmentHighlightSet ?? false) && !(pluginControlBase is INoHighlightingPlugin))
            {
                pnlHighlight.Controls.Clear();

                var ctrl = new HighlightItem(detail);
                ctrl.Dock = DockStyle.Fill;
                pnlHighlight.Controls.Add(ctrl);
                pnlHighlight.Visible = true;
                lblEnvInfo.Visible = false;
                Padding = new Padding(0, 0, 0, 0);
                return;

                BackColor = detail.EnvironmentHighlightingInfo?.Color ?? DefaultBackColor;
                lblEnvInfo.ForeColor = detail.EnvironmentHighlightingInfo?.TextColor ?? DefaultForeColor;
                lblEnvInfo.Text = detail.EnvironmentHighlightingInfo?.Text ?? "";
                lblEnvInfo.Visible = true;
                Padding = new Padding(10, 0, 10, 10);

                lblEnvInfo.Visible = false;

                lblEnvName.ForeColor = detail.EnvironmentHighlightingInfo?.TextColor ?? DefaultForeColor;
                lblEnvName.Text = ($"{detail.EnvironmentHighlightingInfo?.Text ?? ""}{(detail.EnvironmentHighlightingInfo != null ? " - " : "")}{detail.ConnectionName}");

                if (detail.ParentConnectionFile != null)
                {
                    byte[] bytes = Convert.FromBase64String(detail.ParentConnectionFile.Base64Image);

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        pbEnvLogo.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    pbEnvLogo.Visible = false;
                }
                pbEnvLogo.SizeMode = PictureBoxSizeMode.StretchImage;
                pbEnvLogo.Size = new Size(pnlHighlight.Height, pnlHighlight.Height);
                pnlHighlight.BackColor = detail.EnvironmentHighlightingInfo?.Color ?? DefaultBackColor;
                pnlHighlight.Visible = true;
                Padding = new Padding(0, 0, 0, 0);
            }
            else
            {
                Padding = new Padding(0, 0, 0, 0);
                lblEnvInfo.Visible = false;
                pnlHighlight.Visible = false;
            }

            Invalidate();
        }

        private void DisplayHighlightForMultipleConnections(ConnectionDetail detail, List<ConnectionDetail> targetDetails)
        {
            pnlHighlight.Controls.Clear();
            var list = new List<HighlightItem>();

            var ctrl = new HighlightItem(detail);
            ctrl.Dock = DockStyle.Left;
            pnlHighlight.BackColor = detail.EnvironmentHighlightingInfo?.Color ?? DefaultBackColor;

            foreach (var td in targetDetails)
            {
                var tCtrl = new HighlightItem(td);
                tCtrl.Dock = DockStyle.Right;
                list.Add(tCtrl);
            }

            var iCtrl = new HighlightItem(ctrl.BackColor, list.First().BackColor);
            iCtrl.Dock = DockStyle.Fill;

            pnlHighlight.Controls.Add(iCtrl);
            pnlHighlight.Controls.Add(ctrl);
            pnlHighlight.Controls.AddRange(list.ToArray());

            pnlHighlight.Visible = true;
            lblEnvInfo.Visible = false;
            Padding = new Padding(0, 0, 0, 0);

            return;

            lblEnvInfo.Visible = false;
            Padding = new Padding(0, 0, 0, 0);
            tlpHighlight.Visible = false;

            if (detail?.IsEnvironmentHighlightSet ?? false && !(pluginControlBase is INoHighlightingPlugin))
            {
                Padding = new Padding(10, 0, 10, 10);
                tlpHighlight.Visible = true;

                lblSourceConnection.Text = detail.EnvironmentHighlightingInfo.Text;
                lblSourceConnection.ForeColor = detail.EnvironmentHighlightingInfo.TextColor ?? Color.Black;
                lblSourceConnection.BackColor = detail.EnvironmentHighlightingInfo.Color ?? DefaultBackColor;
            }
            else
            {
                lblSourceConnection.Text = detail?.EnvironmentHighlightingInfo?.Text ?? detail?.ConnectionName ?? "(Unknown connection name)";
                lblSourceConnection.ForeColor = Color.Black;
                lblSourceConnection.BackColor = DefaultBackColor;
            }

            var backColor = DefaultBackColor;
            var color = DefaultForeColor;

            if (!(pluginControlBase is INoHighlightingPlugin))
            {
                foreach (var td in targetDetails)
                {
                    if (td?.IsEnvironmentHighlightSet ?? false)
                    {
                        Padding = new Padding(10, 0, 10, 10);
                        tlpHighlight.Visible = true;
                    }

                    if (td?.EnvironmentHighlightingInfo != null)
                    {
                        backColor = td.EnvironmentHighlightingInfo.Color ?? DefaultBackColor;
                        color = td.EnvironmentHighlightingInfo.TextColor ?? DefaultForeColor;
                    }
                }

                lblTargetConnections.Text = string.Join(", ", targetDetails.Select(c => c.EnvironmentHighlightingInfo?.Text ?? c.ConnectionName));
                lblTargetConnections.ForeColor = color;
                lblTargetConnections.BackColor = backColor;

                tlpHighlight.Visible = true;
            }

            BackColor = backColor;

            Invalidate();
        }

        private void PluginControlBase_OnCloseTool(object sender, System.EventArgs e)
        {
            Close();
        }

        private void PluginForm_DockStateChanged(object sender, System.EventArgs e)
        {
            if (lastStatusEventArgs != null)
            {
                if (DockState == DockState.Float || DockState == DockState.Hidden)
                {
                    SendMessageToStatusBar?.Invoke(Control, new StatusBarMessageEventArgs(null, null));
                }

                var timer = new Timer { Interval = 100 };
                timer.Tick += (s, evt) =>
                {
                    timer.Stop();
                    StatusBarMessager_SendMessageToStatusBar(Control, lastStatusEventArgs);
                };
                timer.Start();
            }
        }

        private void PluginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            pluginControlBase.Dispose();
        }

        private void PluginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.MdiFormClosing && Options.Instance.ClosePluginsSilentlyOnWindowsShutdown) return;

            e.Cancel = !CloseWithReason(ToolBoxCloseReason.CloseCurrent);
        }

        private void StatusBarMessager_SendMessageToStatusBar(object sender, StatusBarMessageEventArgs e)
        {
            void Mi()
            {
                lastStatusEventArgs = e;

                if (DockState != DockState.Float)
                {
                    statusStrip1.Visible = false;
                    SendMessageToStatusBar?.Invoke(sender, e);
                    return;
                }

                SendMessageToStatusBar?.Invoke(sender, new StatusBarMessageEventArgs(null, null));

                statusStrip1.Visible = true;

                if (string.IsNullOrEmpty(e.Message) && e.Progress == null)
                {
                    statusStrip1.Visible = false;
                }
                else if (!string.IsNullOrEmpty(e.Message) && e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress.Value;
                    toolStripStatusLabel.Text = e.Message;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = true;
                    statusStrip1.Visible = true;
                }
                else if (!string.IsNullOrEmpty(e.Message))
                {
                    toolStripStatusLabel.Text = e.Message;
                    toolStripStatusLabel.Visible = true;
                    toolStripProgressBar.Visible = false;
                    statusStrip1.Visible = true;
                }
                else if (e.Progress != null)
                {
                    toolStripProgressBar.Value = e.Progress.Value;
                    toolStripProgressBar.Visible = true;
                    toolStripStatusLabel.Visible = false;
                    statusStrip1.Visible = true;
                }
            }

            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.Invoke((MethodInvoker)Mi);
            }
            else
            {
                Mi();
            }
        }
    }

    internal static class MruTabSwitcher
    {
        private static readonly ConditionalWeakTable<DockPanel, SwitcherState> States =
            new ConditionalWeakTable<DockPanel, SwitcherState>();

        public static void Track(DockPanel panel)
        {
            if (panel != null)
            {
                States.GetValue(panel, dockPanel => new SwitcherState(dockPanel));
            }
        }

        public static bool HandleShortcut(DockPanel panel, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Tab) || keyData == (Keys.Control | Keys.Shift | Keys.Tab))
            {
                return Cycle(panel, (keyData & Keys.Shift) != 0);
            }

            if (keyData == Keys.Escape && IsCycling(panel))
            {
                Cancel(panel);
                return true;
            }

            return false;
        }

        public static bool Cycle(DockPanel panel, bool backwards)
        {
            if (panel == null)
            {
                return false;
            }

            var state = States.GetValue(panel, dockPanel => new SwitcherState(dockPanel));
            state.RefreshDocuments();
            if (state.Documents.Count < 2)
            {
                return false;
            }

            if (!state.Cycling)
            {
                state.RememberActiveFocus();
                state.Cycling = true;
                state.Index = state.Documents.IndexOf(panel.ActiveDocument as DockContent);
                if (state.Index < 0)
                {
                    state.Index = 0;
                }
            }

            state.Index = (state.Index + (backwards ? -1 : 1) + state.Documents.Count) % state.Documents.Count;
            state.ShowSwitcher();
            return true;
        }

        public static bool IsCycling(DockPanel panel)
        {
            return panel != null && States.TryGetValue(panel, out var state) && state.Cycling;
        }

        public static void Complete(DockPanel panel)
        {
            if (panel == null || !States.TryGetValue(panel, out var state) || !state.Cycling)
            {
                return;
            }

            var selected = state.Index >= 0 && state.Index < state.Documents.Count
                ? state.Documents[state.Index] : null;
            state.Cycling = false;
            state.HideSwitcher();
            if (selected != null && !selected.IsDisposed)
            {
                var focusTarget = state.GetRememberedFocus(selected);
                selected.Activate();
                state.RestoreFocus(selected, focusTarget);
            }
        }

        public static void Cancel(DockPanel panel)
        {
            if (panel == null || !States.TryGetValue(panel, out var state))
            {
                return;
            }

            state.Cycling = false;
            state.HideSwitcher();
        }

        private sealed class SwitcherState
        {
            [DllImport("user32.dll")]
            private static extern IntPtr GetFocus();

            private readonly DockPanel panel;
            private readonly Dictionary<DockContent, Control> rememberedFocus = new Dictionary<DockContent, Control>();
            private readonly Dictionary<DockContent, HashSet<Control>> watchedControls = new Dictionary<DockContent, HashSet<Control>>();
            private readonly Dictionary<Control, DockContent> controlOwners = new Dictionary<Control, DockContent>();
            private MruTabSwitcherWindow window;
            private DockContent lastActiveDocument;

            public SwitcherState(DockPanel panel)
            {
                this.panel = panel;
                panel.ActiveContentChanged += Panel_ActiveContentChanged;
                RefreshDocuments();
                lastActiveDocument = panel.ActiveDocument as DockContent;
                WatchDocument(lastActiveDocument);
                RememberFocusedDescendant(lastActiveDocument);
                Record(lastActiveDocument);
            }

            public List<DockContent> Documents { get; } = new List<DockContent>();
            public bool Cycling { get; set; }
            public int Index { get; set; }

            private void Panel_ActiveContentChanged(object sender, System.EventArgs e)
            {
                RememberFocusedDescendant(lastActiveDocument);
                lastActiveDocument = panel.ActiveDocument as DockContent;
                WatchDocument(lastActiveDocument);
                RememberFocusedDescendant(lastActiveDocument);
                if (!Cycling)
                {
                    Record(lastActiveDocument);
                }
            }

            private void Record(DockContent content)
            {
                if (content == null || content.IsDisposed || content.DockState != DockState.Document)
                {
                    return;
                }

                Documents.Remove(content);
                Documents.Insert(0, content);
            }

            public void RefreshDocuments()
            {
                var open = panel.Documents.OfType<DockContent>()
                    .Where(document => !document.IsDisposed && document.DockState == DockState.Document)
                    .ToList();
                var closed = Documents.Where(document => !open.Contains(document)).ToList();
                Documents.RemoveAll(document => !open.Contains(document));
                foreach (var document in closed)
                {
                    UnwatchDocument(document);
                }

                foreach (var document in open)
                {
                    if (!Documents.Contains(document))
                    {
                        Documents.Add(document);
                    }

                    WatchDocument(document);
                }
            }

            public void RememberActiveFocus()
            {
                RememberFocusedDescendant(panel.ActiveDocument as DockContent);
            }

            private void WatchDocument(DockContent document)
            {
                if (document == null || document.IsDisposed || watchedControls.ContainsKey(document))
                {
                    return;
                }

                watchedControls[document] = new HashSet<Control>();
                WatchControlTree(document, document);
            }

            private void WatchControlTree(Control control, DockContent document)
            {
                if (!watchedControls[document].Add(control))
                {
                    return;
                }

                controlOwners[control] = document;
                control.GotFocus += Control_GotFocus;
                control.ControlAdded += Control_ControlAdded;
                foreach (Control child in control.Controls)
                {
                    WatchControlTree(child, document);
                }
            }

            private void Control_ControlAdded(object sender, ControlEventArgs e)
            {
                if (sender is Control parent && controlOwners.TryGetValue(parent, out var document) &&
                    !document.IsDisposed)
                {
                    WatchControlTree(e.Control, document);
                }
            }

            private void Control_GotFocus(object sender, System.EventArgs e)
            {
                if (sender is Control control && controlOwners.TryGetValue(control, out var document) &&
                    !document.IsDisposed)
                {
                    rememberedFocus[document] = control;
                }
            }

            private void RememberFocusedDescendant(DockContent document)
            {
                if (document == null || document.IsDisposed)
                {
                    return;
                }

                var focusedWindow = Control.FromChildHandle(GetFocus());
                var focused = IsWithinDocument(document, focusedWindow)
                    ? focusedWindow : FindFocusedControl(document);
                if (focused != null)
                {
                    rememberedFocus[document] = focused;
                }
            }

            private static bool IsWithinDocument(DockContent document, Control control)
            {
                for (var current = control; current != null; current = current.Parent)
                {
                    if (ReferenceEquals(current, document))
                    {
                        return true;
                    }
                }

                return false;
            }

            private static Control FindFocusedControl(Control parent)
            {
                if (parent.Focused)
                {
                    return parent;
                }

                foreach (Control child in parent.Controls)
                {
                    var focused = FindFocusedControl(child);
                    if (focused != null)
                    {
                        return focused;
                    }
                }

                return null;
            }

            public Control GetRememberedFocus(DockContent document)
            {
                if (rememberedFocus.TryGetValue(document, out var control) && !control.IsDisposed)
                {
                    return control;
                }

                return null;
            }

            public void RestoreFocus(DockContent document, Control control)
            {
                if (control == null || control.IsDisposed)
                {
                    return;
                }

                try
                {
                    document.BeginInvoke((MethodInvoker)(() =>
                    {
                        if (!document.IsDisposed && !control.IsDisposed && control.Visible &&
                            control.Enabled && control.CanFocus)
                        {
                            control.Focus();
                        }
                    }));
                }
                catch (InvalidOperationException)
                {
                    // The content may close before the deferred focus restore runs.
                }
            }

            private void UnwatchDocument(DockContent document)
            {
                if (!watchedControls.TryGetValue(document, out var controls))
                {
                    return;
                }

                foreach (var control in controls)
                {
                    control.GotFocus -= Control_GotFocus;
                    control.ControlAdded -= Control_ControlAdded;
                    controlOwners.Remove(control);
                }

                watchedControls.Remove(document);
                rememberedFocus.Remove(document);
            }

            public void ShowSwitcher()
            {
                if (window == null || window.IsDisposed)
                {
                    window = new MruTabSwitcherWindow();
                }

                window.UpdateDocuments(Documents, Index);
                if (!window.Visible)
                {
                    window.Show(panel.FindForm());
                }

                var owner = panel.FindForm();
                if (owner != null)
                {
                    window.Location = new Point(owner.Left + (owner.Width - window.Width) / 2,
                        owner.Top + (owner.Height - window.Height) / 2);
                }
            }

            public void HideSwitcher()
            {
                if (window != null && !window.IsDisposed)
                {
                    window.Hide();
                }
            }
        }

        private sealed class MruTabSwitcherWindow : Form
        {
            private readonly ListBox list;

            public MruTabSwitcherWindow()
            {
                FormBorderStyle = FormBorderStyle.None;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                Size = new Size(420, 260);
                Padding = new Padding(1);
                BackColor = CustomTheme.Instance.IsActive
                    ? CustomTheme.Instance.Background3 : SystemColors.ControlDark;

                list = new ListBox
                {
                    BorderStyle = BorderStyle.None,
                    Dock = DockStyle.Fill,
                    DrawMode = DrawMode.OwnerDrawFixed,
                    ItemHeight = 28,
                    IntegralHeight = false,
                    Font = new Font("Segoe UI", 10f)
                };
                list.DrawItem += DrawItem;
                Controls.Add(list);
                if (CustomTheme.Instance.IsActive)
                {
                    list.BackColor = CustomTheme.Instance.Background1;
                    list.ForeColor = CustomTheme.Instance.ForeColor1;
                }
            }

            protected override bool ShowWithoutActivation => true;

            protected override CreateParams CreateParams
            {
                get
                {
                    var parameters = base.CreateParams;
                    parameters.ExStyle |= 0x08000000; // WS_EX_NOACTIVATE
                    return parameters;
                }
            }

            public void UpdateDocuments(IList<DockContent> documents, int selectedIndex)
            {
                list.BeginUpdate();
                list.Items.Clear();
                foreach (var document in documents)
                {
                    list.Items.Add(document);
                }
                list.EndUpdate();
                if (selectedIndex >= 0 && selectedIndex < list.Items.Count)
                {
                    list.SelectedIndex = selectedIndex;
                }

                Height = Math.Min(260, list.ItemHeight * list.Items.Count + 8);
                Invalidate();
            }

            private void DrawItem(object sender, DrawItemEventArgs e)
            {
                if (e.Index < 0 || !(list.Items[e.Index] is DockContent content))
                {
                    return;
                }

                var theme = CustomTheme.Instance;
                var selected = (e.State & DrawItemState.Selected) != 0;
                var background = theme.IsActive
                    ? selected ? theme.HighlightColor : theme.Background1
                    : selected ? SystemColors.Highlight : SystemColors.Window;
                var foreground = theme.IsActive
                    ? selected ? theme.ForeColor5 : theme.ForeColor1
                    : selected ? SystemColors.HighlightText : SystemColors.WindowText;
                using (var brush = new SolidBrush(background))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }
                var title = string.IsNullOrWhiteSpace(content.TabText) ? content.Text : content.TabText;
                TextRenderer.DrawText(e.Graphics, title, list.Font,
                    Rectangle.Inflate(e.Bounds, -10, 0), foreground,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            }
        }
    }
}
