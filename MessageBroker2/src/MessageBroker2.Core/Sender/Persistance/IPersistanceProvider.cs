using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker2.Core.Sender.Persistance
{
    public interface IPersistanceProvider
    {
        PersisanceId ExistsInPersistanceStore(string businessId);
        PersisanceId StoreInPersinstance(string businessId, string dataToStore);

        void SoftDeleteFromStore(PersisanceId idDataToDelete);
        void DeleteFromStore(PersisanceId idDataToDelete);

    }

    public class PersisanceId
    {
        public Guid ProcessId { get; set; }
        public string BussinesId { get; set; }
    }
}
