using MsCrmTools.MetadataBrowser.AppCode;
using System;

namespace MsCrmTools.MetadataBrowser.UserControls
{
    public class ColumnSettingsUpdatedEventArgs : EventArgs
    {
        public EntityPropertiesControl Control { get; set; }
        public ListViewColumnsSettings Settings { get; set; }
    }
}