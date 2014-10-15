using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MsCrmTools.MetadataBrowser.AppCode;

namespace MsCrmTools.MetadataBrowser.UserControls
{
    public class ColumnSettingsUpdatedEventArgs : EventArgs
    {
        public ListViewColumnsSettings Settings { get; set; }

        public EntityPropertiesControl Control { get; set; }
    }
}
