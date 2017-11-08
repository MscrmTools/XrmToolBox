using System;
using System.Collections.Generic;
using McTools.Xrm.Connection;

namespace XrmToolBox.Extensibility
{
    public class AdditionalConnections
    {
        private readonly List<ConnectionDetail> connections;

        public AdditionalConnections()
        {
            connections = new List<ConnectionDetail>();
        }

        public event EventHandler<ConnectionDetailEventArgs> AdditionalConnectionAdded;

        public List<ConnectionDetail> Connections => connections;

        public void Add(ConnectionDetail detail)
        {
            if (connections.Contains(detail)) return;
            connections.Add(detail);
            AdditionalConnectionAdded?.Invoke(this, new ConnectionDetailEventArgs(detail));
        }

        public void Clear()
        {
            connections.Clear();
        }

        public void Remove(ConnectionDetail detail)
        {
            connections.Remove(detail);
        }

        public bool Contains(ConnectionDetail detail)
        {
            return connections.Contains(detail);
        }
    }

    public class ConnectionDetailEventArgs : EventArgs
    {
        public ConnectionDetailEventArgs()
        {
        }

        public ConnectionDetailEventArgs(ConnectionDetail detail)
        {
            Detail = detail;
        }

        public ConnectionDetail Detail { get; set; }
    }
}