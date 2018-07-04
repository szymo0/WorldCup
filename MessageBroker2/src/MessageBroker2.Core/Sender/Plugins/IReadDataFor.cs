using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessageBroker2.Core.Sender.Plugins
{
    public interface IReadDataFor
    {
        Task<IEnumerable<IDataToSend>> ReadData();
    }
}