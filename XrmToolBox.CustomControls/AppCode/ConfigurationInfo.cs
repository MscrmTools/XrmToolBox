using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace XrmToolBox.CustomControls
{
    #region Enums
    /// <summary>
    /// Logging level for general actions in the code
    /// </summary>
    [Serializable]
    public enum EnumLoggingLevel
    {
        None,
        Exception,
        Information,
        Verbose
    }

    /// <summary>
    /// Types of Entities that should be retrieved
    /// </summary>
    [Serializable]
    public enum EnumEntityTypes
    {
        Custom,
        System,
        BothCustomAndSystem
    }

    /// <summary>
    /// Display mode for the List View control
    /// </summary>
    [Serializable]
    public enum ListViewColumnDisplayMode
    {
        /// <summary>
        /// Display expanded columns
        /// </summary>
        Expanded,
        /// <summary>
        /// Display compact view of columns
        /// </summary>
        Compact
    }

    /// <summary>
    /// Type of filter action to be applied for Filter Text
    /// </summary>
    [Serializable]
    public enum EnumFilterMatchType
    {
        /// <summary>
        /// Exact string comparison
        /// </summary>
        Equals,
        /// <summary>
        /// Match on string contains
        /// </summary>
        Contains,
        /// <summary>
        /// Match on string begins with
        /// </summary>
        StartsWith,
        /// <summary>
        /// Match on string ends with
        /// </summary>
        EndsWith,
        /// <summary>
        /// Match using regular expression
        /// </summary>
        RegEx
    }
    #endregion

    /// <summary>
    /// Class that provides a method for capturing general filter criteria
    /// </summary>
    [DisplayName("Filter Criteria")]
    [Category("Filter Settings")]
    [Description("Class containing information about exclusion filters")]
    [DefaultProperty("FilterString")]
    [Serializable]
    public class FilterInfo
    {
        /// <summary>
        /// Text that will be matched on the filter
        /// </summary>
        [DisplayName("Filter Text")]
        [Description("Provide the filter text to be applied")]
        [Category("Filter Settings")]
        public string FilterString{ get; set; }

        /// <summary>
        /// The type of matching that should be performed.
        /// </summary>
        [DisplayName("Filter Match")]
        [Description("Choose how this Filter String will be applied")]
        [Category("Filter Settings")]
        public EnumFilterMatchType FilterMatchType { get; set; }

        /// <summary>
        /// Helper method for formatting the display of the filter criteria
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return FilterMatchType.ToString() + ": " + FilterString;
        }
    }

    /// <summary>
    /// Helper class to capture general configuration settings
    /// </summary>
    [Serializable]
    public class ConfigurationInfo
    {
        #region Constructor 
        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigurationInfo() {

            EntityFilters = new List<FilterInfo>() {
            };

            EntityTypes = EnumEntityTypes.BothCustomAndSystem;
            RetrieveAsIfPublished = true;

            EntityRequestFilters = new List<EntityFilters>() { Microsoft.Xrm.Sdk.Metadata.EntityFilters.Default };
        }
        #endregion

        #region Helper methods 

        /// <summary>
        /// Helper method that will tell whether to filter a given Entity based on its Logical Name
        /// </summary>
        /// <param name="logicalName">Entity Logical Name</param>
        /// <returns></returns>
        public bool FilterEntity(string logicalName)
        {
            return FilterItem(logicalName, EntityFilters.ToList<FilterInfo>());
        }

        /// <summary>
        /// Iterate through all of the filters for the specific item, return true if it matches one of the filter criteria
        /// </summary>
        /// <param name="matchName">Item to be matched, such as Attribute Schema Name or Entity Logical Name</param>
        /// <param name="filters">Filter Info collection</param>
        private bool FilterItem(string matchName, List<FilterInfo> filters)
        {
            if (filters == null) {
                return false;
            }

            matchName = matchName.ToLower();

            // default to false.  must find a match to return true... allows for empty list
            bool filtersMatch = false;
            foreach (var filter in filters)
            {
                switch (filter.FilterMatchType)
                {
                    case EnumFilterMatchType.Contains:
                        filtersMatch = matchName.Contains(filter.FilterString.ToLower());
                        break;
                    case EnumFilterMatchType.EndsWith:
                        filtersMatch = matchName.EndsWith(filter.FilterString.ToLower());
                        break;
                    case EnumFilterMatchType.StartsWith:
                        filtersMatch = matchName.StartsWith(filter.FilterString.ToLower());
                        break;
                    case EnumFilterMatchType.Equals:
                        filtersMatch = (matchName == filter.FilterString.ToLower());
                        break;
                    case EnumFilterMatchType.RegEx:
                        Regex regex = new Regex(filter.FilterString);
                        Match match = regex.Match(matchName);
                        filtersMatch = match.Success;
                        break;
                }
                if (filtersMatch == true) {
                    break;
                }
            }

            return filtersMatch;
        }

        #endregion

        #region Public properties
        /// <summary>
        /// Which Entity types should be loaded on retrieve.
        /// </summary>
        [DisplayName("Entity Types")]
        [Description("Which Entity types should be loaded on retrieve.")]
        [Category("Filter Settings")]
        public EnumEntityTypes EntityTypes { get; set; }

        /// <summary>
        /// List of filters to be applied to Entity retrieval and generation. These are Entities that you want to be excluded from the list and not generated in the template.
        /// </summary>
        [DisplayName("Entity Filters")]
        [Description("List of filters to be applied to Entity retrieval. These are Entities that you want to be excluded from the list.")]
        [Category("Filter Settings")]
        [ListBindable(BindableSupport.Yes)]
        public List<FilterInfo> EntityFilters { get; set; }

        /// <summary>
        /// List of EntityFilters to be applied on the 
        /// </summary>
        [DisplayName("Entity Request Filters")]
        [Description("List of EntityFilters to be applied to Entity retrieval. This is the EntityFilter structure passed with the RetrieveAllEntitiesRequest")]
        [Category("Filter Settings")]
        [ListBindable(BindableSupport.Yes)]
        public List<EntityFilters> EntityRequestFilters { get; set; }

        /// <summary>
        /// Flag indicating whether to retrieve the metadata that has not been published
        /// </summary>
        [DisplayName("Retrieve As If Published")]
        [Description("Flag indicating whether to retrieve the metadata that has not been published")]
        [Category("Project Settings")]
        public bool RetrieveAsIfPublished { get; set; }

        /// <summary>
        /// Display additional column details or Name and Entity Logical Name only
        /// </summary>
        [DisplayName("Column Display Mode")]
        [Description("Display additional column details or Name and Entity Logical Name only")]
        [Category("Project Settings")]
        public ListViewColumnDisplayMode ColumnDisplayMode { get; set; }

        /// <summary>
        /// Toggle to enable logging while generating the templates
        /// </summary>
        [DisplayName("Logging Level")]
        [Description("Toggle to enable logging while generating the templates")]
        [Category("Project Settings")]
        public EnumLoggingLevel LoggingLevel { get; set; }

        #endregion
    }
}
