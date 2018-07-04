using System.Threading.Tasks;

namespace MessageBroker2.Core.Sender.Logic
{
    public interface IProcessMessageStrategies
    {
        Task<IProcessMessageRaport> Process();
    }
}
