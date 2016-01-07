using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace MsCrmTools.WebResourcesManager.Forms
{
    public partial class FindAndReplaceForm : Form
    {
        private static bool caseSensitive = false;
        private static bool searchUp;
        private static string textToFind = "";
        private static FindAndReplaceForm theDialog;
        private static bool useRegex;
        private static bool useWildcards;
        private static bool wholeWord = false;
        private readonly TextEditor editor;

        public FindAndReplaceForm(TextEditor editor)
        {
            InitializeComponent();

            this.editor = editor;

            txtFind.Text = txtFind2.Text = textToFind;
            cbCaseSensitive.Checked = caseSensitive;
            cbWholeWord.Checked = wholeWord;
            cbRegex.Checked = useRegex;
            cbWildcards.Checked = useWildcards;
            cbSearchUp.Checked = searchUp;
        }

        public static void ShowForReplace(TextEditor editor, bool replace)
        {
            if (theDialog == null)
            {
                theDialog = new FindAndReplaceForm(editor);
                theDialog.tabMain.SelectedIndex = replace ? 1 : 0;
                theDialog.Show();
                theDialog.Activate();
            }
            else
            {
                theDialog.tabMain.SelectedIndex = replace ? 1 : 0;
                theDialog.Activate();
            }

            if (!editor.TextArea.Selection.IsMultiline)
            {
                theDialog.txtFind.Text = theDialog.txtFind2.Text = editor.TextArea.Selection.GetText();
                theDialog.txtFind.SelectAll();
                theDialog.txtFind2.SelectAll();
                theDialog.txtFind2.Focus();
            }

            if (replace)
            {
                theDialog.txtFind2.Focus();
            }
            else
            {
                theDialog.txtFind.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FindAndReplaceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            textToFind = txtFind2.Text;
            caseSensitive = (cbCaseSensitive.Checked);
            wholeWord = (cbWholeWord.Checked);
            useRegex = (cbRegex.Checked);
            useWildcards = (cbWildcards.Checked);
            searchUp = (cbSearchUp.Checked);

            theDialog = null;
        }

        private bool FindNext(string textToFind)
        {
            Regex regex = GetRegEx(textToFind);
            int start = regex.Options.HasFlag(RegexOptions.RightToLeft)
                ? editor.SelectionStart
                : editor.SelectionStart + editor.SelectionLength;
            Match match = regex.Match(editor.Text, start);

            if (!match.Success) // start again from beginning or end
            {
                if (regex.Options.HasFlag(RegexOptions.RightToLeft))
                    match = regex.Match(editor.Text, editor.Text.Length);
                else
                    match = regex.Match(editor.Text, 0);
            }

            if (match.Success)
            {
                editor.Select(match.Index, match.Length);
                TextLocation loc = editor.Document.GetLocation(match.Index);
                editor.ScrollTo(loc.Line, loc.Column);
            }

            return match.Success;
        }

        private void FindNext2Click(object sender, EventArgs e)
        {
            if (!FindNext(txtFind2.Text))
                SystemSounds.Beep.Play();
        }

        private void FindNextClick(object sender, EventArgs e)
        {
            if (!FindNext(txtFind.Text))
                SystemSounds.Beep.Play();
        }

        private Regex GetRegEx(string textToFind, bool leftToRight = false)
        {
            RegexOptions options = RegexOptions.None;
            if (cbSearchUp.Checked && !leftToRight)
                options |= RegexOptions.RightToLeft;
            if (cbCaseSensitive.Checked == false)
                options |= RegexOptions.IgnoreCase;

            if (cbRegex.Checked)
            {
                return new Regex(textToFind, options);
            }
            else
            {
                string pattern = Regex.Escape(textToFind);
                if (cbWildcards.Checked)
                    pattern = pattern.Replace("\\*", ".*").Replace("\\?", ".");
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                return new Regex(pattern, options);
            }
        }

        private void ReplaceAllClick(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Replace All occurences of \"" +
                                txtFind2.Text + "\" with \"" + txtReplace.Text + "\"?",
                "Replace All", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Regex regex = GetRegEx(txtFind2.Text, true);
                int offset = 0;
                editor.BeginChange();
                foreach (Match match in regex.Matches(editor.Text))
                {
                    editor.Document.Replace(offset + match.Index, match.Length, txtReplace.Text);
                    offset += txtReplace.Text.Length - match.Length;
                }
                editor.EndChange();
            }
        }

        private void ReplaceClick(object sender, EventArgs e)
        {
            Regex regex = GetRegEx(txtFind2.Text);
            string input = editor.Text.Substring(editor.SelectionStart, editor.SelectionLength);
            Match match = regex.Match(input);
            bool replaced = false;
            if (match.Success && match.Index == 0 && match.Length == input.Length)
            {
                editor.Document.Replace(editor.SelectionStart, editor.SelectionLength, txtReplace.Text);
                replaced = true;
            }

            if (!FindNext(txtFind2.Text) && !replaced)
                SystemSounds.Beep.Play();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                FindNextClick(null, null);
            }
            else if (e.KeyData == Keys.Escape)
            {
                btnCancel_Click(null, null);
            }
        }
    }
}