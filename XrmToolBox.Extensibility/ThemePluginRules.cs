using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                        For<DataGridView>(ApplyDataGridViewTheme),
                    }
                },
                {
                    FetchXmlBuilderCompany,
                    new[]
                    {
                        For<Scintilla>(ApplyXmlEditorTheme),
                        For<DataGridView>(ApplyDataGridViewTheme),
                    }
                },
                {
                    PluginRegistrationCompany,
                    new[]
                    {
                        For<PropertyGrid>(ApplyPropertyGridTheme),
                        For<DataGridView>((grid, theme) => ApplyDataGridViewTheme(grid, theme, true)),
                    }
                }
            };

        public static void Apply(Control control, CustomTheme theme)
        {
            // Resolve identity from the plugin root; child framework controls
            // often report Microsoft or another component vendor as their company.
            var pluginRoot = FindPluginRoot(control);
            var pluginCompany = pluginRoot?.CompanyName;
            if (string.IsNullOrWhiteSpace(pluginCompany) ||
                !RulesByPluginCompany.TryGetValue(pluginCompany, out var rules))
            {
                return;
            }

            foreach (var rule in rules)
            {
                rule.Apply(control, theme);
            }
        }

        private static void ApplyDataGridViewTheme(DataGridView grid, CustomTheme theme)
        {
            ApplyDataGridViewTheme(grid, theme, false);
        }

        private static void ApplyDataGridViewTheme(
            DataGridView grid,
            CustomTheme theme,
            bool useFlatBorders)
        {
            var alternatingRowColor = Blend(theme.Background1, theme.Background2, 35);

            grid.BackgroundColor = theme.Background1;
            grid.GridColor = theme.Background3;
            grid.DefaultCellStyle.ForeColor = theme.ForeColor2;
            grid.DefaultCellStyle.BackColor = theme.Background1;
            grid.ColumnHeadersDefaultCellStyle.BackColor = theme.Background2;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = theme.ForeColor4;
            grid.RowHeadersDefaultCellStyle.BackColor = theme.Background2;
            grid.RowsDefaultCellStyle.BackColor = theme.Background1;
            grid.AlternatingRowsDefaultCellStyle.BackColor = alternatingRowColor;

            if (useFlatBorders)
            {
                grid.BorderStyle = BorderStyle.None;
                grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                grid.EnableHeadersVisualStyles = false;
            }

            // Reapplying a theme must not accumulate formatting handlers.
            grid.CellFormatting -= DataGridViewCellFormatting;
            grid.CellFormatting += DataGridViewCellFormatting;
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
            SetStyle(editor, Style.Sql.CommentLineDoc, theme.CommentColor, theme.Background1);
            SetStyle(editor, Style.Sql.Number, theme.NumberColor, theme.Background1);
            SetStyle(editor, Style.Sql.Word, theme.KeywordColor, theme.Background1);
            SetStyle(editor, Style.Sql.Word2, theme.KeywordColor, theme.Background1);
            SetStyle(editor, Style.Sql.Identifier, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Sql.User1, theme.ForeColor1, theme.Background1);
            SetStyle(editor, Style.Sql.User2, theme.OperatorColor, theme.Background1);
            SetStyle(editor, Style.Sql.String, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Sql.Character, theme.StringColor, theme.Background1);
            SetStyle(editor, Style.Sql.Operator, theme.OperatorColor, theme.Background1);

            editor.SetSelectionBackColor(true, theme.Background2);
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
