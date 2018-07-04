using System;
using System.Collections.Generic;

namespace MessageBroker2.Core.Sender.Logic
{
    public class NotReadAnyDataToProcess : IProcessMessageRaport
    {
        public int ReadedMessageCount => 0;
        public int SendedMessageCount => 0;
        public int UnsenedMessageCount => 0;


    }
}