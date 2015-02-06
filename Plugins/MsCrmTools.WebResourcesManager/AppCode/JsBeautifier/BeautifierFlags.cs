namespace Jsbeautifier
{
    using System;

    public class BeautifierFlags
    {
        public BeautifierFlags(string mode)
        {
            this.PreviousMode = "BLOCK";
            this.Mode = mode;
            this.VarLine = false;
            this.VarLineTainted = false;
            this.VarLineReindented = false;
            this.InHtmlComment = false;
            this.IfLine = false;
            this.ChainExtraIndentation = 0;
            this.InCase = false;
            this.InCaseStatement = false;
            this.CaseBody = false;
            this.EatNextSpace = false;
            this.IndentationLevel = 0;
            this.TernaryDepth = 0;
        }

        public string PreviousMode { get; set; }

        public string Mode { get; set; }

        public bool VarLine { get; set; }

        public bool VarLineTainted { get; set; }

        public bool VarLineReindented { get; set; }

        public bool InHtmlComment { get; set; }

        public bool IfLine { get; set; }

        public int ChainExtraIndentation { get; set; }

        public bool InCase { get; set; }

        public bool InCaseStatement { get; set; }

        public bool CaseBody { get; set; }

        public bool EatNextSpace { get; set; }

        public int IndentationLevel { get; set; }

        public int TernaryDepth { get; set; }
    }
}