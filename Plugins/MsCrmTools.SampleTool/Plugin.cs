using System.ComponentModel.Composition;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace MsCrmTools.SampleTool
{
    /// <summary>
    /// This class describe how to expose a plugin for XrmToolBox
    /// </summary>
    /// <remarks>
    /// A plugin is exposed to XrmToolBox through a class decorated with attribute
    /// [Export(typeof(IXrmToolBoxPlugin))]
    /// All ExportMetadata described in this plugin are mandatory
    /// Name and Description are not more used from Assembly information
    /// For image Metadata, paste your base64 encoded image or null to use "No logo" logo
    /// For color Metadata, you can use color name or hexadecimal code
    ///
    /// If this is not obvious, it is possible to include multiple plugins in one assembly
    /// as plugins are retrieved by class and not by assembly anymore
    ///
    /// Required references:
    /// - XrmToolBox.Extensions
    /// - System.ComponentModel.Composition
    /// - Microsoft Dynamics CRM SDK libraries
    /// </remarks>
    [Export(typeof(IXrmToolBoxPlugin)),
    ExportMetadata("Name", "A Sample Tool"),
    ExportMetadata("Description", "This is a tool to learn XrmToolBox developement"),
    ExportMetadata("SmallImageBase64", null),
    ExportMetadata("BigImageBase64", null),
    ExportMetadata("BackgroundColor", "Lavender"),
    ExportMetadata("PrimaryFontColor", "#000000"),
    ExportMetadata("SecondaryFontColor", "DarkGray")]
    public class Plugin : PluginBase
    {
        /// <summary>
        /// This method return the actual usercontrol that will
        /// be used in XrmToolBox
        /// </summary>
        /// <returns>User control to display</returns>
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new SampleTool();
        }
    }
}