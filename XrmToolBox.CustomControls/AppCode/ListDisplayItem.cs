namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Helper class for binding to UI controls
    /// </summary>
    public class ListDisplayItem
    {
        internal ListDisplayItem(string name, string displayName, string description, object boundObject)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Object = boundObject;
        }

        /// <summary>
        /// Short name for the element
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name for the element
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Summary of Name and Display Name
        /// </summary>
        public string SummaryName { get=> this.ToString(); }

        /// <summary>
        /// Description for the item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Object being bound to the list
        /// </summary>
        public object Object { get; set; }

        /// <summary>
        /// Mask for the To String display value
        /// </summary>
        public string ToStringMask { get; set; } = "{0} ({1})";

        /// <summary>
        /// ToString representation of the item details
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // var str = (DisplayName == null) ? Name : $"{DisplayName} ({Name})";
            return string.Format(ToStringMask, DisplayName, Name);
        }
    }
}
