using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace XrmToolBox.Extensibility
{
    public class AbstractControlDescriptionProvider<TAbstract, TBase> : TypeDescriptionProvider
    {
        public AbstractControlDescriptionProvider()
            : base(TypeDescriptor.GetProvider(typeof(TAbstract)))
        {
        }

        public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
        {
            if (objectType == typeof(TAbstract))
                objectType = typeof(TBase);

            return base.CreateInstance(provider, objectType, argTypes, args);
        }

        public override Type GetReflectionType(Type objectType, object instance)
        {
            if (objectType == typeof(TAbstract))
                return typeof(TBase);

            return base.GetReflectionType(objectType, instance);
        }
    }

    [PartNotDiscoverable]
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<MultipleConnectionsPluginControlBase, UserControl>))]
    public abstract class MultipleConnectionsPluginControlBase : PluginControlBase
    {
        protected MultipleConnectionsPluginControlBase()
        {
            AdditionalConnectionDetails = new ObservableCollection<ConnectionDetail>();
            AdditionalConnectionDetails.CollectionChanged += (sender, e) => { ConnectionDetailsUpdated(e); };
        }

        public ObservableCollection<ConnectionDetail> AdditionalConnectionDetails { get; set; }

        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail,
            string actionName, object parameter)
        {
            if (actionName == "AdditionalOrganization")
            {
                if (!AdditionalConnectionDetails.Contains(detail))
                {
                    AdditionalConnectionDetails.Add(detail);
                }
            }
            else
            {
                base.UpdateConnection(newService, detail, actionName, parameter);
            }
        }

        protected void AddAdditionalOrganization()
        {
            var args = new RequestConnectionEventArgs { ActionName = "AdditionalOrganization", Control = this };
            OnConnectionRequested(this, args);
        }

        protected abstract void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e);

        protected void RemoveAdditionalOrganization(ConnectionDetail detail)
        {
            if (AdditionalConnectionDetails.Contains(detail))
            {
                AdditionalConnectionDetails.Remove(detail);
            }
        }
    }
}