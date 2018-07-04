using System;
using System.Collections.Generic;

namespace MessageBroker2.Core.Sender.Logic
{
    public interface IProcessMessageRaport
    {
        int ReadedMessageCount { get; }
        int SendedMessageCount { get; }
        int UnsenedMessageCount { get; }

    }
}