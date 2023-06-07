using System;

namespace XrmToolBox.AppCode
{
    public class SettingsPropertyEventArgs : EventArgs
    {
        public SettingsPropertyEventArgs(string propertyName, object value)
        {
            PropertyName = propertyName;
            Value = value;
        }

        public string PropertyName { get; set; }

        public object Value { get; set; }
    }
}