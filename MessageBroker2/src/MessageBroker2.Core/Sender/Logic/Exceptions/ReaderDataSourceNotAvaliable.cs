using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBroker2.Core.Sender.Logic.Exceptions
{
    [Serializable]
    public class ReaderDataSourceNotAvaliable:Exception
    {
        public ReaderDataSourceNotAvaliable(Exception innerException) : 
            this(null,innerException)
        {

        }

        public ReaderDataSourceNotAvaliable(string connectionData,Exception innerException) :
            base("Data source from witch data is read is not avaliable. Please check connection", innerException)
        {
            ConnectionData = connectionData;
        }

        public string ConnectionData { get; set; }
    }
}
