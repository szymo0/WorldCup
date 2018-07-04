using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker2.Core.Sender.Plugins
{
    public interface ISendDataFor
    {
        Task Send(IDataToSend dataToSend);
    }
}
