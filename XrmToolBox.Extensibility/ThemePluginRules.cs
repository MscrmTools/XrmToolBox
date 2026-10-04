using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    /// <summary>
    /// Applies specialized styling to controls hosted by known plugins. Plugin company metadata
    /// selects a set of reusable rules that map common component types to their theme handlers.
    /// </summary>
    internal static class ThemePluginRules
    {
        private const int MaxPluginParentTraversalDepth = 1000;

        // Company names identify the hosted plugin without relying on its concrete control types.
        private const string FetchXmlBuilderCompany = "Jonas Rapp, Sweden";
        private const string PluginRegistrationCompany = "Microsoft Corporation";
        private const string Sql4CdsCompany = "MarkMpn.Sql4Cds.XTB";

        private static readonly IDictionary<string, ControlThemeRule[]> RulesByPluginCompany =
            new Dictionary<string, ControlThemeRule[]>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    Sql4CdsCompany,
                    new[]
                    {
                        For<Scintilla>(ApplySqlEditorTheme),
                        For<StatusStrip>(ApplySqlStatusStripTheme),
                    }
                },
                {
                    FetchXmlBuilderCompany,
                    new[]
                    {
                        For<Scintilla>(ApplyXmlEditorTheme),
                    }
                },
                {
                    PluginRegistrationCompany,
                    new[]
                    {
                        For<PropertyGrid>(ApplyPropertyGridTheme),
                        For<DataGridView>(ApplyRegistrationGridTheme),
                    }
                }
            };

        private static readonly ConditionalWeakTable<StatusStrip, SqlStatusAccent> SqlStatusAccents =
            new ConditionalWeakTable<StatusStrip, SqlStatusAccent>();

        public static void Apply(Control control, CustomTheme theme, Color originalBackColor)
        {
            // All hosted grids need readable cells; plugin rules only add exceptions.
            if (control is DataGridView grid)
            {
                ApplyDataGridViewTheme(grid, theme);
            }

            // Resolve identity from the plugin root; child framework controls
            // often report Microsoft or another component vendor as their company.
            var pluginRoot = FindPluginRoot(control);
            var pluginCompany = pluginRoot?.CompanyName;
            if (string.IsNullOrWhiteSpace(pluginCompany) ||
                !RulesByPluginCompany.TryGetValue(pluginCompany, out var rules))
            {
                return;
            }

            if (pluginCompany.Equals(Sql4CdsCompany, StringComparison.OrdinalIgnoreCase) &&
                control is StatusStrip statusStrip &&
                !SqlStatusAccents.TryGetValue(statusStrip, out _))
            {
                SqlStatusAccents.Add(statusStrip, new SqlStatusAccent { Color = originalBackColor });
            }

            foreach (var rule in rules)
            {
                rule.Apply(control, theme);
            }
        }

        private static void ApplyDataGridViewTheme(DataGridView grid, CustomTheme theme)
        {
            var alternatingRowColor = Blend(theme.Background1, theme.Background2, 35);

            grid.BackgroundColor = theme.Background1;
            grid.GridColor = theme.Background3;
            grid.DefaultCellStyle.ForeColor = theme.ForeColor2;
            grid.DefaultCellStyle.BackColor = theme.Background1;
            grid.DefaultCellStyle.SelectionBackColor = theme.HighlightColor;
            grid.DefaultCellStyle.SelectionForeColor = theme.ForeColor5;
            grid.ColumnHeadersDefaultCellStyle.BackColor = theme.Background2;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = theme.ForeColor4;
            grid.RowHeadersDefaultCellStyle.BackColor = theme.Background2;
            grid.RowHeadersDefaultCellStyle.ForeColor = theme.ForeColor4;
            grid.RowsDefaultCellStyle.BackColor = theme.Background1;
            grid.AlternatingRowsDefaultCellStyle.BackColor = alternatingRowColor;
            grid.EnableHeadersVisualStyles = false;

            // Reapplying a theme must not accumulate formatting handlers.
            grid.CellFormatting -= DataGridViewCellFormatting;
            grid.CellFormatting += DataGridViewCellFormatting;
        }

        private static void ApplyRegistrationGridTheme(DataGridView grid, CustomTheme theme)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private static void ApplySqlStatusStripTheme(StatusStrip strip, CustomTheme theme)
        {
            // SQL 4 CDS restores its environment color when the connection changes.
            strip.BackColorChanged -= SqlStatusStripColorChanged;
            strip.BackColorChanged += SqlStatusStripColorChanged;
            strip.ForeColorChanged -= SqlStatusStripColorChanged;
            strip.ForeColorChanged += SqlStatusStripColorChanged;
            strip.Paint -= PaintSqlStatusAccent;
            strip.Paint += PaintSqlStatusAccent;
            strip.BackColor = theme.Background2;
            strip.ForeColor = theme.ForeColor2;
        }

        private static void SqlStatusStripColorChanged(object sender, EventArgs e)
        {
            var theme = CustomTheme.Instance;
            if (!theme.IsActive || !(sender is StatusStrip strip))
            {
                return;
            }

            if (strip.BackColor != theme.Background2)
            {
                if (SqlStatusAccents.TryGetValue(strip, out var accent))
                {
                    accent.Color = strip.BackColor;
                }
                strip.BackColor = theme.Background2;
                strip.Invalidate();
            }

            if (strip.ForeColor != theme.ForeColor2)
            {
                strip.ForeColor = theme.ForeColor2;
            }
        }

        private static void PaintSqlStatusAccent(object sender, PaintEventArgs e)
        {
            if (!(sender is StatusStrip strip) ||
                !SqlStatusAccents.TryGetValue(strip, out var accent))
            {
                return;
            }

            using (var brush = new SolidBrush(accent.Color))
            {
                e.Graphics.FillRectangle(brush, 0, strip.Height - 2, strip.Width, 2);
            }
        }

        private sealed class SqlStatusAccent
        {
            public Color Color;
        }

        private static void ApplyPropertyGridTheme(PropertyGrid grid, CustomTheme theme)
        {
            grid.BackColor = theme.Background1;
            grid.ForeColor = theme.ForeColor2;
            grid.ViewBackColor = theme.Background1;
            grid.ViewForeColor = theme.ForeColor2;
            grid.LineColor = theme.Background2;
            grid.CategoryForeColor = theme.ForeColor1;
            grid.HelpBackColor = theme.Background1;
            grid.HelpForeColor = theme.ForeColor2;
            grid.SelectedItemWithFocusBackColor = theme.Background3;
            grid.SelectedItemWithFocusForeColor = theme.ForeColor5;
        }

        private static void ApplySqlEditorTheme(Scintilla editor, CustomTheme theme)
        {
            editor.StyleResetDefault();
            editor.CaretForeColor = theme.ForeColor5;
            editor.CaretLineBackColor = theme.Background1;

            SetStyle(editor, Style.Default, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.LineNumber, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Sql.Default, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Sql.Comment, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.CommentLine, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.CommentDoc, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.CommentLineDoc, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.CommentDocKeyword, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.CommentDocKeywordError, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.Number, theme.NumberColor, theme.Background1);
            // SQL 4 CDS puts SQL keywords, built-in functions, and join/boolean
            // operators in Scintilla's Word, Word2, and User1 lists respectively.
            SetStyle(editor, Style.Sql.Word, theme.KeywordColor, theme.Background1);
            SetStyle(editor, Style.Sql.Word2, theme.KeywordColor, theme.Background1);
            SetStyle(editor, Style.Sql.User1, theme.KeywordColor, theme.Background1);
            SetStyle(editor, Style.Sql.Identifier, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Sql.QuotedIdentifier, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Sql.User2, theme.OperatorColor, theme.Background1);
            SetStyle(editor, Style.Sql.User3, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Sql.User4, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Sql.String, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Sql.Character, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Sql.Operator, theme.OperatorColor, theme.Background1);
            SetStyle(editor, Style.Sql.QOperator, theme.OperatorColor, theme.Background1);
            SetStyle(editor, Style.Sql.SqlPlus, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Sql.SqlPlusPrompt, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Sql.SqlPlusComment, theme.CommentColor, theme.Background1);

            editor.SetSelectionBackColor(true, theme.Background2);
            ApplySqlAutocompleteTheme(editor, theme);
        }

        private static void ApplySqlAutocompleteTheme(Scintilla editor, CustomTheme theme)
        {
            // SQL 4 CDS uses its own component for suggestions, separate from Scintilla.
            // Its menu is stored on the query control rather than in the control tree.
            var depth = 0;
            for (var current = editor.Parent;
                current != null && depth < MaxPluginParentTraversalDepth;
                current = current.Parent, depth++)
            {
                if (current.GetType().FullName != "MarkMpn.Sql4Cds.XTB.SqlQueryControl")
                {
                    continue;
                }

                var tooltip = current.GetType()
                    .GetField("_tooltip", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(current) as ToolTip;
                ApplySqlTooltipTheme(tooltip, theme);

                var menu = current.GetType()
                    .GetField("_autocomplete", BindingFlags.Instance | BindingFlags.NonPublic)
                    ?.GetValue(current);
                if (menu == null)
                {
                    return;
                }

                var colors = menu.GetType().GetProperty("Colors")?.GetValue(menu);
                if (colors == null)
                {
                    return;
                }

                SetColor(colors, "ForeColor", theme.ForeColor2);
                SetColor(colors, "BackColor", theme.Background1);
                SetColor(colors, "SelectedForeColor", theme.ForeColor5);
                SetColor(colors, "SelectedBackColor", theme.HighlightColor);
                SetColor(colors, "SelectedBackColor2", theme.Background3);
                SetColor(colors, "HighlightingColor", theme.HighlightColor);

                var listView = menu.GetType().GetProperty("ListView")?.GetValue(menu) as Control;
                if (listView != null)
                {
                    listView.BackColor = theme.Background1;
                    listView.ForeColor = theme.ForeColor2;
                    listView.Invalidate();

                    // Item descriptions are displayed by a separate WinForms ToolTip.
                    var itemTooltip = listView.GetType()
                        .GetField("toolTip", BindingFlags.Instance | BindingFlags.NonPublic)
                        ?.GetValue(listView) as ToolTip;
                    ApplySqlTooltipTheme(itemTooltip, theme);
                }

                return;
            }
        }

        private static void SetColor(object target, string propertyName, Color color)
        {
            target.GetType().GetProperty(propertyName)?.SetValue(target, color);
        }

        private static void ApplySqlTooltipTheme(ToolTip tooltip, CustomTheme theme)
        {
            if (tooltip == null)
            {
                return;
            }

            tooltip.BackColor = theme.Background2;
            tooltip.ForeColor = theme.ForeColor2;
            tooltip.OwnerDraw = true;
            tooltip.Draw -= DrawSqlTooltip;
            tooltip.Draw += DrawSqlTooltip;
        }

        private static void DrawSqlTooltip(object sender, DrawToolTipEventArgs e)
        {
            var tooltip = sender as ToolTip;
            var backColor = tooltip?.BackColor ?? SystemColors.Info;
            var foreColor = tooltip?.ForeColor ?? SystemColors.InfoText;
            using (var background = new SolidBrush(backColor))
            using (var border = new Pen(ControlPaint.Light(backColor)))
            {
                e.Graphics.FillRectangle(background, e.Bounds);
                e.Graphics.DrawRectangle(border, e.Bounds.Left, e.Bounds.Top,
                    e.Bounds.Width - 1, e.Bounds.Height - 1);
            }

            var textBounds = Rectangle.Inflate(e.Bounds, -4, -3);
            if (!string.IsNullOrEmpty(tooltip?.ToolTipTitle))
            {
                using (var titleFont = new Font(e.Font, FontStyle.Bold))
                {
                    var titleHeight = TextRenderer.MeasureText(tooltip.ToolTipTitle, titleFont).Height;
                    var titleBounds = new Rectangle(textBounds.Left, textBounds.Top,
                        textBounds.Width, titleHeight);
                    TextRenderer.DrawText(e.Graphics, tooltip.ToolTipTitle, titleFont,
                        titleBounds, foreColor, TextFormatFlags.NoPrefix);
                    textBounds.Y += titleHeight;
                    textBounds.Height -= titleHeight;
                }
            }

            TextRenderer.DrawText(e.Graphics, e.ToolTipText, e.Font,
                textBounds, foreColor, TextFormatFlags.NoPrefix | TextFormatFlags.WordBreak);
        }

        private static void ApplyXmlEditorTheme(Scintilla editor, CustomTheme theme)
        {
            editor.StyleResetDefault();
            editor.BackColor = theme.Background1;
            editor.CaretForeColor = theme.ForeColor5;
            editor.CaretLineBackColor = theme.Background1;

            SetStyle(editor, Style.Default, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.LineNumber, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Xml.Attribute, theme.AttributeColor, theme.Background1);
            SetStyle(editor, Style.Xml.AttributeUnknown, theme.AttributeColor, theme.Background1);
            SetStyle(editor, Style.Xml.CData, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Xml.Comment, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Xml.Default, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Xml.DoubleString, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Xml.Entity, theme.ForeColor2, theme.Background1);
            SetStyle(editor, Style.Xml.Number, theme.NumberColor, theme.Background1);
            SetStyle(editor, Style.Xml.Other, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Xml.SingleString, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Xml.Tag, theme.TagColor, theme.Background1);
            SetStyle(editor, Style.Xml.TagEnd, theme.TagColor, theme.Background1);
            SetStyle(editor, Style.Xml.TagUnknown, theme.TagColor, theme.Background1);

            editor.SetSelectionBackColor(true, theme.Background2);
            editor.Margins[2].BackColor = theme.Background1;
            editor.SetFoldMarginColor(true, theme.Background1);
            editor.SetFoldMarginHighlightColor(true, theme.Background1);
        }

        private static Color Blend(Color first, Color second, int secondWeightPercent)
        {
            var firstWeight = 100 - secondWeightPercent;
            return Color.FromArgb(
                (first.R * firstWeight + second.R * secondWeightPercent) / 100,
                (first.G * firstWeight + second.G * secondWeightPercent) / 100,
                (first.B * firstWeight + second.B * secondWeightPercent) / 100);
        }

        private static void DataGridViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (!(sender is DataGridView grid) ||
                !(e.Value == null || e.Value == DBNull.Value || e.Value.ToString().Trim() == "NULL"))
            {
                return;
            }

            e.CellStyle.ForeColor = grid.DefaultCellStyle.ForeColor;
            e.CellStyle.BackColor = grid.DefaultCellStyle.BackColor;
        }

        private static PluginControlBase FindPluginRoot(Control control)
        {
            // Allow deeply nested plugin controls while guarding against a malformed parent chain.
            var depth = 0;
            for (var current = control;
                current != null && depth < MaxPluginParentTraversalDepth;
                current = current.Parent, depth++)
            {
                if (current is PluginControlBase pluginRoot)
                {
                    return pluginRoot;
                }
            }

            return null;
        }

        private static ControlThemeRule For<TControl>(Action<TControl, CustomTheme> apply)
            where TControl : Control
        {
            return new ControlThemeRule(
                control => control is TControl,
                (control, theme) => apply((TControl)control, theme));
        }

        private static void SetStyle(Scintilla editor, int style, Color foreground, Color background)
        {
            editor.Styles[style].ForeColor = foreground;
            editor.Styles[style].BackColor = background;
        }

        private sealed class ControlThemeRule
        {
            private readonly Action<Control, CustomTheme> apply;
            private readonly Func<Control, bool> matches;

            public ControlThemeRule(
                Func<Control, bool> matches,
                Action<Control, CustomTheme> apply)
            {
                this.matches = matches;
                this.apply = apply;
            }

            public void Apply(Control control, CustomTheme theme)
            {
                if (matches(control))
                {
                    apply(control, theme);
                }
            }
        }
    }
}
