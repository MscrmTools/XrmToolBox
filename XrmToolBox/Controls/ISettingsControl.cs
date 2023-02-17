using System;
using XrmToolBox.AppCode;

namespace XrmToolBox.Controls
{
    internal interface ISettingsControl
    {
        event EventHandler<SettingsPropertyEventArgs> OnSettingsPropertyChanged;

        string PropertyName { get; set; }
    }
}