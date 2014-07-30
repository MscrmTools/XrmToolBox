namespace Jsbeautifier
{
    using System;

    public class BeautifierOptions
    {
        public BeautifierOptions()
        {
            this.IndentSize = 4;
            this.IndentChar = ' ';
            this.IndentWithTabs = false;
            this.PreserveNewlines = true;
            this.MaxPreserveNewlines = 10.0f;
            this.JslintHappy = false;
            this.BraceStyle = Jsbeautifier.BraceStyle.Collapse;
            this.KeepArrayIndentation = false;
            this.KeepFunctionIndentation = false;
            this.EvalCode = false;
            //this.UnescapeStrings = false;
            this.BreakChainedMethods = false;
        }

        public uint IndentSize { get; set; }

        public char IndentChar { get; set; }

        public bool IndentWithTabs { get; set; }

        public bool PreserveNewlines { get; set; }

        public float MaxPreserveNewlines { get; set; }

        public bool JslintHappy { get; set; }

        public BraceStyle BraceStyle { get; set; }

        public bool KeepArrayIndentation { get; set; }

        public bool KeepFunctionIndentation { get; set; }

        public bool EvalCode { get; set; }

        //public bool UnescapeStrings { get; set; }

        public bool BreakChainedMethods { get; set; }

        public static BeautifierOptions DefaultOptions()
        {
            return new BeautifierOptions();
        }
    }
}