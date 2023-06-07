using System;

namespace XrmToolBox.AppCode
{
    internal class SettingsPropertyInfo
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsReadOnly { get; set; }
        public string Name { get; internal set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public Type Type { get; internal set; }
        public bool IsMultiline { get; internal set; }
    }
}