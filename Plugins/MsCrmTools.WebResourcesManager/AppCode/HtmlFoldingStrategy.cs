using HtmlAgilityPack;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsCrmTools.WebResourcesManager.AppCode
{
    public class HtmlFoldingStrategy
    {
        private List<NewFolding> foldMarkers;
        public bool ShowAttributesWhenFolded { get; set; }

        public IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
        {
            foldMarkers = new List<NewFolding>();
            try
            {
                HtmlDocument html = new HtmlDocument();
                html.LoadHtml(document.Text);
                HtmlNodeCollection rootNodes = html.DocumentNode.ChildNodes;

                GetFolds(document, rootNodes);
                firstErrorOffset = -1;
            }
            catch
            {
                firstErrorOffset = 0;
                return new List<NewFolding>();
            }
            foldMarkers.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
            return foldMarkers;
        }

        public void GetFolds(TextDocument document, HtmlNodeCollection nodes)
        {
            foreach (HtmlNode node in nodes)
            {
                switch (node.NodeType)
                {
                    case HtmlNodeType.Element:
                        NewFolding newFold = CreateElementFold(document, node);

                        if (newFold != null)
                            foldMarkers.Add(newFold);

                        if (node.ChildNodes.Count > 0)
                            GetFolds(document, node.ChildNodes);

                        break;
                }
            }
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document and updates the folding manager with them.
        /// </summary>
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            IEnumerable<NewFolding> foldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(foldings, firstErrorOffset);
        }

        private static string GetAttributeFoldText(HtmlNode node)
        {
            StringBuilder text = new StringBuilder();

            for (int i = 0; i < node.Attributes.Count; ++i)
            {
                HtmlAttribute attr = node.Attributes[i];

                text.Append(attr.Name);
                text.Append("=");
                text.Append("\"");
                text.Append(attr.Value);
                text.Append("\"");

                // Append a space if this is not the
                // last attribute.
                if (i < node.Attributes.Count - 1)
                {
                    text.Append(" ");
                }
            }

            return text.ToString();
        }

        private NewFolding CreateElementFold(TextDocument document, HtmlNode node)
        {
            NewFolding newFold = new NewFolding();

            if (node.Line != node.EndNode.Line)
            {
                if (this.ShowAttributesWhenFolded && node.HasAttributes)
                    newFold.Name = String.Concat("<", node.Name, " ", GetAttributeFoldText(node), ">");
                else
                    newFold.Name = String.Concat("<", node.Name, ">");

                newFold.StartOffset = document.GetOffset(node.Line, node.LinePosition - 1);
                newFold.EndOffset = document.GetOffset(node.EndNode.Line, node.EndNode.LinePosition + node.OriginalName.Length + 3);
            }

            return newFold;
        }
    }
}