using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageBroker2.Core.Sender.Logic
{
    public class ProcessMessageRaport : IProcessMessageRaport
    {

        public ProcessMessageRaport(int readedMessagesCount)
        {
            ReadedMessageCount = readedMessagesCount;
            UnsenedMessageCount = 0;
            SendedMessageCount = 0;
        }
        public int ReadedMessageCount { get; }
        public int SendedMessageCount { get; private set; }
        public int UnsenedMessageCount { get; private set; }

        public void AddSuccesfulMessage()
        {
            SendedMessageCount++;
        }

        public void AddUnsuccessfulMessage()
        {
            UnsenedMessageCount++;
        }
    }
}