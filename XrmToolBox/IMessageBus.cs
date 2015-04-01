using System;

namespace XrmToolBox
{
    public interface IMessageBus
    {
        event EventHandler OnOutgoingMessage;

        void OnIncomingMessage();
    }
}
