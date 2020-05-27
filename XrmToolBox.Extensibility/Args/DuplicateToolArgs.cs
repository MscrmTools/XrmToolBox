using System;

namespace XrmToolBox.Extensibility.Args
{
    public class DuplicateToolArgs : EventArgs
    {
        public DuplicateToolArgs(object state, bool newConnection)
        {
            State = state;
            NewConnection = newConnection;
        }

        public bool NewConnection { get; }
        public object State { get; }
    }
}