using System;

namespace XrmToolBox.CustomControls
{
    /// <summary>
    /// Helper class for defining ListView Columns and Groups
    /// </summary>
    [Serializable]
    public class ListViewColumnDef
    {
        /// <summary>
        /// Default constructor for designer
        /// </summary>
        public ListViewColumnDef()
        {
        }
        /// <summary>
        /// Helper constructor for default values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="order"></param>
        /// <param name="displayName"></param>
        public ListViewColumnDef(string name, int order, string displayName = null)
        {
            Name = name;
            Order = order;
            if (displayName != null) {
                DisplayName = displayName;
            }
            else
            {
                DisplayName = name;
            }
        }
        /// <summary>
        /// Internal Name/Key for the column
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name for the Column
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Display order of this column
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Flag indicating whether this column should be used in Grouping
        /// </summary>
        public bool IsGroupColumn { get; set; } = false;

        /// <summary>
        /// Flag indicating whether this column should be included in the Filtering
        /// </summary>
        public bool IsFilterColumn { get; set; } = false;
        /// <summary>
        /// ListView column item width
        /// </summary>
        public int Width { get; set; } = 100;

        /// <summary>
        /// Easier to read ...
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var filter = (IsFilterColumn) ? "Filter" : "No Filter";
            var group = (IsGroupColumn) ? "Grouping" : "No Grouping";
            return $"{DisplayName} ({Name}), Order: {Order}, {filter}, {group}, {Width} wide";
        }
    }
}
